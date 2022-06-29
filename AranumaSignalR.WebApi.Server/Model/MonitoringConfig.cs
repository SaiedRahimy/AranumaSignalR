namespace AranumaSignalR.WebApi.Server.Model
{
    public class MonitoringConfig
    {
        public string PrometheusIpOnYourMachine { get; set; }
        public int PrometheusPortOnYourMachine { get; set; }
        public string PushJobName { get; set; }
        public string PushGatewayEndpoint { get; set; }
        public bool UseMetricPusher { get; set; }
        public string PrefixNameForMetric { get; set; }
    }
}
