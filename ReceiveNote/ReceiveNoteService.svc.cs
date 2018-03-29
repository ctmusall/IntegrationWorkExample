using System;
using Adeptive.ResWare.Services;
using ReceiveNote.Factories;
using ReceiveNote.Managers;

namespace ReceiveNote
{
    public class Service : IReceiveNoteService
    {
        private readonly INoteDocManager _noteDocManager;

        public Service() : this(DependencyFactory.Resolve<INoteDocManager>()) { }

        public Service(INoteDocManager noteDocManager) { _noteDocManager = noteDocManager; }

        public ReceiveNoteResponse ReceiveNote(ReceiveNoteData NoteData)
        {
            try
            {
                var noteDocResult = _noteDocManager.PlaceNoteDoc(NoteData);

                if (noteDocResult.Result > 0)
                {
                    return new ReceiveNoteResponse
                    {
                        ResponseCode = 0,
                        Message = $"Filenumber {NoteData.FileNumber}: NoteDoc Received"
                    };
                }

                return new ReceiveNoteResponse
                {
                    ResponseCode = 0,
                    Message = $"ERROR saving! Did not receive filenumber {NoteData.FileNumber}. {noteDocResult.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ReceiveNoteResponse
                {
                    Message = $"ERROR! Message: {ex.Message} \n\n Inner Exception: {ex.InnerException} \n\n Stack Trace: {ex.StackTrace}",
                    ResponseCode = 0
                };
            }
        }
    }
}
