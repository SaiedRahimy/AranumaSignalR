using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;
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
            txtClientName.Text = Environment.MachineName.ToString() + Process.GetCurrentProcess().Id + "Sensor";
        }

        public IPEndPoint CreateIPEndPoint(string endPoint)
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

            try
            {

                string baseUrl = txtUrl.Text;
                var uri = baseUrl == null ? new Uri("net.tcp://94.182.180.208:1234") : new Uri(baseUrl);
                var credential = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1")
                    .GetBytes("SomeUserName" + ":" + "SomePassword"));
                var connectionBuilder = new HubConnectionBuilder();

                //if (uri.Scheme.Contains("net.tcp"))
                //{
                //    connectionBuilder.WithEndPoint(uri);
                //}
                //else
                {
                    connectionBuilder.WithUrl(uri, options =>
                    {
                        options.Headers.Add("Authorization", $"Basic {credential}");
                        //options.AccessTokenProvider = () => Task.FromResult(tokenString); // Not working
                        // Need a solution like this: options.Token = tokenString

                    });
                }


                _connection = connectionBuilder
                    .WithAutomaticReconnect()//
                    .Build();


                _connection.On<string, string>("recive", (s1, s2) => OnRecive(s1, s2));
                _connection.On<string>("identificationResponse", (message) => IdentificationResponse(message));



                CancellationTokenSource closedTokenSource = null;

                _connection.Closed += e =>
                {
                    // This should never be null by the time this fires
                    closedTokenSource.Cancel();

                    Log("Connection closed...");
                    return Task.CompletedTask;

                    //await Task.Delay(new Random().Next(0, 5) * 1000);
                    //await _connection.StartAsync();
                };

                closedTokenSource?.Dispose();

                // Create a new token for this run
                closedTokenSource = new CancellationTokenSource();


                await _connection.StartAsync();
                


                await _connection.InvokeAsync("Identification", txtClientName.Text);


                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnSend.Enabled = true;
                btnLogout.Enabled = true;
                txtMessage.Focus();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Redesign();
                return;
            }
        }


        private void Redesign()
        {
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnSend.Enabled = false;
            btnLogout.Enabled = false;
        }
        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                Redesign();
                await _connection.StopAsync();


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
                await _connection.InvokeAsync("Send", txtClientName.Text, txtMessage.Text);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void Log(string message)
        {
            if (!txtResult.InvokeRequired)
            {
                txtResult.Text += "\r\n" + message;
            }
            else
            {
                txtResult.Invoke(new MethodInvoker(delegate
                {

                    txtResult.Text += "\r\n" + message;

                }));
            }
        }
        private void OnRecive(string name, string message)
        {
            txtResult.Text += "\r\n" + name + ": " + message;
        }
        private void IdentificationResponse(string message)
        {
            txtResult.Text += "\r\n" + "Server: " + message;
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                await _connection.InvokeAsync("Logout");
            }
            catch
            {

            }
        }
    }
}
