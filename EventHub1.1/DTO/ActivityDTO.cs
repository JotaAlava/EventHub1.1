using EventHub1._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHub1._1.DTO
{
    public class ActivityDTO
    {
        public ActivityDTO(Activity activity)
        {
            if (activity != null)
            {
                ActivityId = activity.ActivityId;
                Name = activity.Name;
                DayOfWeek = activity.DayOfWeek;

                Time = activity.Time;
                LocationId = activity.LocationId;
                Active = activity.Active;

                PreferredNumberOfPlayers1 = activity.PreferredNumberOfPlayers1;
                RequiredNumberOfPlayers1 = activity.RequiredNumberOfPlayers1;
            }            
        }

        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string DayOfWeek{ get; set; }

        public DateTime Time { get; set; }
        public int LocationId { get; set; }
        public bool Active { get; set; }

        public int PreferredNumberOfPlayers1 { get; set; }
        public int RequiredNumberOfPlayers1 { get; set; }
    }
}