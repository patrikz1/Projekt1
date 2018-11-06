using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt
{
    public interface Interface
    {
        void HideSelection(ListView podcast, ListView categories);
        void FullRowSelect(ListView podcast);
        void SelectedIndex(ComboBox comboFrekvens, ComboBox comboCategory);

    }
}
