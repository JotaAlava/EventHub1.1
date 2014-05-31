using EventHub1._1.DAL.Services;
using EventHub1._1.Models;
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
        private ActivityService activityService = new ActivityService();
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var result = new Event();
            var activeActivitiesInDb = activityService.GetAllActivities();
            var currentlyExistingEvents = eventService.GetAllEvents();

            foreach (var activity in activeActivitiesInDb)
            {
                if (!IsAlreadyEventForToday(activity, currentlyExistingEvents))
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
                    continue;
                }

                // For all the existing events, make sure that the activity's Active property
                // matches the event's Active property. That is, if the activity is inactive
                // then make the event inactive.
                foreach (var ev in currentlyExistingEvents)
                {
                    if (ev.Activity.Name == activity.Name)
                    {
                        if (ev.Active != activity.Active)
                        {
                            ev.Active = activity.Active;
                            eventService.UpdateEvent(ev);
                        }
                    }
                }
            }
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
            var result = true;
            var listOfEvents = currentlyExistingEvents as List<Event>;

            if (listOfEvents.Count == 0)
            {
                return result = false;
            }

            foreach (var ev in currentlyExistingEvents)
            {
                if (ev.Activity.Name == activity.Name)
                {
                    result = true;
                    return result;
                }
            }

            return result;
        }
    }
}