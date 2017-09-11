/*
 *  HMon - Network packet
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
using Newtonsoft.Json;

namespace HMonServer
{
	public class DataPacket
	{
		/*
		 * Command code constants
		 */
		public const int STORE = 1;
		public const int GET = 2;
		public const int REPLAY = 3;
		public const int REPLY = 4;
		public const int ERROR = 5;

		public int CommandCode { get; set; }
		public long Time { get; set; }
		public string PatientId { get; set; }
		public string Name { get; set; }
		public bool Stop { get; set; }
		public string Comment { get; set; }

		public DataPacket ()
		{
			this.Stop = false;
		}

		public string Serialize()
		{
			return JsonConvert.SerializeObject (this);
		}

		public static DataPacket Deserialize(string json)
		{
			return JsonConvert.DeserializeObject<DataPacket> (json);
		}

		public void Save()
		{
			this.Save (this.Serialize ());
		}

		public void Save(string data)
		{
			int num;
			string fname, dirname;
			MainWindow win = MainWindow.Instance;

			dirname = win.DataDirectory + Path.DirectorySeparatorChar + this.PatientId;
			num = Directory.CreateDirectory (dirname).GetFiles().Length + 1;
			fname = dirname + Path.DirectorySeparatorChar + "hmon-data-" + num + ".json";
			File.WriteAllText (fname, data);
		}
	}
}

