using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Mirth;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal abstract class RequestTitleOpinion : ActionEvent
    {
        protected internal readonly IMirthServiceClient MirthServiceClient;

        internal RequestTitleOpinion() : this(ReswareOrderDependencyFactory.Resolve<IMirthServiceClient>()) { }

        internal RequestTitleOpinion(IMirthServiceClient mirthServiceClient)
        {
            MirthServiceClient = mirthServiceClient;
        }
    }
}
