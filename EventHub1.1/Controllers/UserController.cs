using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.DTO;
using EventHub1._1.Models;
using System;


namespace EventHub1._1.Controllers
{
    
    public class UserController : ApiController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("user/admins")]
        public IEnumerable<User> GetAdmins()
        {
            return userService.GetAdmins();
        }

        [HttpGet]
        [Route("user")]
        public IEnumerable<User> GetAllActiveUsers()
        {
            return userService.GetAllActiveUsers();
        }

        [HttpGet]
        [Route("user/{id}/")]
        public User GetUserById(int id)
        {
            return userService.GetUserById(id);
        }

        [HttpGet]
        [Route("user/GetInactive")]
        public IEnumerable<User> GetAllInActivteUsers()
        {
            return userService.GetAllInactiveUsers();
        }

        [HttpPost]
        [Route("user")]
        public HttpResponseMessage CreateUser(User activityToAdd)
        {
            userService.CreateUser(activityToAdd);
            var responnse = Request.CreateResponse(HttpStatusCode.Created, activityToAdd);

            return responnse;
        }

        [HttpPost]
        [Route("user/ToggleActiveById/{id}/")]

        public HttpResponseMessage ToggleActiveById(int id)
        {
            userService.ToggleActiveById(id);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpPost]
        [Route("user/ToggleAdminById/{id}/")]

        public HttpResponseMessage ToggleAdminById(int id)
        {
            userService.ToggleAdminById(id);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpDelete]
        [Route("user/{id}/")]
        public HttpResponseMessage DeleteUserById(int id)
        {
            userService.DeleteUserById(id);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpPut]
        [Route("user")]
        public HttpResponseMessage UpdateUser(User userToUpdate)
        {
            userService.UpdateUser(userToUpdate);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpPost]
        [Route("user/updateemail")]
        public HttpResponseMessage UpdateCurrentUserEMail(string newEmail)
        {
            userService.UpdateEmailForCurrentUser(User.Identity.Name, newEmail);
            var responnse = Request.CreateResponse(HttpStatusCode.Created, newEmail);

            return responnse;
        }

        [HttpGet]
        [Route("user/getcurrentuser")]
        public UserDTO GetCurrentUserByUserName()
        {
            var userId = userService.GetUserIdByUsername(User.Identity.Name);
            return new UserDTO(userService.GetUserById(userId));
        }

        [HttpGet]
        [Route("user/generate")]
        public User GenerateUser()
        {
            var currentUser = User.Identity;
            var isUserAlreadyInDb = userService.UserExistsInDb(currentUser.Name);

            var newUser = new User()
            {
                Username = currentUser.Name,
                Name = currentUser.Name.Substring(7),
                EMail = String.Empty,
                IsAdmin = false,
                Active = true
            };

            if (!isUserAlreadyInDb)
                userService.CreateUser(newUser);

            return newUser;
        }
    }
}
