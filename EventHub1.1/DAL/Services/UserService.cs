using EventHub1._1.Models;
using System.Collections.Generic;

namespace EventHub1._1.DAL.Services
{
    public class UserService : IUserService
    {
        private UnitOfWork uow = new UnitOfWork();
        public IEnumerable<User> GetAllActiveUsers()
        {
            return uow.UserRepository.Get(user => user.Active);
        }

        public User GetUserById(int id)
        {
            return uow.UserRepository.GetByID(id);
        }

        public void CreateUser(User userToAdd)
        {
            uow.UserRepository.Insert(userToAdd);
            uow.Commit();
        }

        public void ToggleActiveById(int id)
        {
            var userToModify = uow.UserRepository.GetByID(id);

            userToModify.Active = !userToModify.Active;

            uow.UserRepository.Update(userToModify);
            uow.Commit();
        }

        public void DeleteUserById(int id)
        {
            uow.UserRepository.Delete(id);
            uow.Commit();
        }

        public void UpdateUser(User userToUpdate)
        {
            uow.UserRepository.Update(userToUpdate);
            uow.Commit();
        }
    }

    public interface IUserService
    {

        IEnumerable<User> GetAllActiveUsers();

        User GetUserById(int id);

        void CreateUser(User activityToAdd);

        void ToggleActiveById(int id);

        void DeleteUserById(int id);

        void UpdateUser(User userToUpdate);
    }
}