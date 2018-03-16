using System;
using System.Runtime.Serialization;

namespace OrderPlacement.Models
{
    [Serializable]
    [DataContract]
    public class PropertyAddressResult : AddressResult
    {
        [DataMember]
        public Guid OrderId { get; set; }
        [DataMember]
        public string County { get; set; }
    }
}