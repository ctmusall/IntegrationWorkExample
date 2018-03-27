using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resware.Entities.Signings.SigningParties
{
    public class SigningParty
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Signing")]
        public Guid SigningId { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }

        public virtual Signing Signing { get; set; }

    }
}