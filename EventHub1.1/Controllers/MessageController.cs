using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;
using EventHub1._1.DTO;


namespace EventHub1._1.Controllers
{
    
    public class MessageController : ApiController
    {
        private IMessageService messageService;
        private IUserService userService;

        public MessageController(IMessageService messageService, IUserService userService)
        {
            this.messageService = messageService;
            this.userService = userService;
        }

        [HttpGet]
        [Route("message")]
        public IEnumerable<MessageDTO> GetAllMessages()
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
        public HttpResponseMessage CreateMessage(Message messageToAdd)
        {
            messageToAdd.UserId = userService.GetUserIdByUsername(User.Identity.Name);
            messageToAdd.Deleted = false;
            messageToAdd.TimeCreated = DateTime.Now;

            messageService.CreateMessage(messageToAdd);
            messageToAdd.User = userService.GetUserById(messageToAdd.UserId);

            var responnse = Request.CreateResponse(HttpStatusCode.Created, messageToAdd);

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
