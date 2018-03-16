using System.Collections.Generic;

namespace ReceiveNote.Models
{
    public class NoteDocReaderResult
    {
        internal Note Note { get; set; }
        internal ICollection<Document> Documents { get; set; }
    }
}