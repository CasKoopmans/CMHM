using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMonServer.Commands
{
    public class CommandCreatePatient : AbstractCommand
    {
        private string DataDirectory;

        public CommandCreatePatient(string datadir)
        {
            this.DataDirectory = datadir;
        }

        public override void Execute(IClient client, DataPacket packet)
        {
            string dirname = this.DataDirectory + Path.DirectorySeparatorChar + packet.Data.PatientId;
            string fname = this.DataDirectory + Path.DirectorySeparatorChar +
                packet.Data.PatientId + Path.DirectorySeparatorChar + "patient.json";

            Directory.CreateDirectory(dirname);
            File.WriteAllText(fname, packet.Data.ToString());
        }
    }
}
