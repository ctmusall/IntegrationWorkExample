using System.Collections.Generic;
using Resware.Entities.Notes;
using Resware.Entities.Notes.Documents;

namespace ReceiveNote.Models
{
    public class NoteDocReaderResult
    {
        internal Note Note { get; set; }
        internal ICollection<Document> Documents { get; set; }
    }
}