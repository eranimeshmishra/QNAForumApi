using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using QNAForum.Core.Logging;

namespace QNAForumApi.Core.Logging
{
    public class UnHandledExceptionLogger : ExceptionLogger
    {
        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            ILogger logger = new HandledExceptionLogger();
            logger.LogError(context.Exception.Message, context.Exception);
            return Task.FromResult<object>(null);
        }


    }
}