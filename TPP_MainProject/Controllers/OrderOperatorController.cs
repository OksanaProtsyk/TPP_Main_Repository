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
using TPP_MainProject.Models.constants;

namespace TPP_MainProject.Controllers
{
    public class OrderOperatorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        private IEnumerable<Order> activeOrders;

        // GET: /OrderOperator/
        public ActionResult Index()
        {
            activeOrders  = unitOfWork.OrderRepository.Get().Where(s => s.orderStartus.Equals(OrderStatus.Initiating));
            return View(activeOrders);
        }



        public ActionResult Reject(int? id)
        {
            Order ord = unitOfWork.OrderRepository.GetByID(id);
            ord.orderStartus = OrderStatus.Rejected;
            unitOfWork.OrderRepository.Update(ord);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }


        public ActionResult Confrim(int? id)
        {

            IEnumerable<ApplicationUser> them = unitOfWork.UserRepository.Get().Where(s => s.RoleName.Equals(RolesConst.MANAGER));

            ViewBag.pm = them;
            //make enumerable from enum
            //ViewBag.ps = (IEnumerable<ProjectStatus>)Enum.GetValues(typeof(ProjectStatus)); 
            Project proj = new Project();
           
            return View(proj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confrim(Project pro)
        {
            if (ModelState.IsValid)
            {
                Order ord = unitOfWork.OrderRepository.GetByID(pro.id);

                ord.orderStartus = OrderStatus.Processiong;
                unitOfWork.OrderRepository.Update(ord);

             
                pro.order = ord;
                pro.costs = ord.Total;
                //глаза мои етого не видели и руки не писали @Pifagor
                IEnumerable<ApplicationUser> them =  unitOfWork.UserRepository.Get().Where(s => s.RoleName.Equals(RolesConst.MANAGER));
                foreach (ApplicationUser manager in them)
                {
                    if (manager.LastName.Equals(pro.nameProjectManager))
                        pro.projectManager = manager;
                }
              
                pro.projectStatus = ProjectStatus.Initial;

                unitOfWork.ProjectRepository.Insert(pro);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View();
        }

        //// GET: /OrderOperator/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// GET: /OrderOperator/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /OrderOperator/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="id,orderDate,completeDate,orderStartus,detailDescription,Total")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Orders.Add(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(order);
        //}

        //// GET: /OrderOperator/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Order order = unitOfWork.OrderRepository.GetByID(id);
        //    //activeOrders = unitOfWork.OrderRepository.Get().Where(s => s.orderStartus.Equals(OrderStatus.Initiating));
        //    //foreach (Order item in activeOrders)
        //    //{
        //    //    if (item.id == id)
        //    //        order = item;
        //    //}
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// POST: /OrderOperator/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="id,orderDate,completeDate,orderStartus,detailDescription,Total")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(order).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(order);
        //}

        //// GET: /OrderOperator/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    IEnumerable<Project> newProjects = unitOfWork.ProjectRepository.Get();
        //     return View();
        //}

        //// POST: /OrderOperator/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Order order = db.Orders.Find(id);
        //    db.Orders.Remove(order);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
