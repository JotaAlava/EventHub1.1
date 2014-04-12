using System.Collections.Generic;
using EventHub1._1.DAL;
using EventHub1._1.Models;

namespace EventHub1._1.Controllers
{
    public class ActivityService : IActivityService
    {
        private UnitOfWork uow = new UnitOfWork();
        public ActivityService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<Activity> GetActiveActivities()
        {
            return uow.ActivityRepository.Get(location => location.Active);
        }

        public Activity GetActivityById(int id)
        {
            return uow.ActivityRepository.GetByID(id);
        }

        public void CreateActivity(Activity locationToAdd)
        {
            uow.ActivityRepository.Insert(locationToAdd);
            uow.Save();
        }

        public void ToggleActiveById(int id)
        {
            var locationToModify = uow.ActivityRepository.GetByID(id);

            locationToModify.Active = !locationToModify.Active;

            uow.ActivityRepository.Update(locationToModify);
            uow.Save();
        }

        public void DeleteActivityById(int id)
        {
            uow.ActivityRepository.Delete(id);
            uow.Save();
        }


        public void UpdateActivity(Activity locationToUpdate)
        {
            uow.ActivityRepository.Update(locationToUpdate);
            uow.Save();
        }
    }

    public interface IActivityService
    {

        IEnumerable<Activity> GetActiveActivities();

        Activity GetActivityById(int id);

        void CreateActivity(Activity activityToAdd);

        void ToggleActiveById(int id);

        void DeleteActivityById(int id);

        void UpdateActivity(Activity activityToUpdate);
    }
}