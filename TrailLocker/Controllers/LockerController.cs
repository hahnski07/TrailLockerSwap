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

using TrailLocker.Authentication;
using AttributeRouting;
using AttributeRouting.Web;
using AttributeRouting.Web.Mvc;

namespace TrailLocker.Controllers
{ 
    public class LockerController : Controller
    {
        private IRepository<Locker> repository;
        private IAuthenticatedUserProvider provider; 

        public LockerController()
        {
            this.repository = new Repository<Locker>(new DBUnitOfWork());
            this.provider = new FormsAuthenticatedUserProvider(this);
        }

        public LockerController(IRepository<Locker> repository, IAuthenticatedUserProvider provider)
        {
            this.repository = repository;
            this.provider = provider;
        }

        // Should this return only the logged in user's lockers?
        [GET("lockers")]
        public ViewResult Index()
        {
            return View(repository.FindAll().ToList());
        }

        // Unit test user permissions
        [GET("locker/{id:guid}")]
        public ViewResult Details(Guid id)
        {
            Locker locker = repository.FindBy(x => x.LockerID == id).Single();
            return View(locker);
        }

        [GET("locker/create")]
        public ActionResult Create()
        {
            return View();
        }

        [POST("locker/create")]
        public ActionResult Create(Locker locker)
        {
            if (ModelState.IsValid)
            {
                locker.LockerID = Guid.NewGuid();
                repository.Add(locker);
                repository.Commit();
                return RedirectToAction("Index");
            }

            return View(locker);
        }
        
        [GET("locker/edit/{id:guid}")]
        public ActionResult Edit(Guid id)
        {
            Locker locker = repository.FindBy(x => x.LockerID == id).Single();
            return View(locker);
        }

        [POST("locker/edit")]
        public ActionResult Edit(Locker locker)
        {
            if (ModelState.IsValid)
            {
                repository.Attach(locker);
                repository.Commit();
                return RedirectToAction("Index");
            }
            return View(locker);
        }

        [GET("locker/delete/{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            Locker locker = repository.FindBy(x => x.LockerID == id).Single();
            return View(locker);
        }

        
        [POST("locker/delete/{id:guid}")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Locker locker = repository.FindBy(x => x.LockerID == id).Single();
            repository.Remove(locker);
            repository.Commit();
            return RedirectToAction("Index");
        }
    }
}
