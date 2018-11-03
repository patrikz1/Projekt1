using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt
{
    class Categories
    {
        public void Categoriess(ListView listview, ComboBox cbox)
        {
            listview.Items.Add("All Categories");
            listview.Items.Add("Comedy");
            listview.Items.Add("Space");
            listview.Items.Add("Crime");
            listview.Items.Add("Romance");

            cbox.Items.Add("All Categories");
            cbox.Items.Add("Comedy");
            cbox.Items.Add("Space");
            cbox.Items.Add("Crime");
            cbox.Items.Add("Romance");
        }

        public void AddCategories(ListView lvCategories, ComboBox comboCategory, string newCategory, TextBox txtBoxCategories)
        {
            lvCategories.Items.Add(newCategory);
            comboCategory.Items.Add(newCategory);
            txtBoxCategories.Clear();
        }

        public void BtnRemoveCategory(ListView categories, ComboBox comboCategory)
        {
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
                    }
                }
            
        }
    }
}
