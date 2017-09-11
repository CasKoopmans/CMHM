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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HMonServer
{
	public partial class MainWindow : Form
	{
		public string Address { get; set; }
		public string Certificate {get;set;}
		public string DataDirectory { get; set; }
		public int Port { get; set; }
		private bool Connected;
		private static MainWindow self;

		private MainWindow()
		{
			this.Connected = false;
			this.InitializeComponent ();
			this.data_dir_data.Text = this.DataDirectory = "/home/michel/workspace/mono/HMon/HMonServer/data";
			this.ConnectEvents ();
		}

		public static MainWindow Instance
		{
			get {
				if (self == null)
					self = new MainWindow ();
				return self;
			}
		}

		private void ConnectEvents()
		{
			this.exit.Click += new EventHandler (this.ExitClick);
			this.connect.Click += new EventHandler (this.ConnectClick);
			this.cert_open.Click += new EventHandler (this.OpenCertClick);
			this.data_dir_open.Click += new EventHandler (this.OpenDataDirClick);
		}

		private void ExitClick(object sender, EventArgs e)
		{
			Application.Exit ();
		}

		private void OpenCertClick(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog ();
			if (dialog.ShowDialog () == DialogResult.OK) {
				this.cert_data.Text = dialog.FileName;
				this.Certificate = dialog.FileName;
			}
		}

		private void OpenDataDirClick(object sender, EventArgs e)
		{
			var dialog = new FolderBrowserDialog ();

			if (dialog.ShowDialog () == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath)) {
				this.data_dir_data.Text = dialog.SelectedPath;
				this.DataDirectory = dialog.SelectedPath;
			}
		}

		private void ConnectClick(object sender, EventArgs e)
		{
			string sport;
			int port;

			if (this.Connected) {
				this.connect.Text = "Connect";
				this.Connected = false;
				this.AppendToStatus ("Stopping server...");
				HMonServer.Stop ();
				return;
			}

			this.Address = this.address_data.Text;
			sport = this.port_data.Text;

			if (Int32.TryParse (sport, out port)) {
				this.Port = port;
			} else {
				MessageBox.Show ("Invalid port number!");
				return;
			}

			if (!File.Exists (this.Certificate)) {
				MessageBox.Show ("Invalid certificate file!");
				return;
			}

			if (this.DataDirectory == null) {
				MessageBox.Show ("Invalid data directory!");
				return;
			} else {
				System.IO.Directory.CreateDirectory (this.DataDirectory);
			}

			this.AppendToStatus ("Starting server...");
			HMonServer.Start ();
			this.connect.Text = "Disconnect";
			this.Connected = true;
		}

		public void AppendToStatus(string text)
		{
			text += System.Environment.NewLine;
			this.status_data.Text += text;
		}

		public static void Open()
		{
			Application.Run (MainWindow.Instance);
		}
	}
}
