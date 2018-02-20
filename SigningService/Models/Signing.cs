using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SigningService.Models
{
    public class Signing
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public string FileNumber { get; set; }
        public DateTime? ClosingDateTime { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string EmailAddress { get; set; }
        public string ClosingLocation { get; set; }
        public string ClosingAddress { get; set; }
        public string ClosingCity { get; set; }
        public string ClosingState { get; set; }
        public string ClosingZip { get; set; }
        public string ClosingCounty { get; set; }

        public virtual ICollection<SigningParty> SigningParties { get; set; }
    }
}