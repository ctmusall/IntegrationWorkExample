using System;
using System.Collections.Generic;
using ReceiveNote.Factories;
using ReceiveNote.Models;
using ReceiveNote.Parsers;
using ReceiveNote.Repositories;

namespace ReceiveNote.Managers
{
    internal class NoteDocServiceResultManager : INoteDocServiceResultManager
    {
        private readonly IReswareNoteDocRepository _reswareNoteDocRepository;
        private readonly NoteDocResultParser _noteDocResultParser;
        private readonly NoteDocParser _noteDocParser;

        public NoteDocServiceResultManager() : this (NoteDocDependencyFactory.Resolve<IReswareNoteDocRepository>(), NoteDocDependencyFactory.Resolve<NoteDocResultParser>(), NoteDocDependencyFactory.Resolve<NoteDocParser>()) { }

        internal NoteDocServiceResultManager(IReswareNoteDocRepository reswareNoteDocRepository, NoteDocResultParser noteDocResultParser, NoteDocParser noteDocParser)
        {
            _reswareNoteDocRepository = reswareNoteDocRepository;
            _noteDocResultParser = noteDocResultParser;
            _noteDocParser = noteDocParser;
        }
        public ICollection<NoteDocServiceResult> GetAllNotesAndDocs()
        {
            try
            {
                var noteDocs = _reswareNoteDocRepository.GetAllNoteDocs();
                return _noteDocResultParser.ParseAllNoteDocResults(noteDocs);
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
                var noteDoc = _reswareNoteDocRepository.GetNoteDocById(id);
                return _noteDocResultParser.ParseNoteDocResult(noteDoc);
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
                return _reswareNoteDocRepository.DeleteNoteDocById(id);
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
                var noteDoc = _noteDocParser.ParseNoteDoc(noteDocServiceResult);
                return _reswareNoteDocRepository.UpdateNoteDoc(noteDoc);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}