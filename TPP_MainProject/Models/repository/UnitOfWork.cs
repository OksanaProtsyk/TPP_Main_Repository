using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPP_MainProject.Models.entities;

namespace TPP_MainProject.Models.repository
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<Order> orderRepository;
        private GenericRepository<ApplicationUser> applicationRepository;

        public GenericRepository<Order> DepartmentRepository
        {
            get
            {

                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(context);
                }
                return orderRepository;
            }
        }

        public GenericRepository<ApplicationUser> CourseRepository
        {
            get
            {

                if (this.applicationRepository == null)
                {
                    this.applicationRepository = new GenericRepository<ApplicationUser>(context);
                }
                return applicationRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}