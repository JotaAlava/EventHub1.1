using System;
using EventHub1._1.DAL.Repositories;
using EventHub1._1.Models;

namespace EventHub1._1.DAL
{
    public class UnitOfWork : IDisposable
    {
        private EventHub1Entities2 context = new EventHub1Entities2();
        private GenericRepository<Location> locationRepository;
        private GenericRepository<Activity> activityRepository;

        private GenericRepository<Message> messageRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<Event> eventRepository;

        private GenericRepository<PlusOne> plusOneRepository; 

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

        public GenericRepository<Activity> ActivityRepository
        {
            get
            {
                if (this.activityRepository == null)
                {
                    this.activityRepository = new GenericRepository<Activity>(context);
                }
                return activityRepository;
            }
        }

        public GenericRepository<Message> MessageRepository
        {
            get
            {
                if (this.messageRepository == null)
                {
                    this.messageRepository = new GenericRepository<Message>(context);
                }
                return messageRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public GenericRepository<Event> EventRepository
        {
            get
            {
                if (this.eventRepository == null)
                {
                    this.eventRepository = new GenericRepository<Event>(context);
                }
                return eventRepository;
            }
        }

        public GenericRepository<PlusOne> PlusOneRepository
        {
            get
            {
                if (this.plusOneRepository == null)
                {
                    this.plusOneRepository = new GenericRepository<PlusOne>(context);
                }
                return plusOneRepository;
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        private bool disposed;

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