using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UNIbugger.Models
{
    public class Notification
    {
        public Guid Id { get; set; }

        [DisplayName("Ticket")]
        public Guid TicketId { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Message")]
        public string Message { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Creation Date")]
        public DateTimeOffset Created { get; set; }

        [Required]
        [DisplayName("Recipient")]
        public Guid RecipientId { get; set; }

        [Required]
        [DisplayName("Sender")]
        public Guid SenderId { get; set; }

        [DisplayName("Has been viewed")]
        public bool Viewed { get; set; }

        // Navigation Properties

        public virtual Ticket Ticket { get; set; }

        public virtual BTUser Recipient { get; set; }

        public virtual BTUser Sender { get; set; }
    }
}