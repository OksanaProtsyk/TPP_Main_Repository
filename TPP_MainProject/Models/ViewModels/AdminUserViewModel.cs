using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPP_MainProject.Models.entities;

namespace TPP_MainProject.Models.ViewModels
{
    public class AdminUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FistName { get; set; }     
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }


        public AdminUserViewModel() { }
        public AdminUserViewModel(ApplicationUser user)
        {
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.FistName = user.FistName;
            this.LastName = user.LastName;
            this.Organization = user.Organization;
            this.City = City;
            this.Country = Country;
            this.Role = user.RoleName;
            this.Password = user.PasswordHash;
        }
    }
}