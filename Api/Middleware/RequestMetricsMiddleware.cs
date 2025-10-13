using CrossCutting.Monitoring;
using System.Diagnostics;

namespace Api.Middleware
{
    public class RequestMetricsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetricsService _metricsService;

        public RequestMetricsMiddleware(RequestDelegate next, IMetricsService metrics)
        {
            _next = next;
            _metricsService = metrics;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? string.Empty;

            if (path.StartsWith("/metrics") || path.StartsWith("/swagger") || path.StartsWith("/favicon.ico"))
            {
                await _next(context);
                return;
            }
            
            var sw = Stopwatch.StartNew();

            try
            {
                await _next(context);
            }
            finally
            {
                sw.Stop();

                var endpoint = context.GetEndpoint() as RouteEndpoint;
                var routeTemplate = endpoint?.RoutePattern?.RawText ?? path;

                var method = context.Request.Method;
                var status = context.Response?.StatusCode ?? 0;
                var durationSeconds = sw.Elapsed.TotalSeconds;

                _metricsService.ObserveRequest(method, routeTemplate, status, durationSeconds);
            }
        }
    }
}
