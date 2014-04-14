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

        public IEnumerable<Location> GetActiveLocations()
        {
            return uow.LocationRepository.Get(location => location.Active);
        }

        public Location GetLocationById(int id)
        {
            return uow.LocationRepository.GetByID(id);
        }

        public void CreateLocation(Location locationToAdd)
        {
            uow.LocationRepository.Insert(locationToAdd);
            uow.Commit();
        }

        public void ToggleActiveById(int id)
        {
            var locationToModify = uow.LocationRepository.GetByID(id);

            locationToModify.Active = !locationToModify.Active;

            uow.LocationRepository.Update(locationToModify);
            uow.Commit();
        }
        
        public void DeleteLocationById(int id)
        {
            uow.LocationRepository.Delete(id);
            uow.Commit();
        }


        public void UpdateLocation(Location locationToUpdate)
        {
            uow.LocationRepository.Update(locationToUpdate);
            uow.Commit();
        }
    }

    public interface ILocationService
    {
        IEnumerable<Location> GetActiveLocations();

        void CreateLocation(Location locationToAdd);

        Location GetLocationById(int id);

        void ToggleActiveById(int id);

        void DeleteLocationById(int id);

        void UpdateLocation(Location locationToUpdate);
    }
}