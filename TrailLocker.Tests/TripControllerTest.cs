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
        private IRepository<Trip> notUserRepository = new Repository<Trip>(new InMemoryUnitOfWork());
        private Trip userTrip;
        private Trip notUserTrip;

        public TripControllerTest()
        {
            userTrip = new Trip(loggedInUser.UserID);
            userTrip.current_weight = 10;
            userTrip.description = "My Trip";
            userTrip.destination = "Egypt";
            userTrip.total_capacity = 15;
            userTrip.weather = "Sunny";
            repository.Add(userTrip);

            notUserTrip = new Trip(Guid.NewGuid());
            notUserTrip.current_weight = 15;
            notUserTrip.description = "Trip 1";
            notUserTrip.destination = "Seattle";
            notUserTrip.total_capacity = 20;
            notUserTrip.weather = "Rainy";
            notUserRepository.Add(notUserTrip);

            provider = new MockAuthenticatedUserProvider(loggedInUser);
        }

        [TestMethod]
        public void Index_returns_list_of_correct_Trips()
        {
            TripController controller = new TripController(repository, provider);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            EnumerableQuery<Trip> model = (EnumerableQuery<Trip>)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count());
            Assert.IsTrue(model.Contains(userTrip));
        }

        [TestMethod]
        public void Details_returns_correct_Trip_Details()
        {
            TripController controller = new TripController(repository, provider);
            ViewResult result = controller.Details(userTrip.TripID) as ViewResult;
            Trip model = result.Model as Trip;
            Assert.AreEqual(model, userTrip);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Details_should_not_give_details_for_other_user_trips()
        {
            TripController controller = new TripController(notUserRepository, provider);
            ViewResult result = controller.Details(notUserTrip.TripID) as ViewResult;
            Trip model = result.Model as Trip;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Details_should_not_work_for_nonexsistent_Trip()
        {
            TripController controller = new TripController(repository, provider);
            ViewResult result = controller.Details(Guid.NewGuid()) as ViewResult;
            Trip model = result.Model as Trip;
        }

        [TestMethod]
        public void Create_should_return_view()
        {
            TripController controller = new TripController(repository, provider);
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);            
        }
        
        [TestMethod]
        public void Create_trip_adds_to_repository()
        {
            TripController controller = new TripController(repository, provider);
            Trip newTrip = new Trip(provider.AuthenticatedUser.UserID);
            newTrip.current_weight = 8;
            newTrip.description = "London Trip";
            newTrip.destination = "London";
            newTrip.total_capacity = 12;
            newTrip.weather = "Cloudy";
            controller.Create(newTrip);
            ViewResult result = controller.Index() as ViewResult;
            IQueryable<Trip> model = result.Model as IQueryable<Trip>;
            Assert.IsTrue(model.Contains(newTrip));
            Assert.AreEqual(2, model.Count());

        }

        [TestMethod]
        public void Edit_Trip_returns_Correct_Trip()
        {
            TripController controller = new TripController(repository, provider);
            ViewResult result = controller.Edit(userTrip.TripID) as ViewResult;
            Trip model = result.Model as Trip;
            Assert.AreEqual(model, userTrip);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Cannot_edit_other_users_trips()
        {
            TripController controller = new TripController(notUserRepository, provider);
            ViewResult result = controller.Edit(notUserTrip.TripID) as ViewResult;
            Trip model = result.Model as Trip;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Cannot_Edit_nonexistent_Trip()
        {
            TripController controller = new TripController(repository, provider);
            ViewResult result = controller.Edit(Guid.NewGuid()) as ViewResult;
            Trip model = result.Model as Trip;
        }

        [TestMethod]
        public void Edit_edits_correct_Trip()
        {
            TripController controller = new TripController(repository, provider);
            userTrip.description = "My editted trip";
            userTrip.destination = "No where";
            controller.Edit(userTrip);
            ViewResult result =  controller.Index() as ViewResult;
            IQueryable<Trip> model = result.Model as IQueryable<Trip>;
            Assert.AreEqual(1, model.Count());
            Assert.IsTrue(model.Contains(userTrip));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Cannot_Edit_Post_changes_to_other_users_trips()
        {
            TripController controller = new TripController(notUserRepository, provider);
            notUserTrip.description = "Editted Trip";
            controller.Edit(notUserTrip);
            ViewResult result = controller.Index() as ViewResult;
            IQueryable<Trip> model = result.Model as IQueryable<Trip>;
        }

        [TestMethod]
        public void Delete_returns_correct_Trip()
        {
            TripController controller = new TripController(repository, provider);
            ViewResult result = controller.Delete(userTrip.TripID) as ViewResult;
            Trip model = result.Model as Trip;
            Assert.AreEqual(userTrip, model);
        }
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Cannot_Delete_other_user_trips()
        {
            TripController controller = new TripController(notUserRepository, provider);
            ViewResult result = controller.Delete(notUserTrip.TripID) as ViewResult;
            Trip model = result.Model as Trip;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Cannot_Delete_nonexistent_Trip()
        {
            TripController controller = new TripController(repository, provider);
            ViewResult result = controller.Delete(Guid.NewGuid()) as ViewResult;
            Trip model = result.Model as Trip;
        }

        [TestMethod]
        public void DeleteConfirmed_deletes_correct_Trip()
        {
            TripController controller = new TripController(repository, provider);
            controller.DeleteConfirmed(userTrip.TripID);
            ViewResult result = controller.Index() as ViewResult;
            IQueryable<Trip> model = result.Model as IQueryable<Trip>;
            Assert.IsFalse(model.Contains(userTrip));
            Assert.AreEqual(0, model.Count());
        }
        
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Cannot_DeleteConfirmed_other_user_trips()
        {
            TripController controller = new TripController(notUserRepository, provider);
            ViewResult result = controller.DeleteConfirmed(notUserTrip.TripID) as ViewResult;
            IQueryable<Trip> model = result.Model as IQueryable<Trip>;

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Cannot_DeleteConfirmed_nonexistent_Trip()
        {
            TripController controller = new TripController(repository, provider);
            ViewResult result = controller.DeleteConfirmed(Guid.NewGuid()) as ViewResult;
            IQueryable<Trip> model = result.Model as IQueryable<Trip>;
        }
    }
}
