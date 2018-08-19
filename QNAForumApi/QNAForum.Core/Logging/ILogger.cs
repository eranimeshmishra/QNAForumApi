using System;

namespace QNAForum.Core.Logging
{
    public interface ILogger
    {
        void LogDebug(string message, params object[] args);
        string LogError(string message, Exception exception);
        string LogCritical(string message, Exception exception);
        void LogInfo(string message, params object[] args);
        void LogWarning(string message, params object[] args);
    }
}