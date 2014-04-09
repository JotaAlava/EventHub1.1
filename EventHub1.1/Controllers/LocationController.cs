using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;

namespace EventHub1._1.Controllers
{
    public class LocationController : ApiController
    {
        private ILocationService locationService;
        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        public IEnumerable<Location> GetAllActiveLocations()
        {
            return locationService.GetAllActiveLocations();
        }
    }
}
