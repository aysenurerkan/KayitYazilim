using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GnyYazilim
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-6AUCR7C4\SQLEXPRESS;Initial Catalog=GnyYazilimDB;Integrated Security=True");

        public bool isAdmin;
        private void button1_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.StartPosition = FormStartPosition.CenterScreen;
            frm4.isAdmin = isAdmin;
            frm4.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5();
            frm5.StartPosition = FormStartPosition.CenterScreen;
            frm5.isAdmin = isAdmin;
            frm5.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            //frm6.button1.Visible = false;
            frm6.button2.Visible = true;
            frm6.StartPosition = FormStartPosition.CenterScreen;
            Form1 frm1 = new Form1();
            frm6.isAdmin = isAdmin;
            frm6.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7();
            frm7.StartPosition = FormStartPosition.CenterScreen;
            frm7.isAdmin = isAdmin;
            frm7.Show();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.StartPosition = FormStartPosition.CenterScreen;
            frm1.Show();
            this.Hide();
        }
    }
}
