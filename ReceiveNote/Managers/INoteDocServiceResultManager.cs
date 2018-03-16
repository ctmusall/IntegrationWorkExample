using System;
using System.Collections.Generic;
using ReceiveNote.Models;

namespace ReceiveNote.Managers
{
    public interface INoteDocServiceResultManager
    {
        ICollection<NoteDocServiceResult> GetAllNotesAndDocs();
        NoteDocServiceResult GetNoteDocById(Guid id);
        int DeleteNoteDocById(Guid id);
        int UpdateNoteDoc(NoteDocServiceResult noteDocServiceResult);
    }
}
