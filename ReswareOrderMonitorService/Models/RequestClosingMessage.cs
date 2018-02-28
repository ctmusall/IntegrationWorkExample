namespace ReswareOrderMonitorService.Models
{
    internal class RequestClosingMessage
    {
        internal string OrderId { get; set; }
        internal string CustomerId { get; set; }
        internal string CustomerContact { get; set; }
        internal string LenderName { get; set; }
        internal string BorrowerFirstName { get; set; }
        internal string BorrowerLastName { get; set; }
        internal string BorrowerSuffix { get; set; }
        internal string BorrowerMiddleName { get; set; }
        internal string Product { get; set; }
        internal string FileNumber { get; set; }
        internal string OrderRequestedDate { get; set; }
        internal string OrderRequestedTime { get; set; }
        internal string ClosingDate { get; set; }
        internal string ClosingTime { get; set;}
        internal string BorrowerAddress1 { get; set; }
        internal string BorrowerCity { get; set; }
        internal string BorrowerState { get; set; }
        internal string BorrowerZipCode { get; set; }
        internal string BorrowerPhone1 { get; set; }
        internal string BorrowerPhone2 { get; set; }
        internal string BorrowerEmail { get; set; }
        internal string CoBorrowerFirstName { get; set; }
        internal string CoBorrowerLastName { get; set; }
        internal string CoBorrowerMiddleName { get; set; }
        internal string CoBorrowerSuffix { get; set; }
        internal string ClosingAddress1 { get; set; }
        internal string ClosingCity { get; set; }
        internal string ClosingState { get; set; }
        internal string ClosingZipCode { get; set; }
        internal string ClosingCounty { get; set; }
        internal string Service1 { get; set; }
        internal string Service2 { get; set; }
        internal string Service3 { get; set; }
        internal string Service4 { get; set; }
        internal string Service5 { get; set; }
        internal string Service6 { get; set; }
        internal string Service7 { get; set; }
        internal string Service8 { get; set; }
        internal string Service9 { get; set; }
        internal string Service10 { get; set; }
        internal string DocsToAttorney { get; set; }
        internal string Notes { get; set; }
        internal string CustomerProduct { get; set; }
    }
}
