using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPP_MainProject.Models;
using TPP_MainProject.Models.entities;

namespace TPP_MainProject.Controllers
{
    public class SalesManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SalesManager
        public ActionResult Index()
        {
            return View(db.ProductItems.ToList());
        }

        // GET: SalesManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = db.ProductItems.Find(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(productItem);
        }

        // GET: SalesManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,shortDescription,description,price")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                db.ProductItems.Add(productItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productItem);
        }

        // GET: SalesManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = db.ProductItems.Find(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(productItem);
        }

        // POST: SalesManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,shortDescription,description,price")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productItem);
        }

        // GET: SalesManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = db.ProductItems.Find(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(productItem);
        }

        // POST: SalesManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductItem productItem = db.ProductItems.Find(id);
            db.ProductItems.Remove(productItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
