using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrailLocker.Models;
using TrailLocker.Repository;
using TrailLocker.Models.Items;

namespace TrailLocker.Controllers
{ 
    public class LockerController : SuperController
    {

         DBUnitOfWork unitOfWork = new DBUnitOfWork();

        private Repository<Locker> LockerDB;

        public LockerController()
        {
            LockerDB  = new Repository<Locker>(unitOfWork);
        }

        public ViewResult Index()
        {
            return View(LockerDB.FindAll().ToList());
        }

        //
        // GET: /Item/Details/5

        public ViewResult Details(Guid id)
        {
            Locker locker = LockerDB.FindBy(x => x.LockerID == id).Single();
            return View(locker);
        }

        //
        // GET: /Item/Create

        public ActionResult Create()
        {

            return View();
        }


        //
        // POST: /Item/Create

        [HttpPost]
        public ActionResult Create(Locker locker)
        {
            if (ModelState.IsValid)
            {
                locker.LockerID = Guid.NewGuid();
                LockerDB.Add(locker);
                LockerDB.Commit();
                return RedirectToAction("Index");
            }

            return View(locker);
        }
        
        //
        // GET: /Item/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Locker locker = LockerDB.FindBy(x => x.LockerID == id).Single();
            return View(locker);
        }

        //
        // POST: /Item/Edit/5

        [HttpPost]
        public ActionResult Edit(Locker locker)
        {
            if (ModelState.IsValid)
            {
                LockerDB.Attach(locker);
                LockerDB.Commit();
                return RedirectToAction("Index");
            }
            return View(locker);
        }

        //
        // GET: /Item/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Locker locker = LockerDB.FindBy(x => x.LockerID == id).Single();
            return View(locker);
        }

        //
        // POST: /Item/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Locker locker = LockerDB.FindBy(x => x.LockerID == id).Single();
            LockerDB.Remove(locker);
            LockerDB.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            LockerDB.Dispose();
            base.Dispose(disposing);
        }
    }
}
