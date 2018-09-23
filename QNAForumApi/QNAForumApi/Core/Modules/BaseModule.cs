using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QNAForumApi.Core.Modules
{
    public class BaseModule : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(OnPreRequestHandlerExecute);
            context.PostRequestHandlerExecute+= new EventHandler(OnPostRequestHandlerExecute);

        }

        public virtual void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public virtual void OnPostRequestHandlerExecute(object sender, EventArgs e)
        {

        }
    }
}