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
        private GenericRepository<Accountant> accountantRepository;

        public GenericRepository<Order> OrderRepository
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


        public GenericRepository<Accountant> AccountantRepository
        {
            get
            {

                if (this.accountantRepository == null)
                {
                    this.accountantRepository = new GenericRepository<Accountant>(context);
                }
                return accountantRepository;
            }
        }
        public GenericRepository<ApplicationUser> UserRepository
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