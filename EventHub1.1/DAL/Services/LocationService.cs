using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventHub1._1.Models;

namespace EventHub1._1.DAL.Services
{
    public class LocationService : ILocationService
    {
        private UnitOfWork uow = new UnitOfWork();
        public LocationService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<Location> GetAllActiveLocations()
        {
            return uow.LocationRepository.Get(location => location.Active);
        }
    }

    public interface ILocationService
    {
        IEnumerable<Location> GetAllActiveLocations();
    }
}