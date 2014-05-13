using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;

namespace EventHub1._1.Controllers
{
    public class EventController : ApiController
    {
        private IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
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
    }
}
