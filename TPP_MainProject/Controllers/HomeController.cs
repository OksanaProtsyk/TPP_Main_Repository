using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPP_MainProject.Models;
using TPP_MainProject.Models.entities;
using System.Data;

namespace TPP_MainProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Order()
        {
            return View();

        }
       /* [HttpPost]
        public ActionResult Create(Order  newOrder)
        {

            if (ModelState.IsValid)
            {
                db.AddToMovies(newOrder);
                db.SaveChanges(newOrder);

                return RedirectToAction("Index");
            }
            else
            {
                return View(newOrder);
            }
        }*/

        public ActionResult Customer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Custumer(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                //-------------------------------
                // реальная отправка сообщения

                return View("CustomerSent");
            }
            return View();
        }


    }
}