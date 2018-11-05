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
            "All Categories",
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

            foreach (var item in allCategories)
            {
                comboCategories.Items.Add(item);
            }
        }
        public string[] ReadAllCategories()
        {
            var serializer = new Serializer();
            return serializer.DeSerializer(serializer.categoryFile);

        }

        public void AddCategories(ListView lvCategories, ComboBox comboCategory, string newCategory)
        {
            var serializer = new Serializer();
            var validation = new Validation();
            if (validation.IsDuplicate(newCategory, ReadAllCategories()))
            {
                MessageBox.Show(newCategory + " finns redan i listan");
            }
            else
            {             
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
            var podcastFile = serializer.DeSerialize(serializer.DeSerializer(serializer.listFile));

            filesystem.ClearFile(serializer.listFile);
            foreach (ListViewItem item in podcastFile)
            {
                var tempArray = new string[item.SubItems.Count];
                if (item.SubItems[3].Text.Equals(currentCategory))
                {
                    item.SubItems[3].Text = newCategory;
                }
                for (int i = 0; i < tempArray.Length; i++)
                {
                    tempArray[i] = item.SubItems[i].Text;
                }
                serializer.EveryRow(serializer.SerializeListView(tempArray), serializer.listFile);

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
                        if (comboCategory.Items.Contains(category))
                        {
                            comboCategory.Items.Remove(category);
                        }
                        categories.Items.Remove(item);
                    filesystem.ClearFile(serializer.categoryFile);
                    serializer.Serialize(categories, serializer.categoryFile);
                    }
                }
            
        }
    }
}
