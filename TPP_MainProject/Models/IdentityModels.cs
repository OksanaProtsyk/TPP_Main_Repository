using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TPP_MainProject.Models.entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;
using System;
using System.Linq;
using TPP_MainProject.Models.constants;

namespace TPP_MainProject.Models
{


       /* public DbSet<Order> Orders { get; set; }
        public DbSet<ProductItem> Items { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Resourse> Resourses { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Order>()
                .Map<Order>(m => m.Requires("Type").HasValue("Order"))
                .Map<CustomOrder>(m => m.Requires("Type").HasValue("CustomOrder"))
                .Map<TemplateOrder>(m => m.Requires("Type").HasValue("TemplateOrder"));
           modelBuilder.Entity<ApplicationUser>()
               .Map<Customer>(m => m.Requires("RoleId").HasValue("Customer"))
               .Map<Accountant>(m => m.Requires("RoleId").HasValue("Accountant"))
               .Map<Manager>(m => m.Requires("RoleId").HasValue("Manager"))
               .Map<Operator>(m => m.Requires("RoleId").HasValue("Operator"))
               .Map<Programmer>(m => m.Requires("RoleId").HasValue("Programmer"))
               .Map<ResourceManager>(m => m.Requires("RoleId").HasValue("ResourseManager"));
           
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
*/
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("ModelBuilder is NULL");
            }

            base.OnModelCreating(modelBuilder);

            //Defining the keys and relations
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<ApplicationRole>().HasKey<string>(r => r.Id).ToTable("AspNetRoles");
            modelBuilder.Entity<ApplicationUser>().HasMany<ApplicationUserRole>((ApplicationUser u) => u.UserRoles);
            modelBuilder.Entity<ApplicationUserRole>().HasKey(r => new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");
        }

        public bool Seed(ApplicationDbContext context)
        {

            bool success = false;
#if DEBUG
            ApplicationRoleManager _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            success = this.CreateRole(_roleManager, RolesConst.ADMIN, "Global Access");
            if (!success == true) return success;
            success = this.CreateRole(_roleManager, RolesConst.CUSTOMER, "Customer");
            if (!success == true) return success;
            success = this.CreateRole(_roleManager, RolesConst.ACCOUNTANT, "Pidrahyu");
            if (!success == true) return success;
            success = this.CreateRole(_roleManager, RolesConst.PROGRAMER, "Make work");
            if (!success == true) return success;
            success = this.CreateRole(_roleManager, RolesConst.HR, "Human Resourse Management");
            if (!success == true) return success;
            success = this.CreateRole(_roleManager, RolesConst.OPERATOR, "Proceed Orders");
            if (!success == true) return success;
            success = this.CreateRole(_roleManager, RolesConst.RESOURSE_MANAGER, "Manage Resourses");
            if (!success == true) return success;
            success = this.CreateRole(_roleManager, RolesConst.Sales_MANAGER, "Sales Manager");
            if (!success == true) return success;
            success = this.CreateRole(_roleManager, RolesConst.MANAGER, "Manager");
            if (!success == true) return success;
            // Create my debug (testing) objects here

            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            ApplicationUser user = new ApplicationUser();
            PasswordHasher passwordHasher = new PasswordHasher();
            user.UserName = "admin@admin.com";
            user.Email = "admin@admin.com";
            user.RoleName = RolesConst.ADMIN;
            user.FistName = RolesConst.ADMIN;
            user.LastName = RolesConst.ADMIN;
            IdentityResult result = userManager.Create(user, "Pas@123");
            success = this.AddUserToRole(userManager, user.Id, RolesConst.ADMIN);
            if (!success) return success;
         

            ApplicationUser user2 = new Customer();
            PasswordHasher passwordHasher2 = new PasswordHasher();
            user2.UserName = "okpr@gmail.com";
            user2.RoleName = RolesConst.CUSTOMER;
            user2.Email = "okpr@gmail.com";
             user2.FistName = RolesConst.CUSTOMER;
             user2.LastName = RolesConst.CUSTOMER;
            IdentityResult result2 = userManager.Create(user2, "Pas@123");
            success = this.AddUserToRole(userManager, user2.Id, RolesConst.CUSTOMER);
            if (!success) return success;

            ApplicationUser user3 = new ApplicationUser();
            PasswordHasher passwordHasher3 = new PasswordHasher();
            user3.UserName = "accounter@gmail.com";
            user3.Email = "accounter@gmail.com";
            user3.RoleName = RolesConst.ACCOUNTANT;
            user3.FistName = RolesConst.ACCOUNTANT;
            user3.LastName = RolesConst.ACCOUNTANT;
            IdentityResult result3 = userManager.Create(user3, "Pas@123");
            success = this.AddUserToRole(userManager, user3.Id, RolesConst.ACCOUNTANT);
            if (!success) return success;       

            ApplicationUser user4 = new ApplicationUser();
            PasswordHasher passwordHasher4 = new PasswordHasher();
            user4.UserName = "programmer@gmail.com";
            user4.Email =   "programmer@gmail.com";
            user4.RoleName = RolesConst.PROGRAMER;
            user4.FistName = RolesConst.PROGRAMER;
            user4.LastName = RolesConst.PROGRAMER;
            IdentityResult result4 = userManager.Create(user4, "Pas@123");
            success = this.AddUserToRole(userManager, user4.Id, RolesConst.PROGRAMER);
            if (!success) return success;

            ApplicationUser user5 = new ApplicationUser();
            PasswordHasher passwordHasher5 = new PasswordHasher();
            user5.UserName = "operator@gmail.com";
            user5.Email = "operator@gmail.com";
            user5.RoleName = RolesConst.OPERATOR;
            user5.FistName = RolesConst.OPERATOR;
            user5.LastName = RolesConst.OPERATOR;
            IdentityResult result5 = userManager.Create(user5, "Pas@123");
            success = this.AddUserToRole(userManager, user5.Id, RolesConst.OPERATOR);
            if (!success) return success;

            ApplicationUser user6 = new ApplicationUser();
            PasswordHasher passwordHasher6 = new PasswordHasher();
            user6.UserName = "sales@gmail.com";
            user6.Email = "sales@gmail.com";
            user6.RoleName = RolesConst.Sales_MANAGER;
            user6.FistName = RolesConst.Sales_MANAGER;
            user6.LastName = RolesConst.Sales_MANAGER;
            IdentityResult result6 = userManager.Create(user6, "Pas@123");
            success = this.AddUserToRole(userManager, user6.Id, RolesConst.Sales_MANAGER);
            if (!success) return success;
            ApplicationUser user7 = new ApplicationUser();
            PasswordHasher passwordHasher7 = new PasswordHasher();
            user7.UserName = "sales2@gmail.com";
            user7.Email = "sales2@gmail.com";
            user7.RoleName = RolesConst.Sales_MANAGER;
            user7.FistName = RolesConst.Sales_MANAGER;
            user7.LastName = RolesConst.Sales_MANAGER;
            IdentityResult result7 = userManager.Create(user7, "Pas@123");
            success = this.AddUserToRole(userManager, user7.Id, RolesConst.Sales_MANAGER);
            if (!success) return success;
            ApplicationUser user8 = new ApplicationUser();
            PasswordHasher passwordHasher8 = new PasswordHasher();
            user8.UserName = "customer1@gmail.com";
            user8.Email = "customer1@gmail.com";
            user8.RoleName = RolesConst.CUSTOMER;
            user8.FistName = RolesConst.CUSTOMER;
            user8.LastName = RolesConst.CUSTOMER;
            IdentityResult result8 = userManager.Create(user8, "Pas@123");
            success = this.AddUserToRole(userManager, user8.Id, RolesConst.CUSTOMER);
            if (!success) return success;
            ApplicationUser user9 = new ApplicationUser();
            PasswordHasher passwordHasher9 = new PasswordHasher();
            user9.UserName = "customer2@gmail.com";
            user9.Email = "customer2@gmail.com";
            user9.RoleName = RolesConst.CUSTOMER;
            user9.FistName = RolesConst.CUSTOMER;
            user9.LastName = RolesConst.CUSTOMER;
            IdentityResult result9 = userManager.Create(user9, "Pas@123");
            success = this.AddUserToRole(userManager, user9.Id, RolesConst.CUSTOMER);
            if (!success) return success;
            ApplicationUser user10 = new ApplicationUser();
            PasswordHasher passwordHasher10 = new PasswordHasher();
            user10.UserName = "customer3@gmail.com";
            user10.Email = "customer3@gmail.com";
            user10.RoleName = RolesConst.CUSTOMER;
            user10.FistName = RolesConst.CUSTOMER;
            user10.LastName = RolesConst.CUSTOMER;
            IdentityResult result10 = userManager.Create(user10, "Pas@123");
            success = this.AddUserToRole(userManager, user10.Id, RolesConst.CUSTOMER);
            if (!success) return success;
            ApplicationUser user11 = new ApplicationUser();
            PasswordHasher passwordHaser11 = new PasswordHasher();
            user11.UserName = "customer4@gmail.com";
            user11.Email = "customer4@gmail.com";
            user11.RoleName = RolesConst.Sales_MANAGER;
            user11.FistName = RolesConst.Sales_MANAGER;
            user11.LastName = RolesConst.Sales_MANAGER;
            IdentityResult result11 = userManager.Create(user11, "Pas@123");
            success = this.AddUserToRole(userManager, user11.Id, RolesConst.CUSTOMER);
            if (!success) return success;

            ProductItem item1 = new ProductItem()
            {
                name = "Шаблон 1",
                price = 100,
                shortDescription = "blabla",
                description = "blablaballll"
            };
            ProductItem item2 = new ProductItem()
            {
                name = "Шаблон 2",
                price = 234,
                shortDescription = "blabla",
                description = "blablaballll"
            };
            this.ProductItems.Add(item1);
            this.ProductItems.Add(item2);
            this.SaveChanges();

#endif
            return success;

        }

        public bool RoleExists(ApplicationRoleManager roleManager, string name)
        {
            return roleManager.RoleExists(name);
        }

        public bool CreateRole(ApplicationRoleManager _roleManager, string name, string description = "")
        {
            var idResult = _roleManager.Create<ApplicationRole, string>(new ApplicationRole(name, description));
            return idResult.Succeeded;
        }

        public bool AddUserToRole(ApplicationUserManager _userManager, string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
       
            return idResult.Succeeded;
        }

        public void ClearUserRoles(ApplicationUserManager userManager, string userId)
        {
            var user = userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.UserRoles);
            foreach (ApplicationUserRole role in currentRoles)
            {
                userManager.RemoveFromRole(userId, role.Role.Name);
            }
        }


        public void RemoveFromRole(ApplicationUserManager userManager, string userId, string roleName)
        {
            userManager.RemoveFromRole(userId, roleName);
        }

        public void DeleteRole(ApplicationDbContext context, ApplicationUserManager userManager, string roleId)
        {
            var roleUsers = context.Users.Where(u => u.UserRoles.Any(r => r.RoleId == roleId));
            var role = context.Roles.Find(roleId);

            foreach (var user in roleUsers)
            {
                this.RemoveFromRole(userManager, user.Id, role.Name);
            }
            context.Roles.Remove(role);
            context.SaveChanges();
        }

        /// <summary>
        /// Context Initializer
        /// </summary>
        public class DropCreateAlwaysInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        public System.Data.Entity.DbSet<TPP_MainProject.Models.entities.Accountant> Accountants { get; set; }

        public System.Data.Entity.DbSet<TPP_MainProject.Models.entities.Worker> Workers { get; set; }

        public System.Data.Entity.DbSet<TPP_MainProject.Models.entities.Operator> ApplicationUsers { get; set; }

        public System.Data.Entity.DbSet<TPP_MainProject.Models.entities.Resourse> Resource { get; set; }

        public System.Data.Entity.DbSet<TPP_MainProject.Models.entities.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<TPP_MainProject.Models.entities.ProductItem> ProductItems { get; set; }
    }   
}