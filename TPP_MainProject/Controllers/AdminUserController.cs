using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using TPP_MainProject.Models;
using TPP_MainProject.Models.entities;
using TPP_MainProject.Models.repository;
using TPP_MainProject.Models.ViewModels;
using System.Collections.ObjectModel;
using TPP_MainProject.Models.constants;
using System.Data.Entity;
using PagedList;

namespace TPP_MainProject.Controllers
{
    public class AdminUserController : Controller
    {        
        private ApplicationUserManager _userManager;
        private UnitOfWork unityOfWork = new UnitOfWork();
       // ApplicationUser user;

        public AdminUserController()
        { }
        public AdminUserController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

     
        // GET: AdminUser

        private ApplicationDbContext _db = new ApplicationDbContext();

       
        //
        // GET: /Admin/Index
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString,int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.RoleSortParm = sortOrder == "Role" ? "Role_desc" : "Role";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var rolesList = new Collection<AdminUserViewModel>();
            var users = from s in _db.Users
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                case "Role":
                    users = users.OrderBy(s => s.RoleName);
                    break;
                case "Role_desc":
                    users = users.OrderByDescending(s => s.RoleName);
                    break;
                default:
                    users = users.OrderBy(s => s.UserName);
                    break;
            }
            foreach (var role in users)
            {
                var moselItem = new AdminUserViewModel(role);
                rolesList.Add(moselItem);
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(rolesList.ToPagedList(pageNumber, pageSize));
            
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.roles = _db.Roles.ToList();
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
         public ActionResult Create(AdminUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                var role = model.RoleName;

                /*  
                  switch (role)
                  { 
                          /*NEED TO CHANGE - gamnocode*/
                /*
                    case RolesConst.ADMIN:
                        this.user = new ApplicationUser()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FistName = model.FistName,
                            LastName = model.LastName,
                            Organization = model.Organization,
                            City = model.City,
                            Country = model.Country,
                            RoleName = model.RoleName
                        };
                        break;
                    case RolesConst.ACCOUNTANT:
                        this.user = new Accountant()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FistName = model.FistName,
                            LastName = model.LastName,
                            Organization = model.Organization,
                            City = model.City,
                            Country = model.Country,
                            RoleName = model.RoleName
                        };
                        break;
                    case RolesConst.RESOURSE_MANAGER:
                        this.user = new ResourceManager()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FistName = model.FistName,
                            LastName = model.LastName,
                            Organization = model.Organization,
                            City = model.City,
                            Country = model.Country,
                            RoleName = model.RoleName
                        };
                        break;
                    case RolesConst.CUSTOMER:
                        this.user = new Customer()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FistName = model.FistName,
                            LastName = model.LastName,
                            Organization = model.Organization,
                            City = model.City,
                            Country = model.Country,
                            RoleName = model.RoleName
                        };

                        break;
                    case RolesConst.HR:
                        this.user = new HR()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FistName = model.FistName,
                            LastName = model.LastName,
                            Organization = model.Organization,
                            City = model.City,
                            Country = model.Country,
                            RoleName = model.RoleName
                        };
                        break;
                    case RolesConst.MANAGER:
                        this.user = new Manager()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FistName = model.FistName,
                            LastName = model.LastName,
                            Organization = model.Organization,
                            City = model.City,
                            Country = model.Country,
                            RoleName = model.RoleName
                        };
                        break;
                    case RolesConst.OPERATOR:
                        this.user = new Operator()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FistName = model.FistName,
                            LastName = model.LastName,
                            Organization = model.Organization,
                            City = model.City,
                            Country = model.Country,
                            RoleName = model.RoleName
                        };
                        break;
                    case RolesConst.PROGRAMER:
                        this.user = new Programmer()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FistName = model.FistName,
                            LastName = model.LastName,
                            Organization = model.Organization,
                            City = model.City,
                            Country =
                            model.Country,
                            RoleName = model.RoleName
                        };
                        break;
                    default:
                        this.user = new Customer()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FistName = model.FistName,
                            LastName = model.LastName,
                            Organization = model.Organization,
                            City = model.City,
                            Country = model.Country,
                            RoleName = model.RoleName
                        };
                        break;

                }
               */
                var user = new Customer()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FistName = model.FistName,
                    LastName = model.LastName,
                    Organization = model.Organization,
                    City = model.City,
                    Country = model.Country,
                    RoleName = model.RoleName
                };
              

                IdentityResult result = UserManager.Create(user, model.Password);
                _db.AddUserToRole(UserManager, user.Id, model.RoleName);
                _db.SaveChanges();

                return RedirectToAction("Index", "AdminUser");


                // If we got this far, something failed, redisplay form

            }
            else
                return View();
        }

        //
        // GET: /Department/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id = "")
        {
            ApplicationUser user = _db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        //
        // GET: /Department/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id = "")
        {
            ApplicationUser user = _db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            EditUserViewModel model = new EditUserViewModel(user);
            ViewBag.roles = _db.Roles.ToList();
            return View(model);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
               ApplicationUser user =unityOfWork.UserRepository.GetByID(model.Id);
               user.LastName = model.LastName;
               user.FistName = model.FistName;
               user.Country = model.Country;
               user.City = model.City;
               user.Organization = model.Organization;
               if (user.RoleName != model.Role)
               {
                   _db.RemoveFromRole(UserManager, user.Id, model.Role);
                   user.RoleName = model.Role;
                   _db.AddUserToRole(UserManager, user.Id, model.Role);
               }
                unityOfWork.Save();

               // _db.Entry(user).State = EntityState.Modified;

                // TODO
              // _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // GET: /Department/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id = "")
        {
            ApplicationUser department = _db.Users.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        //
        // POST: /Department/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser user = _db.Users.Find(id);
            _db.Users.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }  

    }
}