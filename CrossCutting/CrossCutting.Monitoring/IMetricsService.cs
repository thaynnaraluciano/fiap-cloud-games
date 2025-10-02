namespace CrossCutting.Monitoring
{
    public interface IMetricsService
    {
        void ObserveRequest(string method, string endpoint, int statusCode, double durationSeconds);
    }
}
