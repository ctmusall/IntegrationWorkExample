using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceiveNote.Models
{
    public class Note
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public string FileNumber { get; set; }
        public bool Processed { get; set; }
        public DateTime? ProcessedDateTime { get; set; }
        public string NoteSubject { get; set; }
        public string NoteBody { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}