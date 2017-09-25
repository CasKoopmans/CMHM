/*
 *  HMon - AbstractCommand
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
using System.IO;
using System.Collections.Generic;

namespace HMonServer
{
	public abstract class AbstractCommand : Command
	{
		public string PacketToFilename(string dirname, DataPacket dp)
		{
			int num;

			dirname += dp.Data.PatientId;
			num = Directory.CreateDirectory (dirname).GetFiles().Length + 1;
			return dirname + Path.DirectorySeparatorChar + "hmon-data-" + num + ".json";
		}

		public string PacketToDatadir(string dirname, DataPacket dp)
		{
			return dirname + dp.Data.PatientId;
		}

		protected List<DataMessage> ReadAllDataFrom(DirectoryInfo inf)
		{
			StreamReader io;
			string data = "";
			string line;
			List<DataMessage> packets;

			packets = new List<DataMessage>();
			foreach (var file in inf.GetFiles("*.json")) {
				io = file.OpenText ();
				while((line = io.ReadLine()) != null) {
					data += line;
				}
				packets.Add (DataMessage.Deserialize (data));
				data = "";
				io.Close ();
			}
			return packets;
		}

		public abstract void Execute(IClient client, DataPacket data);
	}
}

