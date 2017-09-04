/*
 *  HMon - MainWindow
 *  Copyright (C) 2017   Michel Megens <dev@bietje.net>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace HMonServer
{
	#region Windows Form Designer generated code
	public partial class MainWindow
	{
		protected System.Windows.Forms.Label address;
		protected System.Windows.Forms.Label port;
		protected System.Windows.Forms.Button connect;
		protected System.Windows.Forms.Button exit;
		protected System.Windows.Forms.TextBox address_data;
		protected System.Windows.Forms.TextBox port_data;
		protected System.Windows.Forms.RichTextBox status_data;
		protected System.Windows.Forms.Label status;
		protected System.Windows.Forms.Label cert;
		protected System.Windows.Forms.Label cert_data;
		protected System.Windows.Forms.Button cert_open;

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
			this.cert = new System.Windows.Forms.Label();
			this.cert_data = new System.Windows.Forms.Label();
			this.cert_open = new System.Windows.Forms.Button();
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
			this.connect.Location = new System.Drawing.Point(40, 128);
			this.connect.TabIndex = 2;
			this.connect.Size = new System.Drawing.Size(96, 23);
			this.connect.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.connect.Text = "Connect";
			this.connect.UseVisualStyleBackColor = true;
			// 
			// exit
			// 
			this.exit.Name = "exit";
			this.exit.Location = new System.Drawing.Point(160, 128);
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
			this.status_data.Location = new System.Drawing.Point(24, 192);
			this.status_data.TabIndex = 6;
			this.status_data.Size = new System.Drawing.Size(512, 304);
			this.status_data.ReadOnly = true;
			this.status_data.Text = "";
			// 
			// status
			// 
			this.status.Name = "status";
			this.status.Location = new System.Drawing.Point(40, 160);
			this.status.Image = null;
			this.status.TabIndex = 7;
			this.status.Text = "Status:";
			// 
			// cert
			// 
			this.cert.Name = "cert";
			this.cert.Location = new System.Drawing.Point(16, 96);
			this.cert.Image = null;
			this.cert.TabIndex = 8;
			this.cert.Text = "SSL certificate:";
			// 
			// cert_data
			// 
			this.cert_data.Name = "cert_data";
			this.cert_data.Location = new System.Drawing.Point(168, 96);
			this.cert_data.Image = null;
			this.cert_data.TabIndex = 9;
			this.cert_data.Size = new System.Drawing.Size(232, 23);
			this.cert_data.Text = "Open certificate..";
			// 
			// cert_open
			// 
			this.cert_open.Name = "cert_open";
			this.cert_open.Location = new System.Drawing.Point(416, 96);
			this.cert_open.TabIndex = 10;
			this.cert_open.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.cert_open.Text = "Open..";
			this.cert_open.UseVisualStyleBackColor = true;
			// 
			// MainWindow
			// 
			this.Name = "MainWindow";
			this.ClientSize = new System.Drawing.Size(567, 517);
			this.Controls.Add(this.address);
			this.Controls.Add(this.port);
			this.Controls.Add(this.connect);
			this.Controls.Add(this.exit);
			this.Controls.Add(this.address_data);
			this.Controls.Add(this.port_data);
			this.Controls.Add(this.status_data);
			this.Controls.Add(this.status);
			this.Controls.Add(this.cert);
			this.Controls.Add(this.cert_data);
			this.Controls.Add(this.cert_open);
			this.Text = "MainWindow";
		}
	}
	#endregion
}

