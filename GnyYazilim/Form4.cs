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
using System.Text.RegularExpressions;
using System.IO;

namespace GnyYazilim
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-6AUCR7C4\SQLEXPRESS;Initial Catalog=GnyYazilimDB;Integrated Security=True");
        public bool isAdmin;
        private void kullanicilari_goster()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter kullanicalari_listele = new SqlDataAdapter
                    ("SELECT TcNo AS[TC KİMLİK NO], Ad AS[AD], Soyad AS[SOYAD], Yetki AS[YETKİ], KullaniciAdi AS[KULLANICI ADI], Sifre AS[ŞİFRE] FROM Yoneticiler", baglanti);

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

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 20;
            textBox4.CharacterCasing = CharacterCasing.Upper;
            toolTip1.SetToolTip(this.textBox1, "TC Kimlik No 11 Karakter Olmalı!");
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            kullanicilari_goster();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 11)
            {
                errorProvider1.SetError(textBox1, "TC Kimlik No 11 karakter olmalı!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            /*
            if (textBox4.Text.Length != 5)
            {
                errorProvider1.SetError(textBox4, "Kullanıcı adı en az 5 karakter olmalı!");
            }
            else
            {
                errorProvider1.Clear();
            }
            */
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        int parola_skoru = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox5.Text;

            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');

            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Paroladaki Türkçe karakterler İngilizce karakterlere dönüştürülmüştür!");
            }
            // 1 küçük harf 10 puan, 2 ve üzeri 20 puan
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a - z]", "").Length;
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;

            // 1 büyük harf 10 puan, 2 ve üzeri 20 puan
            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A - Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;

            // 1 rakam 10 puan, 2 ve üzeri 20 puan
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0 - 9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            // 1 sembol 10 puan, 2 ve üzeri 20 puan
            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;

            if (sifre.Length == 9)
                parola_skoru += 10;
            else if (sifre.Length == 10)
                parola_skoru += 20;

            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
                label9.Text = "Büyük, küçük harf, rakam ve sembol kullanmalısın!";

            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
                label9.Text = "";

            if (parola_skoru < 20)
                parola_seviyesi = "Kabul edilemez!";
            else if (parola_skoru >= 20 && parola_skoru < 60)
                parola_seviyesi = "Güçlü";
            else if (parola_skoru >= 60)
                parola_seviyesi = "Çok Güçlü";

            label10.Text = "%" + Convert.ToString(parola_skoru);
            label11.Text = parola_seviyesi;
            progressBar1.Value = parola_skoru;

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != textBox5.Text)
            {
                errorProvider1.SetError(textBox6, "Parola tekrarı eşleşmiyor!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void Form4_temizle()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
            textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string yetki = "";
            bool kayitkontrol = false;

            baglanti.Open();
            SqlCommand selectsorgu = new SqlCommand("SELECT * FROM Yoneticiler WHERE TcNo='" + textBox1.Text + "'", baglanti);
            SqlDataReader kayitokuma = selectsorgu.ExecuteReader();

            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglanti.Close();

            if (kayitkontrol == false)
            {
                //TC Kimlik No kontrolü
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;

                //Adı veri kontrolü
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;

                //Soyadı veri kontrolü
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;

                //Kullanıcı adı veri kontrolü
                /* if (textBox4.Text.Length != 5 ||  textBox4.Text == "")
                     label5.ForeColor = Color.Red;
                 else 
                     label5.ForeColor = Color.Black;
                */

                //Parola veri kontrolü
                if (textBox5.Text == "" || parola_skoru < 20)
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;

                //Parola tekrar veri kontrolü
                if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;

                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != ""
                    && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1
                    && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != ""
                    && textBox5.Text == textBox6.Text && parola_skoru >= 20)
                {
                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";

                    else if (radioButton2.Checked == true)
                        yetki = "Kullanıcı";

                    try
                    {
                        baglanti.Open();
                        SqlCommand eklekomutu = new SqlCommand("INSERT INTO Yoneticiler VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + yetki + "','" + textBox4.Text + "','" + textBox5.Text + "','" + 1 + "')", baglanti);
                        eklekomutu.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Yeni kullanıcı kaydı oluşturuldu!", "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Form4_temizle();
                        kullanicilari_goster();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        baglanti.Close();
                    }
                    finally
                    {

                    }
                }
                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz!",
                        "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarası önceden kayıtlıdır!", "GNY Kayıt Programı",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
