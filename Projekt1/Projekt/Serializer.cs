using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Projekt
{
  
       public class Serializer
        {

        public Serializer()
        {
            
        }
        public void EveryRow(string addedContent)
        {
            //varje podcast divide med |
            string filepath = "Serializer.txt";
            using (var stream = new FileStream(filepath, FileMode.Append, FileAccess.Write))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(addedContent + "|");
                }
            }
        }
        public string SerializeListView(string[] myRow)
        {
            //varje item i string[] divide med $
            return string.Join("$", myRow);
        }

        public void Serialize(ListView lview)
        {
            int counter = lview.Items.Count;
            ListViewItem[] feeds = new ListViewItem[counter];
            lview.Items.CopyTo(feeds, 0);
            foreach(var item in feeds)
            {
                var lista = new string[item.SubItems.Count];
                for(int i = 0; i<lista.Length; i++)
                {
                    lista[i] = item.SubItems[i].Text;
                }
                EveryRow(SerializeListView(lista));
            }
        }


        public string[] DeSerializer(string filepath)
        {
            using(var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                using(var reader = new StreamReader(stream))
                {
                    var allfeeds = reader.ReadToEnd();
                    return allfeeds.Split(new char[] {'|' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
        }
        public List<ListViewItem> DeSerialize(string[] allRows)
        {
            var ListStringArray = new List<string[]>();
            var ListLvItems = new List<ListViewItem>();
            foreach(var item in allRows)
            {
                ListStringArray.Add(item.Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries));
            }
            foreach(var item in ListStringArray)
            {
                ListLvItems.Add(new ListViewItem(item));
            }
            return ListLvItems;
        }
        public void AddAllFeeds(ListView listview, List<ListViewItem> content)
        {
            foreach(var item in content)
            {
                listview.Items.Add(item);
            }
        }
       }
}


