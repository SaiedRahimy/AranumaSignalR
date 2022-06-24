using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Connections
{
    //
    // Summary:
    //     A factory abstraction for creating connections to an endpoint.
    public interface IConnectionFactory12
    {
        //
        // Summary:
        //     Creates a new connection to an endpoint.
        //
        // Parameters:
        //   endpoint:
        //     The System.Net.EndPoint to connect to.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A System.Threading.Tasks.ValueTask`1 that represents the asynchronous connect,
        //     yielding the Microsoft.AspNetCore.Connections.ConnectionContext for the new connection
        //     when completed.
        //ValueTask<ConnectionContext> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default(CancellationToken));
        Task<ConnectionContext> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default(CancellationToken));
    }
}
