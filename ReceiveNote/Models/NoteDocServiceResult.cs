using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ReceiveNote.Models
{
    [Serializable]
    [DataContract]
    public class NoteDocServiceResult
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public DateTime CreatedDateTime { get; set; }
        [DataMember]
        public string FileNumber { get; set; }
        [DataMember]
        public bool Processed { get; set; }
        [DataMember]
        public DateTime? ProcessedDateTime { get; set; }
        [DataMember]
        public string NoteSubject { get; set; }
        [DataMember]
        public string NoteBody { get; set; }
        [DataMember]
        public ICollection<DocumentServiceResult> Documents { get; set; }
    }
}