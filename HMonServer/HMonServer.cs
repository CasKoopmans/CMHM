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
using System.Collections.Concurrent;

using Newtonsoft.Json;

namespace HMonServer
{
	public class HMonServer
	{
		private Thread server;
		private static HMonServer self;
		private SSLServer SslListener;
		private ConcurrentDictionary<int, Command> Commands;

		private HMonServer()
		{
			this.Commands = new ConcurrentDictionary<int, Command> ();
			this.Commands [DataPacket.STORE] = new CommandPut (MainWindow.Instance.DataDirectory);
			this.Commands [DataPacket.GET] = new CommandGet (MainWindow.Instance.DataDirectory);
			this.Commands [DataPacket.STOP] = new CommandStop (MainWindow.Instance.DataDirectory);
			this.Commands [DataPacket.REPLAY] = new CommandReplay (MainWindow.Instance.DataDirectory);
			this.Commands [DataPacket.CREATE_SESSION] = new CommandCreateSession (MainWindow.Instance.DataDirectory);
			this.Commands [DataPacket.GET_SESSIONS] = new CommandGetSessions (MainWindow.Instance.DataDirectory);
		}

		public static HMonServer Instance
		{
			get {
				if (self == null)
					self = new HMonServer ();

				return self;
			}
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
                MainWindow.Instance.AppendToStatus("Client connect..");
				new Thread (HandleNewClient).Start (stream);
			}
		}

		public void HandleNewClient(object raw)
		{
			string msg;
			Command cmd;
			DataPacket data;
			SslStream stream = raw as SslStream;
			Client client = new Client (stream);

			while (true) {
				try {
					msg = client.Read ();
					data = DataPacket.Deserialize (msg);
					cmd = this.Commands[data.CommandCode];

					if(cmd == null)
						throw new InvalidCommandException("Command unknown");

					cmd.Execute(client, data);

					if(data.Data != null)
						MainWindow.Instance.AppendToStatus ("Received a packet from patient: " + data.Data.PatientId);

				} catch (JsonReaderException ex) {
					MainWindow.Instance.AppendToStatus (ex.Message);
					DataPacket dp = new DataPacket ();
					dp.CommandCode = DataPacket.ERROR;
					dp.ErrorCode = DataPacket.INVALID_REQUEST;
					client.Write (dp.Serialize());
					continue;
				} catch(InvalidCommandException) {
					data = new DataPacket ();
					data.CommandCode = DataPacket.ERROR;
					data.ErrorCode = DataPacket.INVALID_REQUEST;
					client.Write (data.Serialize ());
					continue;
				} catch(StopRequestException) {
					client.Close ();
					stream.Close ();
					return;
				}
			}
		}

		/*
		 * Server maintance methods
		 * 
		 * - Create
		 * - Start
		 * - Stop
		 * - Destroy
		 */

		public static void Stop()
		{
			HMonServer.Instance.DestroyServer ();
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

		public static void Start()
		{
			HMonServer.Instance.CreateServer ();
		}
	}
}

