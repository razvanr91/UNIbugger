using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace UNIbugger.Models
{
    public class Project
    {
        public Guid Id { get; set; }

        public string CompanyId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public Guid ProjectPriorityId { get; set; }

        public IFormFile FormFile { get; set; }

        public string FileName { get; set; }

        public byte[] FileData { get; set; }

        public string FileContentType { get; set; }

        // Navigation Properties

        public virtual Company Company { get; set }

        public virtual ProjectPriority ProjectPriority { get; set; }

        public virtual ICollection<Member> Members { get; set; } = new HashSet<Member>();

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
