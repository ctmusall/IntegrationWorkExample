﻿using System;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Factories.Documents;
using ReswareOrderMonitorService.ReswareNoteDocs;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.Monitors
{
    internal class DocumentMonitor : IDocumentMonitor
    {
        private readonly ReceiveNoteServiceClient _receiveNoteServiceClient;
        private readonly OrderPlacementServiceClient _orderPlacementServiceClient;
        private readonly IClientDocumentFactory _clientClosingDocumentFactory;

        public DocumentMonitor() : this(new ReceiveNoteServiceClient(), new OrderPlacementServiceClient(), ReswareOrderDependencyFactory.Resolve<IClientDocumentFactory>()) { }

        internal DocumentMonitor(ReceiveNoteServiceClient receiveNoteServiceClient, OrderPlacementServiceClient orderPlacementServiceClient, IClientDocumentFactory clientClosingDocumentFactory)
        {
            _receiveNoteServiceClient = receiveNoteServiceClient;
            _orderPlacementServiceClient = orderPlacementServiceClient;
            _clientClosingDocumentFactory = clientClosingDocumentFactory;
        }

        public void MonitorDocuments()
        {
            try
            {
                var notesAndDocs = _receiveNoteServiceClient.GetAllNotesAndDocs().Where(nd => !nd.Processed && nd.ProcessedDateTime == null).ToList();

                if (notesAndDocs.Count == 0) return;
                
                var orders = _orderPlacementServiceClient.GetAllOrders();

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
                        _receiveNoteServiceClient.UpdateNoteDoc(noteDoc);
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
