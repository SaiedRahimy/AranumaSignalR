using Core.Infrustructure.Monitoring.Models.Metrics;

namespace AranumaSignalR.WebApi.Server.Model
{
    public class MetricPack
    {
        public Gauge Gauge { get; set; }
        public int Counter { get; set; }
    }
}
