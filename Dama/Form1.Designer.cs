namespace Dama
{
    partial class Form1
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bExit = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.pQaraGedisleri = new System.Windows.Forms.Panel();
            this.pAgGedisleri = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pVurulanQaralar = new System.Windows.Forms.Panel();
            this.pVurulanAglar = new System.Windows.Forms.Panel();
            this.bYenile = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Dama.Properties.Resources.wood;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.bYenile);
            this.panel2.Controls.Add(this.pVurulanAglar);
            this.panel2.Controls.Add(this.pVurulanQaralar);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.pQaraGedisleri);
            this.panel2.Controls.Add(this.pAgGedisleri);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 600);
            this.panel2.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Controls.Add(this.bExit);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(900, 28);
            this.panel4.TabIndex = 5;
            this.panel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseDown);
            this.panel4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseMove);
            this.panel4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseUp);
            // 
            // bExit
            // 
            this.bExit.Location = new System.Drawing.Point(870, 2);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(27, 23);
            this.bExit.TabIndex = 0;
            this.bExit.Text = "X";
            this.bExit.UseVisualStyleBackColor = true;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Location = new System.Drawing.Point(93, 67);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(505, 503);
            this.panel3.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = global::Dama.Properties.Resources.checckers;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(11, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 480);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Oyuna başla";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pQaraGedisleri
            // 
            this.pQaraGedisleri.BackColor = System.Drawing.Color.Transparent;
            this.pQaraGedisleri.Location = new System.Drawing.Point(788, 403);
            this.pQaraGedisleri.Name = "pQaraGedisleri";
            this.pQaraGedisleri.Size = new System.Drawing.Size(100, 156);
            this.pQaraGedisleri.TabIndex = 3;
            // 
            // pAgGedisleri
            // 
            this.pAgGedisleri.BackColor = System.Drawing.Color.Transparent;
            this.pAgGedisleri.Location = new System.Drawing.Point(788, 107);
            this.pAgGedisleri.Name = "pAgGedisleri";
            this.pAgGedisleri.Size = new System.Drawing.Size(100, 135);
            this.pAgGedisleri.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(786, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ağ daşlar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(783, 364);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Qara daşlar";
            // 
            // pVurulanQaralar
            // 
            this.pVurulanQaralar.BackColor = System.Drawing.Color.Transparent;
            this.pVurulanQaralar.Location = new System.Drawing.Point(605, 79);
            this.pVurulanQaralar.Name = "pVurulanQaralar";
            this.pVurulanQaralar.Size = new System.Drawing.Size(156, 217);
            this.pVurulanQaralar.TabIndex = 8;
            // 
            // pVurulanAglar
            // 
            this.pVurulanAglar.BackColor = System.Drawing.Color.Transparent;
            this.pVurulanAglar.Location = new System.Drawing.Point(605, 332);
            this.pVurulanAglar.Name = "pVurulanAglar";
            this.pVurulanAglar.Size = new System.Drawing.Size(156, 227);
            this.pVurulanAglar.TabIndex = 9;
            // 
            // bYenile
            // 
            this.bYenile.Location = new System.Drawing.Point(95, 34);
            this.bYenile.Name = "bYenile";
            this.bYenile.Size = new System.Drawing.Size(70, 23);
            this.bYenile.TabIndex = 10;
            this.bYenile.Text = "Yenilə";
            this.bYenile.UseVisualStyleBackColor = true;
            this.bYenile.Visible = false;
            this.bYenile.Click += new System.EventHandler(this.bYenile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pAgGedisleri;
        private System.Windows.Forms.Panel pQaraGedisleri;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.Panel pVurulanAglar;
        private System.Windows.Forms.Panel pVurulanQaralar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bYenile;
    }
}

