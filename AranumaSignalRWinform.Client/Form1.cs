using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AranumaSignalRWinform.Client
{
    public partial class Form1 : Form
    {
        private HubConnection _connection;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //txtUrl.Text = "http://localhost:5000/chat/";
        }

        public  IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (!IPAddress.TryParse(ep[0], out ip))
            {
                throw new FormatException("Invalid ip-adress");
            }
            int port;
            if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (false)
            {
                try
                {
                    _connection = new HubConnectionBuilder()
                   .WithUrl(txtUrl.Text)
                   .Build();

                _connection.On<string, string>("alert", (s1, s2) => OnSend(s1, s2));

               
                    await _connection.StartAsync();

                    _connection.Closed += async (error) =>
                    {
                        await Task.Delay(new Random().Next(0, 5) * 1000);
                        await _connection.StartAsync();
                    };
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                    return;
                }
            }
            else
            {
                try
                {
                    //var endpoint = new IPEndPoint(IPAddress.Loopback, 5060);
                    

                    string baseUrl = txtUrl.Text;
                    var uri = baseUrl == null ? new Uri("tcp://94.182.180.208:1234") : new Uri(baseUrl);






                    var connectionBuilder = new HubConnectionBuilder();
                


                    
                    if (uri.Scheme == "net.tcp")
                    {
                        connectionBuilder.WithEndPoint(uri);                        
                    }
                    else
                    {
                        connectionBuilder.WithUrl(uri);
                    }


                    _connection = connectionBuilder.Build();
                    


                    _connection.On<string, string>("alert", (s1, s2) => OnSend(s1, s2));

                    CancellationTokenSource closedTokenSource = null;

                    _connection.Closed += e =>
                    {
                        // This should never be null by the time this fires
                        closedTokenSource.Cancel();

                        Log("Connection closed...");
                        return Task.CompletedTask;
                    };

                    closedTokenSource?.Dispose();

                    // Create a new token for this run
                    closedTokenSource = new CancellationTokenSource();


                    if (!await ConnectAsync(_connection))
                    {
                        return;
                    }


                    
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                    return;
                }
               

            }

            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;
            btnSend.Enabled = true;
            txtMessage.Focus();
        }

        private  async Task<bool> ConnectAsync(HubConnection connection)
        {
            // Keep trying to until we can start
            while (true)
            {
                try
                {
                    await connection.StartAsync();
                    return true;
                }
                catch (ObjectDisposedException)
                {
                    // Client side killed the connection
                    return false;
                }
                catch (Exception)
                {
                    Log("Failed to connect, trying again in 5000(ms)");

                    await Task.Delay(5000);
                }
            }
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                await _connection.StopAsync();
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
                btnSend.Enabled = false;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }

        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                await _connection.InvokeAsync("Send", "Fire Sensor", txtMessage.Text);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void Log(string message)
        {
            txtResult.Text += "\r\n" + message;
        }
        private void OnSend(string name, string message)
        {
            txtResult.Text += "\r\n" + name + ": " + message;
        }


    }
}
