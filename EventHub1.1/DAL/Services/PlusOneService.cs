using System.Collections.Generic;

namespace EventHub1._1.DAL.Services
{
    public class PlusOneService : IPlusOneService
    {
        private UnitOfWork uow = new UnitOfWork();
        public IEnumerable<Models.PlusOne> GetAllPlusOnes()
        {
            return uow.PlusOneRepository.Get();
        }

        public Models.PlusOne GetPlusOneById(int id)
        {
            return uow.PlusOneRepository.GetByID(id);
        }

        public void CreateUser(Models.PlusOne plusOneToAdd)
        {
            uow.PlusOneRepository.Insert(plusOneToAdd);
            uow.Commit();
        }

        public void DeletePlusOneById(int id)
        {
            uow.PlusOneRepository.Delete(id);
            uow.Commit();
        }

        public void UpdatePlusOne(Models.PlusOne plusOneToUpdate)
        {
            uow.PlusOneRepository.Update(plusOneToUpdate);
            uow.Commit();
        }
    }

    public interface IPlusOneService
    {

        IEnumerable<Models.PlusOne> GetAllPlusOnes();

        Models.PlusOne GetPlusOneById(int id);

        void CreateUser(Models.PlusOne plusOneToAdd);

        void DeletePlusOneById(int id);

        void UpdatePlusOne(Models.PlusOne plusOneToUpdate);
    }
}