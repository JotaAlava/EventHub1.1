using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;

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
        public IEnumerable<PlusOne> GetAllPlusOnes()
        {
            return plusOneService.GetAllPlusOnes();
        }

        [HttpGet]
        [Route("plusone/{id}/")]
        public PlusOne GetPlusOneById(int id)
        {
            return plusOneService.GetPlusOneById(id);
        }

        [HttpPost]
        [Route("plusone")]
        public HttpResponseMessage CreatePlusOne(PlusOne plusOneToAdd)
        {
            plusOneService.CreateUser(plusOneToAdd);
            var responnse = Request.CreateResponse(HttpStatusCode.Created, plusOneToAdd);

            return responnse;
        }

        [HttpDelete]
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
