using System;
using ActionEventService.Factories;
using ActionEventService.Managers;
using Adeptive.ResWare.Services;

namespace ActionEventService
{
    public class Service : IReceiveActionEventService
    {
        private readonly IActionEventManager _actionEventManager;

        public Service() : this(ActionEventDependencyFactory.Resolve<IActionEventManager>())
        {
            
        }

        public Service(IActionEventManager actionEventManager)
        {
            _actionEventManager = actionEventManager;
        }

        public ReceiveActionEventResponse ReceiveActionEvent(ReceiveActionEventData data)
        {
            try
            {
                var actionEventResult = _actionEventManager.PlaceActionEvent(data);

                if (actionEventResult.Result > 0)
                {
                    return new ReceiveActionEventResponse
                    {
                        ResponseCode = 0,
                        Message = $"Filenumber {data.FileNumber}: ActionEvent Received"
                    };
                }

                return new ReceiveActionEventResponse
                {
                    ResponseCode = 0,
                    Message = $"ERROR saving! Did not receive filenumber {data.FileNumber}. {actionEventResult.Message}"

                };
            }
            catch (Exception ex)
            {
                return new ReceiveActionEventResponse
                {
                    ResponseCode = 0,
                    Message = $"ERROR! Message: {ex.Message} \n\n Inner Exception: {ex.InnerException} \n\n Stack Trace: {ex.StackTrace}"
                };
            }
        }
    }
}
