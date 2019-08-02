using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace servisornek
{
    public partial class Service1 : ServiceBase
    {
        string ipadress = "";
        int timersayac = 0;
        int sure = 0;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ipadress = ipogren();

            while (true)
            {
                var webClient = new WebClient();
                string dnsString = webClient.DownloadString("http://checkip.dyndns.org");
                dnsString = (new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")).Match(dnsString).Value;
                webClient.Dispose();
                if (ipadress == dnsString)
                {
                    WriteToFile(ipadress + "true");
                }
                else
                {
                    WriteToFile(ipadress + "false");
                    /// mesaj atsın cep teline 
                }
                sure = 100;
                timer1.Interval = 60000;
                timer1.Enabled = true;


            }


        }

        protected override void OnStop()
        {
        }
        public string ipogren()
        {
            string ipadres = "";
            try
            {
                var webClient = new WebClient();
                string dnsString = webClient.DownloadString("http://checkip.dyndns.org");
                dnsString = (new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")).Match(dnsString).Value;
                webClient.Dispose();
                ipadres = dnsString;
            }
            catch (Exception)
            {

                //throw;
            }
            return ipadress;
        }

        public int mesajat(string ipadresi, string ceptelno)
        {
            string link = "https://platform.clickatell.com/messages/http/send?apiKey=Bryl1QARSnqOD1g_13z1YQ==&to=905413009388&content=studiogonderi";
            var webClient = new WebClient();
            string sayfa = webClient.DownloadString(link);
            return 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timersayac++;
            if (timersayac > sure)
            {
                timer1.Enabled = false;
            }

        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
