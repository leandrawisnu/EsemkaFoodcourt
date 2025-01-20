using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsemkaFoodcourt
{
    public partial class Form4 : Form
    {
        EsemkaFoodcourtEntities db = new EsemkaFoodcourtEntities();
        int[] availableTable;
        public Form4(int[] availableTable)
        {
            this.availableTable = availableTable;
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            foreach (var table in availableTable)
            {
                comboBox1.Items.Add($"Table {table}");
            }
            comboBox1.SelectedIndex = 0;

            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 4;
        }
    }
}
