using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrailLocker.Models;
using TrailLocker.Repository;
using System.Web.Security;

using Facebook;

using TrailLocker.Authentication;

namespace TrailLocker.Controllers
{
    public class UserController : Controller
    {
        private IRepository<User> repository;
        private IAuthenticatedUserProvider provider; 

        public UserController()
        {
            this.repository = new Repository<User>(new DBUnitOfWork());
            this.provider = new FormsAuthenticatedUserProvider(this);
        }

        public UserController(IRepository<User> repository, IAuthenticatedUserProvider provider)
        {
            this.repository = repository;
            this.provider = provider;
        }

        //
        // GET: /User/

        public ViewResult Index()
        {
            return View(repository.FindAll().ToList());
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(Guid id)
        {
            User user = repository.FindBy(x => x.UserID == id).Single();
            return View(user);
        }

        public ActionResult Authenticate(String code)
        {
            FacebookClient client = new FacebookClient();

            dynamic temp = client.Post("oauth/access_token", new
            {
                client_id = "603680269694814",
                client_secret = "c45641c9de012c138f1658aa95a6c27d",
                redirect_uri = Url.Action("Authenticate", "User", null, Request.Url.Scheme),
                code = code
            });

            client.AccessToken = temp.access_token;

            dynamic properties = client.Get("me?fields=first_name,last_name,id,email");
            String email = properties.email;

            User user = repository.FindBy(u => u.email == email).SingleOrDefault();

            if (user == null)
                user = CreateNewUser(properties);

            FormsAuthentication.SetAuthCookie(user.email, false);
            return RedirectToAction("Index", "User");
        }

        private User CreateNewUser(dynamic me)
        {
            User new_user = new User(me.first_name, me.last_name, me.email);
            new_user.locker = new Locker();

            repository.Add(new_user);
            repository.Commit();

            //Locker new_locker = new Locker(new_user.UserID);


            //.Add(new_locker);
            //LockerDB.Commit();

            //new_user.locker = new_locker;
            //UserDB.Attach(new_user);
            //UserDB.Commit();
            return new_user;
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserID = Guid.NewGuid();
                repository.Add(user);
                repository.Commit();
                return RedirectToAction("Index");
            }

            return View(user);
        }
        
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            User user = repository.FindBy(x => x.UserID == id).Single();
            return View(user);
        }

        public ActionResult Account()
        {
            return View(provider.AuthenticatedUser);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                repository.Attach(user);
                repository.Commit();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            User user = repository.FindBy(x => x.UserID == id).Single();
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = repository.FindBy(x => x.UserID == id).Single();
            repository.Remove(user);
            repository.Commit();
            return RedirectToAction("Index");
        }
    }
}
