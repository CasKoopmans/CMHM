using HMonPatient.Pages;
using StaMa;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMonPatient
{
    class Test
    {
        int sessioncount = 0;
        bool failed = false;
        SessionPage Session;
        string feedback = "";

        public Test(SessionPage session)
        {
            Session = session;
            Warmup();
        }

        private async void Warmup()
        {
            Debug.WriteLine("Warmup");
            Session.setStateText("Warmup");
            Session.setResistance(85);          
            await Task.Delay(120000);
            Fase1();
        }

        private async void Fase1()
        {
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
                    await Task.Delay(3000);
                    if (Session.getLastPulse() < 130)
                    {
                        Session.setResistance(Session.getLastResistance() + 5);
                        feedback += "Pulse to low.\n";
                        failed = true;
                    }
                    await Task.Delay(2000);
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
                    await Task.Delay(10000);
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

        }
    }
}
