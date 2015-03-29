using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPP_MainProject.Models.entities;
using TPP_MainProject.Models;
using TPP_MainProject.Models.repository;

namespace TPP_MainProject.Controllers
{
    public class AccountantController : Controller
    {
       // private ApplicationDbContext db = new ApplicationDbContext();
   
        GenericRepository<Accountant> db = new UnitOfWork().AccountantRepository;
        //// GET: /Accountant/
        public ActionResult Index()
        {
           
            return View();
        }

        //// GET: /Accountant/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Accountant accountant = db.Users.Find(id);
        //    if (accountant == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(accountant);
        //}

        //// GET: /Accountant/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /Accountant/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="Id,FistName,LastName,Organization,City,Country,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Salary,startWorkDate")] Accountant accountant)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(accountant);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(accountant);
        //}

        //// GET: /Accountant/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Accountant accountant = db.Users.Find(id);
        //    if (accountant == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(accountant);
        //}

        //// POST: /Accountant/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="Id,FistName,LastName,Organization,City,Country,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Salary,startWorkDate")] Accountant accountant)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(accountant).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(accountant);
        //}

        //// GET: /Accountant/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Accountant accountant = db.Users.Find(id);
        //    if (accountant == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(accountant);
        //}

        //// POST: /Accountant/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Accountant accountant = db.Users.Find(id);
        //    db.Users.Remove(accountant);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
