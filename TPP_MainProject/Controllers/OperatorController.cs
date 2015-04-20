using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TPP_MainProject.Models.entities;
using TPP_MainProject.Models.repository;
using TPP_MainProject.Models.ViewModels;

namespace TPP_MainProject.Controllers
{
    public class OperatorController : Controller
    {
        
        UnitOfWork unitOfWork = new UnitOfWork();

         [Authorize(Roles = "Operator")] 
        // GET: Operator
        public ActionResult Index()
        {
            IEnumerable<WorkItem> workItems = unitOfWork.WorkItemRepository.Get().ToList();
            return View(workItems);
        }

        // GET: Operator/Details/5

        public ActionResult Details(string id = "")
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
                unitOfWork.Save();

                return RedirectToAction("Index", "Operator");
        }

        // GET: Operator/Edit/5
        public ActionResult Edit(string id = "")
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
            WorkItem workItem = unitOfWork.WorkItemRepository.GetByID(model.Id);
            workItem.Name = model.Name;
            workItem.Description = model.Description;
            workItem.DueDate = model.DueDate;
            workItem.Status = model.Status;
            workItem.AssignedWorker = model.AssignedWorker;
            workItem.assignedProject = model.AssignedProject;

            unitOfWork.WorkItemRepository.Insert(workItem);
            unitOfWork.Save();
            
            return RedirectToAction("Index");
        }

        // GET: Operator/Delete/5
        public ActionResult Delete(string id = "")
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
        public ActionResult DeleteConfirmed(string id = "")
        {
            WorkItem @workItem = unitOfWork.WorkItemRepository.GetByID(id);
            unitOfWork.WorkItemRepository.Delete(@workItem);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

    
    }
}
