using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bilgisayarAdi = Dns.GetHostName();
            textBox1.Text = "Bilgisayar Adı: " + bilgisayarAdi;
            string ipAdresi = Dns.GetHostByName(bilgisayarAdi).AddressList[0].ToString();
            textBox2.Text = "IP Adresi " + ipAdresi;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void globalipogren_Click(object sender, EventArgs e)
        {
            var webClient = new WebClient();

            string dnsString = webClient.DownloadString("http://checkip.dyndns.org");
            dnsString = (new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")).Match(dnsString).Value;

            webClient.Dispose();
            textBox3.Text = dnsString;

        }
    }
}
