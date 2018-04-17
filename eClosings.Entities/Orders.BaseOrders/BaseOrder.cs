using eClosings.Entities.Addresses;
using eClosings.Entities.Persons;

namespace eClosings.Entities.Orders.BaseOrders
{
    public class BaseOrder
    {
        public Person Borrower { get; set; }
        public Address ClosingAddress { get; set; }
        public string ClosingDate { get; set; }
        public string ClosingLocation { get; set; }
        public string ClosingTime { get; set; }
        public Person CoBorrower { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerId { get; set; }
        public string DeliveryMethod { get; set; }
        public string FileNumber { get; set; }
        public string LenderName { get; set; }
        public string LoanNumber { get; set; }
        public string OrderId { get; set; }
        public string Product { get; set; }
        public string RequestedClosingDate { get; set; }
        public string RequestedClosingTime { get; set; }
    }
}
