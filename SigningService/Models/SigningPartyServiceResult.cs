using System;
using System.Runtime.Serialization;

namespace SigningService.Models
{
    [Serializable]
    [DataContract]
    public class SigningPartyServiceResult
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid SigningId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Phone { get; set; }
    }
}