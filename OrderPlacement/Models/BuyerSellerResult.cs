using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using OrderPlacement.Common;

namespace OrderPlacement.Models
{
    [Serializable]
    [DataContract]
    public class BuyerSellerResult
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid OrderId { get; set; }
        [DataMember]
        public string Prefix { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Suffix { get; set; }
        [DataMember]
        public string EntityName { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public bool Spouse { get; set; }
        [DataMember]
        public BuyerSellerEnum Type { get; set; }
        [DataMember]
        public ICollection<BuyerSellerAddressResult> Address { get; set; }
    }
}