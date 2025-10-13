using Api.Middleware;

namespace Api.Extensions
{
    public static class RequestMetricsMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMetrics(this IApplicationBuilder app)
        => app.UseMiddleware<RequestMetricsMiddleware>();
    }
}
