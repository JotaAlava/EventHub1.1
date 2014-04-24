using System.Collections.Generic;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.DTO;
using EventHub1._1.Models;
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

        [HttpGet]
        [Route("location")]
        public IEnumerable<LocationDTO> GetActiveLocations()
        {
            return locationService.GetActiveLocations();
        }

        [HttpGet]
        [Route("location/{id}/")]
        public LocationDTO GetLocationById(int id)
        {
            return locationService.GetLocationById(id);
        }

        [HttpPost]
        [Route("location")]
        public HttpResponseMessage PostLocation(Location locationToAdd)
        {
            locationService.CreateLocation(locationToAdd);
            var responnse = Request.CreateResponse<Location>(HttpStatusCode.Created, locationToAdd);

            return responnse;
        }

        [HttpPost]
        [Route("location/ToggleActiveById/{id}/")]

        public HttpResponseMessage ToggleActiveById(int id)
        {
            locationService.ToggleActiveById(id);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpDelete]
        [Route("location/{id}/")]
        public HttpResponseMessage DeleteLocationById(int id)
        {
            locationService.DeleteLocationById(id);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpPut]
        [Route("location")]
        public HttpResponseMessage UpdateLocation(Location locationToUpdate)
        {
            locationService.UpdateLocation(locationToUpdate);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }
    }
}
