using System;
using System.Diagnostics;
using ReswareOrderMonitorService.ReswareActionEvent;

namespace ReswareOrderMonitorService.Repositories
{
    internal class ReceiveActionEventRepository : IReceiveActionEventRepository
    {
        private readonly ReceiveActionEventServiceClient _receiveActionEventServiceClient;

        public ReceiveActionEventRepository() : this(new ReceiveActionEventServiceClient()) { }

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
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
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
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
                return -1;
            }
        }
    }
}
