using System.Collections.Generic;
using EventHub1._1.DTO;
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

        public IEnumerable<LocationDTO> GetActiveLocations()
        {
            var allTheActiveLocationsFromTheDatabase = uow.LocationRepository.Get(location => location.Active);
            var resultsFromDbConvertedIntoDTOs = new List<LocationDTO>();

            foreach (var location in allTheActiveLocationsFromTheDatabase)
            {
                resultsFromDbConvertedIntoDTOs.Add(new LocationDTO(location));
            }

            return resultsFromDbConvertedIntoDTOs;
        }

        public IEnumerable<LocationDTO> GetInactiveLocations()
        {
            var allTheActiveLocationsFromTheDatabase = uow.LocationRepository.Get(location => location.Active == false);
            var resultsFromDbConvertedIntoDTOs = new List<LocationDTO>();

            foreach (var location in allTheActiveLocationsFromTheDatabase)
            {
                resultsFromDbConvertedIntoDTOs.Add(new LocationDTO(location));
            }

            return resultsFromDbConvertedIntoDTOs;
        }

        public LocationDTO GetLocationById(int id)
        {
            var resultFromDb = uow.LocationRepository.GetByID(id);
            var resultConvertedToDTO = new LocationDTO(resultFromDb);

            return resultConvertedToDTO;
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
        IEnumerable<LocationDTO> GetActiveLocations();

        IEnumerable<LocationDTO> GetInactiveLocations();

        void CreateLocation(Location locationToAdd);

        LocationDTO GetLocationById(int id);

        void ToggleActiveById(int id);

        void DeleteLocationById(int id);

        void UpdateLocation(Location locationToUpdate);
    }
}