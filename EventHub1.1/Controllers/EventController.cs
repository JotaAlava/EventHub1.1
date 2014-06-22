using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;
using EventHub1._1.DTO;
using System.Linq;
using System;


namespace EventHub1._1.Controllers
{
    
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
            var result = eventService.GetAllActiveEvents();
            var test = result.First().Messages.First().TimeCreated;
            return result;
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

        [HttpGet]
        [Route("event/generate")]
        public IEnumerable<Event> GenerateEvents()
        {
            var result = new List<Event>();
            var allActivities = activityService.GetAllActivities();
            var activeActivitiesInDb = from act in allActivities
                                       where act.Active == true
                                       select act;
            var currentlyExistingEvents = eventService.GetAllEvents();

            foreach (var activity in activeActivitiesInDb)
            {
                if (!IsAlreadyEventForToday(activity, currentlyExistingEvents))
                {
                    if (activity.DayOfWeek == DateTime.Today.DayOfWeek.ToString())
                    {
                        // Create New Event
                        var newEvent = new Event()
                        {
                            Activity = activity,
                            ActivityId = activity.ActivityId,
                            Active = true,
                            DateCreated = DateTime.Now,
                            Messages = new List<Message>(),
                            Name = activity.Name,
                            PlusOnes = new List<PlusOne>(),
                            Users = new List<User>()
                        };

                        eventService.CreateEvent(newEvent);
                        result.Add(newEvent);
                        continue;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Checks in the second parameter if there exists an event that was created out of the activity
        /// passed in as a first parameter
        /// </summary>
        /// <param name="activity"> Parameter whose existance we want to confirm </param>
        /// <param name="currentlyExistingEvents"> List of events where we will check for existing events </param>
        /// <returns></returns>
        private bool IsAlreadyEventForToday(Activity activity, IEnumerable<Event> currentlyExistingEvents)
        {
            var result = false;
            var listOfEvents = currentlyExistingEvents as List<Event>;

            if (listOfEvents.Count == 0)
            {
                return result = false;
            }

            foreach (var ev in currentlyExistingEvents)
            {
                // Weird ass error where eager loading fails...
                try
                {
                    // If there is an event of such name that was created today
                    if (ev.Activity.Name == activity.Name &&
                        ev.DateCreated.Date == DateTime.Today.Date)
                    {
                        result = true;
                        return result;
                    }
                }
                catch (Exception e)
                {
                    ev.Activity = activityService.GetActivityById(ev.ActivityId);
                    if (ev.Activity.Name == activity.Name)
                    {
                        result = true;
                        return result;
                    }
                }
            }

            return result;
        }
    }
}
