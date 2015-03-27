using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TPP_MainProject.Models.entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TPP_MainProject.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Order> Orders { get; set; }
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
          /*  modelBuilder.Entity<ApplicationUser>()
               .Map<Customer>(m => m.Requires("RoleId").HasValue("Customer"))
               .Map<Accountant>(m => m.Requires("RoleId").HasValue("Accountant"))
               .Map<Manager>(m => m.Requires("RoleId").HasValue("Manager"))
               .Map<Operator>(m => m.Requires("RoleId").HasValue("Operator"))
               .Map<Programmer>(m => m.Requires("RoleId").HasValue("Programmer"))
               .Map<ResourceManager>(m => m.Requires("RoleId").HasValue("ResourseManager"));
           * */

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

        }
    }
}