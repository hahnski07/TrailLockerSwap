using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrailLocker.Repository;
using TrailLocker.Models;
using Facebook;

namespace TrailLocker.Controllers
{
    public class LoginController : SuperController
    {

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        //
        // GET: /Login/


        public LoginController()
        {

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new {
                client_id = "1424766361075600",
                client_secret = "d23d50adbcd2ffc1077714cd63b7976d",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "1424766361075600",
                client_secret = "d23d50adbcd2ffc1077714cd63b7976d",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // TODO: Authenticate User

            // Store the access token in the session
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information
            dynamic me = fb.Get("me?fields=first_name,last_name,id,email");
            string email = me.email;

            try{
                var query = UserDB.FindBy(x => x.email == email);
                var user = query.SingleOrDefault();
                if (user == null)                
                    user = CreateNewUser(me);                                           
            }catch (InvalidOperationException e){ //Create new account                
            }
            FormsAuthentication.SetAuthCookie(email, false);
            return RedirectToAction("Index", "Home");

        }

        private User CreateNewUser(dynamic me)
        {
            User new_user = new User(me.first_name, me.last_name, me.email);

            UserDB.Add(new_user);
            UserDB.Commit();

            Locker new_locker = new Locker(new_user.UserID);


            LockerDB.Add(new_locker);
            LockerDB.Commit();

            new_user.locker = new_locker;
            UserDB.Attach(new_user);
            UserDB.Commit();
            return new_user;
        }
    }
}