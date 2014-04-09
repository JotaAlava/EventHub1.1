//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventHub1._1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public Event()
        {
            this.Messages = new HashSet<Message>();
            this.PlusOnes = new HashSet<PlusOne>();
            this.Users = new HashSet<User>();
        }
    
        public int EventId { get; set; }
        public string Name { get; set; }
        public int ActivityId { get; set; }
        public bool Active { get; set; }
    
        public virtual Activity Activity { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<PlusOne> PlusOnes { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
