using AranumaSignalR.WebApi.Server.Enum;
using System.Threading.Tasks;

namespace AranumaSignalR.WebApi.Server.Monitoring.Contracts
{
    public interface IMonitoringMetrics
    {
        Task AddMetricValue(MonitoringMetricType metricType, int count);
    }
}
