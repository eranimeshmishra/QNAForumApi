using System;
using System.Collections.Generic;
using log4net;
using Newtonsoft.Json;

namespace QNAForum.Core.Logging
{
    public class Logger : ILogger
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ILogger));
        public string LogCritical(string message, Exception exception)
        {
            logger.FatalFormat(message,exception);
            return PopulateExceptionData(exception);
        }

        public void LogDebug(string message, params object[] args)
        {
            logger.DebugFormat(message,args);
        }

        public string LogError(string message, Exception exception)
        {
            var incidentId = PopulateExceptionData(exception);

            logger.Error(message,exception);
            return incidentId;
        }

        private string PopulateExceptionData(Exception exception)
        {
            string incidentId = new Guid().ToString();
            string environmentName = GetEnvironmentName();
            string contextData = GetContextData(exception, environmentName, GetAdditionalData());
            GlobalContext.Properties["incident"] = incidentId;
            GlobalContext.Properties["environment"] = environmentName;
            GlobalContext.Properties["contextData"] = string.IsNullOrEmpty(contextData) ? exception.StackTrace : contextData;
            return incidentId;
        }

        private string GetContextData(Exception exception, string environmentName, Dictionary<string,string> additionalData)
        {
            ErrorData errorData = new ErrorData(exception,environmentName,additionalData);
            return JsonConvert.SerializeObject(errorData);
        }

        public virtual Dictionary<string,string> GetAdditionalData()
        {
            return new Dictionary<string, string>();
        }

        public virtual  string GetEnvironmentName()
        {
            return string.Empty;
        }

        public void LogInfo(string message, params object[] args)
        {
           logger.InfoFormat(message,args);
        }

        public void LogWarning(string message, params object[] args)
        {
            logger.WarnFormat(message,args);
        }
    }
}