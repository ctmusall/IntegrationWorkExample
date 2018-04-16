using System;
using System.Collections.Generic;

namespace ReswareOrderMonitorService.Models
{
    internal class EClosingOrder : EClosingBaseOrder
    {
        internal string AdjournedReason { get; set; }
        internal ICollection<EClosingAttorney> Attorneys { get; set; }
        internal DateTime? BorrowerContactedDateTime { get; set; }
        internal DateTime? BorrowerContactedStatusSentDateTime { get; set; }
        internal string CancelledReason { get; set; }
        internal EClosingAttorney ClosingAttorney { get; set; }
        internal ICollection<EClosingCourier> Couriers { get; set; } 
        internal string CustomerName { get; set; }
        internal string NotClosedReason { get; set; }
        internal string Notes { get; set; }
        internal string Status { get; set; }
        internal decimal TotalBillRate { get; set; }
        internal string UnableReason { get; set; }
    }
}
