using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;

using TrailLocker.Models;
using TrailLocker.Repository;

namespace TrailLocker.Authentication
{
    public class FormsAuthenticatedUserProvider : IAuthenticatedUserProvider
    {
        private IRepository<User> repository;
        private Controller controller;

        public FormsAuthenticatedUserProvider(Controller controller)
        {
            repository = new Repository<User>(new DBUnitOfWork());
            this.controller = controller;
        }

        public User AuthenticatedUser
        {
            get
            {
                return repository.FindBy(u => u.email == controller.User.Identity.Name).SingleOrDefault();
            }
        }
    }
}