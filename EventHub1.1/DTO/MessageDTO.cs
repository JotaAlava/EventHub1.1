using EventHub1._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHub1._1.DTO
{
    public class MessageDTO
    {
        public MessageDTO(Message messageToConvert)
        {
            this.MessageId = messageToConvert.MessageId;
            this.Body = messageToConvert.Body;
            this.UserId = messageToConvert.UserId;
            this.EventId = messageToConvert.EventId;
            this.Deleted = messageToConvert.Deleted;
        }

        public int MessageId { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public bool Deleted { get; set; }
    }
}