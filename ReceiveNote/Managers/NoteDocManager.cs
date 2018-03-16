using System;
using Adeptive.ResWare.Services;
using ReceiveNote.Factories;
using ReceiveNote.Models;
using ReceiveNote.Readers;
using ReceiveNote.Repositories;

namespace ReceiveNote.Managers
{
    public class NoteDocManager : INoteDocManager
    {
        private readonly NoteDocReader _noteDocReader;
        private readonly IReswareNoteDocRepository _reswareNoteDocRepository;

        public NoteDocManager() : this(NoteDocDependencyFactory.Resolve<NoteDocReader>(), NoteDocDependencyFactory.Resolve<IReswareNoteDocRepository>()) { }

        public NoteDocManager(NoteDocReader noteDocReader, IReswareNoteDocRepository reswareNoteDocRepository)
        {
            _noteDocReader = noteDocReader;
            _reswareNoteDocRepository = reswareNoteDocRepository;
        }

        public NoteDocResult PlaceNoteDoc(ReceiveNoteData receiveNoteData)
        {
            try
            {
                var noteDocReaderResult = _noteDocReader.ParseInput(receiveNoteData);
                return new NoteDocResult
                {
                    Result = _reswareNoteDocRepository.SaveReaderResult(noteDocReaderResult)
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