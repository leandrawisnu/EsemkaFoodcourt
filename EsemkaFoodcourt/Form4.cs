using EsemkaFoodcourt.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsemkaFoodcourt
{
    public partial class Form4 : Form
    {
        List<MenuDTO> menuDTOs = new List<MenuDTO>();
        Reservations reservations = new Reservations()
        {
            ReservationDate = DateTime.Now,
        };

        EsemkaFoodcourtEntities db = new EsemkaFoodcourtEntities();
        int[] availableTable;
        Users users = new Users();
        public Form4(int[] availableTable, Users users)
        {
            this.users = users;
            this.availableTable = availableTable;
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            refresh();

            BindingUserForm.DataSource = users;

            foreach (var table in availableTable)
            {
                comboBox1.Items.Add($"Table {table}");
            }
            comboBox1.SelectedIndex = 0;

            numericUpDown1.Minimum = 1; 
            numericUpDown1.Maximum = 4;

            numericUpDown2.Minimum = 1;

            comboBox2.DataSource = db.Menus.Select(f => f.Name).ToArray();

            if (textBox4.Text.Count() < 10 || textBox4.Text.Count() > 15)
            {
                MessageBox.Show("Invalid Phone Number!");
            }

            if (checkBox1.Checked == true)
            {
                groupBox2.Enabled = false;
            } else
            {
                groupBox2.Enabled = true;
            }
        }

        private void refresh()
        {
            BindingUserForm.DataSource = users;
            if (menuDTOs != null)
            {
                dataGridView1.DataSource = menuDTOs.ToList();
            }

            var menuTotal = menuDTOs.Select(f => f.Subtotal).ToArray().Sum();
            label14.Text = 50000.ToString("C", CultureInfo.GetCultureInfo("id-ID"));
            label15.Text = menuTotal.ToString("C", CultureInfo.GetCultureInfo("id-ID"));
            label16.Text = (menuTotal + 50000).ToString("C", CultureInfo.GetCultureInfo("id-ID"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var menu = menuDTOs.FirstOrDefault(f => f.Menu == comboBox2.SelectedItem.ToString());

            if (menu == null)
            {
                var data = db.Menus.FirstOrDefault(f => f.Name == comboBox2.SelectedItem.ToString());
                if (data != null)
                {
                    var add = new MenuDTO()
                    {
                        Menu = data.Name,
                        Qty = 1,
                        Price = data.Price
                    };
                    add.Subtotal = add.Price * add.Qty;

                    menuDTOs.Add(add);
                    refresh();
                } 
            } else
            {
                menu.Qty++;
                menu.Subtotal = menu.Subtotal * menu.Qty;
                refresh();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is MenuDTO menuDTO)
            {
                if (e.ColumnIndex == Action.Index)
                {
                    menuDTOs.Remove(menuDTO);
                    refresh();
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                reservations.UserID = users.ID;
            } else
            {
                int? id = db.Users.FirstOrDefault(f => f.Email == textBox1.Text).ID;
                if (id != null)
                {
                    reservations.UserID = id.Value;
                } else
                {
                    MessageBox.Show("User not found");
                }
            }
            reservations.TableID = db.Tables.First(f => f.Name == comboBox1.SelectedItem.ToString()).ID;
            reservations.NumberOfPeople = (int)numericUpDown1.Value;

            db.Reservations.Add(reservations);

            foreach (var food in menuDTOs)
            {
                var thisFood = db.Menus.First(f => f.Name == food.Menu).ID;

                ReservationDetails details = new ReservationDetails()
                {
                    ReservationID = reservations.ID,
                    MenuID = thisFood,
                    Qty = food.Qty,
                };
            }
        }
    }
}
