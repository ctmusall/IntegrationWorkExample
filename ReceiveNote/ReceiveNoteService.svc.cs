using System;
using System.Collections.Generic;
using Adeptive.ResWare.Services;
using ReceiveNote.Factories;
using ReceiveNote.Managers;
using ReceiveNote.Models;

namespace ReceiveNote
{
    public class Service : IReceiveNoteService
    {
        private readonly INoteDocManager _noteDocManager;
        private readonly INoteDocServiceResultManager _noteDocServiceResultManager;

        public Service() : this(NoteDocDependencyFactory.Resolve<INoteDocManager>(), NoteDocDependencyFactory.Resolve<INoteDocServiceResultManager>()) { }

        public Service(INoteDocManager noteDocManager, INoteDocServiceResultManager noteDocServiceResultManager)
        {
            _noteDocManager = noteDocManager;
            _noteDocServiceResultManager = noteDocServiceResultManager;
        }

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

        public ICollection<NoteDocServiceResult> GetAllNotesAndDocs()
        {
            try
            {
                return _noteDocServiceResultManager.GetAllNotesAndDocs();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public NoteDocServiceResult GetNoteDocById(Guid id)
        {
            try
            {
                return _noteDocServiceResultManager.GetNoteDocById(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int DeleteNoteDocById(Guid id)
        {
            try
            {
                return _noteDocServiceResultManager.DeleteNoteDocById(id);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int UpdateNoteDoc(NoteDocServiceResult noteDocServiceResult)
        {
            try
            {
                return _noteDocServiceResultManager.UpdateNoteDoc(noteDocServiceResult);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
