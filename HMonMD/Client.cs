using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace HMonMD
{
    public class Client
    {
        public static Client ClientInstance;
        public Doctor Doctor { get; set; }

        public Client()
        {
            Client.ClientInstance = this;
            Doctor = new Doctor("Cas Koopmans", "20", "Tilburg", "ad-1234", "Male");
        }

        public static void Connect(String server, int port)
        {
            SslSocketFactory.createTcpClient(server, port);
        }
    }
}
