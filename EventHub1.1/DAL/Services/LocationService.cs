using System.Collections.Generic;
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

        public Location GetLocationById(int Id)
        {
            return uow.LocationRepository.GetByID(Id);
        }

        public void AddLocation(Location locationToAdd)
        {
            uow.LocationRepository.Insert(locationToAdd);
            uow.Save();
        }        
    }

    public interface ILocationService
    {
        IEnumerable<Location> GetAllActiveLocations();

        void AddLocation(Location locationToAdd);

        Location GetLocationById(int Id);
    }
}