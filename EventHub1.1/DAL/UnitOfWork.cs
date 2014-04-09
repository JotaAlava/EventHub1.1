using System;
using EventHub1._1.DAL.Repositories;
using EventHub1._1.Models;

namespace EventHub1._1.DAL
{
    public class UnitOfWork : IDisposable
    {
        private EventHubDb context = new EventHubDb();
        private GenericRepository<Location> locationRepository;

        public GenericRepository<Location> LocationRepository
        {
            get
            {

                if (this.locationRepository == null)
                {
                    this.locationRepository = new GenericRepository<Location>(context);
                }
                return locationRepository;
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