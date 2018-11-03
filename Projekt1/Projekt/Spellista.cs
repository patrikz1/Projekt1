using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.ServiceModel.Syndication;
using System.IO;
using System.Text.RegularExpressions;
namespace Projekt
{
    public class Spellista : Interface
    {      
       
        public void HideSelection (ListView podcast, ListView categories)
        {
            podcast.HideSelection = false;
            categories.HideSelection = false;
        }
       
        public void BtnRemovePod(ListView podcasts, ListBox lbAvsnitt)
        {
            //ta bort den från listview, cleara vår textfil, serialize'a alla items i vår listview igen
            var validation = new Validation();
            var serialize = new Serializer();
            var selectedItem = podcasts.SelectedItems[0];
            if (podcasts.SelectedItems.Count == 1)
            {
                var bekrafta = MessageBox.Show("Är du säker på att du vill radera den här podcasten?", "Radera pocast", MessageBoxButtons.YesNo);
                if (bekrafta == DialogResult.Yes)
                {

                    podcasts.Items.Remove(selectedItem);
                    File.WriteAllText("Serializer.txt", String.Empty);

                    serialize.Serialize(podcasts);
                    lbAvsnitt.Items.Clear();
                }

            }
        }

        public void SelectedIndex(ComboBox comboFrekvens, ComboBox comboCategory)
        {
            comboFrekvens.SelectedIndex = 0;
            comboCategory.SelectedIndex = 0;
        }       

        public void FullRowSelect(ListView listView)
        {
            listView.FullRowSelect = true;
        }

        
      
    }
}
