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
using TPP_MainProject.Models.repository;
using TaskStatus = TPP_MainProject.Models.constants.TaskStatus;

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
        public DbSet<Catagorie> Catagories { get; set; }
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
            success = this.CreateRole(_roleManager, RolesConst.ORDER_OPERATOR, "Order Manaher");
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
            Customer user9 = new Customer();
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
            user10.UserName = "manager@gmail.com";
            user10.Email = "manager@gmail.com";
            user10.RoleName = RolesConst.MANAGER;
            user10.FistName = RolesConst.MANAGER;
            user10.LastName = RolesConst.MANAGER;
            IdentityResult result10 = userManager.Create(user10, "Pas@123");
            success = this.AddUserToRole(userManager, user10.Id, RolesConst.MANAGER);

            if (!success) return success;

            ApplicationUser user11 = new ApplicationUser();
            PasswordHasher passwordHaser11 = new PasswordHasher();
            user11.UserName = "customer4@gmail.com";
            user11.Email = "customer4@gmail.com";
            user11.RoleName = RolesConst.MANAGER;
            user11.FistName = RolesConst.MANAGER;
            user11.LastName = RolesConst.MANAGER + " II";
            IdentityResult result11 = userManager.Create(user11, "Pas@123");
            success = this.AddUserToRole(userManager, user11.Id, RolesConst.MANAGER);
            if (!success) return success;

            if (!success) return success;
            ApplicationUser user12 = new ApplicationUser();
            PasswordHasher passwordHaser12 = new PasswordHasher();
            user12.UserName = "order@gmail.com";
            user12.Email = "order@gmail.com";
            user12.RoleName = RolesConst.ORDER_OPERATOR;
            user12.FistName = RolesConst.ORDER_OPERATOR;
            user12.LastName = RolesConst.ORDER_OPERATOR;
            IdentityResult result12 = userManager.Create(user12, "Pas@123");
            success = this.AddUserToRole(userManager, user12.Id, RolesConst.ORDER_OPERATOR);
            if (!success) return success;

            

            Catagorie cat1 = new Catagorie()
            {
                Name = "Visitka"
            };
            Catagorie blog = new Catagorie()
            {
                Name = "Blog"
            };
            this.Catagories.Add(cat1);
            this.Catagories.Add(blog);
            this.SaveChanges();
            ProductItem item1 = new ProductItem()
            {
                Name = "Шаблон 1",
                Price = 100,
                shortDescription = "blabla",
                description = "blablaballll",
                Categorie = TemplateSiteTypes.Shop
              
            };
            ProductItem item2 = new ProductItem()
            {
                Name = "Шаблон 2",
                Price = 234,
                shortDescription = "blabla",
                description = "blablaballll",
                Categorie = TemplateSiteTypes.VisitCard
            };
            this.ProductItems.Add(item1);
            this.ProductItems.Add(item2);

            WorkItem workItem = new WorkItem()
            {
                Name = "Шаблон 1",
                Description = "blabla",
                // hours, minutes, seconds
                DueDate = DateTime.Today + (new TimeSpan(12, 20, 20)),
                Status = TaskStatus.InProgress
            };
            this.WorkItems.Add(workItem);

            WorkItem workItem1 = new WorkItem()
            {
                Name = "Шаблон 2",
                Description = "blablabla",
                // hours, minutes, seconds
                DueDate = DateTime.Today + (new TimeSpan(12, 20, 20)),
                Status = TaskStatus.InProgress
            };
            this.WorkItems.Add(workItem);

            this.SaveChanges();

            WorkItem workItem2 = new WorkItem()
            {
                Name = "Icon",
                Description = "wow",
                // hours, minutes, seconds
                DueDate = DateTime.Today + (new TimeSpan(12, 20, 20)),
                Status = TaskStatus.InProgress
            };
            this.WorkItems.Add(workItem2);
            this.SaveChanges();

              //  DueDate = new DateTime(2015,05,12);
         

            Resourse r1 = new Resourse() { 
                 Name = "Reorce",
                 Price = 20,
                 Description = "One the rule them all "
                  
            };
            Resourse r2 = new Resourse()
            {
                Name = "Reorce2",
                Price = 40,
                Description = "One the rule them all "

            };
            
            this.Resources.Add(r1);
            this.Resources.Add(r2);

            Order or1 = new Order()
            {
                completeDate = DateTime.Now,
                OrderDate =  DateTime.Now,
                detailDescription = "mememe",
                orderStartus = OrderStatus.Initiating,
                Total = 200,
                customer = new Customer()
                {
                    Email = "@",
                    FistName = "A",
                    LastName = "B",
                    RoleName = RolesConst.CUSTOMER,
                    UserName = "Nam"
                }

            };


            this.Orders.Add(or1);

            Order or2 = new Order()
            {
                completeDate = DateTime.Now,
                OrderDate = DateTime.Now,
                detailDescription = "nyanyayna",
                orderStartus = OrderStatus.Processiong,
                Total = 150,
          

            };

            Order or3 = new Order()
            {
                completeDate = DateTime.Now,
                OrderDate = DateTime.Now,
                detailDescription = "sysysy",
                orderStartus = OrderStatus.Initiating,
                Total = 600,
               

            };

            this.Orders.Add(or2);
            this.Orders.Add(or3);

            Project project = new Project()
            {
                name = "MyProject",
                nameProjectManager = "manager@gmail.com",
                costs = 300,
                projectStatus = ProjectStatus.Initial,
                projectManager = user10,
                order = or1,
                tasks = new List<WorkItem>()
            };
            project.tasks.Add(workItem);
            project.tasks.Add(workItem1);
            this.Projects.Add(project);

            this.SaveChanges();

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

        public System.Data.Entity.DbSet<TPP_MainProject.Models.entities.Resourse> Resources { get; set; }

        public System.Data.Entity.DbSet<TPP_MainProject.Models.entities.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<TPP_MainProject.Models.entities.ProductItem> ProductItems { get; set; }

        public System.Data.Entity.DbSet<TPP_MainProject.Models.entities.WorkItem> WorkItems { get; set; }

        public System.Data.Entity.DbSet<TPP_MainProject.Models.entities.Project> Projects { get; set; }
    }   
}