using EventHub1._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHub1._1.DTO
{
    public class UserDTO
    {
        public UserDTO(User userToConvertToDTO)
        {
            this.UserId = userToConvertToDTO.UserId;
            this.Username = userToConvertToDTO.Username;
            this.Name = userToConvertToDTO.Name;
            this.Email = userToConvertToDTO.EMail;
            this.IsAdmin = userToConvertToDTO.IsAdmin;
            this.Active = userToConvertToDTO.Active;
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool Active { get; set; }
    }
}