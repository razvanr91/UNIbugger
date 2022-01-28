using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UNIbugger.Models
{
    public class Invite
    {
        public Guid Id { get; set; }

        [DisplayName("Date sent")]
        public DateTimeOffset InviteDate { get; set; }

        [DisplayName("Join Date")]
        public DateTimeOffset JoinDate { get; set; }

        [DisplayName("Code")]
        public Guid CompanyToken { get; set; }

        [DisplayName("Company")]
        public Guid CompanyId { get; set; }

        [DisplayName("Project")]
        public Guid ProjectId { get; set; }

        [DisplayName("Invitor")]
        public Guid InvitorId { get; set; }

        [DisplayName("Invitee")]
        public Guid InviteeId { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("Invitee Email")]
        public string InviteeEmail { get; set; }

        [DisplayName("Invitee First Name")]
        public string InviteeFirstName { get; set; }

        [DisplayName("Invitee Last Name")]
        public string InviteeLastName { get; set; }

        public bool IsValid { get; set; }

        // Navigation Properties
        public virtual Company Company { get; set; }

        public virtual BTUser Invitor { get; set; }

        public virtual BTUser Invitee { get; set; }

        public virtual Project Project { get; set; }

    }
}