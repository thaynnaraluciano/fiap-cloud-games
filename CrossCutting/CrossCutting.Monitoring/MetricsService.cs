using Prometheus;

namespace CrossCutting.Monitoring
{
    public class MetricsService : IMetricsService
    {
        private readonly Counter _requestCounter;
        private readonly Histogram _requestDuration;

        public MetricsService()
        {
            _requestCounter = Metrics.CreateCounter(
                "api_requests_total", 
                "Total number of API requests", 
                new CounterConfiguration
                {
                    LabelNames = new[] { "method", "endpoint", "status" }
                });

            _requestDuration = Metrics.CreateHistogram(
                "api_request_duration_seconds",
                "Duração das requisições HTTP em segundos",
                new HistogramConfiguration
                {
                    LabelNames = new[] { "method", "endpoint" },
                    Buckets = Histogram.ExponentialBuckets(0.001, 2, 15)
                });
        }

        public void ObserveRequest(string method, string endpoint, int statusCode, double durationSeconds)
        {
            _requestCounter.WithLabels(method, endpoint, statusCode.ToString()).Inc();
            _requestDuration.WithLabels(method, endpoint).Observe(durationSeconds);
        }
    }
}
