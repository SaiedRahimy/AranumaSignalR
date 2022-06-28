namespace AranumaSignalRWinform.Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.Message = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.Message.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnLogout);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtClientName);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Location = new System.Drawing.Point(7, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(639, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(11, 113);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Client Name:";
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(93, 69);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(454, 23);
            this.txtClientName.TabIndex = 3;
            // 
            // txtUrl
            // 
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.Location = new System.Drawing.Point(6, 22);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(460, 23);
            this.txtUrl.TabIndex = 2;
            this.txtUrl.Text = "http://localhost:5000/chat/";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(553, 22);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 1;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(472, 22);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // Message
            // 
            this.Message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Message.Controls.Add(this.txtResult);
            this.Message.Controls.Add(this.txtMessage);
            this.Message.Controls.Add(this.btnSend);
            this.Message.Location = new System.Drawing.Point(1, 307);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(639, 420);
            this.Message.TabIndex = 1;
            this.Message.TabStop = false;
            this.Message.Text = "Message";
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(6, 52);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(627, 362);
            this.txtResult.TabIndex = 3;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(6, 22);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(546, 23);
            this.txtMessage.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(558, 22);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtToken);
            this.groupBox2.Location = new System.Drawing.Point(7, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(639, 134);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Token";
            // 
            // txtToken
            // 
            this.txtToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToken.Location = new System.Drawing.Point(6, 22);
            this.txtToken.Multiline = true;
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(627, 106);
            this.txtToken.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 735);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Aranuma";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Message.ResumeLayout(false);
            this.Message.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox Message;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtToken;
    }
}
