using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Routing;
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
        public ActionResult Index()
        {
            // Find trips for the logged in user
            IQueryable<Trip> trips = repository.FindBy(t => t.UserID == provider.AuthenticatedUser.UserID);
            return View(trips);
        }

        [GET("trip/{id:guid}")]
        public ActionResult Details(Guid id)
        {
            Trip trip = repository.FindBy(x => x.TripID == id).Single();
            if (trip.UserID != provider.AuthenticatedUser.UserID)
            {
                throw new UnauthorizedAccessException("Trip doesn't belong to user");
            }
            return View(trip);
        }

        [GET("trip/create")]
        public ActionResult Create()
        {
            ViewBag.userID = provider.AuthenticatedUser.UserID;
            return View();
        } 

        [POST("trip/create")]
        public ActionResult Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.TripID = Guid.NewGuid();
                trip.trip_leader = provider.AuthenticatedUser;

                repository.Add(trip);
                repository.Commit();

                return RedirectToAction("Details",
                    new RouteValueDictionary(new { id = trip.TripID}));
            }

            return View(trip);
        }
        
        [GET("trip/edit/{id:guid}")]
        public ActionResult Edit(Guid id)
        {
            Trip trip = repository.FindBy(x => x.TripID == id).Single();
            if (trip.UserID != provider.AuthenticatedUser.UserID)
            {
                throw new UnauthorizedAccessException("Trip doesn't belong to user");
            }
            return View(trip);
        }

        [POST("trip/edit")]
        public ActionResult Edit(Trip trip)
        {
            if (trip.UserID != provider.AuthenticatedUser.UserID)
            {
                throw new UnauthorizedAccessException("Trip doesn't belong to user");
            }
            if (ModelState.IsValid)
            {
                repository.Attach(trip);
                repository.Commit();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        [GET("trip/delete/{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            Trip trip = repository.FindBy(x => x.TripID == id).Single();
            if (trip.UserID != provider.AuthenticatedUser.UserID)
            {
                throw new UnauthorizedAccessException("Trip doesn't belong to user");
            }
            return View(trip);
        }

        [POST("trip/delete/{id:guid}")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Trip trip = repository.FindBy(x => x.TripID == id).Single();
            if (trip.UserID != provider.AuthenticatedUser.UserID)
            {
                throw new UnauthorizedAccessException("Trip doesn't belong to user");
            }
            repository.Remove(trip);
            repository.Commit();
            return RedirectToAction("Index");
        }
    }
}