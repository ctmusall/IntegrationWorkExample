using System;
using System.Collections.Generic;
using ActionEventService.Factories;
using ActionEventService.Managers;
using ActionEventService.Models;
using Adeptive.ResWare.Services;

namespace ActionEventService
{
    public class Service : IReceiveActionEventService
    {
        private readonly IActionEventManager _actionEventManager;
        private readonly IActionEventServiceResultManager _actionEventServiceResultManager;
         
        public Service() : this(ActionEventDependencyFactory.Resolve<IActionEventManager>(), ActionEventDependencyFactory.Resolve<IActionEventServiceResultManager>())
        {
            
        }

        public Service(IActionEventManager actionEventManager, IActionEventServiceResultManager actionEventServiceResultManager)
        {
            _actionEventManager = actionEventManager;
            _actionEventServiceResultManager = actionEventServiceResultManager;
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

        public ICollection<ActionEventServiceResult> GetAllActionEvents()
        {
            try
            {
                return _actionEventServiceResultManager.GetAllActionEvents();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ActionEventServiceResult GetActionEventById(Guid id)
        {
            try
            {
                return _actionEventServiceResultManager.GetActionEventById(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int DeleteActionEventById(Guid id)
        {
            try
            {
                return _actionEventServiceResultManager.DeleteActionEventById(id);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int UpdateActionEvent(ActionEventServiceResult actionEventServiceResult)
        {
            try
            {
                return _actionEventServiceResultManager.UpdateActionEvent(actionEventServiceResult);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
