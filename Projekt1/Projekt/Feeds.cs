using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Projekt
{
    class VirtualRandom
    {
        public virtual string RandomVirtual() {
            return "Im a virtual string which is not overridden or used in Feeds";
        }

    }
    class Feeds : VirtualRandom

    {
        public void Description(string url, SyndicationFeed syndicationFeed, ListView podcast, ListBox lbAvsnitt, TextBox txtBoxDescription)
        {
            //ta url från selecteditem, loada urlen i ett xmldocument
            url = podcast.SelectedItems[0].SubItems[4].Text;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(url);
            //gå ner till description taggen
            XmlNodeList description = xmlDocument.SelectNodes("//rss/channel/item/description");

            var i = lbAvsnitt.SelectedIndex;
            txtBoxDescription.Clear();
            //skriv ut description taggen baserat på selectedindex i avsnitt, regex pga <p> kom med först
            txtBoxDescription.Text = (Regex.Replace(description[i].InnerText, @"<.*?>", ""));
        }

        public async Task BtnNewPod(string url, ComboBox comboFrekvens, ComboBox comboCategory, ListView podcast, ListBox lbAvsnitt, TextBox txtBoxURL)
        {
            
            try
            {
                var serializer = new Serializer();
                var validation = new Validation();
                if (validation.tfInteTomt(txtBoxURL))
                {
                    lbAvsnitt.Items.Clear();
                    var syndicationFeed = LoadFeed(CreateXmlReader(url));
                    int i = Count(syndicationFeed);

                    //subitems varje listviewitem har, vissa som url osv är osynliga i listviewen men kan ändå hemtas
                    string[] row = { i.ToString(), syndicationFeed.Title.Text, comboFrekvens.SelectedItem.ToString(),
                      comboCategory.SelectedItem.ToString(), url, DateTime.Now.ToString() };


                    await Task.Delay(50);
                    //adda row samt serializera den
                    AddContent(podcast, AddContent(row));
                    serializer.EveryRow(serializer.SerializeListView(row), serializer.FeedFile);
                    CreateXmlReader(url).Close();
                    txtBoxURL.Clear();
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Vänligen skriv in en giltig rss fil");
                txtBoxURL.Clear();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Vänligen välj en giltig kategori");

            }
        }
                        
        public void IndexChangedPodcast(ListView podcasts, ListBox lbAvsnitt, string url, SyndicationFeed syndicationFeed)
        {
            lbAvsnitt.Items.Clear();
            url = podcasts.SelectedItems[0].SubItems[4].Text;
            syndicationFeed = LoadFeed(CreateXmlReader(url));
            foreach (SyndicationItem item in syndicationFeed.Items)
            {
                string title = item.Title.Text;
                lbAvsnitt.Items.Add(title);

            }
            CreateXmlReader(url).Close();

        }

        public int Count(SyndicationFeed syndicationFeed)
        {
            return syndicationFeed.Items.Count();
        }

        public SyndicationFeed LoadFeed(XmlReader xmlreader)
        {
            return SyndicationFeed.Load(xmlreader);
        }
        public XmlReader CreateXmlReader(string url)
        {
            return XmlReader.Create(url);
        }
        public ListViewItem AddContent(string[] myRow)
        {
            return new ListViewItem(myRow);
        }

        public void AddContent(ListView listView, ListViewItem listviewitem)
        {
            listView.Items.Add(listviewitem);
        }
    }
}
