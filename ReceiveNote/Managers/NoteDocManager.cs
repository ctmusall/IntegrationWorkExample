using System;
using Adeptive.ResWare.Services;
using ReceiveNote.Factories;
using ReceiveNote.Models;
using ReceiveNote.Readers;
using Resware.Data.NoteDoc.Repository;
using ReswareCommon.Messages;

namespace ReceiveNote.Managers
{
    public class NoteDocManager : INoteDocManager
    {
        private readonly NoteDocReader _noteDocReader;
        private readonly NoteDocRepository _reswareNoteDocRepository;

        public NoteDocManager() : this(DependencyFactory.Resolve<NoteDocReader>(), DependencyFactory.Resolve<NoteDocRepository>()) { }

        public NoteDocManager(NoteDocReader noteDocReader, NoteDocRepository reswareNoteDocRepository)
        {
            _noteDocReader = noteDocReader;
            _reswareNoteDocRepository = reswareNoteDocRepository;
        }

        public NoteDocResult PlaceNoteDoc(ReceiveNoteData receiveNoteData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(receiveNoteData.FileNumber)) return new NoteDocResult {Result = 0, Message = ValidationMessages.FileNumberIsNull};
                var noteDocReaderResult = _noteDocReader.ParseInput(receiveNoteData);
                return new NoteDocResult
                {
                    Result = _reswareNoteDocRepository.SaveNewNoteDoc(noteDocReaderResult.Note, noteDocReaderResult.Documents)
                };
            }
            catch (Exception ex)
            {
                return new NoteDocResult
                {
                    Result = -1,
                    Message = $"ERROR! Message: {ex.Message} \n\n Inner Exception: {ex.InnerException} \n\n Stack Trace: {ex.StackTrace}"
                };
            }
        }
    }
}