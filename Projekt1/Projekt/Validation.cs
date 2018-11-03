using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt
{
    class Validation
    {
        public bool tfInteTomt(TextBox falt)
        {

            if (falt.Text == "")
            {

                MessageBox.Show("Du måste skriva in en URL");
                falt.Clear();
                return false;

            }
            else
            {

                return true;
            }

        }

        public bool TaBort(ListView lview)
        {
            if (lview.Text == "")
            {
                MessageBox.Show("Du måste välja något.");
                return false;
            }
            else
            {

                return true;
            }
        }

        public bool riktigURL(string url)
        {
            try
            {
                var xml = "";
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    xml = client.DownloadString(url);
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Skriv in en giltig URL.");
                return false;
            }
        }
    }
}