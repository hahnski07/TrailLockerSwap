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

namespace TrailLocker.Controllers
{ 
    public class ItemController : Controller
    {
        private IRepository<Item> repository;
        private IAuthenticatedUserProvider provider;

        public ItemController()
        {
            this.repository = new Repository<Item>(new DBUnitOfWork());
            this.provider = new FormsAuthenticatedUserProvider(this);
        }

        public ItemController(IRepository<Item> repository, IAuthenticatedUserProvider provider)
        {
            this.repository = repository;
            this.provider = provider;
        }

        //public ViewResult Index()
        //{
        //    return View(repository.FindAll().ToList());
        //}

        ////
        //// GET: /Item/Details/5

        //public ViewResult Details(Guid id)
        //{

        //    Locker item = repository.FindBy(x => x.LockerID == id).Single();
        //    item.
        //    return View(item);
        //}

        ////
        //// GET: /Item/Create

        //public ActionResult Create()
        //{
        //    return View();
        //} 

        ////
        //// POST: /Item/Create

        //[HttpPost]
        //public ActionResult Create(Locker item)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        item.LockerID = Guid.NewGuid();
        //        ItemDB.Add(item);
        //        ItemDB.Commit();
        //        return RedirectToAction("Index");
        //    }

        //    return View(item);
        //}
        
        ////
        //// GET: /Item/Edit/5
 
        //public ActionResult Edit(Guid id)
        //{
        //    Locker item = ItemDB.FindBy(x => x.LockerID == id).Single();
        //    return View(item);
        //}

        ////
        //// POST: /Item/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Locker item)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ItemDB.Attach(item);
        //        ItemDB.Commit();
        //        return RedirectToAction("Index");
        //    }
        //    return View(item);
        //}

        ////
        //// GET: /Item/Delete/5
 
        //public ActionResult Delete(Guid id)
        //{
        //    Locker item = ItemDB.FindBy(x => x.LockerID == id).Single();
        //    return View(item);
        //}

        ////
        //// POST: /Item/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Locker item = ItemDB.FindBy(x => x.LockerID == id).Single();
        //    ItemDB.Remove(item);
        //    ItemDB.Commit();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    ItemDB.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
