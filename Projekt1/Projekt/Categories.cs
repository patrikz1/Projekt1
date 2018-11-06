using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt
{
    class Categories
    {
        public string[] MyCategories =
        {
            "Comedy",
            "Space",
            "Romance",
            "Crime"
        };
        public void SerializeCategories()
        {
            var serializer = new Serializer();
            if (!File.Exists(serializer.categoryFile))
            {
                foreach (var item in MyCategories)
                {
                    serializer.EveryRow(item, serializer.categoryFile);
                }
            }
        }
        public void AddCategoriesCombo(ComboBox comboCategories, string[] allCategories)
        {
            var serializer = new Serializer();
            //lägger till varje kategori i vår fil till comboboxen också (string[] allCategories) refereras till ReadAllCategories i form1
            foreach (var item in allCategories)
            {
                comboCategories.Items.Add(item);
            }
        }
        public string[] ReadAllCategories()
        {
            //deserializar vår categoryfile
            var serializer = new Serializer();
            return serializer.DeSerializer(serializer.categoryFile);

        }

        public void AddCategories(ListView lvCategories, ComboBox comboCategory, string newCategory)
        {
            var serializer = new Serializer();
            var validation = new Validation();
            //om newcategory (en sträng som är vad vi matar in i txtboxCategories) är lika med något av det som redan ligger i vår deserializade fil
            //som vi returnar på ReadCategories() så skriver du ut att den redan finns
            if (validation.IsDuplicate(newCategory, ReadAllCategories()))
            {
                MessageBox.Show(newCategory + " finns redan i listan");
            }
            else
            {
                //annars adda newCategory till både listan och comboboxen samt serialisera
                lvCategories.Items.Add(newCategory);
                comboCategory.Items.Add(newCategory);
                serializer.EveryRow(newCategory, serializer.categoryFile);
            }
        }

        public void btnSaveCategory(ListView lvCategory, string txtBoxCategories, ListView lvPodcast)
        {
            var filesystem = new FileSystem();
            var serializer = new Serializer();

            var currentCategory = lvCategory.SelectedItems[0].SubItems[0].Text;
            var newCategory = txtBoxCategories;
            var everyGenre = ReadAllCategories();
            var path = serializer.categoryFile;
            var categoryFile = serializer.DeSerialize(serializer.DeSerializer(path));
            var podcastFile = serializer.DeSerialize(serializer.DeSerializer(serializer.FeedFile));

            //cleara
            filesystem.ClearFile(serializer.FeedFile);
            foreach (ListViewItem item in podcastFile)
            {
                var tempArray = new string[item.SubItems.Count];
                //om någons listviewitems kategori tab = selected item
                if (item.SubItems[3].Text.Equals(currentCategory))
                {
                    //så ska du sätta den kategori taben till vad som är inmatat i txtboxen(de nya namnet på den gamla kategorin)
                    item.SubItems[3].Text = newCategory;
                }
                //kör igenom den temporära arrayen
                for (int i = 0; i < tempArray.Length; i++)
                {
                    tempArray[i] = item.SubItems[i].Text;
                }
                //serializera tempArray eftersom den kmr vara uppdaterad med rätt nytt namn för just den kategorin som man ändrat
                serializer.EveryRow(serializer.SerializeListView(tempArray), serializer.FeedFile);

            }
            filesystem.ClearFile(path);
            foreach (var item in categoryFile)
            {
                var tempArray = new string[item.SubItems.Count];
                if (item.SubItems[0].Text.Equals(currentCategory))
                {
                    item.SubItems[0].Text = newCategory;
                }
                for (int i = 0; i < tempArray.Length; i++)
                {
                    tempArray[i] = item.SubItems[i].Text;
                }
                lvCategory.SelectedItems[0].SubItems[0].Text = txtBoxCategories;
                serializer.EveryRow(serializer.SerializeListView(tempArray), path);

            }

        }
        public void BtnRemoveCategory(ListView categories, ComboBox comboCategory)
        {
            var serializer = new Serializer();
            var filesystem = new FileSystem();
            foreach (ListViewItem item in categories.Items)
            {
                if (item.Selected)
                {
                    string category = item.Text;
                    //om comboboxen innehåller selected item
                    if (comboCategory.Items.Contains(category))
                    {
                        //ta bort det från combo
                        comboCategory.Items.Remove(category);
                    }
                    //och från listviewen
                    categories.Items.Remove(item);
                    //cleara och serializera nya
                    filesystem.ClearFile(serializer.categoryFile);
                    serializer.Serialize(categories, serializer.categoryFile);
                }
            }

        }
        public void SelectedCategory(ListView podcast, ListView lvCategory)
        {
            var filesystem = new FileSystem();
            var serializer = new Serializer();
            var feeds = new Feeds();

            string feedFile = "FeedItems.txt";
            var currentCategory = lvCategory.SelectedItems[0].SubItems[0].Text;

            List<ListViewItem> allFeeds = serializer.DeSerialize(serializer.DeSerializer(feedFile));
            podcast.Items.Clear();
            foreach (ListViewItem item in allFeeds)
            {
                //om något listviewitem har likadan kategori som är selected
                if (item.SubItems[3].Text.Equals(currentCategory))
                {
                    //cleara och lägg till listviewitemet med selected kategori
                    feeds.AddContent(podcast, item);
                }

            }

        }

    }
}
