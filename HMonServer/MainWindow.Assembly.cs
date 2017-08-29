namespace HMonServer
{
	#region Windows Form Designer generated code
	public partial class MainWindow
	{
		protected void InitializeComponent()
		{
			this.address = new System.Windows.Forms.Label();
			this.port = new System.Windows.Forms.Label();
			this.connect = new System.Windows.Forms.Button();
			this.exit = new System.Windows.Forms.Button();
			this.address_data = new System.Windows.Forms.TextBox();
			this.port_data = new System.Windows.Forms.TextBox();
			this.status_data = new System.Windows.Forms.RichTextBox();
			this.status = new System.Windows.Forms.Label();
			// 
			// address
			// 
			this.address.Name = "address";
			this.address.Location = new System.Drawing.Point(16, 16);
			this.address.Image = null;
			this.address.TabIndex = 0;
			this.address.Text = "Bind address:";
			// 
			// port
			// 
			this.port.Name = "port";
			this.port.Location = new System.Drawing.Point(16, 56);
			this.port.Image = null;
			this.port.TabIndex = 1;
			this.port.Text = "Bind port:";
			// 
			// connect
			// 
			this.connect.Name = "connect";
			this.connect.Location = new System.Drawing.Point(32, 112);
			this.connect.TabIndex = 2;
			this.connect.Size = new System.Drawing.Size(96, 23);
			this.connect.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.connect.Text = "Connect";
			this.connect.UseVisualStyleBackColor = true;
			// 
			// exit
			// 
			this.exit.Name = "exit";
			this.exit.Location = new System.Drawing.Point(168, 112);
			this.exit.TabIndex = 3;
			this.exit.Size = new System.Drawing.Size(96, 23);
			this.exit.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.exit.Text = "Exit";
			this.exit.UseVisualStyleBackColor = true;
			// 
			// address_data
			// 
			this.address_data.Name = "address_data";
			this.address_data.ForeColor = System.Drawing.SystemColors.WindowText;
			this.address_data.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.address_data.Location = new System.Drawing.Point(168, 16);
			this.address_data.TabIndex = 4;
			this.address_data.Size = new System.Drawing.Size(288, 24);
			this.address_data.BackColor = System.Drawing.SystemColors.Window;
			this.address_data.Text = "127.0.0.1";
			// 
			// port_data
			// 
			this.port_data.Name = "port_data";
			this.port_data.ForeColor = System.Drawing.SystemColors.WindowText;
			this.port_data.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.port_data.Location = new System.Drawing.Point(168, 56);
			this.port_data.TabIndex = 5;
			this.port_data.Size = new System.Drawing.Size(100, 24);
			this.port_data.BackColor = System.Drawing.SystemColors.Window;
			this.port_data.Text = "7100";
			// 
			// status_data
			// 
			this.status_data.Name = "status_data";
			this.status_data.ForeColor = System.Drawing.SystemColors.WindowText;
			this.status_data.ZoomFactor = 1F;
			this.status_data.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.status_data.Location = new System.Drawing.Point(24, 160);
			this.status_data.TabIndex = 6;
			this.status_data.Size = new System.Drawing.Size(512, 304);
			this.status_data.ReadOnly = true;
			this.status_data.Text = "";
			// 
			// status
			// 
			this.status.Name = "status";
			this.status.Location = new System.Drawing.Point(288, 128);
			this.status.Image = null;
			this.status.TabIndex = 7;
			this.status.Text = "Status:";
			// 
			// MainWindow
			// 
			this.Name = "MainWindow";
			this.ClientSize = new System.Drawing.Size(567, 482);
			this.Controls.Add(this.address);
			this.Controls.Add(this.port);
			this.Controls.Add(this.connect);
			this.Controls.Add(this.exit);
			this.Controls.Add(this.address_data);
			this.Controls.Add(this.port_data);
			this.Controls.Add(this.status_data);
			this.Controls.Add(this.status);
			this.Text = "MainWindow";
		}
		private System.Windows.Forms.Label address;
		private System.Windows.Forms.Label port;
		private System.Windows.Forms.Button connect;
		private System.Windows.Forms.Button exit;
		private System.Windows.Forms.TextBox address_data;
		private System.Windows.Forms.TextBox port_data;
		private System.Windows.Forms.RichTextBox status_data;
		private System.Windows.Forms.Label status;
	}
	#endregion
}

