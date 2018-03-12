using System;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Factories.Documents;
using ReswareOrderMonitorService.Repositories;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.Monitors
{
    internal class DocumentMonitor : IDocumentMonitor
    {
        private readonly IReceiveNoteRepository _receiveNoteRepository;
        private readonly IOrderPlacementRepository _orderPlacementRepository;
        private readonly IClientDocumentFactory _clientClosingDocumentFactory;

        public DocumentMonitor() : this(ReswareOrderDependencyFactory.Resolve<IReceiveNoteRepository>(), ReswareOrderDependencyFactory.Resolve<IOrderPlacementRepository>(), ReswareOrderDependencyFactory.Resolve<IClientDocumentFactory>()) { }

        internal DocumentMonitor(IReceiveNoteRepository receiveNoteRepository, IOrderPlacementRepository orderPlacementRepository, IClientDocumentFactory clientClosingDocumentFactory)
        {
            _receiveNoteRepository = receiveNoteRepository;
            _orderPlacementRepository = orderPlacementRepository;
            _clientClosingDocumentFactory = clientClosingDocumentFactory;
        }

        public void MonitorDocuments()
        {
            try
            {
                var notesAndDocs = _receiveNoteRepository.GetAllNotesAndDocs().Where(nd => !nd.Processed && nd.ProcessedDateTime == null).ToList();

                if (notesAndDocs.Count == 0) return;
                
                var orders = _orderPlacementRepository.GetAllOrders();

                if (orders.Length == 0) return;

                notesAndDocs.ForEach(noteDoc => 
                {
                    if (noteDoc.Documents.Length == 0) return;

                    var noteDocOrder = orders.FirstOrDefault(order => string.Equals(order.FileNumber, noteDoc.FileNumber, StringComparison.CurrentCultureIgnoreCase));
                    if (noteDocOrder == null) return;

                    noteDoc.Documents.ForEach(doc =>
                    {
                        var result = _clientClosingDocumentFactory.ResolveDocumentReaderFactory(noteDocOrder.ClientId)?.ResolveDocumentSender(doc.DocumentTypeId)?.SendDocs(doc, noteDocOrder);
                        if (result == null || !result.Value) return;
                        noteDoc.Processed = true;
                        noteDoc.ProcessedDateTime = DateTime.Now;
                        _receiveNoteRepository.UpdateNoteDoc(noteDoc);
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
