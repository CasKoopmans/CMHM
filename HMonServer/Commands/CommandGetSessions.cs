using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace HMonServer
{
	public class CommandGetSessions : AbstractCommand
	{
		public string DataDirectory { get; set; }

		public CommandGetSessions (string datadir)
		{
			this.DataDirectory = datadir;
		}

		/* Get all session IDs for a given client */
		public override void Execute(IClient client, DataPacket packet)
		{
			var dirs = new List<String> (Directory.GetDirectories (this.DataDirectory + Path.DirectorySeparatorChar + packet.Data.PatientId));
			List<string> IDs = new List<string>();
			DataPacket dp;

			foreach(string path in dirs) {
				IDs.Add (Path.GetFileName (path));
			}

			dp = new DataPacket ();
			dp.CommandCode = DataPacket.REPLY;
			dp.Data = new DataMessage ();
			dp.Data.SessionIDs = IDs;
			client.Write (dp.Serialize ());
		}
	}
}

