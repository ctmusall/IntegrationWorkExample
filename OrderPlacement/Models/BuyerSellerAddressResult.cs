using System;
using System.Runtime.Serialization;

namespace OrderPlacement.Models
{
    [Serializable]
    [DataContract]
    public class BuyerSellerAddressResult : AddressResult
    {
        [DataMember]
        public Guid BuyerSellerId { get; set; }
    }
}