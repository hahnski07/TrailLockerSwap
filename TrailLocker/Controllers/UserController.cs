using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrailLocker.Models;
using TrailLocker.Repository;

namespace TrailLocker.Controllers
{
    public class UserController : SuperController
    {

        //
        // GET: /User/

        public ViewResult Index()
        {
            return View(UserDB.FindAll().ToList());
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(Guid id)
        {
            User user = UserDB.FindBy(x => x.UserID == id).Single();
            return View(user);
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
                UserDB.Add(user);
                UserDB.Commit();
                return RedirectToAction("Index");
            }

            return View(user);
        }
        
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            User user = UserDB.FindBy(x => x.UserID == id).Single();
            return View(user);
        }

        public ActionResult Account()
        {
            User user = get_current_user();
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                UserDB.Attach(user);
                UserDB.Commit();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            User user = UserDB.FindBy(x => x.UserID == id).Single();
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = UserDB.FindBy(x => x.UserID == id).Single();
            UserDB.Remove(user);
            UserDB.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            UserDB.Dispose();
            base.Dispose(disposing);
        }
    }
}
