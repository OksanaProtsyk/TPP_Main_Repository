﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPP_MainProject.Models;
using TPP_MainProject.Models.constants;
using TPP_MainProject.Models.ViewModels;

namespace TPP_MainProject.Controllers
{
    public class DistributorController : Controller
    {

        private ApplicationDbContext _db = new ApplicationDbContext();


        // GET: /Distributor/
        public ActionResult Index()
        {
            var rolesList = new List<RoleViewModel>();
            foreach (var role in _db.Roles)
            {
                var roleModel = new RoleViewModel(role);
                rolesList.Add(roleModel);
            }
            if (User.IsInRole(RolesConst.ACCOUNTANT))
            {
                return RedirectToAction("Index", RolesConst.ACCOUNTANT);
            }
            if (User.IsInRole(RolesConst.PROGRAMER))
            {
                return RedirectToAction("Index", RolesConst.PROGRAMER);
            }
            //_db.Dispose();
             return RedirectToAction("About", "Home");;
        }

       
	}
}