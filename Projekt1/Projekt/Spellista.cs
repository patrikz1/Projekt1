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
 
        public void FullRowSelect(ListView listView)
        {
            //så man markerar hela raden
            listView.FullRowSelect = true;
        }

        public void HideSelection (ListView podcast, ListView categories)
        { 
            //när dessa listviews förlorar fokus har de fortfarande 1 selected
            podcast.HideSelection = false;
            categories.HideSelection = false;
        }

        public void UpdateFeedFreqAndGen(ListView lv)
        {
            var filesystem = new FileSystem();
            var serializer = new Serializer();

            string path = "FeedItems.txt";            
            List<ListViewItem> allFeeds = serializer.DeSerialize(serializer.DeSerializer(path));
            ListViewItem[] newFeeds = new ListViewItem[lv.Items.Count];
            lv.Items.CopyTo(newFeeds, 0);                        
            foreach (ListViewItem allFeedItem in allFeeds)
            {  
                foreach (ListViewItem currentFeed in newFeeds)
                { 
                    string currentFeedUrl = currentFeed.SubItems[4].Text; 
                    string allFeedUrl = allFeedItem.SubItems[4].Text;  
                    //om urlerna på newFeeds och AllFeeds är samma
                    if (allFeedUrl == currentFeedUrl)

                    { 
                        //kolla om frekvens/kategori inte är samma i de 2, isåfall sätt allfeeds till de nya uppdaterade värdet i currentfeed
                        if (currentFeed.SubItems[2].Text != allFeedItem.SubItems[2].Text || currentFeed.SubItems[3].Text != allFeedItem.SubItems[3].Text)
                        {
                            allFeedItem.SubItems[2].Text = currentFeed.SubItems[2].Text;
                            allFeedItem.SubItems[3].Text = currentFeed.SubItems[3].Text;

                        }
                    }
                }
            }
            //cleara Feeditems.txt
            filesystem.ClearFile(path);
            foreach (ListViewItem item in allFeeds)
            { 
                //gör en tempArray, loopar igenom samt sedan serializerar då denna har det uppdaterade värdet
                string[] tempArray = new string[item.SubItems.Count];
                for (int i = 0; i < tempArray.Length; i++)
                { 
                    tempArray[i] = item.SubItems[i].Text; 
                }
               serializer.EveryRow(serializer.SerializeListView(tempArray), path);
        

            }
        }

        public void BtnRemovePod(ListView podcasts, ListBox lbAvsnitt)
        {
            var validation = new Validation();
            var serialize = new Serializer();
            var filesystem = new FileSystem();
            
            if (podcasts.SelectedItems.Count == 1)
            {
                //vill du radera ja/nej
                var bekrafta = MessageBox.Show("Är du säker på att du vill radera den här podcasten?", "Radera pocast", MessageBoxButtons.YesNo);
                //om ja
                if (bekrafta == DialogResult.Yes)
                {
                    //ta bort selecteditem, cleara filen och serializera den på nytt utan de gamla itemet
                    podcasts.Items.Remove(podcasts.SelectedItems[0]);
                    filesystem.ClearFile(serialize.FeedFile);
                    serialize.Serialize(podcasts, serialize.FeedFile);
                    lbAvsnitt.Items.Clear();
                }

            }
        }

        public void SelectedIndex(ComboBox comboFrekvens, ComboBox comboCategory)
        {
            //så de inte är tomma vid startup
            comboFrekvens.SelectedIndex = 0;
            comboCategory.SelectedIndex = 0;
        }       


        
      
    }
}
