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
    }
}