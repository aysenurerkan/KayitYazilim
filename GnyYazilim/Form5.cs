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
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GnyYazilim
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-6AUCR7C4\SQLEXPRESS;Initial Catalog=GnyYazilimDB;Integrated Security=True");
        public bool isAdmin;
        private void kullanicilari_getir()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter kullanicalari_listele = new SqlDataAdapter
                    ("SELECT TcNo AS[TC KİMLİK NO], Ad AS[AD], Soyad AS[SOYAD], Yetki AS[YETKİ] FROM Yoneticiler WHERE Status=1 ORDER BY Ad", baglanti);

                DataSet dshafiza = new DataSet();
                kullanicalari_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
            finally
            {

            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            kullanicilari_getir();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void Form5_temizle()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand silkomutu = new SqlCommand("UPDATE Yoneticiler set Status=0 WHERE TcNo=@TcNo", baglanti);
            silkomutu.Parameters.AddWithValue("@tcno", Convert.ToString(textBox1.Text));
            baglanti.Open();
            silkomutu.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Seçilen kullanıcı silindi!", "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Form5_temizle();
            kullanicilari_getir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.StartPosition = FormStartPosition.CenterScreen;
            frm2.isAdmin = isAdmin;
            frm2.Show();
            this.Hide();
        }
    }
}
