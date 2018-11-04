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

        public void UpdateFeedFileCheckFreqAndGen(ListView lv)
        {
            var filesystem = new FileSystem();
            var serializer = new Serializer();

            string path = "Serializer.txt";            
            List<ListViewItem> allFeeds = serializer.DeSerialize(serializer.DeSerializer(path));
            ListViewItem[] newFeeds = new ListViewItem[lv.Items.Count]; // prepare an array of listviewitems that will hold the currently displaying items.
            lv.Items.CopyTo(newFeeds, 0); //get the current, possibly filtered items, with possibly new values for update frequency and/or genre, and copy to the previously declared array.                        
            foreach (ListViewItem allFeedItem in allFeeds)
            {  
                foreach (ListViewItem currentFeed in newFeeds)
                { 
                    string currentFeedUrl = currentFeed.SubItems[4].Text; // urls are in this case a uniqque identifier, so lets get them from ALL FEED ITEMS (!!!)...
                    string allFeedUrl = allFeedItem.SubItems[4].Text; //... total... in both lists     
 
                    if (allFeedUrl == currentFeedUrl)

                    { // if the urls match that means we have two items destined for love...
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
                string[] tempArray = new string[item.SubItems.Count]; //declare a list that will hold one ListView tuple (make it as big as the subitems.count)).
                for (int i = 0; i < tempArray.Length; i++)
                { 
                    tempArray[i] = item.SubItems[i].Text; // (and add each foreach'd item into the array).
                }
                // --> Serializer.Serialize(feedPath, Serializer.SerializeList(tempList)); // finally, she makes sure it sticks (serialize each listview tuple, appending the to the feed file, one at a time).
               serializer.EveryRow(serializer.SerializeListView(tempArray));
        

            }
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
