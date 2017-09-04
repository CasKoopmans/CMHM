/*
 *  HMon - SSL listener
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
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace HMonServer
{
	public class SSLServer
	{
		private X509Certificate2 cert;
		private TcpListener listener;

		public SSLServer (string addr, int port, string cert)
		{
			this.cert = new X509Certificate2 (cert, "123456");
			this.listener = new TcpListener (IPAddress.Any, port);
			this.listener.Start();
			MainWindow.Instance.AppendToStatus ("Server started!");
		}

		public TcpClient AcceptClient()
		{
			return this.listener.AcceptTcpClient ();
		}

		public SslStream AuthenticateClient(TcpClient client)
		{
			SslStream stream;

			stream = new SslStream (client.GetStream(), false);

			try {
				stream.AuthenticateAsServer(this.cert, false, SslProtocols.Ssl3, true);
				stream.WriteTimeout = 5000;
				stream.ReadTimeout = 5000;
			} catch(AuthenticationException e) {
				MainWindow.Instance.AppendToStatus ("Could not authenticate client!");
				Program.DumpException (e);
				stream.Close ();
				client.Close ();
				return null;
			}

			return stream;
		}

		public void Stop()
		{
			this.listener.Stop ();
		}
	}
}

