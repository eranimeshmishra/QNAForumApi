using System;
using System.Collections.Generic;

namespace QNAForum.Core.Logging
{
    internal class ErrorData
    {
        public ErrorData(Exception exception, string environmentName, Dictionary<string, string> additionalData)
        {
            Messages = new List<ErrorMessage>();
            if (exception is AggregateException)
            {
                foreach (Exception ex in ((AggregateException)(exception)).InnerExceptions)
                {
                    Messages.Add(new ErrorMessage { Message = ex.Message, FlaggedAt = ex.TargetSite.Name });
                }
            }
            else
            {
                Messages.Add(new ErrorMessage{Message = exception.Message, FlaggedAt = exception.TargetSite.Name});
            }

            TimeStamp = DateTime.Now;
            ApplicationName = "QNA Forum";
            Platform = ".Net";
            Level = ErrorLevel.Error.ToString();
            ServerName = System.Environment.MachineName;
            Environment = "Dev";
            ApplicationLocation = AppDomain.CurrentDomain.BaseDirectory;
            Environment = environmentName;

        }
        public DateTime TimeStamp { get; private set; }
        public string ApplicationName { get; private set; }
        public string Platform { get; private set; }
        public string Level { get; private set; }
        public string ServerName { get; private set; }
        public string Environment { get; private set; }
        public string ApplicationLocation { get; private set; }
        public Dictionary<string, string> Data { get; private set; }

        public List<ErrorMessage> Messages { get; set; }


    }

    public class ErrorMessage
    {
        public ErrorMessage()
        {
            MessageId = Guid.NewGuid().ToString();
        }

        public string MessageId { get; private set; }

        public string Message { get; set; }

        public string FlaggedAt { get; set; }
    }

    enum ErrorLevel
    {
        Error,
        Debug,
        Warning
    }
}