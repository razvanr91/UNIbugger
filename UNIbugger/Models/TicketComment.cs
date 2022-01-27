using System;
using System.ComponentModel;

namespace UNIbugger.Models
{
    public class TicketComment
    {
        public Guid Id { get; set; }

        [DisplayName("Member Comment")]
        public string Comment { get; set; }

        [DisplayName("Creation Time")]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Ticket")]
        public Guid TicketId { get; set; }

        [DisplayName("Team Member")]
        public string UserId { get; set; }

        // Navigation Properties

        public virtual Ticket Ticket { get; set; }

        public virtual BTUser User { get; set; }
    }
}
