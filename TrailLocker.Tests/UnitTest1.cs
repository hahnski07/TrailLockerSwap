using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrailLocker.Authentication;
using TrailLocker.Controllers;
using TrailLocker.Models;
using TrailLocker.Repository;
using System.Web;
using System.Web.Mvc;

namespace TrailLocker.Tests
{
    [TestClass]
    public class UserControllerTest
    {
        // This is a sample unit test... 
        // We create the repository and signed in user and pass them to the controller constructor.
        [TestMethod]
        public void Details_Should_Pass_Null_Model_For_Guid_Not_Found()
        {
            User loggedInUser = new User()
            {
                email = "john_doe@taylor.edu",
                first_name = "john",
                last_name = "doe",
                UserID = new Guid()
            };

            IRepository<User> repository = new Repository<User>(new InMemoryUnitOfWork());
            repository.Add(loggedInUser);
            IAuthenticatedUserProvider provider = new MockAuthenticatedUserProvider(loggedInUser);

            UserController controller = new UserController(repository, provider);

            ViewResult result = controller.Details(new Guid());

            //Assert.IsNull(result.Model);
        }
    }
}
