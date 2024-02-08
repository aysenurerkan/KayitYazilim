using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace GnyYazilim
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-6AUCR7C4\SQLEXPRESS;Initial Catalog=GnyYazilimDB;Integrated Security=True");

        public static string tcno, adi, soyadi, yetki;
        public string kullaniciAdi;
        bool durum = false;


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand selectsorgu = new SqlCommand("SELECT * FROM Yoneticiler WHERE Status = 1", baglanti);
                SqlDataReader kayitokuma = selectsorgu.ExecuteReader();

                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitokuma["KullaniciAdi"].ToString() == textBox1.Text && kayitokuma["Sifre"].ToString() == textBox2.Text && kayitokuma["Yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form2 frm2 = new Form2();
                            frm2.StartPosition = FormStartPosition.CenterScreen;
                            frm2.isAdmin = radioButton1.Checked;
                            frm2.Show();
                            break;
                        }
                    }
                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["KullaniciAdi"].ToString() == textBox1.Text && kayitokuma["Sifre"].ToString() == textBox2.Text && kayitokuma["Yetki"].ToString() == "Kullanýcý")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form3 frm3 = new Form3();
                            frm3.tcno = tcno;
                            frm3.isTimerStop1 = false;
                            frm3.isTimerStop2 = false;
                            frm3.isTimerStop3 = false;
                            frm3.isForm1 = true;
                            kullaniciAdi = kayitokuma.GetValue(4).ToString();
                            frm3.kullaniciadi = kullaniciAdi;
                            frm3.StartPosition = FormStartPosition.CenterScreen;
                            frm3.isAdmin = radioButton1.Checked;
                            frm3.Show();
                            break;
                        }
                    }
                }
                if (durum == false)
                {
                    MessageBox.Show("Kullanýcý adý ya da þifre yanlýþ", "GNY Kayýt Programý", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA!" + ex.ToString);
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
            textBox1.CharacterCasing = CharacterCasing.Upper;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}