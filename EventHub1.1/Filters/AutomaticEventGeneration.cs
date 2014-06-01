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
                        continue;
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