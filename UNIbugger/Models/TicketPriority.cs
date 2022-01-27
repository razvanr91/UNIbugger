using System;
using System.ComponentModel;

namespace UNIbugger.Models
{
    public class TicketPriority
    {
        public Guid Id { get; set; }

        [DisplayName("Priority Name")]
        public string Name { get; set; }
    }
}
