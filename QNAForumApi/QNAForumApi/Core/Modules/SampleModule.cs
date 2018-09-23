using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QNAForumApi.Core.Modules
{
    public class SampleModule : BaseModule
    {
        public override void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            //base.OnPreRequestHandlerExecute(sender, e);
            HttpApplication httpApplication = (HttpApplication) sender;
            httpApplication.Response.Status+=("With love from module");
        }

        public override void OnPostRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication httpApplication = (HttpApplication) sender;
            httpApplication.Response.Status += "With love from module";
        }
    }
}