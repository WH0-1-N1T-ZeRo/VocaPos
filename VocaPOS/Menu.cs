using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VocaPOS
{
    public static class Menu
    {
        static string name = "";
        public static void Dashboard(Form form)
        {
            new POS().Show();
            form.Close();
        }
        public static void Inventory(Form form)
        {
            Inventory_Item inventory = new Inventory_Item();
            inventory.Text += $" - {name}";
            inventory.Show();
            form.Close();
        }

        public static void POS(Form form)
        {
            POS pos = new POS();
            pos.Text += $" - {name}";
            pos.Show();
            form.Close();
        }
        public static void Account(Form form)
        {
            new POS().Show();
            form.Close();
        }
        public static void Setting(Form form)
        {
            new POS().Show();
            form.Close();
        }
    }
}
