using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;
using EventHub1._1.DTO;
using EventHub1._1.Filters;

namespace EventHub1._1.Controllers
{
    [AutomaticEventGeneration]
    public class EventController : ApiController
    {
        private IEventService eventService;
        private IActivityService activityService;
        private IUserService userService;

        public EventController(IEventService eventService, IActivityService activityService, IUserService userService)
        {
            this.eventService = eventService;
            this.activityService = activityService;
            this.userService = userService;
        }

        [HttpGet]
        [Route("event")]
        public IEnumerable<Event> GetAllActiveEvents()
        {
            return eventService.GetAllActiveEvents();
        }

        [HttpGet]
        [Route("event/{id}")]
        public Event GetEventById(int id)
        {
            return eventService.GetEventById(id);
        }

        [HttpPost]
        [Route("event")]
        public HttpResponseMessage CreateEvent(Event eventToAdd)
        {
            eventToAdd.Activity = activityService.GetActivityById(eventToAdd.ActivityId);

            eventService.CreateEvent(eventToAdd);
            var responnse = Request.CreateResponse(HttpStatusCode.Created, eventToAdd);

            return responnse;
        }

        [HttpPost]
        [Route("event/ToggleActiveById/{id}/")]
        public HttpResponseMessage ToggleActiveById(int id)
        {
            eventService.ToggleActiveById(id);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpDelete]
        [Route("event/{id}/")]
        public HttpResponseMessage DeleteEventById(int id)
        {
            eventService.DeleteEventById(id);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpPut]
        [Route("event")]
        public HttpResponseMessage UpdateEvent(Event eventToUpdate)
        {
            eventService.UpdateEvent(eventToUpdate);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpPut]
        [Route("event/attend/{eventId}/")]
        public HttpResponseMessage JoinEvent(int eventId)
        {
            HttpResponseMessage response;
            EventServiceResponseCodes result = eventService.JoinEvent(eventId, User.Identity.Name);

            if (result == EventServiceResponseCodes.CannotJoinTwice)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, result.ToString() + "ForEventId:" + eventId);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, result.ToString() + "ForEventId:" + eventId);
            }

            return response;
        }

        [HttpPut]
        [Route("event/leave/{eventId}/")]
        public HttpResponseMessage LeaveEvent(int eventId)
        {
            HttpResponseMessage response;
            EventServiceResponseCodes result = eventService.LeaveEvent(eventId, User.Identity.Name);

            if (result == EventServiceResponseCodes.CannotLeaveIfNotJoined)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, result.ToString() + "ForEventId:" + eventId);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, result.ToString() + "ForEventId:" + eventId);
            }

            return response;
        }

        [HttpGet]
        [Route("event/getParticipantsByEventId/{eventId}/")]
        public List<UserDTO> GetParticipantsByEventId(int eventId)
        {
            var result = eventService.GetParticipantsByEventId(eventId);

            return result;
        }

        [HttpGet]
        [Route("event/sendEmail")]
        public HttpResponseMessage SendEmail()
        {
            var result = eventService.SendEmail();

            return Request.CreateResponse(HttpStatusCode.OK, result.ToString());
        }
    }
}
