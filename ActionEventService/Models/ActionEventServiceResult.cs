using System;
using System.Runtime.Serialization;

namespace ActionEventService.Models
{
    [Serializable]
    [DataContract]
    public class ActionEventServiceResult
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string FileNumber { get; set; }
        [DataMember]
        public string ActionEventCode { get; set; }
        [DataMember]
        public DateTime CreatedDateTime { get; set; }
        [DataMember]
        public bool ActionCompleted { get; set; }
        [DataMember]
        public DateTime? ActionCompletedDateTime { get; set; }
    }
}