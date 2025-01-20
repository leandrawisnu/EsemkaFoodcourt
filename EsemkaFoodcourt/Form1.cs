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
    public partial class Form1 : Form
    {
        EsemkaFoodcourtEntities db = new EsemkaFoodcourtEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = db.Users.First();
            if (data != null)
            {
                var Form3 = new Form3(data);
                Form3.Show();
                this.Hide();
                if (data.Password == textBox2.Text)
                {
                    MessageBox.Show("Login Berhasil");
                    if (data.RoleID == 1)
                    {
                        var Form2 = new Form2();
                        Form2.Show();
                        this.Hide();
                    }
                    else
                    { 

                    }
                }
                else
                {
                    MessageBox.Show("Password Salah");
                }
            }
            else
            {
                MessageBox.Show("Email Tidak Terdaftar");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
