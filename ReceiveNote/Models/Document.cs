using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceiveNote.Models
{
    public class Document
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Note")]
        public Guid NoteId { get; set; }

        public string FileName { get; set; }
        public string Description { get; set; }
        public byte[] DocumentBody { get; set; }
        public int DocumentTypeId { get; set; }

        public virtual Note Note { get; set; }
    }
}