using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //oyna baslamaq
        OyunTaxtasi oyunTaxtasi = new OyunTaxtasi();
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Add(oyunTaxtasi.TaxtaYarat());
            pAgGedisleri.Controls.Add(oyunTaxtasi.lbAglarinGedisi);
            pQaraGedisleri.Controls.Add(oyunTaxtasi.lbQaralarinGedisi);
            pVurulanQaralar.Controls.Add(oyunTaxtasi.pVurulanQaraDasSekilleri);
            pVurulanAglar.Controls.Add(oyunTaxtasi.pVurulanAgDasSekilleri);
           
            button1.Visible = false;
            bYenile.Visible = true;
        }

        //exit
        private void bExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //oynu yenile
        private void bYenile_Click(object sender, EventArgs e)
        {
            oyunTaxtasi = null;
            oyunTaxtasi = new OyunTaxtasi();

            panel1.Controls.Clear();
            pAgGedisleri.Controls.Clear();
            pQaraGedisleri.Controls.Clear();
            pVurulanQaralar.Controls.Clear();
            pVurulanAglar.Controls.Clear();

            panel1.Controls.Add(oyunTaxtasi.TaxtaYarat());
            pAgGedisleri.Controls.Add(oyunTaxtasi.lbAglarinGedisi);
            pQaraGedisleri.Controls.Add(oyunTaxtasi.lbQaralarinGedisi);
            pVurulanQaralar.Controls.Add(oyunTaxtasi.pVurulanQaraDasSekilleri);
            pVurulanAglar.Controls.Add(oyunTaxtasi.pVurulanAgDasSekilleri);
        }


        //panelin hereketi hereket
        int mos, mosX, mosY;
        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            mos = 1; mosX = e.X; mosY = e.Y;
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (mos == 1)
            {
                this.SetDesktopLocation(MousePosition.X - mosX, MousePosition.Y - mosY);
            }
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            mos = 0;
        }
    }
}
