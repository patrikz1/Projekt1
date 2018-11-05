using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt
{
    public class Validation
    {
        public bool IsDuplicate(string newContent, string[] content)
        {
            return content.Any((c) => String.Equals(c, newContent, StringComparison.OrdinalIgnoreCase));
        }

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
    }
}