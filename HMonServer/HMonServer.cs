/*
 *  HMon - Server
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
using System.Collections;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HMonServer
{
	public class HMonServer
	{
		private Thread server;
		private static HMonServer self;
		private SSLServer SslListener;

		private HMonServer()
		{
		}

		public static HMonServer Instance
		{
			get {
				if (self == null)
					self = new HMonServer ();

				return self;
			}
		}

		public static void start()
		{
			HMonServer.Instance.CreateServer ();
		}

		private void CreateServer()
		{
			this.server = new Thread (new ThreadStart (this.Run));
			this.server.Start ();
		}

		private void DestroyServer()
		{
			this.SslListener.Stop ();
			this.server.Abort ();
		}

		private void Run()
		{
			SslStream stream;

			try {
				SslListener = new SSLServer (MainWindow.Instance.Address,
				                                  MainWindow.Instance.Port,
				                                  MainWindow.Instance.Certificate);
			} catch(ArgumentNullException e) {
				MainWindow.Instance.AppendToStatus ("Failed to start server!");
				Program.DumpException (e);
				this.server.Abort ();
			}

			while (true) {
				stream = SslListener.AuthenticateClient (SslListener.AcceptClient ());
				new Thread (HandleNewClient).Start (stream);
			}
		}

		public void HandleNewClient(object raw)
		{
			string msg;
			SslStream stream = raw as SslStream;
			Client client = new Client (stream);

			msg = client.Read ();
			MainWindow.Instance.AppendToStatus (msg);
			client.Write ("Message received!<EOF>");
		}

		public static void Stop()
		{
			HMonServer.Instance.DestroyServer ();
		}
	}
}

