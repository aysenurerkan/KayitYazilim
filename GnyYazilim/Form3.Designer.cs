namespace GnyYazilim
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            comboBox1 = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            label4 = new Label();
            textBox3 = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            button3 = new Button();
            pictureBox1 = new PictureBox();
            button4 = new Button();
            timer2 = new System.Windows.Forms.Timer(components);
            timer3 = new System.Windows.Forms.Timer(components);
            label5 = new Label();
            comboBox2 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(94, 27);
            label1.Name = "label1";
            label1.Size = new Size(110, 28);
            label1.TabIndex = 0;
            label1.Text = "İş Emri No";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(94, 78);
            label2.Name = "label2";
            label2.Size = new Size(98, 28);
            label2.TabIndex = 1;
            label2.Text = "Parça No";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(94, 180);
            label3.Name = "label3";
            label3.Size = new Size(66, 28);
            label3.TabIndex = 2;
            label3.Text = "Kabin";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(233, 24);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(217, 31);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(233, 75);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(217, 31);
            textBox2.TabIndex = 4;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = SystemColors.Window;
            comboBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            comboBox1.ForeColor = SystemColors.WindowText;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(233, 175);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(217, 33);
            comboBox1.TabIndex = 5;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(94, 406);
            button1.Name = "button1";
            button1.Size = new Size(130, 76);
            button1.TabIndex = 6;
            button1.Text = "Aktif İşlemler";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(94, 292);
            button2.Name = "button2";
            button2.Size = new Size(130, 76);
            button2.TabIndex = 7;
            button2.Text = "İş Ekle";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(94, 127);
            label4.Name = "label4";
            label4.Size = new Size(132, 28);
            label4.TabIndex = 8;
            label4.Text = "Revizyon No";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(233, 124);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(217, 31);
            textBox3.TabIndex = 9;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 5000;
            timer1.Tick += timer1_Tick;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Location = new Point(320, 292);
            button3.Name = "button3";
            button3.Size = new Size(130, 76);
            button3.TabIndex = 10;
            button3.Text = "Kayıt Başlat";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.WhatsApp_Image_2023_10_12_at_18_48_41;
            pictureBox1.Location = new Point(508, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(90, 79);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            button4.Location = new Point(320, 406);
            button4.Name = "button4";
            button4.Size = new Size(130, 76);
            button4.TabIndex = 13;
            button4.Text = "Çıkış";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click_2;
            // 
            // timer2
            // 
            timer2.Enabled = true;
            timer2.Interval = 5000;
            timer2.Tick += timer2_Tick;
            // 
            // timer3
            // 
            timer3.Enabled = true;
            timer3.Interval = 5000;
            timer3.Tick += timer3_Tick;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(94, 232);
            label5.Name = "label5";
            label5.Size = new Size(124, 28);
            label5.TabIndex = 14;
            label5.Text = "Kayıt Süresi";
            // 
            // comboBox2
            // 
            comboBox2.BackColor = SystemColors.Window;
            comboBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            comboBox2.ForeColor = SystemColors.WindowText;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(233, 227);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(217, 33);
            comboBox2.TabIndex = 5;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(629, 504);
            Controls.Add(comboBox2);
            Controls.Add(label5);
            Controls.Add(button4);
            Controls.Add(pictureBox1);
            Controls.Add(button3);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form3";
            Text = "OPERATÖR EKRANI";
            Load += Form3_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
        private ComboBox comboBox1;
        private Button button1;
        private Button button2;
        private Label label4;
        private TextBox textBox3;
        private Button button3;
        private PictureBox pictureBox1;
        public System.Windows.Forms.Timer timer1;
        private Button button4;
        public System.Windows.Forms.Timer timer2;
        public System.Windows.Forms.Timer timer3;
        private Label label5;
        public ComboBox comboBox2;
    }
}