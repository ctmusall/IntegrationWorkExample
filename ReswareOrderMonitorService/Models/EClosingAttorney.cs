using System.Collections.Generic;

namespace ReswareOrderMonitorService.Models
{
    internal class EClosingAttorney : EClosingPerson
    {
        internal string AttorneyId { get; set; }
        internal ICollection<EClosingService> Services { get; set; }
    }
}
