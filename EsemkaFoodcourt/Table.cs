using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsemkaFoodcourt
{
    public partial class Table : UserControl
    {
        int id;
        int[] reserved;
        public Table(int id, int[] reserved)
        {
            InitializeComponent();
            this.id = id;
            this.reserved = reserved;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void Table_Load(object sender, EventArgs e)
        {
            label1.Text = id.ToString();
            if (reserved != null && reserved.Any(f => f == id))
            {
                pictureBox1.BringToFront();
            }
        }
    }
}
