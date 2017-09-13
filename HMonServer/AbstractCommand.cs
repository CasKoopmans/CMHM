using System;
using System.IO;

namespace HMonServer
{
	public abstract class AbstractCommand : Command
	{
		public string PacketToFilename(string dirname, DataPacket dp)
		{
			int num;

			dirname += dp.PatientId;
			num = Directory.CreateDirectory (dirname).GetFiles().Length + 1;
			return dirname + Path.DirectorySeparatorChar + "hmon-data-" + num + ".json";
		}

		public abstract void Execute(IClient client, DataPacket data);
	}
}

