using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;

namespace EventHub1._1.Controllers
{
    public class MessageController : ApiController
    {
        private IMessageService messageService;

        public MessageController(IMessageService sportService)
        {
            this.messageService = sportService;
        }

        [HttpGet]
        [Route("message")]
        public IEnumerable<Message> GetAllMessages()
        {
            return messageService.GetAllMessages();
        }

        [HttpGet]
        [Route("message/{id}/")]
        public Message GetMessageByAuthor(int id)
        {
            return messageService.GetMessageByAuthor(id);
        }

        [HttpPost]
        [Route("message")]
        public HttpResponseMessage CreateMessage(Message activityToAdd)
        {
            messageService.CreateMessage(activityToAdd);
            var responnse = Request.CreateResponse(HttpStatusCode.Created, activityToAdd);

            return responnse;
        }

        [HttpDelete]
        [Route("message/{id}/")]
        public HttpResponseMessage DeleteMessageById(int id)
        {
            messageService.DeleteMessageById(id);
            var responnse = Request.CreateResponse(HttpStatusCode.OK);

            return responnse;
        }
    }
}
