using System;
using System.Linq;
using Aspose.Words;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusDocumentBuilders
{
    internal abstract class StatusDocumentBuilder : IStatusDocumentBuilder
    {
        private readonly DocumentBuilder _documentBuilder;

        internal StatusDocumentBuilder(): this(new Document()) { }

        internal StatusDocumentBuilder(Document document)
        {
            _documentBuilder = new DocumentBuilder(document);
        }
       
        protected internal void AddHeader(DocumentBuilder documentBuilder)
        {
            documentBuilder.MoveToHeaderFooter(HeaderFooterType.HeaderPrimary);
            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Font.Hidden = false;
            documentBuilder.Font.Size = 12;
            documentBuilder.Font.Name = "Times New Roman";
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            documentBuilder.Write($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");

            documentBuilder.MoveToDocumentStart();
            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();
        }

        protected internal void AddFooter(DocumentBuilder documentBuilder)
        {
            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Font.Bold = true;
            documentBuilder.Font.Size = 10;
            documentBuilder.Font.Name = "Verdana";
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Justify;

            documentBuilder.Writeln();

            documentBuilder.Writeln("Thank you for the order.");

            documentBuilder.Writeln();

            documentBuilder.Writeln("PCN Network");
            documentBuilder.Writeln("Administrative Agent for PC Law Associates Ltd");
            documentBuilder.Writeln("200 Fleet Street, Suite 1100");
            documentBuilder.Writeln("Pittsburgh, PA  15220");
            documentBuilder.Writeln("Fax - 412-928-2459");
            documentBuilder.Writeln("Main Line - 412-928-2450");
            documentBuilder.Writeln("Scheduling	- 412-928-2782");
            documentBuilder.Writeln("Docs/Post-Closing - 412-928-2783");
            documentBuilder.Writeln("Disbursements - 412-928-2784");
            documentBuilder.Writeln("Accounting - 412-928-2788");

            documentBuilder.MoveToHeaderFooter(HeaderFooterType.FooterPrimary);
            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Font.Hidden = false;
            documentBuilder.Font.Name = "Courier New";
            documentBuilder.Font.Size = 9;
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            documentBuilder.Writeln("PC Law Associates Ltd");
            documentBuilder.Writeln("Main Office: 200 Fleet Street, Suite 1100, Pittsburgh, PA 15220, Phone: 866-583-091");
            documentBuilder.Writeln("Delaware Office: 42 Reads Way, New Castle, DE 19720, Phone: 302-327-8084");
            documentBuilder.Writeln("South Carolina Office: 226 State Street, West Columbia, SC, Phone: 803-873-9198");
        }

        public Document BuildDocument(OrderResult reswareOrder, GetOrderResult eClosingOrder)
        {
            AddHeader(_documentBuilder);

            AddBody(_documentBuilder, reswareOrder, eClosingOrder);

            AddFooter(_documentBuilder);

            return _documentBuilder.Document;
        }

        protected internal void AddFeeSchedule(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            documentBuilder.Font.Bold = true;
            documentBuilder.Font.Name = "Verdana";
            documentBuilder.Font.Size = 11;

            documentBuilder.Writeln("FEE SCHEDULE");

            documentBuilder.Font.Size = 10;

            documentBuilder.StartTable();

            var services = eClosingOrder.Order.Attorneys.SelectMany(attorney => attorney.Services).ToList();
            services.AddRange(eClosingOrder.Order.ClosingAttorney.Services);

            services.ForEach(service =>
            {
                documentBuilder.InsertCell();
                documentBuilder.Font.Bold = true;
                documentBuilder.Write($"{service.Name}");
                documentBuilder.Font.Bold = false;
                documentBuilder.InsertCell();
                documentBuilder.Write($"{service.BillRate:C}");
                documentBuilder.EndRow();
            });

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Total Fee");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.TotalBillRate:C}");
            documentBuilder.EndRow();

            documentBuilder.EndTable();
        }


        protected internal abstract void AddBody(DocumentBuilder documentBuilder, OrderResult reswareOrder, GetOrderResult eClosingOrder);
    }
}
