using System.Collections.Generic;

namespace EventHub1._1.DAL.Services
{
    public class EventService : IEventService
    {
        private UnitOfWork uow = new UnitOfWork();

        public IEnumerable<Models.Event> GetAllActiveEvents()
        {
            return uow.EventRepository.Get(ev => ev.Active);
        }

        public Models.Event GetEventById(int id)
        {
            return uow.EventRepository.GetByID(id);
        }

        public void CreateEvent(Models.Event eventToAdd)
        {
            uow.EventRepository.Insert(eventToAdd);
            uow.Commit();
        }

        public void ToggleActiveById(int id)
        {
            var result = uow.EventRepository.GetByID(id);
            result.Active = !result.Active;
            uow.EventRepository.Update(result);

            uow.Commit();
        }

        public void DeleteEventById(int id)
        {
            uow.EventRepository.Delete(id);
        }

        public void UpdateEvent(Models.Event eventToUpdate)
        {
            uow.EventRepository.Delete(eventToUpdate);
        }
    }

    public interface IEventService
    {

        IEnumerable<Models.Event> GetAllActiveEvents();

        Models.Event GetEventById(int id);

        void CreateEvent(Models.Event eventToAdd);

        void ToggleActiveById(int id);

        void DeleteEventById(int id);

        void UpdateEvent(Models.Event eventToUpdate);
    }
}