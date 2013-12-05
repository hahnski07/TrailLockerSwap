using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrailLocker.Authentication;
using TrailLocker.Controllers;
using TrailLocker.Models;
using TrailLocker.Repository;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TrailLocker.Tests
{
    [TestClass]
    public class TripControllerTest
    {
        private User loggedInUser = new User()
        {
            email = "john_doe@taylor.edu",
            first_name = "john",
            last_name = "doe",
            UserID = new Guid()
        };

        private IAuthenticatedUserProvider provider;
        private IRepository<Trip> repository = new Repository<Trip>(new InMemoryUnitOfWork());
        private Trip userTrip;
        private Trip notUserTrip;

        public TripControllerTest()
        {
            userTrip = new Trip(loggedInUser.UserID);
            notUserTrip = new Trip(Guid.NewGuid());
            repository.Add(userTrip);
            provider = new MockAuthenticatedUserProvider(loggedInUser);
        }

        [TestMethod]
        public void Index_returns_list_of_correct_Trips()
        {
            TripController controller = new TripController(repository, provider);
            ViewResult result = controller.Index();
            Assert.IsNotNull(result);
            EnumerableQuery<Trip> model = (EnumerableQuery<Trip>)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count());
            Assert.IsTrue(model.Contains(userTrip));
        }
    }
}
