﻿#region Copyright Header
// Copyright (c) 2017 Adeptive Software Corporation. All rights reserved. This
// software and documentation constitute an unpublished work and contain
// valuable trade secrets and proprietary information belonging to Adeptive
// Software Corporation (ASC). None of the foregoing material may be copied,
// duplicated or disclosed without the express written permission of ASC. ASC
// EXPRESSLY DISCLAIMS ANY AND ALL WARRANTIES CONCERNING THIS SOFTWARE AND
// DOCUMENTATION, INCLUDING ANY WARRANTIES OF MERCHANTABILITY AND/OR FITNESS
// FOR ANY PARTICULAR PURPOSE, AND WARRANTIES OF PERFORMANCE, AND ANY WARRANTY
// THAT MIGHT OTHERWISE ARISE FROM COURSE OF DEALING OR USAGE OF TRADE. NO
// WARRANTY IS EITHER EXPRESS OR IMPLIED WITH RESPECT TO THE USE OF THE
// SOFTWARE OR DOCUMENTATION. Under no circumstances shall ASC be liable for
// incidental, special, indirect, direct or consequential damages or loss of
// profits, interruption of business, or related expenses which may arise from
// use of software or documentation, including but not limited to those
// resulting from defects in software and/or documentation, or loss inaccuracy
// of data of any kind.
// 
#endregion

using System;
using Adeptive.ResWare.Services;

namespace ActionEventService
{
    
    public class Service : IReceiveActionEventService
    {

        public ReceiveActionEventResponse ReceiveActionEvent(ReceiveActionEventData data)
        {
            //throw new NotImplementedException();
            // Add business logic here and Error checking below.
            String FileNumber = data.FileNumber;
            String ActionEvenCode = data.ActionEventCode;





            return new ReceiveActionEventResponse
            {
                Message = "Action Received",
                ResponseCode = 0
            };
        }
    }
}
