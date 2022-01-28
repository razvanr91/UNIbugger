using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UNIbugger.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        
        [DisplayName("Company Name")]
        public string Name { get; set; }

        [DisplayName("Company Description")]
        public string Description { get; set; }

        // Navigation Properties
        public virtual ICollection<BTUser> Members { get; set; } = new HashSet<BTUser>();

        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
