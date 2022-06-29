
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Timers;
using AranumaSignalR.WebApi.Server.Enum;
using AranumaSignalR.WebApi.Server.Model;
using AranumaSignalR.WebApi.Server.Monitoring.Contracts;
using Core.Infrustructure.Monitoring;
using Core.Infrustructure.Monitoring.Models.Metrics;
using Microsoft.Extensions.Options;

namespace AranumaSignalR.WebApi.Server.Monitoring
{
    public static class MonitoringMetricsStatics
    {
        public static ConcurrentDictionary<MonitoringMetricType, MetricPack> Gauges;
        public static Timer Timer;
    }

    public class MonitoringMetrics : IMonitoringMetrics
    {
        #region Variables

        IMonitoringClient _monitoringClient;
        readonly IOptions<MonitoringConfig> _configuration;

        #endregion


        public MonitoringMetrics(IMonitoringClient monitoringClient, IOptions<MonitoringConfig> configuration)
        {
            _monitoringClient = monitoringClient;
            _configuration = configuration;

            if (MonitoringMetricsStatics.Timer == null)
            {
                MonitoringMetricsStatics.Gauges = new ConcurrentDictionary<MonitoringMetricType, MetricPack>();

                MonitoringMetricsStatics.Timer = new Timer(1000);

                MonitoringMetricsStatics.Timer.Elapsed += (s, e) =>
                {
                    SendMetrics();
                };
                MonitoringMetricsStatics.Timer.Enabled = true;

            }
        }

       
        #region Private Methods
        private void SendMetrics()
        {
            var metricPackes = MonitoringMetricsStatics.Gauges.Values;

            foreach (var metric in metricPackes)
            {
                SendGaugeMetric(metric.Gauge, metric.Counter);
                metric.Counter = 0;
            }

        }

        private void SendGaugeMetric(Gauge gaugeMetric, int courrentValue)
        {
            if (gaugeMetric.Value > courrentValue)
            {
                gaugeMetric.DecTo(courrentValue);
            }
            else
            {
                gaugeMetric.IncTo(courrentValue);
            }
        }

        #endregion

        #region Public Methods
        public async Task AddMetricValue(MonitoringMetricType metricType, int count)
        {
            if (MonitoringMetricsStatics.Gauges.ContainsKey(metricType))
            {
                MonitoringMetricsStatics.Gauges[metricType].Counter += count;
            }
            else
            {
                var metricTypeName = metricType.ToString();
                var gauge = _monitoringClient.CreateGauge(_configuration.Value.PrefixNameForMetric + metricTypeName, metricTypeName);
                var result = MonitoringMetricsStatics.Gauges.TryAdd(metricType, new MetricPack { Gauge = gauge, Counter = count });
                if (!result)
                {
                    //log
                }
            }
            await Task.CompletedTask;

        }

        #endregion
    }
}
