using Resware.Data.Signing.Repository;
using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.ActionEvents.Solidifi;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Common.Solidifi;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.Utilities;
using ReswareOrderMonitorService.Utilities.Solidifi;

namespace ReswareOrderMonitorService.Factories.ActionEvents
{
    internal class SolidifiActionEventFactory : ActionEventFactory
    {
        internal SolidifiActionEventFactory() { }

        internal SolidifiActionEventFactory(IServiceUtilityFactory serviceUtilityFactory) : base(serviceUtilityFactory) { }

        internal override ActionEvent ResolveActionEvent(string actionEventCode)
        {
            switch (actionEventCode)
            {
                case SolidifiActionEventConstants.RescheduleClosing:
                    return new SchedulingReschedule(ServiceUtilityFactory.ResolveServiceUtility(OrderTypeEnum.Closing), DependencyFactory.Resolve<IIntegrationServiceRepository>(), DependencyFactory.Resolve<SigningRepository>(), DependencyFactory.Resolve<IMirthServiceClient>());
                case SolidifiActionEventConstants.RequestClosing:
                    return new SolidifiRequestClosing(DependencyFactory.Resolve<SigningRepository>(), DependencyFactory.Resolve<IMirthServiceClient>(), ServiceUtilityFactory.ResolveServiceUtility(OrderTypeEnum.Closing));
                case SolidifiActionEventConstants.RequestTitleOpinion:
                    return new SolidifiRequestTitleOpinion(DependencyFactory.Resolve<SigningRepository>(), DependencyFactory.Resolve<IMirthServiceClient>(), ServiceUtilityFactory.ResolveServiceUtility(OrderTypeEnum.TitleOpinion), DependencyFactory.Resolve<IDateTimeUtility>());
                case SolidifiActionEventConstants.RequestDocPrep:
                    return new SolidifiRequestDocPrep(DependencyFactory.Resolve<SigningRepository>(), DependencyFactory.Resolve<IMirthServiceClient>(),ServiceUtilityFactory.ResolveServiceUtility(OrderTypeEnum.DocPrep), DependencyFactory.Resolve<IDateTimeUtility>());
                case SolidifiActionEventConstants.FundingAuth:
                    return new FundingAuth(new SolidifiFundingAuthMailUtility());
                 default:
                    return null;   
            }
        }
    }
}
