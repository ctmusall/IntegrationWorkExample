using eClosings.Core.Utilities;
using eClosings.Data.IntegrationService.Repository;
using eClosings.Mirth.Clients;
using Resware.Core.ActionEvent.FundingAuth.MailUtility;
using Resware.Core.ActionEvent.RequestClosing.ActionEvents;
using Resware.Core.ActionEvent.RequestDocPrep.ActionEvents;
using Resware.Core.ActionEvent.RequestReschedule.ActionEvents;
using Resware.Core.ActionEvent.RequestTitleOpinion.ActionEvents;
using Resware.Core.Services.Factories.ServiceUtilities;
using Resware.Data.Signing.Repository;
using ReswareCommon.Constants.Solidifi;
using ReswareCommon.Enums;

namespace Resware.Core.ActionEvent.Factories.ActionEvents
{
    internal class SolidifiActionEventFactory : ActionEventFactory
    {
        internal SolidifiActionEventFactory() { }

        internal SolidifiActionEventFactory(ServiceUtilityFactory serviceUtilityFactory) : base(serviceUtilityFactory) { }

        internal override Core.ActionEvent.ActionEvents.ActionEvent ResolveActionEvent(string actionEventCode)
        {
            switch (actionEventCode)
            {
                case SolidifiActionEventConstants.RescheduleClosing:
                    return new SolidifiRequestReschedule(ServiceUtilityFactory.ResolveServiceUtility(OrderTypeEnum.Closing), DependencyFactory.Resolve<IIntegrationServiceRepository>(), DependencyFactory.Resolve<SigningRepository>(), DependencyFactory.Resolve<IMirthServiceClient>());
                case SolidifiActionEventConstants.RequestClosing:
                    return new SolidifiRequestClosing(DependencyFactory.Resolve<SigningRepository>(), DependencyFactory.Resolve<IMirthServiceClient>(), ServiceUtilityFactory.ResolveServiceUtility(OrderTypeEnum.Closing));
                case SolidifiActionEventConstants.RequestTitleOpinion:
                    return new SolidifiRequestTitleOpinion(DependencyFactory.Resolve<SigningRepository>(), DependencyFactory.Resolve<IMirthServiceClient>(), ServiceUtilityFactory.ResolveServiceUtility(OrderTypeEnum.TitleOpinion), DependencyFactory.Resolve<IDateTimeUtility>());
                case SolidifiActionEventConstants.RequestDocPrep:
                    return new SolidifiRequestDocPrep(DependencyFactory.Resolve<SigningRepository>(), DependencyFactory.Resolve<IMirthServiceClient>(),ServiceUtilityFactory.ResolveServiceUtility(OrderTypeEnum.DocPrep), DependencyFactory.Resolve<IDateTimeUtility>());
                case SolidifiActionEventConstants.FundingAuth:
                    return new RequestFundingAuth.ActionEvents.RequestFundingAuth(new SolidifiFundingAuthMailUtility());
                 default:
                    return null;   
            }
        }
    }
}
