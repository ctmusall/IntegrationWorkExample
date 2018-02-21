using System;
using System.Collections.Generic;
using System.Linq;
using Adeptive.ResWare.Services;
using ReceiveNote.Models;

namespace ReceiveNote.Readers
{
    public class NoteDocReader
    {
        internal NoteDocReaderResult ParseInput(ReceiveNoteData receiveNoteData)
        {
            var result = new NoteDocReaderResult {Note = MapNote(receiveNoteData)};
            result.Documents = MapDocuments(receiveNoteData, result.Note);

            return result;
        }

        private static Note MapNote(ReceiveNoteData receiveNoteData)
        {
            return new Note
            {
                CreatedDateTime = DateTime.Now,
                FileNumber = receiveNoteData.FileNumber,
                NoteBody = receiveNoteData.NoteBody,
                NoteSubject = receiveNoteData.NoteSubject
            };
        }

        private static ICollection<Document> MapDocuments(ReceiveNoteData receiveNoteData, Note note)
        {
            return receiveNoteData.Documents.Select(document => new Document
            {
                Note = note,
                Description = document.Description,
                DocumentBody = document.DocumentBody,
                FileName = document.FileName,
                DocumentTypeId = document.DocumentTypeID
            }).ToList();
        }
    }
}