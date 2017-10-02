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
using System.Collections.Generic;
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
		public const int STOP = 6;
		public const int CREATE_SESSION = 7;
		public const int GET_SESSIONS = 8;

		public int CommandCode { get; set; }
		public int ErrorCode { get; set; }
		public string SessionId { get; set; }
		public DataMessage Data { get; set; }

		public const int EOK = 0;

		public DataPacket ()
		{
			this.ErrorCode = EOK;
		}

		public string Serialize()
		{
			return JsonConvert.SerializeObject (this);
		}

		public static string SerializeMany(List<DataPacket> data)
		{
			return JsonConvert.SerializeObject (data);
		}

		public static DataPacket Deserialize(string json)
		{
			return JsonConvert.DeserializeObject<DataPacket> (json);
		}

		public override string ToString()
		{
			return this.Serialize ();
		}
	}
}

