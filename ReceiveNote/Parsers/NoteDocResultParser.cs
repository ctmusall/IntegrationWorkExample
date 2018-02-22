using System.Collections.Generic;
using System.Linq;
using ReceiveNote.Models;

namespace ReceiveNote.Parsers
{
    internal class NoteDocResultParser
    {
        internal ICollection<NoteDocServiceResult> ParseAllNoteDocResults(ICollection<Note> noteDocs)
        {
            return noteDocs.Select(noteDoc => new NoteDocServiceResult
            {
                Id = noteDoc.Id,
                CreatedDateTime = noteDoc.CreatedDateTime,
                FileNumber = noteDoc.FileNumber,
                Processed = noteDoc.Processed,
                ProcessedDateTime = noteDoc.ProcessedDateTime,
                NoteSubject = noteDoc.NoteSubject,
                NoteBody = noteDoc.NoteBody,
                Documents = ParseDocuments(noteDoc.Documents)
            }).ToList();
        }

        private static ICollection<DocumentServiceResult> ParseDocuments(ICollection<Document> noteDocDocuments)
        {
            return noteDocDocuments.Select(noteDocDocument => new DocumentServiceResult
            {
                Id = noteDocDocument.Id,
                NoteId = noteDocDocument.NoteId,
                FileName = noteDocDocument.FileName,
                Description = noteDocDocument.Description,
                DocumentBody = noteDocDocument.DocumentBody,
                DocumentTypeId = noteDocDocument.DocumentTypeId
            }).ToList();
        }

        internal NoteDocServiceResult ParseNoteDocResult(Note noteDoc)
        {
            return new NoteDocServiceResult
            {
                Id = noteDoc.Id,
                CreatedDateTime = noteDoc.CreatedDateTime,
                FileNumber = noteDoc.FileNumber,
                Processed = noteDoc.Processed,
                ProcessedDateTime = noteDoc.ProcessedDateTime,
                NoteSubject = noteDoc.NoteSubject,
                NoteBody = noteDoc.NoteBody,
                Documents = ParseDocuments(noteDoc.Documents)
            };
        }
    }
}