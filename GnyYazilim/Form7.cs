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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Security.Cryptography;
//using iTextSharp;
//using iTextSharp.text.pdf;
//using iTextSharp.text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection.Metadata;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using LicenseContext = OfficeOpenXml.LicenseContext;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.IO.Packaging;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using GnyYazilim;
using System.Text.Unicode;
using PdfSharp;
using OfficeOpenXml.Style.XmlAccess;

namespace GnyYazilim
{
   
    public partial class Form7 : Form
    {

        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataSet dataSet;

        public Form7()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            GlobalFontSettings.FontResolver = new CustomFontResolver();
        }
        private void InitializeDatabaseConnection()
        {
            // Veritabanı bağlantısı oluşturuluyor.
            string connectionString = (@"Data Source=LAPTOP-6AUCR7C4\SQLEXPRESS;Initial Catalog=GnyYazilimDB;Integrated Security=True");
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataSet = new DataSet();
        }

        static string constring = (@"Data Source=LAPTOP-6AUCR7C4\SQLEXPRESS;Initial Catalog=GnyYazilimDB;Integrated Security=True");
        SqlConnection baglanti = new SqlConnection(constring);
        public bool isAdmin;

        private void kayitlari_getir()
        {
            string getir = "SELECT IsEmriNo AS[İŞ EMRİ NO],MAX(KullaniciAdi) AS[KULLANICI ADI],MAX(ParcaNo) AS[PARÇA NO],MAX(ParcaAdi) AS[PARÇA ADI], MAX(Kabin) AS[KABİN],MAX(BaslangicTarih) AS[BAŞLANGIÇ TARİH],MAX(BitisTarih) AS[BİTİŞ TARİH],MAX(BaslangicSaat) AS[BAŞLANGIÇ SAAT],MAX(BitisSaat) AS[BİTİŞ SAAT],MAX(Sicaklik) AS[SICAKLIK],MAX(Nem) AS[NEM]FROM Tamamlanan_Isler GROUP BY IsEmriNo ORDER BY IsEmriNo";
            SqlCommand komut = new SqlCommand(getir, baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            baglanti.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
           
            kayitlari_getir();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            textBox1.CharacterCasing = CharacterCasing.Upper;
            textBox2.CharacterCasing = CharacterCasing.Upper;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.StartPosition = FormStartPosition.CenterScreen;
            frm2.isAdmin = isAdmin;
            frm2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgula = "SELECT IsEmriNo AS[İŞ EMRİ NO],MAX(KullaniciAdi) AS[KULLANICI ADI],MAX(ParcaNo) AS[PARÇA NO],MAX(ParcaAdi) AS[PARÇA ADI], MAX(Kabin) AS[KABİN],MAX(BaslangicTarih) AS[BAŞLANGIÇ TARİH],MAX(BitisTarih) AS[BİTİŞ TARİH],MAX(BaslangicSaat) AS[BAŞLANGIÇ SAAT],MAX(BitisSaat) AS[BİTİŞ SAAT],MAX(Sicaklik) AS[SICAKLIK],MAX(Nem) AS[NEM] FROM Tamamlanan_Isler WHERE IsEmriNo=@IsEmriNo OR ParcaNo=@ParcaNo OR BaslangicTarih BETWEEN @tarih1 AND @tarih2 GROUP BY IsEmriNo ORDER BY IsEmriNo";

            SqlCommand komut = new SqlCommand(sorgula, baglanti);

            komut.Parameters.AddWithValue("@IsEmriNo", textBox1.Text);
            komut.Parameters.AddWithValue("@ParcaNo", textBox2.Text);

            SqlDataAdapter da = new SqlDataAdapter(komut);

            da.SelectCommand.Parameters.AddWithValue("@tarih1", dateTimePicker1.Value);
            da.SelectCommand.Parameters.AddWithValue("@tarih2", dateTimePicker2.Value);

            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Seçilen satırın iş numarasını alıyoruz.
            string secilenIsEmriNo = dataGridView1.SelectedRows[0].Cells["İŞ EMRİ NO"].Value.ToString();

            // İlgili iş numarasına sahip verileri alıyoruz.
            string getir = $"SELECT KullaniciAdi AS[KULLANICI ADI], IsEmriNo AS[İŞ EMRİ NO], ParcaNo AS[PARÇA NO], ParcaAdi AS[PARÇA ADI], Kabin AS[KABİN], CONVERT(VARCHAR(10),BaslangicTarih,104) AS[TARİH], BaslangicSaat AS[SAAT], Sicaklik AS[SICAKLIK], Nem AS[NEM] FROM Tamamlanan_Isler WHERE IsEmriNo = '{secilenIsEmriNo}'";
                
            adapter.SelectCommand.CommandText = getir;

            try
            {
                connection.Open();
                adapter.Fill(dataSet, "secilenIsEmriNo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Dosyası (*.pdf)|*.pdf";
            saveFileDialog.Title = "PDF DOSYASI OLUŞTURMA";
            saveFileDialog.FileName = "PdfDosyasi_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {    
                ExportToPDF(dataSet.Tables["secilenIsEmriNo"], saveFileDialog.FileName);
                dataSet.Tables["secilenIsEmriNo"].Clear();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataSet.Tables["secilenIsEmriNo"].Clear();

            string secilenIsEmriNo = dataGridView1.SelectedRows[0].Cells["İŞ EMRİ NO"].Value.ToString();

            string getir = $"SELECT KullaniciAdi AS[KULLANICI ADI], IsEmriNo AS[İŞ EMRİ NO], ParcaNo AS[PARÇA NO],ParcaAdi AS[REVİZYON NO], Kabin AS[KABİN], CONVERT(VARCHAR(10),BaslangicTarih,104) AS[TARİH], BaslangicSaat AS[SAAT], Sicaklik AS[SICAKLIK], Nem AS[NEM] FROM Tamamlanan_Isler WHERE IsEmriNo = '{secilenIsEmriNo}'";

            adapter.SelectCommand.CommandText = getir;

            try
            {
                connection.Open();
                adapter.Fill(dataSet, "secilenIsEmriNo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" , ex.Message);
            }
            finally
            {
                connection.Close(); 
            }
        }

        private void ExportToPDF(DataTable dataTable, string filePath)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            page.Size = PageSize.A4;
            page.Orientation = PageOrientation.Landscape;
            XGraphics gfx = XGraphics.FromPdfPage(page);
           

            //gfx.DrawRectangle(PdfSharp.Drawing.XPens.Black, 10, 10, 800, 500);
            

            XFont fontBaslik = new XFont("Arial", 9, XFontStyleEx.Bold | XFontStyleEx.Underline );
            XFont fontSatir = new XFont("Arial", 9);

            // Başlık satırını yazdırma
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                gfx.DrawString(dataTable.Columns[i].ColumnName, fontBaslik, XBrushes.Black, new XRect(20 + i * 80, 10, 140, 50), XStringFormats.Center);

            }

            // Verileri yazdırma
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    gfx.DrawString(dataTable.Rows[i][j].ToString(), fontSatir, XBrushes.Black, new XRect(20 + j * 80, 30 + (i * 20), 140, 50), XStringFormats.Center);
                    
                   
                }
            }

            

            // PDF dosyasını kaydetme
            document.Save(filePath);

            MessageBox.Show("PDF dosyası oluşturuldu!", "GNY Kayıt Yazılımı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Seçilen satırın iş emri no değerini alıyoruz.
            string secilenIsEmriNo = dataGridView1.SelectedRows[0].Cells["İŞ EMRİ NO"].Value.ToString();

            //İlgili iş emri no değerine sahip verileri alıyoruz.
            string getir = $"SELECT KullaniciAdi AS[KULLANICI ADI], IsEmriNo AS[İŞ EMRİ NO], ParcaNo AS[PARÇA NO], ParcaAdi AS[PARÇA ADI], Kabin AS[KABİN], CONVERT(VARCHAR(10),BaslangicTarih,104) As[TARİH], BaslangicSaat AS[SAAT], Sicaklik AS[SICAKLIK], Nem AS[NEM] FROM Tamamlanan_Isler WHERE IsEmriNo = '{secilenIsEmriNo}'";

            adapter.SelectCommand.CommandText = getir;

            try
            {
                connection.Open();
                adapter.Fill(dataSet, "secilenIsEmriNo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // Excel dosyasını kaydetme

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "EXCEL DOSYASI OLUŞTURMA";
            saveFileDialog.FileName = "ExcelDosyasi_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToExcel(dataSet.Tables["secilenIsEmriNo"], saveFileDialog.FileName);
                dataSet.Tables["secilenIsEmriNo"].Clear();
            }
        }

        private void ExportToExcel(DataTable dataTable, string filePath)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

            // Başlık satırını yazdırma
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
            }

            // Verileri yazdırma
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j].ToString();
                }
            }

            // Excel dosyasını kaydetme
            workbook.SaveAs(filePath);

            // Excel uygulamasını kapatma
            excelApp.Quit();

            MessageBox.Show("Excel dosyası oluşturuldu!", "GNY Kayıt Yazılımı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            /*
            Form customDialog = new Form();
            //customDialog.Text = "GNY Kayıt Yazılımı";
            customDialog.StartPosition = FormStartPosition.CenterScreen;

            
            Label messageLabel = new Label();
            messageLabel.Text = "Biçimi Seçin";
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;
            messageLabel.Font = new System.Drawing.Font(messageLabel.Font, messageLabel.Font.Style | FontStyle.Bold);
            messageLabel.Dock = DockStyle.Top;
            customDialog.Controls.Add(messageLabel);
            

            Button pdfButton = new Button();
            pdfButton.Text = "PDF";
            pdfButton.Font = new System.Drawing.Font(pdfButton.Font, pdfButton.Font.Style | FontStyle.Bold);
            pdfButton.Size = new System.Drawing.Size(100, 64);
            pdfButton.DialogResult = DialogResult.Yes;
            pdfButton.Dock = DockStyle.Left;
            customDialog.AcceptButton = pdfButton;
            customDialog.Controls.Add(pdfButton);

            Button excelButton = new Button();
            excelButton.Text = "EXCEL";
            excelButton.Font = new System.Drawing.Font(excelButton.Font, excelButton.Font.Style | FontStyle.Bold);
            excelButton.Size = new System.Drawing.Size(100, 64);
            excelButton.DialogResult = DialogResult.No;
            excelButton.Dock = DockStyle.Right;
            customDialog.CancelButton = excelButton;
            customDialog.Controls.Add(excelButton);

            customDialog.ShowDialog();

            if (customDialog.DialogResult == DialogResult.Yes)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Dosyası (*.pdf)|*.pdf";
                saveFileDialog.Title = "PDF DOSYASI OLUŞTURMA";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    document.Open();

                    PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);
                    table.DefaultCell.Padding = 2;
                    table.WidthPercentage = 100;
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.DefaultCell.BorderWidth = 1;


                    // Türkçe karakterleri desteklemek için doğru yazı tipini ve karakter kodlamasını ayarlayın
                    BaseFont baseFont = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 9);

                    // DataGridView sütun başlıklarını PDF tablosuna ekleyin
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, font));
                        table.AddCell(cell);
                    }

                    // Seçilen satırın verilerini PDF tablosuna ekleyin
                    int selectedRowIndex = dataGridView1.CurrentRow.Index;
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        string cellValue = dataGridView1[i, selectedRowIndex].Value.ToString();
                        PdfPCell cell = new PdfPCell(new Phrase(cellValue, font));
                        table.AddCell(cell);
                    }

                    document.Add(table);
                    document.Close();
                    MessageBox.Show("PDF dosyası oluşturuldu!", "GNY Kayıt Yazılımı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            else if (customDialog.DialogResult == DialogResult.No)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Dosyası (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "EXCEL DOSYASI OLUŞTURMA";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Veri Sayfası");

                        // DataGridView sütun başlıklarını Excel'e ekleyin
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = dataGridView1.Columns[i].HeaderText;
                            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                        }

                        // Seçilen satırın verilerini Excel'e ekleyin
                        int selectedRowIndex = dataGridView1.CurrentRow.Index;
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            string cellValue = dataGridView1[i, selectedRowIndex].Value.ToString();
                            worksheet.Cells[2, i + 1].Value = cellValue;
                        }

                        // Excel dosyasını kaydet
                        var fileInfo = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(fileInfo);
                    }

                    MessageBox.Show("Excel dosyası oluşturuldu!", "GNY Kayıt Yazılımı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            */
        }
    }
}
