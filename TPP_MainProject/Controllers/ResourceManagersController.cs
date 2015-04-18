﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPP_MainProject.Models;
using TPP_MainProject.Models.entities;
using TPP_MainProject.Models.repository;

namespace TPP_MainProject.Controllers
{
    public class ResourceManagersController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: ResourceManagers
        public ActionResult Index()
        {
            IEnumerable<Resourse> resourceItems = unitOfWork.ResourceRepository.Get().ToList<Resourse>();
            return View(resourceItems);
        }

        // GET: ResourceManagers/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resourse resource = unitOfWork.ResourceRepository.GetByID(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // GET: ResourceManagers/Create
        public ActionResult Create()
        {
            ViewBag.resourses = unitOfWork.ResourceRepository.Get().ToList();
            return View();
        }

        // POST: ResourceManagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Resourse resourceItem)
        {
            
                unitOfWork.ResourceRepository.Insert(resourceItem);
                unitOfWork.Save();

                return RedirectToAction("Index", "ResourceManagers");
        }

        // GET: ResourceManagers/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resourse resourceItem = unitOfWork.ResourceRepository.GetByID(id);
            if (resourceItem == null)
            {
                return HttpNotFound();
            }
            return View(resourceItem);
        }

        // POST: ResourceManagers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Resourse resourceItem)
        {
            if (ModelState.IsValid)
            {
               unitOfWork.ResourceRepository.Update(resourceItem);
               unitOfWork.Save();
               
              //  db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resourceItem);
        }

        // GET: ResourceManagers/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resourse resource = unitOfWork.ResourceRepository.GetByID(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // POST: ResourceManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resourse resource = unitOfWork.ResourceRepository.GetByID(id);
            unitOfWork.ResourceRepository.Delete(resource);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

    
    }
}
