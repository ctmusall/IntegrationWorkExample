using System;

namespace ReswareOrderMonitorService.Utilities
{
    internal interface IDateTimeUtility
    {
        DateTime ResolveTitleOpinionClosingDateTime(DateTime closingDateTime);

        DateTime ResolveDocPrepClosingDateTime();
    }
}
