using System;
using System.Configuration;
using System.Web.Http.ExceptionHandling;
using QNAForum.Business;
using QNAForum.Core.Logging;
using QNAForum.Core.Data;
using QNAForum.Data;
using QNAForum.Data.Model;
using QNAForumApi.Core.Logging;
using SimpleInjector;

namespace QNAForumApi.Core.DI
{
    internal class DIRegistration
    {
        internal static void RegisterDependencies(Container container)
        {
            container.RegisterSingleton<IConnectionFactory>(() =>
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SampleDBConnStr"].ConnectionString;
                return new SqlConnectionFactory(connectionString);
            });
            #region Services
            container.Register<IQuestionService, QuestionService>(Lifestyle.Scoped);
            #endregion

            #region Respository
            container.Register<IQuestionRepository, QuestionRepository>(Lifestyle.Scoped);
            #endregion

            container.Register<ILogger, Logger>(Lifestyle.Scoped);
            container.Register<IExceptionLogger, UnHandledExceptionLogger>();


        }
    }
}