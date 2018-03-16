using ReswareOrderMonitorService.ReswareNoteDocs;

namespace ReswareOrderMonitorService.Repositories
{
    internal interface IReceiveNoteRepository
    {
        NoteDocServiceResult[] GetAllNotesAndDocs();
        int UpdateNoteDoc(NoteDocServiceResult noteDoc);
    }
}
