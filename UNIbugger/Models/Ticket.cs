using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UNIbugger.Models
{
    public class Ticket
    {
        // Primary key
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created date")]
        public DateTimeOffset Created { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated Date")]
        public DateTimeOffset? Updated { get; set; }

        [DisplayName("Archived Status")]
        public bool Archived { get; set; }

        [DisplayName("Project")]
        public Guid ProjectId { get; set; }

        [DisplayName("Ticket Type")]
        public Guid TicketTypeId { get; set; }

        [DisplayName("Ticket Priority")]
        public Guid TicketPriorityId { get; set; }

        [DisplayName("Ticket Status")]
        public Guid TicketStatusId { get; set; }

        [DisplayName("Ticket Owner")]
        public string OwnerUserId { get; set; }

        [DisplayName("Ticket Developer")]
        public string DeveloperUserId { get; set; }

        // Navigation Properties

        public virtual Project Project { get; set; }

        public virtual TicketType TicketType { get; set; }

        public virtual TicketPriority TicketPriority { get; set; }

        public virtual TicketStatus TicketStatus { get; set; }

        public virtual BTUser OwnerUser { get; set; }

        public virtual BTUser DeveloperUser { get; set; }

        public virtual ICollection<TicketComment> Comments { get; set; } = new HashSet<TicketComment>();

        public virtual ICollection<TicketAttachment> Attachments { get; set; } = new HashSet<TicketAttachment>();

        public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();

        public virtual ICollection<TicketHistory> History { get; set; } = new HashSet<TicketHistory>();
    }
}
