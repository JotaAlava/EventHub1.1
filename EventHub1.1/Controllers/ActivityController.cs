using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;

namespace EventHub1._1.Controllers
{
    public class ActivityController : ApiController
    {
        private IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }

        [HttpGet]
        [Route("activity")]
        public IEnumerable<Activity> GetActiveActivities()
        {
            return activityService.GetActiveActivities();
        }

        [HttpGet]
        [Route("activity/{id}/")]
        public Activity GetActivityById(int id)
        {
            return activityService.GetActivityById(id);
        }

        [HttpPost]
        [Route("activity")]
        public HttpResponseMessage CreateActivity(Activity activityToAdd)
        {
            activityService.CreateActivity(activityToAdd);
            var responnse = Request.CreateResponse(HttpStatusCode.Created, activityToAdd);

            return responnse;
        }

        [HttpPost]
        [Route("activity/ToggleActiveById/{id}/")]

        public HttpResponseMessage ToggleActiveById(int id)
        {
            activityService.ToggleActiveById(id);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpDelete]
        [Route("activity/{id}/")]
        public HttpResponseMessage DeleteActivityById(int id)
        {
            activityService.DeleteActivityById(id);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpPut]
        [Route("activity")]
        public HttpResponseMessage UpdateActivity(Activity activityToUpdate)
        {
            activityService.UpdateActivity(activityToUpdate);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }
    }
}
