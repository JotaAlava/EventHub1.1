using System.Collections.Generic;
using EventHub1._1.Models;
using EventHub1._1.DTO;
using System.Linq;

namespace EventHub1._1.DAL.Services
{
    public class ActivityService : IActivityService
    {
        private UnitOfWork uow = new UnitOfWork();
        public IEnumerable<ActivityDTO> GetActiveActivities()
        {
            var allTheActiveActivitiesFromTheDatabase = uow.ActivityRepository.Get(location => location.Active);
            var resultsFromDbConvertedIntoDTOs = new List<ActivityDTO>();

            foreach (var activity in allTheActiveActivitiesFromTheDatabase)
            {
                resultsFromDbConvertedIntoDTOs.Add(new ActivityDTO(activity));
            }

            return resultsFromDbConvertedIntoDTOs;
        }

        public IEnumerable<ActivityDTO> GetInactiveActivities()
        {
            var allTheActiveActivitiesFromTheDatabase = uow.ActivityRepository.Get(location => location.Active == false);
            var resultsFromDbConvertedIntoDTOs = new List<ActivityDTO>();

            foreach (var activity in allTheActiveActivitiesFromTheDatabase)
            {
                resultsFromDbConvertedIntoDTOs.Add(new ActivityDTO(activity));
            }

            return resultsFromDbConvertedIntoDTOs;
        }

        public Activity GetActivityById(int id)
        {
            return uow.ActivityRepository.GetByID(id);
        }

        public void CreateActivity(Activity activityToAdd)
        {
            activityToAdd.Time = activityToAdd.Time.ToUniversalTime();
            activityToAdd.Location = uow.LocationRepository.GetByID(activityToAdd.LocationId);

            uow.ActivityRepository.Insert(activityToAdd);
            uow.Commit();
        }

        public void ToggleActiveById(int id)
        {
            var locationToModify = uow.ActivityRepository.GetByID(id);

            locationToModify.Active = !locationToModify.Active;

            uow.ActivityRepository.Update(locationToModify);
            uow.Commit();
        }

        public void DeleteActivityById(int id)
        {
            uow.ActivityRepository.Delete(id);
            uow.Commit();
        }


        public void UpdateActivity(Activity locationToUpdate)
        {
            uow.ActivityRepository.Update(locationToUpdate);
            uow.Commit();
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            var activeActivities = uow.ActivityRepository.Get(activity => activity.Active).ToList();
            var inActiveActivities = uow.ActivityRepository.Get(activity => activity.Active == false).ToList();

            var allActivities = activeActivities.Union(inActiveActivities).ToList();

            return allActivities;
        }
    }

    public interface IActivityService
    {
        IEnumerable<Activity> GetAllActivities();
        IEnumerable<ActivityDTO> GetActiveActivities();
                            
        IEnumerable<ActivityDTO> GetInactiveActivities();

        Activity GetActivityById(int id);

        void CreateActivity(Activity activityToAdd);

        void ToggleActiveById(int id);

        void DeleteActivityById(int id);

        void UpdateActivity(Activity activityToUpdate);
    }
}