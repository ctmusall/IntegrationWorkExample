using System;
using System.Diagnostics;
using System.Linq;
using Resware.Data.NoteDoc.Repository;
using Resware.Data.Order.Repository;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Factories.Documents;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.Monitors
{
    internal class DocumentMonitor : IDocumentMonitor
    {
        private readonly NoteDocRepository _receiveNoteRepository;
        private readonly OrderRepository _orderPlacementRepository;
        private readonly IClientDocumentFactory _clientClosingDocumentFactory;

        public DocumentMonitor() : this(DependencyFactory.Resolve<NoteDocRepository>(), DependencyFactory.Resolve<OrderRepository>(), DependencyFactory.Resolve<IClientDocumentFactory>()) { }

        internal DocumentMonitor(NoteDocRepository receiveNoteRepository, OrderRepository orderPlacementRepository, IClientDocumentFactory clientClosingDocumentFactory)
        {
            _receiveNoteRepository = receiveNoteRepository;
            _orderPlacementRepository = orderPlacementRepository;
            _clientClosingDocumentFactory = clientClosingDocumentFactory;
        }

        public void MonitorDocuments()
        {
            try
            {
                var notesAndDocs = _receiveNoteRepository.GetAllNoteDocs().Where(nd => !nd.Processed && nd.ProcessedDateTime == null).ToList();

                if (notesAndDocs.Count == 0) return;
                
                var orders = _orderPlacementRepository.GetAllOrders();

                if (orders.Count == 0) return;

                notesAndDocs.ForEach(noteDoc => 
                {
                    if (noteDoc.Documents.Count == 0) return;

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
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
