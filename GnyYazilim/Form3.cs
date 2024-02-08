using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using System.IO;
using System.Timers;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Net.Sockets;
using System.Net;
using NModbus;
using NModbus.Data;
using NModbus.Device;
using NModbus.Utility;
using NModbus.Extensions;
using NModbus.IO;



namespace GnyYazilim
{
    public partial class Form3 : Form
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public Form3()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            string connectionString = @"Data Source=LAPTOP-6AUCR7C4\SQLEXPRESS;Initial Catalog=GnyYazilimDB;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
        }

        public string tcno, kullaniciadi;
        public string isEmriNo, parcaNo, parcaAdi, kabin;
        bool durum = false;
        public bool Istrue = false;
        public bool isAdmin;
        public int astarSicaklik,boyamaSicaklik,kurutmaSicaklik,astarNem,boyamaNem,kurutmaNem;
        public string secilenDeger;
        public bool isTimerStop1 = false;
        public bool isTimerStop2 = false;
        public bool isTimerStop3 = false;
        public bool isSelected1 = false;
        public bool isSelected2 = false;
        public bool isSelected3 = false;
        public bool isForm1 = false;
        public string isEmriNoAstar;
        public string isEmriNoBoyama;
        public string isEmriNoKurutma;
        public int milliseconds;
        public string selectedTime;
        
        private void Form3_Load(object sender, EventArgs e)
        {
            connection.Close();
            if (isTimerStop1 || !isSelected1)
            {
                timer1.Stop();
            }
            if (isTimerStop2 || !isSelected2)
            {
                timer2.Stop();
            }
            if (isTimerStop3 || !isSelected3)
            {
                timer3.Stop();
            }
            if (isForm1 || !(isTimerStop1 || isTimerStop2 || isTimerStop3))
            {
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();
            }

            textBox1.CharacterCasing = CharacterCasing.Upper;
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            comboBox1.Items.Add("ASTAR KABİNİ"); 
            comboBox1.Items.Add("BOYAMA KABİNİ"); 
            comboBox1.Items.Add("KURUTMA KABİNİ");
           
            comboBox2.Items.Add("30 SANİYE"); 
            comboBox2.Items.Add("1 DAKİKA"); 
            comboBox2.Items.Add("3 DAKİKA"); 
            comboBox2.Items.Add("5 DAKİKA");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Close();
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;

            if (comboBox1.Text == "ASTAR KABİNİ")
            {
                isSelected1 = true;
            }
            else if (comboBox1.Text == "BOYAMA KABİNİ")
            {
                isSelected2 = true;
            }
            else if (comboBox1.Text == "KURUTMA KABİNİ")
            {
                isSelected3 = true;
            }
            Form6 frm6 = new Form6(); 
            frm6.StartPosition = FormStartPosition.CenterScreen;
            Form1 frm1 = new Form1();
            frm6.isAdmin = isAdmin;
            frm6.isTimerStop1 = isTimerStop1;
            frm6.isTimerStop2 = isTimerStop2;
            frm6.isTimerStop3 = isTimerStop3;
            frm6.isSelected1 = isSelected1;
            frm6.isSelected2 = isSelected2;
            frm6.isSelected3 = isSelected3;
            frm6.selectedTime = selectedTime;
            frm6.milliseconds = milliseconds;
            frm6.tcno = tcno;
            frm6.kullaniciadi = kullaniciadi;
            frm6.secilenDeger = comboBox1.Text != "" ? comboBox1.SelectedItem.ToString():" ";
            frm6.selectedTime = comboBox2.Text != "" ? comboBox2.SelectedItem.ToString():" ";
            frm6.isEmriNoAstar = isEmriNoAstar;
            frm6.isEmriNoBoyama = isEmriNoBoyama;
            frm6.isEmriNoKurutma = isEmriNoKurutma;
            frm6.Show();
            this.Close();
        }

        private void Form3_temizle()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
        }

        public void button2_Click(object sender, EventArgs e)
        {
            bool kayitkontrol = false;

            connection.Open();
            SqlCommand selectsorgu = new SqlCommand("SELECT * FROM Kaydedilen_Isler WHERE IsEmriNo = '" + textBox1.Text + "'", connection);
            SqlDataReader kayitokuma = selectsorgu.ExecuteReader();

            if (comboBox1.Text == "ASTAR KABİNİ")
            {
                isEmriNoAstar = textBox1.Text;
            }
            else if (comboBox1.Text == "BOYAMA KABİNİ")
            {
                isEmriNoBoyama = textBox1.Text;
            }
            else if (comboBox1.Text == "KURUTMA KABİNİ")
            {
                isEmriNoKurutma = textBox1.Text;
            }

            isEmriNo = textBox1.Text;
            parcaNo = textBox2.Text;
            parcaAdi = textBox3.Text;
            kabin = comboBox1.Text;

            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            connection.Close();

            if (kayitkontrol == false)
            {
                if (textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;

                if (textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;

                if (textBox3.Text == "")
                    label4.ForeColor = Color.Red;
                else
                    label4.ForeColor = Color.Black;

                if (comboBox1.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;

                if (comboBox2.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor= Color.Black;
                
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "")
                {
                    try
                    {
                        connection.Open();

                        string insertQuery = "INSERT INTO Kaydedilen_Isler (IsEmriNo,ParcaNo,ParcaAdi,Kabin,TcNo,Status) VALUES (@isEmriNo,@parcaNo,@parcaAdi,@kabin,@tcno,1)";
                        command.Parameters.AddWithValue("@isEmriNo", isEmriNo);
                        command.Parameters.AddWithValue("@parcaNo", parcaNo);
                        command.Parameters.AddWithValue("@parcaAdi", parcaAdi);
                        command.Parameters.AddWithValue("@kabin", kabin);
                        command.Parameters.AddWithValue("@tcno", tcno);
                        command.CommandText = insertQuery;
                        command.Connection = connection;
                        command.ExecuteNonQuery();

                        connection.Close();

                        MessageBox.Show("İş Kaydı Oluşturuldu!", "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        connection.Close();
                    }
                    finally
                    {

                    }
                }
                else
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz!",
                        "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Girilen İş Emri Numarası önceden kayıtlıdır!", "GNY Kayıt Programı",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
         
        private void kayit_ekle(string isEmriNo)
        {
            //isEmriNo = textBox1.Text;
            parcaNo = textBox2.Text;
            parcaAdi = textBox3.Text;
            kabin = comboBox1.Text;
            DateTime tarih = DateTime.Now;
            string saat = DateTime.Now.Hour.ToString();
            string dakika = DateTime.Now.Minute.ToString();
            string saniye = DateTime.Now.Second.ToString();
            connection.Close();
            connection.Open();
            command.Parameters.Clear();

            if (comboBox1.Text == "ASTAR KABİNİ")
            {
                isSelected1 = true;
            }
            else if (comboBox1.Text == "BOYAMA KABİNİ")
            {
                isSelected2 = true;
            }
            else if (comboBox1.Text == "KURUTMA KABİNİ")
            {
                isSelected3 = true;
            }

            string kayitekleme = "INSERT INTO Tamamlanan_Isler (KullaniciAdi,IsEmriNo,ParcaNo,ParcaAdi,Kabin,BaslangicTarih,BitisTarih,BaslangicSaat,BitisSaat,Sicaklik,Nem,TcNo) VALUES (@kullaniciAdi,@isEmriNo,@parcaNo,@parcaAdi,@kabin,@baslangicTarih,@bitisTarih,@baslangicSaat,@bitisSaat,@sicaklik,@nem,@tcno)";
            command.Parameters.AddWithValue("@kullaniciAdi", kullaniciadi);
            command.Parameters.AddWithValue("@isEmriNo", isEmriNo);
            command.Parameters.AddWithValue("@parcaNo", parcaNo);
            command.Parameters.AddWithValue("@parcaAdi", parcaAdi);
            command.Parameters.AddWithValue("@kabin", kabin);
            command.Parameters.AddWithValue("@baslangicTarih", tarih.Date);
            command.Parameters.AddWithValue("@bitisTarih", tarih.Date);
            command.Parameters.AddWithValue("@baslangicSaat", (saat + ":" + dakika + ":" + saniye));
            command.Parameters.AddWithValue("@bitisSaat", (saat + ":" + dakika + ":" + saniye));

            if (comboBox1.Text == "ASTAR KABİNİ")
            {
                command.Parameters.AddWithValue("@sicaklik", astarSicaklik);
                command.Parameters.AddWithValue("@nem", astarNem);
            }
            else if (comboBox1.Text == "BOYAMA KABİNİ") 
            {
                command.Parameters.AddWithValue("@sicaklik", boyamaSicaklik);
                command.Parameters.AddWithValue("@nem", boyamaNem);
            }
            else if (comboBox1.Text == "KURUTMA KABİNİ")
            {
                command.Parameters.AddWithValue("@sicaklik", kurutmaSicaklik);
                command.Parameters.AddWithValue("@nem", kurutmaNem);
            }
           
            command.Parameters.AddWithValue("@tcno", tcno);

            try
            {
                command.CommandText = kayitekleme;
                command.Connection = connection;
                command.ExecuteNonQuery();
                connection.Close();
                command.Parameters.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void baglanti_olustur()
        {
            try
            {
                // Modbus TCP/IP bağlantısı oluşturma
                var ipAddress = "127.0.0.1"; // Modbus sunucunun(server) ip adresi
                var port = 502; // Modbus TCP/IP bağlantı noktası
                var client = new TcpClient(ipAddress, port);
                var factory = new ModbusFactory();

                IModbusMaster master = factory.CreateMaster(client);

                // Register'ı okuma işlemi
                byte slaveId = 1; // Modbus sunucu slave Id'si (varsayılan olarak 1)
                ushort startAddress = 0; // Okuma başlangıç adresi 
                ushort numberOfPoints = 5; // Okunacak register sayısı

                // Register'ı okuma işlemi
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);

                astarSicaklik = registers[0];
                astarNem = registers[1];
                boyamaSicaklik = registers[2];
                boyamaNem = registers[3];
                kurutmaSicaklik = registers[4];
                kurutmaNem = 0;

                // Bağlantıyı kapatma 
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" HATA " + ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            baglanti_olustur();
            kayit_ekle(isEmriNoAstar);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            baglanti_olustur(); 
            kayit_ekle(isEmriNoBoyama);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            baglanti_olustur();
            kayit_ekle(isEmriNoKurutma);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;


            // Seçilen süreyi al
            selectedTime = comboBox2.SelectedItem.ToString();

            // Seçilen süreyi milisaniyeye çevir
            switch (selectedTime)
            {
                case "30 SANİYE":
                    milliseconds = 30000;
                    break;
                case "1 DAKİKA":
                    milliseconds = 60000;
                    break;
                case "3 DAKİKA":
                    milliseconds = 180000;
                    break;
                case "5 DAKİKA":
                    milliseconds = 300000;
                    break;
                default:
                    // Varsayılan olarak 30 saniye ayarla
                    //milliseconds = 30000;
                    break;
            }
            
            secilenDeger = comboBox1.SelectedItem.ToString();

            if (secilenDeger == "ASTAR KABİNİ")
            {
                timer1.Interval = milliseconds;
                timer1.Start();
                MessageBox.Show("Kayıt Başladı!", "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (secilenDeger == "BOYAMA KABİNİ" )
            { 
                timer2.Interval = milliseconds;
                timer2.Start();
                MessageBox.Show("Kayıt Başladı!", "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                timer3.Interval = milliseconds;
                timer3.Start();
                MessageBox.Show("Kayıt Başladı!", "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /*
        private void button4_Click(object sender, EventArgs e)
        {
            // timer1.Stop();
            // MessageBox.Show("Kayıt Durdu!", "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
        }
        */

        private void button4_Click_2(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.StartPosition = FormStartPosition.CenterScreen;
            frm1.Show();
            this.Hide();
        }
    }
}
