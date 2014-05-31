using System;
using System.Collections.Generic;
using System.Linq;
using EventHub1._1.Models;

namespace EventHub1._1.DTO
{
    public class PlusOneDTO
    {
        public PlusOneDTO(PlusOne plusOneToConvertToDTO)
        {
            if (plusOneToConvertToDTO != null)
            {
                PlusOneId = plusOneToConvertToDTO.PlusOneId;
                Name = plusOneToConvertToDTO.Name;
                UserId = plusOneToConvertToDTO.UserId;
                EventId = plusOneToConvertToDTO.EventId;
            }
        }

        public int PlusOneId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
    }
}