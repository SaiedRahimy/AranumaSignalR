using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http.Connections.Client;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;

namespace AranumaSignalRWinform.Client
{
    internal class RawSample
    {
        internal static void Register(CommandLineApplication app)
        {
            app.Command("raw", cmd =>
            {
                cmd.Description = "Tests a connection to an endpoint";

                var baseUrlArgument = cmd.Argument("<BASEURL>", "The URL to the Chat EndPoint to test");

                cmd.OnExecute(() => ExecuteAsync(baseUrlArgument.Value));
            });

        }

        public static async Task<int> ExecuteAsync(string baseUrl)
        {
            baseUrl = string.IsNullOrEmpty(baseUrl) ? "net.tcp://94.182.180.208:1234" : baseUrl;

            Console.WriteLine($"Connecting to {baseUrl}...");
            var connection = new HttpConnection(new Uri(baseUrl));
            try
            {
                await connection.StartAsync(TransferFormat.Text);

                Console.WriteLine($"Connected to {baseUrl}");
                var shutdown = new TaskCompletionSource<object>();
                Console.CancelKeyPress += (sender, a) =>
                {
                    a.Cancel = true;
                    shutdown.TrySetResult(null);
                };

                _ = ReceiveLoop(Console.Out, connection.Transport.Input);
                _ = SendLoop(Console.In, connection.Transport.Output);

                await shutdown.Task;
            }
            catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
            {
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                await connection.DisposeAsync();
            }
            return 0;
        }

        private static async Task ReceiveLoop(TextWriter output, PipeReader input)
        {
            while (true)
            {
                var result = await input.ReadAsync();
                var buffer = result.Buffer;

                try
                {
                    if (!buffer.IsEmpty)
                    {
                        await output.WriteLineAsync(Encoding.UTF8.GetString(buffer.ToArray()));
                    }
                    else if (result.IsCompleted)
                    {
                        // No more data, and the pipe is complete
                        break;
                    }
                }
                finally
                {
                    input.AdvanceTo(buffer.End);
                }
            }
        }

        private static async Task SendLoop(TextReader input, PipeWriter output)
        {
            while (true)
            {
                var result = await input.ReadLineAsync();
                await output.WriteAsync(Encoding.UTF8.GetBytes(result));
            }
        }
    }
}
