using System.Diagnostics;
using System.Threading.Tasks;

namespace HMonPat
{
    class Test
    {
        int sessioncount = 0, maxHR, pastPulse = 0;
        bool failed = false;
        MainWindow Session;
        string feedback = "";

        public Test(MainWindow session)
        {
            Session = session;
            Warmup();
        }

        private async void Warmup()
        {
            Debug.WriteLine("Warmup");
            Session.setStateText("Warmup");
            Session.setResistance(50);          
            await Task.Delay(120000);
            Fase1();
        }

        private async void Fase1()
        {
            Debug.WriteLine("Fase1");
            Session.setStateText("Fase1");
            Session.askData();
            await Task.Delay(1400);
            Session.setResistance(100);
            await Task.Delay(29000);
            Session.setResistance(125);
            await Task.Delay(29000);
            Session.askData();
            await Task.Delay(1400);
            Session.setResistance(135);
            await Task.Delay(29000);
            Session.setResistance(150);
            await Task.Delay(29000);
            Fase2();
        }

        private async void Fase2()
        {
            Debug.WriteLine("Fase2");
            Session.setStateText("Fase2");
            while (true)
            {
                feedback = "";
                if (sessioncount < 8)
                {
                    Session.askData();
                    await Task.Delay(1000);
                    if(pastPulse == 0) { pastPulse = Session.getLastPulse(); }
                    if(Session.getLastPulse() > Session.maxHF) { EmergencyStop(); }
                    if (Session.getLastPulse() < 130)
                    {
                        Session.setResistance(Session.getLastResistance() + 5);
                        feedback += "Pulse to low.\n";
                        failed = true;
                    }
                    if (pastPulse + 5 < Session.getLastPulse() || pastPulse-5 > Session.getLastPulse())
                    {
                        failed = true;
                        feedback += "Pulse not steady.\n";
                    }
                    await Task.Delay(1000);
                    if (Session.getLastRPM() < 50)
                    {
                        Session.setResistance(Session.getLastResistance() - 5);
                        feedback += "Rounds per minute to low.\n";
                        failed = true;
                    }
                    else if(Session.getLastRPM() > 60)
                    {
                        Session.setResistance(Session.getLastResistance() + 5);
                        feedback += "Rounds per minute to high.\n";
                        failed = true;
                    }
                    sessioncount++;
                    if (failed){
                        Failed();
                        return;
                    }
                    pastPulse = Session.getLastPulse();
                    await Task.Delay(13000);
                }
                else break;
            }
            Cooldown();
            
        }

        private void EmergencyStop()
        {
            Session.EmergencyBreak();
        }

        private async void Cooldown()
        {
            Debug.WriteLine("Cooldown");
            Session.setStateText("Cooldown");
            Session.setResistance(50);
            await Task.Delay(60000);
            Done();
        }

        private void Failed()
        {
            Debug.WriteLine("Failed");
            Session.setStateText("Failed");
            Session.setFeedbackText(feedback);
            sessioncount = 0;
            failed = false;
            Fase2();
        }

        private void Done()
        {
            Session.Done();
        }
    }
}
