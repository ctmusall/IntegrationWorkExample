using System.Collections.Generic;
using System.Linq;
using ReceiveNote.Models;

namespace ReceiveNote.Parsers
{
    internal class NoteDocParser
    {
        internal Note ParseNoteDoc(NoteDocServiceResult noteDocServiceResult)
        {
            return new Note
            {
                Id = noteDocServiceResult.Id,
                CreatedDateTime = noteDocServiceResult.CreatedDateTime,
                FileNumber = noteDocServiceResult.FileNumber,
                Processed = noteDocServiceResult.Processed,
                ProcessedDateTime = noteDocServiceResult.ProcessedDateTime,
                NoteSubject = noteDocServiceResult.NoteSubject,
                NoteBody = noteDocServiceResult.NoteBody,
                Documents = ParseDocuments(noteDocServiceResult.Documents)
            };
        }

        private static ICollection<Document> ParseDocuments(IEnumerable<DocumentServiceResult> documents)
        {
            return documents.Select(document => new Document
            {
                Id = document.Id,
                NoteId = document.NoteId,
                FileName = document.FileName,
                Description = document.Description,
                DocumentBody = document.DocumentBody,
                DocumentTypeId = document.DocumentTypeId
            }).ToList();
        }
    }
}