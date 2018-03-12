using System;
using ReswareOrderMonitorService.ReswareNoteDocs;

namespace ReswareOrderMonitorService.Repositories
{
    internal class ReceiveNoteRepository : IReceiveNoteRepository
    {
        private readonly ReceiveNoteServiceClient _receiveNoteServiceClient;

        public ReceiveNoteRepository() : this(new ReceiveNoteServiceClient()) { }

        internal ReceiveNoteRepository(ReceiveNoteServiceClient receiveNoteServiceClient)
        {
            _receiveNoteServiceClient = receiveNoteServiceClient;
            _receiveNoteServiceClient.GetAllNotesAndDocs();
        }

        public NoteDocServiceResult[] GetAllNotesAndDocs()
        {
            try
            {
                return _receiveNoteServiceClient.GetAllNotesAndDocs();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public int UpdateNoteDoc(NoteDocServiceResult noteDoc)
        {
            try
            {
                return _receiveNoteServiceClient.UpdateNoteDoc(noteDoc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return -1;
            }
        }
        
    }
}
