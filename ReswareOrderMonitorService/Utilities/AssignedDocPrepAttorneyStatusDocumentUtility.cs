using System;
using Aspose.Words;
using ReswareOrderMonitorService.eClosingIntegrationService;

namespace ReswareOrderMonitorService.Utilities
{
    internal class AssignedDocPrepAttorneyStatusDocumentUtility : AssignedAttorneyStatusDocumentUtility
    {
        protected internal override void AddClosingDueDateTime(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
            throw new NotImplementedException();
        }

        protected internal override void AddAttorneyInfo(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
            throw new NotImplementedException();
        }

        protected internal override void AddAdditionalAttorneyInfo(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
            throw new NotImplementedException();
        }

        protected internal override void AddFeeSchedule(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
            throw new NotImplementedException();
        }
    }
}
