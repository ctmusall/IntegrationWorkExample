using System;
using System.Diagnostics;
using ReswareOrderMonitorService.ReswareSigning;

namespace ReswareOrderMonitorService.Repositories
{
    internal class ReceiveSigningServiceRepository : IReceiveSigningServiceRepository
    {
        private readonly ReceiveSigningServiceClient _receiveSigningServiceClient;

        public ReceiveSigningServiceRepository() : this(new ReceiveSigningServiceClient()) { }

        internal ReceiveSigningServiceRepository(ReceiveSigningServiceClient receiveSigningServiceClient)
        {
            _receiveSigningServiceClient = receiveSigningServiceClient;
        }

        public SigningServiceResult[] GetAllSignings()
        {
            try
            {
                return _receiveSigningServiceClient.GetAllSignings();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
                return null;
            }
        }
    }
}
