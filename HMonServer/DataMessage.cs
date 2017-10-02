/*
 *  HMon - DataMessage
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
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HMonServer
{
	public class DataMessage
	{
		public long TimeStamp { get; set; }
		public string PatientId { get; set; }
		public int Pulse {get;set;}
		public int RPM { get; set;}
		public int BPM { get; set;}
		public float Speed { get; set; }
		public float Resistance { get; set; }
		public int Minutes { get; set;}
		public int Seconds { get; set; }
		public float Energy { get ; set; }

		public List<string> SessionIDs { get; set; }

		public DataMessage ()
		{
		}

		public string Serialize()
		{
			return JsonConvert.SerializeObject (this);
		}

		public static string SerializeMany(List<DataMessage> data)
		{
			return JsonConvert.SerializeObject (data);
		}

		public static DataMessage Deserialize(string json)
		{
			return JsonConvert.DeserializeObject<DataMessage> (json);
		}

		public override string ToString()
		{
			return this.Serialize ();
		}
	}
}

