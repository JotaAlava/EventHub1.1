using EventHub1._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHub1._1.DAL.Services
{
    public class UserService : IUserService
    {
        private UnitOfWork uow = new UnitOfWork();
        public IEnumerable<User> GetAllActiveUsers()
        {
            return uow.UserRepository.Get(user => user.Active);
        }

        public IEnumerable<User> GetAdmins()
        {
            return uow.UserRepository.Get(user => user.IsAdmin == true);
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

        public bool UserExistsInDb(string username)
        {
            return uow.UserRepository.UserExistsInDb(username);
        }


        public void ToggleAdminById(int id)
        {
            var userToModify = uow.UserRepository.GetByID(id);

            userToModify.IsAdmin = !userToModify.IsAdmin;

            uow.UserRepository.Update(userToModify);
            uow.Commit();
        }


        public int GetUserIdByUsername(string username)
        {
            // I do it this way because for an organization, the windows username should be unique!
            var result = uow.UserRepository.Get(user => user.Username == username) as List<User>;
            return result[0].UserId;
        }


        public IEnumerable<User> GetAllInactiveUsers()
        {
            return uow.UserRepository.Get(user => user.Active == false);
        }


        public void UpdateEmailForCurrentUser(string currentUserUsername, string newEmail = "")
        {
            var userToUpdate = uow.UserRepository.context.Users.FirstOrDefault(user => user.Username == currentUserUsername);

            userToUpdate.EMail = newEmail == null ? String.Empty : newEmail;
            uow.UserRepository.Update(userToUpdate);
            uow.Commit();

            return;
        }
    }

    public interface IUserService
    {
        IEnumerable<User> GetAdmins();

        IEnumerable<User> GetAllActiveUsers();

        IEnumerable<User> GetAllInactiveUsers();

        User GetUserById(int id);

        void CreateUser(User activityToAdd);

        void ToggleActiveById(int id);

        void DeleteUserById(int id);

        void UpdateUser(User userToUpdate);

        bool UserExistsInDb(string username);

        void ToggleAdminById(int id);

        int GetUserIdByUsername(string username);

        void UpdateEmailForCurrentUser(string newEmail, string currentUserUsername);
    }
}