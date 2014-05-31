using EventHub1._1.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventHub1._1.Filters
{
    public class AutomaticEventGeneration : ActionFilterAttribute, IActionFilter
    {
        private EventService eventService = new EventService();
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var currentUser = filterContext.HttpContext.User.Identity;
            //var isUserAlreadyInDb = eventService.UserExistsInDb(currentUser.Name);

            //var newUser = new Event()
            //{
            //    Username = currentUser.Name,
            //    Name = currentUser.Name.Substring(7),
            //    EMail = "No email has been assigned to this user.",
            //    IsAdmin = false,
            //    Active = true
            //};

            //if (!isUserAlreadyInDb)
            //    userService.CreateUser(newUser);
        }
    }
}