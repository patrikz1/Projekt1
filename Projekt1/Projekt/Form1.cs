using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Projekt
{
    public partial class PodcastPlayer : Form
    {
        public PodcastPlayer()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            var spellista = new Spellista();
            var categories = new Categories();
            var serializer = new Serializer();
            var updatefrequency = new UpdateFrequency();
            categories.SerializeCategories();
            categories.AddCategoriesCombo(comboKategori, categories.ReadAllCategories());

            serializer.AddAllFeeds(lvPodcasts, serializer.DeSerialize(serializer.DeSerializer(serializer.FeedFile)));
            serializer.AddAllFeeds(lvCategories, serializer.DeSerialize(serializer.DeSerializer(serializer.categoryFile)));



            updatefrequency.AddFrequency(comboFrekvens);
            updatefrequency.Updates(updatefrequency.List(lvPodcasts));

            spellista.FullRowSelect(lvPodcasts);
            spellista.SelectedIndex(comboFrekvens, comboKategori);
            spellista.HideSelection(lvPodcasts, lvCategories);
        }

        private void btnSavePod_Click(object sender, EventArgs e)
        {
            var serializer = new Serializer();
            var spellista = new Spellista();
            if (lvPodcasts.SelectedItems.Count > 0)
            {
                lvPodcasts.SelectedItems[0].SubItems[2].Text = comboFrekvens.SelectedItem.ToString();
                lvPodcasts.SelectedItems[0].SubItems[3].Text = comboKategori.SelectedItem.ToString();
                spellista.UpdateFeedFreqAndGen(lvPodcasts);
                lvPodcasts.Items.Clear();
                serializer.AddAllFeeds(lvPodcasts, serializer.DeSerialize(serializer.DeSerializer("FeedItems.txt")));

            }
        }

        private async void btnNewPod_Click(object sender, EventArgs e)
        {
            var feeds = new Feeds();
            await feeds.BtnNewPod(txtBoxURL.Text, comboFrekvens, comboKategori, lvPodcasts, lbAvsnitt, txtBoxURL);
        }

        private void btnNewCategory_Click(object sender, EventArgs e)
        {
            var categories = new Categories();
            categories.AddCategories(lvCategories, comboKategori, txtBoxCategories.Text.ToString());
            txtBoxCategories.Clear();

        }

        private void listPodcasts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var feeds = new Feeds();
            if (lvPodcasts.SelectedItems.Count > 0)
            {
                feeds.IndexChangedPodcast(lvPodcasts, lbAvsnitt, lvPodcasts.SelectedItems[0].SubItems[4].Text,
                feeds.LoadFeed(feeds.CreateXmlReader(lvPodcasts.SelectedItems[0].SubItems[4].Text)));
            }
        }

        private void btnRemovePod_Click(object sender, EventArgs e)
        {
            var spellista = new Spellista();
            if (lvPodcasts.SelectedItems.Count > 0)
            {
                spellista.BtnRemovePod(lvPodcasts, lbAvsnitt);
            }
        }

        private void btnRemoveCategory_Click(object sender, EventArgs e)
        {
            var categories = new Categories();
            categories.BtnRemoveCategory(lvCategories, comboKategori);
        }

        private void lbAvsnitt_SelectedIndexChanged(object sender, EventArgs e)
        {
            var feeds = new Feeds();
            if (lbAvsnitt.SelectedItems.Count > 0)
            {
                feeds.Description(lvPodcasts.SelectedItems[0].SubItems[4].Text, feeds.LoadFeed(feeds.CreateXmlReader(lvPodcasts.SelectedItems[0].SubItems[4].Text)),
                    lvPodcasts, lbAvsnitt, txtBoxDescription);
            }
        }

        private void txtBoxDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            var spellista = new Spellista();
            var serializer = new Serializer();
            var categories = new Categories();
            var filesystem = new FileSystem();
            if (lvCategories.SelectedItems.Count > 0)
            {
                categories.btnSaveCategory(lvCategories, txtBoxCategories.Text, lvPodcasts);
                lvPodcasts.Items.Clear();
                comboKategori.Items.Clear();
                serializer.AddAllFeeds(lvPodcasts, serializer.DeSerialize(serializer.DeSerializer(serializer.FeedFile)));
                categories.AddCategoriesCombo(comboKategori, categories.ReadAllCategories());
                spellista.SelectedIndex(comboFrekvens, comboKategori);
                txtBoxCategories.Clear();
            }
        }

        private void lvCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            var categories = new Categories();
            if (lvCategories.SelectedItems.Count > 0)
            {
                categories.SelectedCategory(lvPodcasts, lvCategories);
            }
        }
    }
}
