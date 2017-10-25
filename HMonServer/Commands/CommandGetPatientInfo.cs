using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMonServer.Commands
{
    public class CommandGetPatientInfo : AbstractCommand
    {
        private string DataDirectory;

        public CommandGetPatientInfo(string datadir)
        {
            this.DataDirectory = datadir;
        }

        public override void Execute(IClient client, DataPacket packet)
        {
            string fname = this.DataDirectory + Path.DirectorySeparatorChar +
                packet.Data.PatientId + Path.DirectorySeparatorChar + "patient.json";
            string data;

            data = File.ReadAllText(fname);
            DataMessage msg = DataMessage.Deserialize(data);
            DataPacket dp = new DataPacket
            {
                CommandCode = DataPacket.REPLY,
                Data = new DataMessage
                {
                    PatientAge = msg.PatientAge,
                    PatientName = msg.PatientName,
                    IsFemale = msg.IsFemale,
                    PatientWeight = msg.PatientWeight,
                    PatientId = msg.PatientId,
                },
            };

            client.Write(dp.Serialize());
        }
    }
}
