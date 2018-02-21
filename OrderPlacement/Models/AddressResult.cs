using System;
using System.Runtime.Serialization;

namespace OrderPlacement.Models
{
    [Serializable]
    [DataContract]
    public abstract class AddressResult
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string StreetNumber { get; set; }
        [DataMember]
        public string StreetDirection { get; set; }
        [DataMember]
        public string StreetName { get; set; }
        [DataMember]
        public string StreetSuffix { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public string Zip { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string AddressStreetInfo { get; set; }
        [DataMember]
        public string Description { get; set; }


    }
}