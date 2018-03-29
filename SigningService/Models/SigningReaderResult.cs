using System.Collections.Generic;
using Resware.Entities.Signings;
using Resware.Entities.Signings.SigningParties;

namespace SigningService.Models
{
    internal class SigningReaderResult
    {
        internal Signing Signing { get; set; }
        internal ICollection<SigningParty> SigningParties { get; set; }
    }
}