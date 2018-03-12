using System;
using ReswareOrderMonitorService.ReswareSigning;

namespace ReswareOrderMonitorService.Repositories
{
    internal class ReceiveSigningServiceRepository : IReceiveSigningServiceRepository
    {
        private readonly ReceiveSigningServiceClient _receiveSigningServiceClient;

        internal ReceiveSigningServiceRepository() : this(new ReceiveSigningServiceClient()) { }

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
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}
