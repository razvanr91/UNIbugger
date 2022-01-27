using System;
using System.ComponentModel;

namespace UNIbugger.Models
{
    public class TicketStatus
    {
        public Guid Id { get; set; }

        [DisplayName("Status Name")]
        public string Name { get; set; }
    }
}
