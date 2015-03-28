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

namespace TPP_MainProject.Controllers
{
    public class AdminUserController : Controller
    {
          
         private ApplicationUserManager _userManager;
        private UnitOfWork unityOfWork = new UnitOfWork();

        public AdminUserController()
        { }
        public AdminUserController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

     
        // GET: AdminUser

        private ApplicationDbContext _db = new ApplicationDbContext();

       
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Index()
        {
            var rolesList = new Collection<AdminUserViewModel>();
            foreach (var role in _db.Users)
            {
                var moselItem = new AdminUserViewModel(role);
                rolesList.Add(moselItem);
            }
            return View(rolesList);
        }

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdminUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Customer()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FistName = model.FistName,
                    LastName = model.LastName,
                    Organization = model.Organization,
                    City = model.City,
                    Country = model.Country,

                };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Customer");
                    user.RoleName = UserManager.GetRoles(user.Id).First();
                    UserManager.Update(user);
                    await SignInAsync(user, isPersistent: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

   
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}