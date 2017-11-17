using System;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Vostok.Tracing;
using Vostok.Clusterclient;
using Vostok.Clusterclient.Topology;
using Vostok.Clusterclient.Transport.Http;
using Vostok.Logging.Logs;

namespace Alice.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("{*url}")]
        public object Echo()
        {
            var url = new Uri("https://ya.ru");
            var log = new ConsoleLog();
            var cluster = new ClusterClient(
                log,
                config =>
                {
                    config.ClusterProvider = new FixedClusterProvider(url);
                    config.Transport = new VostokHttpTransport(log);
                });

            var result = cluster.Send(Vostok.Clusterclient.Model.Request.Get("/"));
            result.Response.EnsureSuccessStatusCode();

            return Json(new
            {
                url = Request.GetUri(),
                bobAnswer = result.Response.ToString(),
                traceId = $"http://localhost:6301/{TraceContext.Current.TraceId}"
            });
        }
    }
}
