using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ExamenTecnicoAcciona.App_Start
{
    public class CustomTrace : Attribute, IActionFilter
    {
        public bool AllowMultiple => true;

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            Trace.WriteLine(string.Format("Action ejecutada: {0} || Hora:{1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortTimeString()), "CustomLogger||");

            return continuation();
        }
    }
}