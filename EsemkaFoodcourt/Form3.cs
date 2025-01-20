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
    public partial class Form3 : Form
    {
        EsemkaFoodcourtEntities db = new EsemkaFoodcourtEntities();
        Users users;
        public Form3(Users users)
        {
            this.users = users;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text += string.Join(" ", users.FirstName, users.LastName);

            int[] reservations = db.Reservations.Select(Select => Select.TableID).ToArray();

            var tables= db.Tables.ToList();

            foreach (var item in tables)
            {
                flowLayoutPanel1.Controls.Add(new Table(item.ID, reservations)
                {

                });
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
