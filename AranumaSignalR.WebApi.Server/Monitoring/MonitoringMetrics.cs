
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

    public class MonitoringMetrics : IMonitoringMetrics
    {
        #region Variables

        IMonitoringClient _monitoringClient;
        readonly IOptions<MonitoringConfig> _configuration;
        public ConcurrentDictionary<MonitoringMetricType, MetricPack> _gauges;
        public Timer _timer;

        #endregion


        public MonitoringMetrics(IMonitoringClient monitoringClient, IOptions<MonitoringConfig> configuration)
        {
            _monitoringClient = monitoringClient;
            _configuration = configuration;

            _gauges = new ConcurrentDictionary<MonitoringMetricType, MetricPack>();
            _timer = new Timer(1000);

            _timer.Elapsed += (s, e) =>
            {
                SendMetrics();
            };
            _timer.Enabled = true;


        }


        #region Private Methods
        private void SendMetrics()
        {
            var metricPackes = _gauges.Values;

            foreach (var metric in metricPackes)
            {
                SendGaugeMetric(metric.Gauge, metric.Counter);
                if (metric.ResetAfterEachSend)
                {
                    metric.Counter = 0;
                }
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
        public async Task AddMetricValue(MonitoringMetricType metricType, int count, bool resetAfterEachSend = true)
        {
            if (_gauges.ContainsKey(metricType))
            {
                _gauges[metricType].Counter += count;
            }
            else
            {
                var metricTypeName = metricType.ToString();
                var gauge = _monitoringClient.CreateGauge(_configuration.Value.PrefixNameForMetric + metricTypeName, metricTypeName);
                var result = _gauges.TryAdd(metricType, new MetricPack { Gauge = gauge, Counter = count, ResetAfterEachSend = resetAfterEachSend });
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
