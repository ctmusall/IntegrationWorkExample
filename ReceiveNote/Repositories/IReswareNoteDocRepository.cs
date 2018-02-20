using ReceiveNote.Models;

namespace ReceiveNote.Repositories
{
    public interface IReswareNoteDocRepository
    {
        int SaveReaderResult(NoteDocReaderResult noteDocReaderResult);
    }
}
