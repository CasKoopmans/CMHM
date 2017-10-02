using System;
using System.Web.SessionState;
using System.IO;

namespace HMonServer
{
	public class CommandCreateSession : AbstractCommand
	{
		public string DataDirectory { get; set; }

		public CommandCreateSession (string dir)
		{
			this.DataDirectory = dir;
		}

		public override void Execute(IClient client, DataPacket packet)
		{
			string id = Guid.NewGuid().ToString("D");
			DataPacket dp;
			string raw_dp;

			dp = new DataPacket ();
			dp.CommandCode = DataPacket.REPLY;
			dp.SessionId = id;

			this.CreateSession (packet.Data.PatientId, id);
			raw_dp = dp.Serialize ();
			client.Write (raw_dp);
		}

		private void CreateSession(string patient, string id)
		{
			Directory.CreateDirectory (this.DataDirectory + Path.DirectorySeparatorChar +
				patient + Path.DirectorySeparatorChar + id);
		}
	}
}

