/*
 *  HMon - Replay command
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
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace HMonServer
{
	public class CommandReplay : AbstractCommand
	{
		public string Directory { get; set; }

		public CommandReplay (string directory) : base()
		{
			this.Directory = directory + Path.DirectorySeparatorChar;
		}

		public override void Execute(IClient client, DataPacket dp)
		{
			string directory = this.PacketToDatadir (this.Directory, dp);
			DirectoryInfo info;
			List<DataMessage> msgs;
			long current_reltime, reltime;
			int waitperiod;
			DataPacket reply;

			info = new DirectoryInfo (directory);
			msgs = this.ReadAllDataFrom (info);

			current_reltime = reltime = 0L;
			foreach (var msg in msgs) {
				reltime = this.RelativeTimeToMilis (msg.Minutes, msg.Seconds);
				waitperiod = (int)(reltime - current_reltime);

				if (waitperiod > 0) {
					Thread.Sleep (waitperiod);
					current_reltime += waitperiod;
				}

				reply = new DataPacket ();
				reply.CommandCode = DataPacket.REPLAY;
				reply.Data = msg;
				client.Write (reply.Serialize ());
			}
		}

		private long RelativeTimeToMilis(int minutes, int seconds)
		{
			seconds += minutes * 60;
			return seconds * 1000;
		}
	}
}

