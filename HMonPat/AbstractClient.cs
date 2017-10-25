/*
 *  HMon - Abstract client
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
using System.Text;
using System.Collections;
using System.Net.Security;

namespace HMonPat
{
	public abstract class AbstractClient
	{
		protected SslStream Stream { get; set; }
		protected StreamReader Reader { get; set; }
		protected string EndOfMessage { get { return "<EOF>"; } }

		public AbstractClient (SslStream stream)
		{
			this.Stream = stream;
			this.Reader = new StreamReader (stream);
		}

		public void Write(string data)
		{
			byte[] buffer;

			data += this.EndOfMessage;
			buffer = Encoding.ASCII.GetBytes (data);
			this.Stream.Write (buffer, 0, buffer.Length);
		}

		public string ReadLine()
		{
			return this.Reader.ReadLine ();
		}

		public string Read()
		{
			byte[] buffer = new byte[2048];
			Decoder codec;
			int num;
			char[] data;
			StringBuilder builder;

			codec = Encoding.UTF8.GetDecoder ();
			builder = new StringBuilder ();
            try
            {
                while ((num =
                       this.Stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    data = new char[codec.GetCharCount(buffer, 0, num)];
                    codec.GetChars(buffer, 0, num, data, 0);
                    builder.Append(data);

                    if (builder.ToString().Contains("<EOF>"))
                        break;
                }
            } catch(IOException ex)
            {
                throw new Exception();
            }

			return builder.ToString ().Replace ("<EOF>", "");
		}

		public void Close()
		{
			this.Reader.Close ();
		}
	}
}

