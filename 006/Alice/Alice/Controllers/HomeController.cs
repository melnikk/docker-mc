using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Vostok.Tracing;

namespace Alice.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("{*url}")]
        public object Echo()
        {
            return Json(new
            {
                url = Request.GetUri(),
                traceId = $"http://localhost:6301/{TraceContext.Current.TraceId}"
            });
        }
    }
}
