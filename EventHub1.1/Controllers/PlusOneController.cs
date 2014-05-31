using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;
using EventHub1._1.DTO;

namespace EventHub1._1.Controllers
{
    public class PlusOneController : ApiController
    {
        private IPlusOneService plusOneService;

        public PlusOneController(IPlusOneService plusOneService)
        {
            this.plusOneService = plusOneService;
        }

        [HttpGet]
        [Route("plusone")]
        public IEnumerable<PlusOneDTO> GetAllPlusOnes()
        {
            return plusOneService.GetAllPlusOnes();
        }

        [HttpGet]
        [Route("plusone/{id}/")]
        public PlusOne GetPlusOneById(int id)
        {
            return plusOneService.GetPlusOneById(id);
        }

        [HttpGet]
        [Route("plusone/byeventid/{id}/")]
        public List<PlusOneDTO> GetAllPlusOnesForEventByEventId(int id)
        {
            return plusOneService.GetAllPlusOnesForEventByEventId(id);
        }

        [HttpPost]
        [Route("plusone/{eventId}")]
        public HttpResponseMessage CreatePlusOne(int eventId, string nameForPlusOne)
        {
            HttpResponseMessage response;
            PlusOneServiceResponseCodes result = plusOneService.AddPlusOne(eventId, User.Identity.Name, nameForPlusOne);

            if (result == PlusOneServiceResponseCodes.CannotAddDuplicatePlusOnes)
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
        [Route("plusone/{id}/")]
        public HttpResponseMessage DeletePlusOneById(int id)
        {
            plusOneService.DeletePlusOneById(id);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }

        [HttpPut]
        [Route("plusone")]
        public HttpResponseMessage UpdatePlusOne(PlusOne plusOneToUpdate)
        {
            plusOneService.UpdatePlusOne(plusOneToUpdate);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }
    }
}
