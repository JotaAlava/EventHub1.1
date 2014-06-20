using EventHub1._1.DAL.Services;
using System.Web.Mvc;

namespace EventHub1._1.Filters
{
    public class AdminAuth : ActionFilterAttribute, IActionFilter
    {
        private IUserService userService = new UserService();

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = userService.GetUserIdByUsername(filterContext.HttpContext.User.Identity.Name);
            var currentUser = userService.GetUserById(userId);

            if(!currentUser.IsAdmin)
                filterContext.Result = new RedirectResult("~/Home/Index");
        }
    }
}