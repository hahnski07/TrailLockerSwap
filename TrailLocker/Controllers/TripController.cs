using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrailLocker.Models;
using TrailLocker.Repository;

using TrailLocker.Authentication;
using AttributeRouting;
using AttributeRouting.Web;
using AttributeRouting.Web.Mvc;

namespace TrailLocker.Controllers
{ 
    public class TripController : Controller
    {
        private IRepository<Trip> repository;
        private IAuthenticatedUserProvider provider;

        public TripController()
        {
            this.repository = new Repository<Trip>(new DBUnitOfWork());
            this.provider = new FormsAuthenticatedUserProvider(this);
        }

        public TripController(IRepository<Trip> repository, IAuthenticatedUserProvider provider)
        {
            this.repository = repository;
            this.provider = provider;
        }

        [GET("trips")]
        public ViewResult Index()
        {
            // Find trips for the logged in user
            IQueryable<Trip> trips = repository.FindBy(t => t.trip_leader.UserID == provider.AuthenticatedUser.UserID);
            return View(trips);
        }

        // TODO: Add unit test to check when the signed in user doesn't own this trip
        // TODO: Add unit test to check what happens if this id doesn't exists in database
        [GET("trip/{id}")]
        public ViewResult Details(Guid id)
        {
            Trip trip = repository.FindBy(x => x.TripID == id).Single();
            return View(trip);
        }

        [GET("trip/create")]
        public ActionResult Create()
        {
            return View();
        } 

        // TODO: Write unit tests to validate this works correctly
        [POST("trip/create")]
        public ActionResult Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.TripID = Guid.NewGuid();
                trip.trip_leader = provider.AuthenticatedUser;

                repository.Add(trip);
                repository.Commit();

                return RedirectToAction("Details", new { id = trip.TripID });
            }

            return View(trip);
        }
        
        // TODO: Unit test signed in user owns the trip
        // TODO: Unit test what happens on id not found
        [GET("trip/edit/{id}")]
        public ActionResult Edit(Guid id)
        {
            Trip trip = repository.FindBy(x => x.TripID == id).Single();
            return View(trip);
        }

        // TODO: Unit test signed in user owns the trip
        // TODO: Unit test validate this works
        [POST("trip/edit")]
        public ActionResult Edit(Trip trip)
        {
            if (ModelState.IsValid)
            {
                repository.Attach(trip);
                repository.Commit();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        // TODO: Unit test user owns this trip
        // TODO: Unit test returns correct id
        [GET("trip/delete/{id}")]
        public ActionResult Delete(Guid id)
        {
            Trip trip = repository.FindBy(x => x.TripID == id).Single();
            return View(trip);
        }

        [POST("trip/delete/{id}")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Trip trip = repository.FindBy(x => x.TripID == id).Single();
            repository.Remove(trip);
            repository.Commit();
            return RedirectToAction("Index");
        }
    }
}