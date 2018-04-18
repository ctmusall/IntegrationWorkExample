using System;
using System.Collections.Generic;
using eClosings.Entities.Attorneys;
using eClosings.Entities.Couriers;
using eClosings.Entities.Orders.BaseOrders;

namespace eClosings.Entities.Orders
{
    public class Order : BaseOrder
    {
        public string AdjournedReason { get; set; }
        public ICollection<Attorney> Attorneys { get; set; }
        public DateTime? BorrowerContactedDateTime { get; set; }
        public DateTime? BorrowerContactedStatusSentDateTime { get; set; }
        public string CancelledReason { get; set; }
        public Attorney ClosingAttorney { get; set; }
        public ICollection<Courier> Couriers { get; set; } 
        public string CustomerName { get; set; }
        public string NotClosedReason { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public decimal TotalBillRate { get; set; }
        public string UnableReason { get; set; }
    }
}
