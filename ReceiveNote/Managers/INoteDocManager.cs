using Adeptive.ResWare.Services;
using ReceiveNote.Models;

namespace ReceiveNote.Managers
{
    public interface INoteDocManager
    {
        NoteDocResult PlaceNoteDoc(ReceiveNoteData receiveNoteData);
    }
}
