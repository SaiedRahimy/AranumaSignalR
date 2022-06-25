using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AranumaSignalRWinform.Client.TCP;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.AspNetCore.SignalR.Client
{
    public static class TcpHubConnectionBuilderExtensions
    {
        private class TcpConnectionOptionsDerivedHttpEndPoint : UriEndPoint
        {
            public TcpConnectionOptionsDerivedHttpEndPoint(Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Http.Connections.Client.HttpConnectionOptions> httpConnectionOptions)
                : base(httpConnectionOptions.Value.Url)
            {
            }
        }

        private class HubProtocolDerivedHttpOptionsConfigurer : Microsoft.Extensions.Options.IConfigureNamedOptions<Microsoft.AspNetCore.Http.Connections.Client.HttpConnectionOptions>, Microsoft.Extensions.Options.IConfigureOptions<Microsoft.AspNetCore.Http.Connections.Client.HttpConnectionOptions>
        {
            private readonly TransferFormat _defaultTransferFormat;

            public HubProtocolDerivedHttpOptionsConfigurer(Microsoft.AspNetCore.SignalR.Protocol.IHubProtocol hubProtocol)
            {
                _defaultTransferFormat = hubProtocol.TransferFormat;
            }

            public void Configure(string name, Microsoft.AspNetCore.Http.Connections.Client.HttpConnectionOptions options)
            {
                Configure(options);
            }

            public void Configure(Microsoft.AspNetCore.Http.Connections.Client.HttpConnectionOptions options)
            {
                options.DefaultTransferFormat = _defaultTransferFormat;
            }
        }

        public static IHubConnectionBuilder WithEndPoint(this IHubConnectionBuilder builder, Uri uri)
        {

            //if (!string.Equals(uri.Scheme, "net.tcp", StringComparison.Ordinal))
            //{
            //    throw new InvalidOperationException($"URI Scheme {uri.Scheme} not supported.");
            //}

            IPEndPoint endPoint;
            if (string.Equals(uri.Host, "localhost"))
            {
                endPoint = new IPEndPoint(IPAddress.Loopback, uri.Port);
            }
            else
            {
                endPoint = new IPEndPoint(IPAddress.Parse(uri.Host), uri.Port);
            }

            return builder.WithEndPoint(endPoint);
        }

        public static IHubConnectionBuilder WithEndPoint(this IHubConnectionBuilder builder, EndPoint endPoint)
        {
            //builder.Services.AddSingleton<IConnectionFactory>(new TcpConnectionFactory(endPoint));


            //builder.Services.AddSingleton<EndPoint, TcpConnectionOptionsDerivedHttpEndPoint>();
            //builder.Services.AddSingleton<Microsoft.Extensions.Options.IConfigureOptions<Microsoft.AspNetCore.Http.Connections.Client.HttpConnectionOptions>, HubProtocolDerivedHttpOptionsConfigurer>();

            builder.Services.AddSingleton<IConnectionFactory>(new TcpConnectionFactory(endPoint));
            // builder.Services.AddSingleton<IConnectionFactory, Microsoft.AspNetCore.Http.Connections.Client.HttpConnectionFactory>();

            return builder;
        }

        private class TcpConnectionFactory : IConnectionFactory
        {
            private readonly EndPoint _endPoint;

            public TcpConnectionFactory(EndPoint endPoint)
            {
                _endPoint = endPoint;

                //var tts=new TcpConnection(_endPoint).StartAsync();
                //var ts = tts.Result;
            }




            public ValueTask<ConnectionContext> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default)
            {
                return new TcpConnection(_endPoint).StartAsync();//.AsTask();
            }

            //public Task<ConnectionContext> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default)
            //{
            //    return new TcpConnection(_endPoint).StartAsync().AsTask();
            //}

            public Task DisposeAsync(ConnectionContext connection)
            {
                return ((TcpConnection)connection).DisposeAsync();
            }
        }
    }
}