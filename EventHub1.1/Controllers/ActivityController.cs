using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;
using EventHub1._1.DTO;


namespace EventHub1._1.Controllers
{
    public class ActivityController : ApiController
    {
        private ILocationService locationService;
        private IActivityService activityService;

        public ActivityController(IActivityService activityService, ILocationService locationService)
        {
            this.locationService = locationService;
            this.activityService = activityService;
        }

        [HttpGet]
        [Route("activity/all")]
        public IEnumerable<ActivityDTO> GetAllActivities()
        {
            return activityService.GetAllActivitiesAsDto();
        }

        [HttpGet]
        [Route("activity")]
        public IEnumerable<ActivityDTO> GetActiveActivities()
        {
            return activityService.GetActiveActivities();
        }

        [HttpGet]
        [Route("activity/GetInactive")]
        public IEnumerable<ActivityDTO> GetInactiveActivities()
        {
            return activityService.GetInactiveActivities();
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
            HttpResponseMessage responnse;
            try
            {
                activityService.DeleteActivityById(id);
                responnse = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                responnse = Request.CreateResponse(HttpStatusCode.BadRequest);
            }


            return responnse;
        }

        [HttpPut]
        [Route("activity")]
        public HttpResponseMessage UpdateActivity(Activity activityToUpdate)
        {
            //activityToUpdate.Time = activityToUpdate.Time.ToUniversalTime();

            activityService.UpdateActivity(activityToUpdate);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }
    }
}
