namespace ReswareOrderMonitorService.Models
{
    internal class EClosingBaseOrder
    {
        internal EClosingPerson Borrower { get; set; }
        internal EClosingAddress ClosingAddress { get; set; }
        internal string ClosingDate { get; set; }
        internal string ClosingLocation { get; set; }
        internal string ClosingTime { get; set; }
        internal EClosingPerson CoBorrower { get; set; }
        internal string CustomerContact { get; set; }
        internal string CustomerId { get; set; }
        internal string DeliveryMethod { get; set; }
        internal string FileNumber { get; set; }
        internal string LenderName { get; set; }
        internal string LoanNumber { get; set; }
        internal string OrderId { get; set; }
        internal string Product { get; set; }
        internal string RequestedClosingDate { get; set; }
        internal string RequestedClosingTime { get; set; }
    }
}
