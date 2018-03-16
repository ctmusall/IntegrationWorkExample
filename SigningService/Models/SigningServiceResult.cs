using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SigningService.Models
{
    [Serializable]
    [DataContract]
    public class SigningServiceResult
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public DateTime CreatedDateTime { get; set; }
        [DataMember]
        public string FileNumber { get; set; }
        [DataMember]
        public DateTime? ClosingDateTime { get; set; }
        [DataMember]
        public string MobilePhone { get; set; }
        [DataMember]
        public string HomePhone { get; set; }
        [DataMember]
        public string WorkPhone { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string ClosingLocation { get; set; }
        [DataMember]
        public string ClosingAddress { get; set; }
        [DataMember]
        public string ClosingCity { get; set; }
        [DataMember]
        public string ClosingState { get; set; }
        [DataMember]
        public string ClosingZip { get; set; }
        [DataMember]
        public string ClosingCounty { get; set; }

        [DataMember]
        public ICollection<SigningPartyServiceResult> SigningParties { get; set; }
    }
}