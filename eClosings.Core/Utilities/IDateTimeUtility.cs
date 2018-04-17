using System;

namespace eClosings.Core.Utilities
{
    public interface IDateTimeUtility
    {
        DateTime ResolveTitleOpinionClosingDateTime(DateTime closingDateTime);

        DateTime ResolveDocPrepClosingDateTime();
    }
}
