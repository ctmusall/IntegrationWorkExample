using System;
using System.Collections.Generic;
using ReceiveNote.Models;

namespace ReceiveNote.Repositories
{
    public interface IReswareNoteDocRepository
    {
        int SaveReaderResult(NoteDocReaderResult noteDocReaderResult);
        List<Note> GetAllNoteDocs();
        Note GetNoteDocById(Guid id);
        int DeleteNoteDocById(Guid id);
        int UpdateNoteDoc(Note noteDocReaderResult);
    }
}
