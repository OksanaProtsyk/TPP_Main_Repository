using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPP_MainProject.Models;
using TPP_MainProject.Models.constants;
using TPP_MainProject.Models.entities;
using TPP_MainProject.Models.repository;
using TPP_MainProject.Models.ViewModels;

namespace TPP_MainProject.Controllers
{
    public class OperatorController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Operator
        public ActionResult Index()
        {
            IEnumerable<WorkItem> workItems = unitOfWork.WorkItemRepository.Get().ToList();

            /*var @workItem = new WorkItem()
            {
                Id = 0,
                Name = "DefaultWorkItem",
                Description = "DefaultWorkItemDescription",
                DueDate = new DateTime().ToLocalTime(),
                Status = TaskStatus.Completed,
            };

            unitOfWork.WorkItemRepository.Insert(@workItem);*/

            return View(workItems);
        }

        // GET: Operator/Details/5
        
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

        // GET: Operator/Create
        public ActionResult Create()
        {
            ViewBag.workItems = unitOfWork.WorkItemRepository.Get().ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkItemViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var @workItem = new WorkItem() {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    Status = model.Status,
                    AssignedWorker = model.AssignedWorker,
                    assignedProject = model.AssignedProject
                };

                unitOfWork.WorkItemRepository.Insert(@workItem);

                return RedirectToAction("Index", "Operator");
            //}

            //return View();
        }

        // GET: Operator/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkItem @workItem = unitOfWork.WorkItemRepository.GetByID(id);
            if (@workItem == null)
            {
                return HttpNotFound();
            }
            return View(@workItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(@operator).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Operator/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkItem @workItem = unitOfWork.WorkItemRepository.GetByID(id);
            if (@workItem == null)
            {
                return HttpNotFound();
            }
            return View(@workItem);
        }

        // POST: Operator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WorkItem @workItem = unitOfWork.WorkItemRepository.GetByID(id);
            //db.ApplicationUsers.Remove(@workItem);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
