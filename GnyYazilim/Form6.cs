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
using System.Text.RegularExpressions;
using System.IO;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using NModbus;
using System.Net.Sockets;

namespace GnyYazilim
{
    public partial class Form6 : Form
    {

        private SqlCommand command;
        private SqlConnection connection;
        public Form6()
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

        // private Form3 form3;

        public string tcno;
        public string kullaniciadi;
        public string secilenDeger;
        public bool isAdmin;
        public bool isTimerStop1 = false;
        public bool isTimerStop2 = false;
        public bool isTimerStop3 = false;
        public bool isSelected1 = false;
        public bool isSelected2 = false;
        public bool isSelected3 = false;
        public string? isEmriNoAstar;
        public string? isEmriNoBoyama;
        public string? isEmriNoKurutma;
        public bool isOlderStopAstar = false;
        public bool isOlderStopBoyama = false;
        public bool isOlderStopKurutma = false;
        public int astarSicaklik, boyamaSicaklik, kurutmaSicaklik, astarNem, boyamaNem, kurutmaNem;
        public string parcaNo, parcaAdi, kabin;
        public string selectedTime;
        public int milliseconds;
        public List<string> kontrolList = new List<string>();



        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-6AUCR7C4\SQLEXPRESS;Initial Catalog=GnyYazilimDB;Integrated Security=True");
        SqlCommand selectsorgu;
        SqlDataReader kayitokuma;
        SqlDataAdapter verileri_listele;
        DataSet ds;

        void verileri_getir()
        {
            string sql = "SELECT IsEmriNo AS[İŞ EMRİ NO], ParcaNo AS[PARÇA NO], ParcaAdi AS[PARÇA ADI], Kabin AS[KABİN] FROM Kaydedilen_Isler WHERE Status = 1 ORDER BY IsEmriNo ";
            verileri_listele = new SqlDataAdapter(sql, baglanti);
            ds = new DataSet();
            baglanti.Open();

            verileri_listele.Fill(ds, "Kaydedilen_Isler");
            dataGridView1.DataSource = ds.Tables["Kaydedilen_Isler"];
            baglanti.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
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
            
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            if (isSelected1)
            {
                timer1.Interval = milliseconds;
                timer1.Start();
            }
            if (isSelected2)
            {
                timer2.Interval = milliseconds;
                timer2.Start();
            }
            if (isSelected3)
            {
                timer3.Interval = milliseconds;
                timer3.Start();
            }

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            verileri_getir();
        }

        /* private void button1_Click(object sender, EventArgs e)
         {

         }
        */

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                baglanti.Open();
                SqlCommand silmekomutu = new SqlCommand("UPDATE Kaydedilen_Isler set Status=0 WHERE IsEmriNo = '" + dataGridView1.SelectedRows[i].Cells["İŞ EMRİ NO"].Value.ToString() + "'", baglanti);
                silmekomutu.ExecuteNonQuery();
                baglanti.Close();
            }
            verileri_getir();

            // Seçilen satırın indeksini al
            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

            // Seçilen satırdaki ComboBox değerini al
            string selectedComboBoxValueKabin = dataGridView1.Rows[selectedRowIndex].Cells["KABİN"].Value.ToString();
            string selectedComboBoxValueIsEmriNo = dataGridView1.Rows[selectedRowIndex].Cells["İŞ EMRİ NO"].Value.ToString();

            // ComboBox değerine göre kaydı durdur
            if (selectedComboBoxValueKabin == "ASTAR KABİNİ")
            {
                //form3.timer1.Stop();
                kontrolList.Add("Astar");
                isEmriNoAstar = selectedComboBoxValueIsEmriNo;
                isTimerStop1 = true;

                MessageBox.Show("Kayıt Durdu!", "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (selectedComboBoxValueKabin == "BOYAMA KABİNİ")
            {
                //form3.timer2.Stop();
                kontrolList.Add("Boyama");
                isEmriNoBoyama = selectedComboBoxValueIsEmriNo;
                isTimerStop2 = true;

                MessageBox.Show("Kayıt Durdu!", "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //form3.timer3.Stop();
                kontrolList.Add("Kurutma");
                isEmriNoKurutma = selectedComboBoxValueIsEmriNo;
                isTimerStop3 = true;

                MessageBox.Show("Kayıt Durdu!", "GNY Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            button3.Enabled = false;
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();


            if ((isEmriNoAstar != null && selectedComboBoxValueKabin != "ASTAR KABİNİ") && !kontrolList.Contains("Astar"))
            {
                timer1.Interval = milliseconds;
                timer1.Start();
                isOlderStopAstar = true;
            }
            if ((isEmriNoBoyama != null && selectedComboBoxValueKabin != "BOYAMA KABİNİ") && !kontrolList.Contains("Boyama"))
            {
                timer2.Interval = milliseconds;
                timer2.Start();
                isOlderStopBoyama = true;
            }
            if ((isEmriNoKurutma != null && selectedComboBoxValueKabin != "KURUTMA KABİNİ") && !kontrolList.Contains("Kurutma"))
            {
                timer3.Interval = milliseconds;
                timer3.Start();
                isOlderStopKurutma = true;
            }
        }

        /* private void button3_Click(object sender, EventArgs e)
         {

         }
        */

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
       

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (isAdmin == true)
            {
                Form2 frm2 = new Form2();
                frm2.StartPosition = FormStartPosition.CenterScreen;
                frm2.isAdmin = isAdmin;
                frm2.Show();
                this.Hide();
            }
            else
            {
                Form3 frm3 = new Form3();
                frm3.StartPosition = FormStartPosition.CenterScreen;
                frm3.isAdmin = isAdmin;
                frm3.isTimerStop1 = isTimerStop1;
                frm3.isTimerStop2 = isTimerStop2;
                frm3.isTimerStop3 = isTimerStop3;
                frm3.isSelected1 = isSelected1;  
                frm3.isSelected2 = isSelected2;
                frm3.isSelected3 = isSelected3;
                frm3.isEmriNoAstar = isEmriNoAstar;
                frm3.isEmriNoBoyama = isEmriNoBoyama;
                frm3.isEmriNoKurutma = isEmriNoKurutma;
                frm3.selectedTime = selectedTime;
                frm3.milliseconds = milliseconds;
                frm3.isForm1 = false;
                frm3.tcno = tcno;
                frm3.kullaniciadi = kullaniciadi;
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();
                this.Close();
                frm3.Show();
                // this.Hide();
            }
        }

        private void kayit_ekle(string isEmriNo)
        {
            baglanti.Close();
            baglanti.Open();
            string sql = "SELECT IsEmriNo AS[İŞ EMRİ NO], ParcaNo AS[PARÇA NO], ParcaAdi AS[PARÇA ADI], Kabin AS[KABİN] FROM Kaydedilen_Isler WHERE Status = 1 AND IsEmriNo=@P1 ORDER BY IsEmriNo ";
            SqlCommand command2 = new SqlCommand(sql, baglanti);
            command2.Parameters.AddWithValue("@P1", isEmriNo);


            //SqlCommand MyCommand2 = new SqlCommand(sql, baglanti);
            SqlDataReader MyReader2;
            MyReader2 = command2.ExecuteReader();

            while (MyReader2.Read())
            {
                parcaNo = MyReader2[1].ToString();
                parcaAdi = MyReader2[2].ToString();
                kabin = MyReader2[3].ToString();
            }

            DateTime tarih = DateTime.Now;
            string saat = DateTime.Now.Hour.ToString();
            string dakika = DateTime.Now.Minute.ToString();
            string saniye = DateTime.Now.Second.ToString();
            baglanti.Close();
            baglanti.Open();
            command.Parameters.Clear();


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

            if (kabin.ToString() == "ASTAR KABİNİ")
            {
                command.Parameters.AddWithValue("@sicaklik", astarSicaklik);
                command.Parameters.AddWithValue("@nem", astarNem);
            }
            else if (kabin.ToString() == "BOYAMA KABİNİ")
            {
                command.Parameters.AddWithValue("@sicaklik", boyamaSicaklik);
                command.Parameters.AddWithValue("@nem", boyamaNem);
            }
            else if (kabin.ToString() == "KURUTMA KABİNİ")
            {
                command.Parameters.AddWithValue("@sicaklik", kurutmaSicaklik);
                command.Parameters.AddWithValue("@nem", kurutmaNem);
            }

            command.Parameters.AddWithValue("@tcno", tcno);

            try
            {
                command.CommandText = kayitekleme;
                command.Connection = baglanti;
                command.ExecuteNonQuery();
                baglanti.Close();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isEmriNoAstar != " ")
            {
                baglanti_olustur();
                kayit_ekle(isEmriNoAstar);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isEmriNoBoyama != " ")
            {
                baglanti_olustur();
                kayit_ekle(isEmriNoBoyama);
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (isEmriNoKurutma != " ")
            {
                baglanti_olustur();
                kayit_ekle(isEmriNoKurutma);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.StartPosition = FormStartPosition.CenterScreen;
            frm1.Show();
            this.Hide();
        }
    }
}
