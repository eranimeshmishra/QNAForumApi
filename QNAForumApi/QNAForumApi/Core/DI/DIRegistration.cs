using System;
using System.Web.Http.ExceptionHandling;
using QNAForum.Business;
using QNAForum.Core.Logging;
using QNAForum.Data;
using QNAForumApi.Core.Logging;
using SimpleInjector;

namespace QNAForumApi.Core.DI
{
    internal class DIRegistration
    {
        internal static void RegisterDependencies(Container container)
        {
            #region Services
            container.Register<ITestInterface, TestClass>(Lifestyle.Scoped);
            #endregion

            #region Respository
            container.Register<IQuestion, Question>(Lifestyle.Scoped);
            container.Register<ILogger, Logger>(Lifestyle.Scoped);
            container.Register<IExceptionLogger,UnHandledExceptionLogger>();
            #endregion

        }
    }
}