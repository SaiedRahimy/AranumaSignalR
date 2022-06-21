using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            _connection = new HubConnectionBuilder()
               .WithUrl(txtUrl.Text)
               .Build();

            _connection.On<string, string>("alert", (s1, s2) => OnSend(s1, s2));

            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                return;
            }

            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;
            btnSend.Enabled = true;
            txtMessage.Focus();
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
