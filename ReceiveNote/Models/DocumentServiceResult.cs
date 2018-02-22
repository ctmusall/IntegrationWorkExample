using System;
using System.Runtime.Serialization;

namespace ReceiveNote.Models
{
    [Serializable]
    [DataContract]
    public class DocumentServiceResult
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid NoteId { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public byte[] DocumentBody { get; set; }
        [DataMember]
        public int DocumentTypeId { get; set; }
    }
}