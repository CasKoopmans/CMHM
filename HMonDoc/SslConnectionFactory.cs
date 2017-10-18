using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HMonDoc
{
    public class SslConnectionFactory
    {
        public static TcpClient CreateTcpClient(string host, int port)
        {
            return new TcpClient(host, port);
        }

        public static bool ValidateHMonServer(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errs)
        {
            return true;
        }

        public static SslStream CreateFromClient(TcpClient client)
        {
            SslStream stream = new SslStream(
                client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateHMonServer),
                null);

            try
            {
                stream.AuthenticateAsClient("bietje.net", null, SslProtocols.Ssl3, false);
            } catch(AuthenticationException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return stream;
        }
    }
}
