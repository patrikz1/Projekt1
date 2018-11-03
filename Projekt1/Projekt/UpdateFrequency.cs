using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt
{
    class UpdateFrequency
    {
        public void AddFrequency(ComboBox frequency)
        {
            frequency.Items.Add("Ten minutes");
            frequency.Items.Add("One hour");
            frequency.Items.Add("One day");
            frequency.Items.Add("One week");
            frequency.Items.Add("One month");
        }

        public List<ListViewItem> List(ListView listview)
        {
            List<ListViewItem> allItems = new List<ListViewItem>();
            foreach (ListViewItem lvitem in listview.Items)
            {
                allItems.Add(lvitem);
            }
            return allItems;
        }
        public Dictionary<int, bool> Updates(List<ListViewItem> list)
        {
            Dictionary<int, bool> updates = new Dictionary<int, bool>();
            foreach (ListViewItem lvitem in list)
            {
                updates.Add(lvitem.Index, Frequency(lvitem));
            }
            return updates;
        }
        public static bool Frequency(ListViewItem lvitem)
        {

            switch (lvitem.SubItems[2].Text)
            {
                case "Ten minutes":
                    return DateTime.Parse(lvitem.SubItems[5].Text).AddMinutes(10) > DateTime.Now;
                case "One Hour":
                    return DateTime.Parse(lvitem.SubItems[5].Text).AddHours(1) > DateTime.Now;
                case "One day":
                    return DateTime.Parse(lvitem.SubItems[5].Text).AddDays(1) > DateTime.Now;
                case "One week":
                    return DateTime.Parse(lvitem.SubItems[5].Text).AddDays(7) > DateTime.Now;
                case "One month":
                    return DateTime.Parse(lvitem.SubItems[5].Text).AddMonths(1) > DateTime.Now;
                default:
                    return true;
            }
        }
    }
}
