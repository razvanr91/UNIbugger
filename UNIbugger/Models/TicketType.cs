using System;
using System.ComponentModel;

namespace UNIbugger.Models
{
    public class TicketType
    {
        public Guid Id { get; set; }

        [DisplayName("Type Name")]
        public string Name { get; set; }
    }
}
