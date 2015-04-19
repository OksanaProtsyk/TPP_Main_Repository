using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TPP_MainProject.Models.entities;
using TPP_MainProject.Models;
using TPP_MainProject.Models.repository;

namespace TPP_MainProject.Controllers
{
    public class ProgrammerController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        private ApplicationDbContext _db = new ApplicationDbContext();

       /* public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var workItemList = new Collection<WorkItem>();
            var workI = from s in _db.
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                workI = workI.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    workI = workI.OrderByDescending(s => s.Name);
                    break;
                default:
                    workI = workI.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(workI.ToPagedList(pageNumber, pageSize));

            //return View(unitOfWork.ProductItemRepository.Get().ToList());
        } */
     public ActionResult Index()
        {
          
            IEnumerable<WorkItem> workItems =  unitOfWork.WorkItemRepository.Get();
            return View(workItems);
        }

        public ActionResult Details(string id)
        {
           if (id == null)
            {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           WorkItem workItem = unitOfWork.WorkItemRepository.GetByID(id);
            if (workItem == null)
            {
                  return HttpNotFound();
            }
            return View();
        }

        // GET: /Worker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Worker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,FistName,LastName,Organization,City,Country,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Salary,startWorkDate")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                //db.Users.Add(worker);
             //   db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(worker);
        }

        // GET: /Worker/Edit/5
        public ActionResult Edit(string id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Worker worker = db.Users.Find(id);
            //if (worker == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // POST: /Worker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="Id,FistName,LastName,Organization,City,Country,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Salary,startWorkDate")] Worker worker)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(worker).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(worker);
        //}

        //// GET: /Worker/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Worker worker = db.Users.Find(id);
        //    if (worker == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(worker);
        //}

        //// POST: /Worker/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Worker worker = db.Users.Find(id);
        //    db.Users.Remove(worker);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
              //  db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
