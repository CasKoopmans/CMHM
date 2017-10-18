/*
 * SSLSocketFactory
 */

using System;
using Windows.Networking;
using Windows.Networking.Sockets;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Security.Cryptography.Certificates;
using System.Net;

namespace HMonMD
{
    class SslSocketFactory
    {
        private SslSocketFactory()
        {
        }

        public async static void createTcpClient(string host, int port)
        {
            StreamSocket client = new StreamSocket();
            HostName hostname = new HostName(host);
            string service = port.ToString();

            try
            {
                await client.ConnectAsync(hostname, service, SocketProtectionLevel.SslAllowNullEncryption);
                Debug.WriteLine("client connected");
            } catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
