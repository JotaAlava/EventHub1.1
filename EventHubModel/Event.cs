//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventHubModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public Event()
        {
            this.Users = new HashSet<User>();
            this.PlusOnes = new HashSet<PlusOne>();
            this.Messages = new HashSet<Message>();
        }
    
        public int EventId { get; set; }
        public string Name { get; set; }
        public int ActivityId { get; set; }
        public bool Active { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<PlusOne> PlusOnes { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual Activity Activity { get; set; }
    }
}
