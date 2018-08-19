using QNAForum.Core.Logging;

namespace QNAForumApi.Core.Logging
{
    public class HandledExceptionLogger : Logger
    {
        public override string GetEnvironmentName()
        {
            return "Dev";
        }
    }
}