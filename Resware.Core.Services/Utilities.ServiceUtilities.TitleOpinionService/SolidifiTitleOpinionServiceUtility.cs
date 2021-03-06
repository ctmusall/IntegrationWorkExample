﻿using eClosings.Mirth.Messages;
using ReswareCommon.Constants;

namespace Resware.Core.Services.Utilities.ServiceUtilities.TitleOpinionService
{
    internal class SolidifiTitleOpinionServiceUtility : ServiceUtility
    {
        public override void AssignServices(ReswareRequestOrder requestClosingMessage)
        {
            if (string.IsNullOrWhiteSpace(requestClosingMessage.ClosingState))
            {
                requestClosingMessage.Notes += "Did not apply any services because the closing state was empty.";
                return;
            }

            switch (requestClosingMessage.ClosingState)
            {
                case StateConstants.Georgia:
                    requestClosingMessage.Service1 = ServiceNameConstants.TitleOpinionLetter;
                    return;
                case StateConstants.Delaware:
                    requestClosingMessage.Service1 = ServiceNameConstants.TitleOpinionPreparationAndReview;
                    return;
                case StateConstants.Massachusetts:
                    requestClosingMessage.Service1 = ServiceNameConstants.MaMarketableTitleLetter;
                    return;
                case StateConstants.NorthCarolina:
                    requestClosingMessage.Service1 = ServiceNameConstants.TitleOpinionLetter;
                    return;
                case StateConstants.SouthCarolina:
                    requestClosingMessage.Service1 = ServiceNameConstants.TitleOpinionLetter;
                    return;
                case StateConstants.Vermont:
                    requestClosingMessage.Service1 = ServiceNameConstants.TitleOpinionLetter;
                    return;
                case StateConstants.Connecticut:
                    requestClosingMessage.Service1 = ServiceNameConstants.TitleOpinionPreparationAndReview;
                    return;
                case StateConstants.WestVirginia:
                    requestClosingMessage.Service1 = ServiceNameConstants.TitleOpinionLetter;
                    return;
                default:
                    requestClosingMessage.Notes += $"Did not apply any services because the closing state is '{requestClosingMessage.ClosingState}'.";
                    return;
            }
        }
    }
}
