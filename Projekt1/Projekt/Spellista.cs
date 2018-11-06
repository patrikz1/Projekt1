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
            listView.FullRowSelect = true;
        }

        public void HideSelection (ListView podcast, ListView categories)
        {
            podcast.HideSelection = false;
            categories.HideSelection = false;
        }

        public void UpdateFreqNCat(ListView lv)
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
 
                    if (allFeedUrl == currentFeedUrl)

                    { 
                        if (currentFeed.SubItems[2].Text != allFeedItem.SubItems[2].Text || currentFeed.SubItems[3].Text != allFeedItem.SubItems[3].Text)
                        {
                            allFeedItem.SubItems[2].Text = currentFeed.SubItems[2].Text;
                            allFeedItem.SubItems[3].Text = currentFeed.SubItems[3].Text;

                        }
                    }
                }
            }
            filesystem.ClearFile(path);
            foreach (ListViewItem item in allFeeds)
            { 
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
            //ta bort den från listview, cleara vår textfil, serialize'a alla items i vår listview igen
            var validation = new Validation();
            var serialize = new Serializer();
            var filesystem = new FileSystem();
            var selectedItem = podcasts.SelectedItems[0];
            if (podcasts.SelectedItems.Count == 1)
            {
                var bekrafta = MessageBox.Show("Är du säker på att du vill radera den här podcasten?", "Radera pocast", MessageBoxButtons.YesNo);
                if (bekrafta == DialogResult.Yes)
                {

                    podcasts.Items.Remove(selectedItem);
                    filesystem.ClearFile(serialize.FeedFile);
                    serialize.Serialize(podcasts,serialize.FeedFile);
                    lbAvsnitt.Items.Clear();
                }

            }
        }

        public void SelectedIndex(ComboBox comboFrekvens, ComboBox comboCategory)
        {
            comboFrekvens.SelectedIndex = 0;
            comboCategory.SelectedIndex = 0;
        }       


        
      
    }
}
