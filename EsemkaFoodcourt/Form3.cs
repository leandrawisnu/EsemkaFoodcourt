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
        int[] availableTable;

        public Form3(Users users)
        {
            this.users = users;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Edit Welcome Text
            label1.Text += string.Join(" ", users.FirstName, users.LastName);

            // List Table
            var tables = db.Tables.Select(f => f.ID).ToArray();

            // Cari Data untuk hari ini
            var reservationsData = db.Reservations.Where(f => f.ReservationDate == DateTime.Now).ToList();

            // Kalau ada
            if (reservationsData.Any())
            {
                // Buat Array untuk table yang diisi
                int[] reservations = reservationsData.Select(Select => Select.TableID).ToArray();
                foreach (var item in tables)
                {
                    flowLayoutPanel1.Controls.Add(new Table(item, reservations));
                }
                availableTable = tables.Except(reservations).ToArray();
            }
            // Kalau tidak ada
            else if (!reservationsData.Any())
            {
                // Kalau kosong kirim null
                foreach (var item in tables)
                {
                    flowLayoutPanel1.Controls.Add(new Table(item, null));
                }
                availableTable = tables;
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Form4 = new Form4(availableTable, users);
            Form4.Show();
            this.Hide();
        }
    }
}
