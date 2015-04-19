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
using TPP_MainProject.Models;
using TPP_MainProject.Models.entities;
using TPP_MainProject.Models.repository;

namespace TPP_MainProject.Controllers
{
    public class SalesManagerController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: SalesManager
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
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
            var productItemList = new Collection<ProductItem>();
            var productI = from s in _db.ProductItems
                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                productI = productI.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    productI = productI.OrderByDescending(s => s.Name);
                    break;
                default:
                    productI = productI.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(productI.ToPagedList(pageNumber, pageSize));
            
            //return View(unitOfWork.ProductItemRepository.Get().ToList());
        }

        // GET: SalesManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = unitOfWork.ProductItemRepository.GetByID(id);
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
                unitOfWork.ProductItemRepository.Insert(productItem);
                unitOfWork.Save();
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
            ProductItem productItem = unitOfWork.ProductItemRepository.GetByID(id);
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
        public ActionResult Edit( ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.ProductItemRepository.Update(productItem);
                unitOfWork.Save();
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
            ProductItem productItem = unitOfWork.ProductItemRepository.GetByID(id);
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
            ProductItem productItem = unitOfWork.ProductItemRepository.GetByID(id);
            unitOfWork.ProductItemRepository.Delete(productItem);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }



       
    }
}
