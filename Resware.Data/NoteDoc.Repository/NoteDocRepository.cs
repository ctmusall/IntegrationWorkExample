using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Resware.Data.Context;
using Resware.Data.Factory;
using Resware.Data.Repository;
using Resware.Entities.Notes;
using Resware.Entities.Notes.Documents;

namespace Resware.Data.NoteDoc.Repository
{
    public class NoteDocRepository : RepositoryBase
    {
        public NoteDocRepository() : base(DependencyFactory.Resolve<ReswareDbContext>()) { }
        internal NoteDocRepository(ReswareDbContext reswareDbContext) : base(reswareDbContext) { }

        public int SaveNewNoteDoc(Note note, ICollection<Document> documents)
        {
            if (note == null || documents == null) return -1;

            ReswareDbContext.Notes.Add(note);

            if (documents.Count > 0) ReswareDbContext.Documents.AddRange(documents);

            return ReswareDbContext.SaveChanges();
        }

        public List<Note> GetAllNoteDocs()
        {
            return ReswareDbContext.Notes
                .Include(n => n.Documents)
                .ToList();
        }

        public int UpdateNoteDoc(Note updatedNoteDoc)
        {
            var noteDoc = ReswareDbContext.Notes.FirstOrDefault(n => n.Id == updatedNoteDoc.Id);

            if (noteDoc == null) return 0;

            ReswareDbContext.Entry(noteDoc).CurrentValues.SetValues(updatedNoteDoc);

            return ReswareDbContext.SaveChanges();
        }
    }
}