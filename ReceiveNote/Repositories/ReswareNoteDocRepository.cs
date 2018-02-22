using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ReceiveNote.Data;
using ReceiveNote.Factories;
using ReceiveNote.Models;

namespace ReceiveNote.Repositories
{
    public class ReswareNoteDocRepository : IReswareNoteDocRepository
    {
        private readonly ReswareNoteDocContext _reswareNoteDocContext;

        public ReswareNoteDocRepository() : this(NoteDocDependencyFactory.Resolve<ReswareNoteDocContext>())
        {
            
        }

        public ReswareNoteDocRepository(ReswareNoteDocContext reswareNoteDocContext)
        {
            _reswareNoteDocContext = reswareNoteDocContext;
        }

        public int SaveReaderResult(NoteDocReaderResult noteDocReaderResult)
        {
            if (noteDocReaderResult.Note == null || noteDocReaderResult.Documents == null) return -1;

            _reswareNoteDocContext.Notes.Add(noteDocReaderResult.Note);

            _reswareNoteDocContext.Documents.AddRange(noteDocReaderResult.Documents);

            return _reswareNoteDocContext.SaveChanges();
        }

        public List<Note> GetAllNoteDocs()
        {
            return _reswareNoteDocContext.Notes
                .Include(n => n.Documents)
                .ToList();
        }

        public Note GetNoteDocById(Guid id)
        {
            return _reswareNoteDocContext.Notes
                .Include(n => n.Documents)
                .FirstOrDefault(n => n.Id == id);
        }

        public int DeleteNoteDocById(Guid id)
        {
            var noteDoc = _reswareNoteDocContext.Notes.FirstOrDefault(n => n.Id == id);

            if (noteDoc == null) return 0;

            _reswareNoteDocContext.Notes.Remove(noteDoc);

            return _reswareNoteDocContext.SaveChanges();
        }

        public int UpdateNoteDoc(Note updatedNoteDoc)
        {
            var noteDoc = _reswareNoteDocContext.Notes.FirstOrDefault(n => n.Id == updatedNoteDoc.Id);

            if (noteDoc == null) return 0;

            _reswareNoteDocContext.Entry(noteDoc).CurrentValues.SetValues(updatedNoteDoc);

            return _reswareNoteDocContext.SaveChanges();
        }
    }
}