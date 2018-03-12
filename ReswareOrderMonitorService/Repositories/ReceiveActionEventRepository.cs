using System;
using ReswareOrderMonitorService.ReswareActionEvent;

namespace ReswareOrderMonitorService.Repositories
{
    internal class ReceiveActionEventRepository : IReceiveActionEventRepository
    {
        private readonly ReceiveActionEventServiceClient _receiveActionEventServiceClient;

        internal ReceiveActionEventRepository() : this(new ReceiveActionEventServiceClient()) { }

        internal ReceiveActionEventRepository(ReceiveActionEventServiceClient receiveActionEventServiceClient)
        {
            _receiveActionEventServiceClient = receiveActionEventServiceClient;
        }

        public ActionEventServiceResult[] GetAllActionEvents()
        {
            try
            {
                return _receiveActionEventServiceClient.GetAllActionEvents();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public int UpdateActionEvent(ActionEventServiceResult actionEvent)
        {
            try
            {
                return _receiveActionEventServiceClient.UpdateActionEvent(actionEvent);
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
