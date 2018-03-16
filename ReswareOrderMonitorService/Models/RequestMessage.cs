namespace ReswareOrderMonitorService.Models
{
    public class RequestMessage
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerContact { get; set; }
        public string LenderName { get; set; }
        public string BorrowerFirstName { get; set; }
        public string BorrowerLastName { get; set; }
        public string BorrowerSuffix { get; set; }
        public string BorrowerMiddleName { get; set; }
        public string Product { get; set; }
        public string FileNumber { get; set; }
        public string OrderRequestedDate { get; set; }
        public string OrderRequestedTime { get; set; }
        public string ClosingDate { get; set; }
        public string ClosingTime { get; set;}
        public string BorrowerAddress1 { get; set; }
        public string BorrowerCity { get; set; }
        public string BorrowerState { get; set; }
        public string BorrowerZipCode { get; set; }
        public string BorrowerCounty { get; set; }
        public string BorrowerPhone1 { get; set; }
        public string BorrowerPhone2 { get; set; }
        public string BorrowerEmail { get; set; }
        public string CoBorrowerFirstName { get; set; }
        public string CoBorrowerLastName { get; set; }
        public string CoBorrowerMiddleName { get; set; }
        public string CoBorrowerSuffix { get; set; }
        public string ClosingAddress1 { get; set; }
        public string ClosingCity { get; set; }
        public string ClosingState { get; set; }
        public string ClosingZipCode { get; set; }
        public string ClosingCounty { get; set; }
        public string Service1 { get; set; }
        public string Service2 { get; set; }
        public string Service3 { get; set; }
        public string Service4 { get; set; }
        public string Service5 { get; set; }
        public string Service6 { get; set; }
        public string Service7 { get; set; }
        public string Service8 { get; set; }
        public string Service9 { get; set; }
        public string Service10 { get; set; }
        public string DocsToAttorney { get; set; }
        public string Notes { get; set; }
        public string CustomerProduct { get; set; }
    }
}
