using System.Collections.Generic;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;
using System;
using System.Net.Http;
using System.Net;

namespace EventHub1._1.Controllers
{
    public class LocationController : ApiController
    {
        private ILocationService locationService;
        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        public IEnumerable<Location> GetLocations()
        {
            return locationService.GetAllActiveLocations();
        }

        public Location GetLocationById (int Id)
        {
            return locationService.GetLocationById(Id);
        }

        public HttpResponseMessage PostLocation(Location locationToAdd)
        {
            locationService.AddLocation(locationToAdd);
            var responnse = Request.CreateResponse<Location>(HttpStatusCode.Created, locationToAdd);

            return responnse;
        }
    }
}
