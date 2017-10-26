using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

namespace HMonPat
{
    class Test
    {
        int sessioncount = 0, maxHR, pastPulse = 0;
        bool failed = false;
        private static MainWindow Session;
        string feedback = "";
        private static int timer = 0;

        public Test(MainWindow session)
        {
            Session = session;
            Warmup();
        }

        private async void Warmup()
        {
            Timer t = new Timer();

            Session.SetTimeBox("02:00");
            Test.timer = 120;
            t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            t.Interval = 1000;
            t.Enabled = true;

            Debug.WriteLine("Warmup");
            Session.setStateText("Warmup");
            Session.setResistance(50);
            await Task.Delay(120000);
            
            t.Stop();
            Fase1();
        }

        private async void Fase1()
        {
            Timer t = new Timer();

            Session.SetTimeBox("02:00");
            Test.timer = 120;
            t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            t.Interval = 1000;
            t.Enabled = true;

            Debug.WriteLine("Fase1");
            Session.setStateText("Fase1");
            Session.askData();
            await Task.Delay(2000);
            Session.setResistance(100);
            await Task.Delay(29000);
            Session.setResistance(125);
            await Task.Delay(29000);
            Session.askData();
            await Task.Delay(2000);
            Session.setResistance(135);
            await Task.Delay(29000);
            Session.setResistance(150);
            await Task.Delay(29000);

            t.Stop();
            Fase2();
        }

        private static void OnTimedEvent(object src, ElapsedEventArgs e)
        {
            int minutes, seconds;

            timer -= 1;
            minutes = timer / 60;
            seconds = timer % 60;
            Session.SetTimeBox(new SimpleTime(minutes, seconds).ToString());
        }

        private async void Fase2()
        {
            Timer t = new Timer();

            Session.SetTimeBox("02:00");
            Test.timer = 120;
            t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            t.Interval = 1000;
            t.Enabled = true;

            Debug.WriteLine("Fase2");
            Session.setStateText("Fase2");
            while (true)
            {
                if (sessioncount < 8)
                {
                    Session.setFeedbackText("");
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
                    pastPulse = Session.getLastPulse();
                    if (failed){
                        Failed();
                        return;
                    }
                    await Task.Delay(13000);
                }
                else break;
            }
            t.Stop();
            Session.askData();
            Cooldown();
            
        }

        private void EmergencyStop()
        {
            Session.EmergencyBreak();
        }

        private async void Cooldown()
        {
            Timer t = new Timer();

            Session.SetTimeBox("01:00");
            Test.timer = 60;
            t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            t.Interval = 1000;
            t.Enabled = true;

            Debug.WriteLine("Cooldown");
            Session.setStateText("Cooldown");
            Session.setResistance(50);
            await Task.Delay(60000);

            t.Stop();
            Done();
        }

        private void Failed()
        {
            Debug.WriteLine("Failed");
            Session.setStateText("Failed");
            Session.setFeedbackText(feedback);
            sessioncount = 0;
            failed = false;
            timer = 120;
            Session.SetTimeBox("02:00");
            Fase2();
        }

        private void Done()
        {
            Session.Done();
        }
    }
}
