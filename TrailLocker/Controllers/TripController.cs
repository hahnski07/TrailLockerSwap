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
    public class TripController : SuperController
    {

        public TripController()
        {

        }
        //
        // GET: /Trip/

        public ViewResult Index()
        {
            User user = get_current_user();
            ICollection<Trip> trips = user.trips;
            return View(trips);
        }

        //
        // GET: /Trip/Details/5

        public ViewResult Details(Guid id)
        {
            Trip trip = TripDB.FindBy(x => x.TripID == id).Single();

            return View(trip);
        }

        //
        // GET: /Trip/Create

        /*
        public ActionResult Create(Guid userID)
        {
            ViewBag.userID = userID;
            return View();
        } 
        */
        //
        // POST: /Trip/Create

        [HttpPost]
        public ActionResult Create(Trip trip , Guid userID)
        {
            if (ModelState.IsValid)
            {
                User trip_leader = UserDB.FindBy(x => x.UserID ==userID).Single();

                trip.TripID = Guid.NewGuid();
                TripDB.Add(trip);

                trip.trip_leader = trip_leader;
                trip_leader.trips.Add(trip);

                UserDB.Attach(trip_leader);


                UserDB.Commit();
                TripDB.Commit();
                return RedirectToAction("Index");
            }

            return View(trip);
        }

        public ActionResult Create()
        {
            User trip_leader = get_current_user();
            Trip new_trip = new Trip(trip_leader.UserID);

            TripDB.Add(new_trip);
            TripDB.Commit();

            trip_leader.trips.Add(new_trip);
            UserDB.Attach(trip_leader);
            UserDB.Commit();

            return RedirectToAction("Details", new { id = new_trip.TripID });
        }
        
        //
        // GET: /Trip/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Trip trip = TripDB.FindBy(x => x.TripID == id).Single();
            return View(trip);
        }

        //
        // POST: /Trip/Edit/5

        [HttpPost]
        public ActionResult Edit(Trip trip)
        {
            if (ModelState.IsValid)
            {

                TripDB.Attach(trip);
                TripDB.Commit();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        //
        // GET: /Trip/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Trip trip = TripDB.FindBy(x => x.TripID == id).Single();
            return View(trip);
        }

        //
        // POST: /Trip/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            

            Trip trip = TripDB.FindBy(x => x.TripID == id).Single();
            TripDB.Remove(trip);
            TripDB.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            TripDB.Dispose();
            base.Dispose(disposing);
        }
    }
}