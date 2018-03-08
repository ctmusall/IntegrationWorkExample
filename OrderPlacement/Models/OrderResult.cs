using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OrderPlacement.Models
{
    [Serializable]
    [DataContract]
    public class OrderResult
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string FileNumber { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string LenderName { get; set; }
        [DataMember]
        public DateTime? ClosingDateTime { get; set; }
        [DataMember]
        public string Product { get; set; }
        [DataMember]
        public DateTime CreatedDateTime { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public string CustomerProduct { get; set; }
        [DataMember]
        public ICollection<PropertyAddressResult> PropertyAddress { get; set; }
        [DataMember]
        public ICollection<BuyerSellerResult> BuyersAndSellers { get; set; }
    }
}