using System.Collections.Generic;

namespace SigningService.Models
{
    internal class SigningReaderResult
    {
        internal Signing Signing { get; set; }
        internal ICollection<SigningParty> SigningParties { get; set; }
    }
}