using EventHub1._1.DAL;
using EventHub1._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using EventHub1._1.DAL.Services;

namespace EventHub1._1.Filters
{
    public class AutomaticUserCreation : ActionFilterAttribute, IActionFilter
    {
        private UserService userService = new UserService();
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentUser = filterContext.HttpContext.User.Identity;
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
        }
    }
}