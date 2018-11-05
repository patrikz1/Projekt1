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
