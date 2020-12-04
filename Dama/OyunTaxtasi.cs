using Dama.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dama
{
    class OyunTaxtasi
    {
        //oyun taxtasi
        Panel taxta = new Panel();
        Label[,] labels = new Label[8, 8];
        int top = 0;
        int left = 0;

        //gedisleri gostermek ucun ListBoxlar       
        public ListBox lbAglarinGedisi = new ListBox();
        public ListBox lbQaralarinGedisi = new ListBox();

        //gedisleri tutmaq ucun list
        List<Kordinat> aglarinGedisListi = new List<Kordinat>();
        List<Kordinat> qaralarinGedisListi = new List<Kordinat>();

        //daslarin kordinat listi
        List<Kordinat> agDaslarListi = new List<Kordinat>();
        AgDaslar agDas = new AgDaslar();
        List<Kordinat> qaraDaslarListi = new List<Kordinat>();
        QaraDaslar qaraDas = new QaraDaslar();

        //dasin gedisi
        Kordinat hardan = new Kordinat();
        Kordinat hara = new Kordinat();

        //oyun novbesi
        int novbe = 1;

        //vurulan daslari gostermek
        public FlowLayoutPanel pVurulanQaraDasSekilleri = new FlowLayoutPanel();
        public FlowLayoutPanel pVurulanAgDasSekilleri = new FlowLayoutPanel();

        // daslari listlere doldurmaq
        void DaslariListeDoldurmaq()
        {
            agDaslarListi.Add(agDas.D1);
            agDaslarListi.Add(agDas.D2);
            agDaslarListi.Add(agDas.D3);
            agDaslarListi.Add(agDas.D4);
            agDaslarListi.Add(agDas.D5);
            agDaslarListi.Add(agDas.D6);
            agDaslarListi.Add(agDas.D7);
            agDaslarListi.Add(agDas.D8);
            agDaslarListi.Add(agDas.D9);
            agDaslarListi.Add(agDas.D10);
            agDaslarListi.Add(agDas.D11);
            agDaslarListi.Add(agDas.D12);

            qaraDaslarListi.Add(qaraDas.D1);
            qaraDaslarListi.Add(qaraDas.D2);
            qaraDaslarListi.Add(qaraDas.D3);
            qaraDaslarListi.Add(qaraDas.D4);
            qaraDaslarListi.Add(qaraDas.D5);
            qaraDaslarListi.Add(qaraDas.D6);
            qaraDaslarListi.Add(qaraDas.D7);
            qaraDaslarListi.Add(qaraDas.D8);
            qaraDaslarListi.Add(qaraDas.D9);
            qaraDaslarListi.Add(qaraDas.D10);
            qaraDaslarListi.Add(qaraDas.D11);
            qaraDaslarListi.Add(qaraDas.D12);
        }

        //oyun taxtasini yaratmaq
        public Panel TaxtaYarat()
        {
            pVurulanQaraDasSekilleri.Dock = DockStyle.Fill;
            pVurulanAgDasSekilleri.Dock = DockStyle.Fill;
            pVurulanQaraDasSekilleri.BackColor = Color.Transparent;
            pVurulanAgDasSekilleri.BackColor = Color.Transparent;

            lbAglarinGedisi.Dock = DockStyle.Fill;
            lbQaralarinGedisi.Dock = DockStyle.Fill;

            taxta.AutoSize = true;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    labels[i, j] = new Label();
                    labels[i, j].AutoSize = false;
                    labels[i, j].Width = labels[i, j].Height = 60;
                    labels[i, j].Top = top; labels[i, j].Left = left;
                    labels[i, j].BorderStyle = BorderStyle.Fixed3D;
                    labels[i, j].Tag = new Kordinat(i, j);
                    if ((i + j) % 2 == 0)
                    {
                        labels[i, j].BackColor = Color.Red;
                        labels[i, j].Click += OyunTaxtasi_Click;
                    }

                    taxta.Controls.Add(labels[i, j]);
                    left += 60;
                }
                left = 0;
                top += 60;
            }
            DaslariListeDoldurmaq();
            DaslariDuzmek();
            return taxta;
        }

        //Daslari oyun taxtasinda kordinatina gore duzmek
        public void DaslariDuzmek()
        {
            foreach (var item in labels)
            {
                item.Image = null;
            }

            foreach (var item in agDaslarListi)
            {
                if (item.Damka) { labels[item.X, item.Y].Image = Resources.agDamka; }
                else { labels[item.X, item.Y].Image = Resources.agSade; }
            }

            foreach (var item in qaraDaslarListi)
            {
                if (item.Damka) { labels[item.X, item.Y].Image = Resources.qaraDamka; }
                else { labels[item.X, item.Y].Image = Resources.qaraSade; }
            }

        }

        //gedisleri gostermek ve liste yazmaq
        void GedisiListeYaz(Kordinat hardan, Kordinat hara)
        {
            if (novbe == 1)
            {
                aglarinGedisListi.Add(hardan);
                aglarinGedisListi.Add(hara);
                lbAglarinGedisi.Items.Add("(" + hardan.X + "," + hardan.Y + ")" + "-> " + "(" + hara.X + "," + hara.Y + ")");
            }
            else if (novbe == 2)
            {
                qaralarinGedisListi.Add(hardan);
                qaralarinGedisListi.Add(hara);
                lbQaralarinGedisi.Items.Add("(" + hardan.X + "," + hardan.Y + ")" + "-> " + "(" + hara.X + "," + hara.Y + ")");
            }
        }


        //oynanilacag ag ve ya qara das indeksi
        int oynanilacagAgIndeksi, oynanilacagQaraIndeksi;
        bool yenidenVur = false;

        //das secmek ve ya oynamaq (esas kodlar)
        private void OyunTaxtasi_Click(object sender, EventArgs e)
        {
            Label l = sender as Label;
            Kordinat k = (Kordinat)l.Tag;
            int index1 = SecilenAgDasdir(k);
            int index2 = SecilenQaraDasdir(k);

            // dasin yeniden vurmasi varsa yeni das secmesin
            if (yenidenVur)
            {
                index1 = -1; index2 = -1;
            }


            //oyun dasi secilir
            if (index1 >= 0 || index2 >= 0)
            {
                //rengi qaytarmaq
                foreach (var item in labels)
                {
                    if (item.BackColor == Color.Azure ||
                        item.BackColor == Color.LightGreen) item.BackColor = Color.Red;
                }
                hardan = k;

                //ag das click
                if (index1 >= 0 && novbe == 1)
                {
                    novbe = 1;
                    oynanilacagAgIndeksi = index1;
                    l.BackColor = Color.LightGreen;
                    // sade dasin vuraraq gedesi yerleri
                    if (AgDasIreliVurur(k) && agDaslarListi[index1].Damka == false) { return; };
                    //damkanin vuraraq gedesi yeri
                    if (agDaslarListi[index1].Damka)
                    {
                        AgDamkaninHereketi(k); return;
                    }

                    //eger ag das vura bilirse digerleri oynamasin
                    foreach (var item in agDaslarListi)
                    {
                        //damka gelerse
                        if (item.Damka == true)
                        {
                            bool[] boolList = AgDamkaVurur(item);
                            if (boolList[0] || boolList[1] || boolList[2] || boolList[3])
                            {
                                l.BackColor = Color.Red;
                                oynanilacagAgIndeksi = -1; return;
                            }
                        }//sade das gelerse
                        else if (AgDasIreliVurur(item))
                        {
                            l.BackColor = Color.Red;
                            oynanilacagAgIndeksi = -1; return;
                        }
                    }


                    //hereket yerleri
                    if (k.Y < 7 && k.X < 7)
                        if (labels[k.X + 1, k.Y + 1].Image == null)
                            labels[k.X + 1, k.Y + 1].BackColor = Color.Azure;
                    if (k.Y > 0 && k.X < 7)
                        if (labels[k.X + 1, k.Y - 1].Image == null)
                            labels[k.X + 1, k.Y - 1].BackColor = Color.Azure;
                }

                //qara das click
                if (index2 >= 0 && novbe == 2)
                {

                    novbe = 2;
                    oynanilacagQaraIndeksi = index2;
                    l.BackColor = Color.LightGreen;

                    // vuraraq gedesi yerleri                    
                    if (QaraDasIreliVurur(k) && qaraDaslarListi[index2].Damka == false) { return; };

                    //qara Damkanin hereketi
                    if (qaraDaslarListi[index2].Damka == true) { QaraDamkaninHereketi(k); return; }

                    //eger her hansi qara das vura bilirse digerlerini oynamasin
                    foreach (var item in qaraDaslarListi)
                    {
                        if (item.Damka == true) {
                            bool[] boolList = QaraDamkaVurur(item);
                            if (boolList[0] || boolList[1] || boolList[2] || boolList[3])
                            {
                                l.BackColor = Color.Red;
                                oynanilacagQaraIndeksi = -1; return;
                            }
                        }
                        else if (QaraDasIreliVurur(item))
                        {
                            l.BackColor = Color.Red;
                            oynanilacagQaraIndeksi = -1; return;
                        }
                    }


                    if (k.Y < 7 && k.X > 0)
                        if (labels[k.X - 1, k.Y + 1].Image == null)
                            labels[k.X - 1, k.Y + 1].BackColor = Color.Azure;
                    if (k.Y > 0 && k.X > 0)
                        if (labels[k.X - 1, k.Y - 1].Image == null)
                            labels[k.X - 1, k.Y - 1].BackColor = Color.Azure;
                }

            }
            //secilmis das oynanir
            else
            {
                hara = k;
                //ag das oyna               
                if (l.BackColor == Color.Azure && novbe == 1 && oynanilacagAgIndeksi >= 0)
                {
                    GedisiListeYaz(hardan, hara);
                    //rengi qaytarmaq
                    foreach (var item in labels)
                    {
                        if (item.BackColor == Color.Azure ||
                            item.BackColor == Color.LightGreen) item.BackColor = Color.Red;
                    }
                    //adi dasin hereketi ucun
                    if (agDaslarListi[oynanilacagAgIndeksi].Damka == false)
                    {
                        //ag das damkiya cixir
                        if (k.X == 7) agDaslarListi[oynanilacagAgIndeksi].Damka = true;

                        //vurmaq
                        if ((hardan.X + hara.X) % 2 == 0)
                        {
                            int a = SecilenQaraDasdir(new Kordinat((hardan.X + hara.X) / 2, (hardan.Y + hara.Y) / 2));
                            if (a >= 0)
                            {
                                qaraDaslarListi.RemoveAt(a);
                                VurulanQaraDasiKenaraQoy();
                            }
                            //yeniden vurmaq ucun novbe yene agdadir
                            if (AgDasIreliVurur(k) || AgDasGeriVurur(k))
                            {
                                agDaslarListi[oynanilacagAgIndeksi].X = k.X;
                                agDaslarListi[oynanilacagAgIndeksi].Y = k.Y;
                                DaslariDuzmek();

                                hardan = k;
                                yenidenVur = true;
                                novbe = 1; return;
                            }

                        }
                    }
                    //damkanin hereketi ucun
                    else
                    {
                        int a;
                        if (hardan.X < hara.X) { a = hara.X - hardan.X - 1; }
                        else { a = hardan.X - hara.X - 1; }
                        for (int i = 1; i <= a; i++)
                        {

                            if (hardan.X < hara.X && hardan.Y < hara.Y)
                            {
                                int qaraDasVurInd = SecilenQaraDasdir(new Kordinat(hara.X - i, hara.Y - i));
                                if (qaraDasVurInd >= 0)
                                {
                                    qaraDaslarListi.RemoveAt(qaraDasVurInd);
                                    VurulanQaraDasiKenaraQoy();
                                }
                            }
                            else
                                if (hardan.X > hara.X && hardan.Y < hara.Y)
                            {
                                int qaraDasVurInd = SecilenQaraDasdir(new Kordinat(hara.X + i, hara.Y - i));
                                if (qaraDasVurInd >= 0)
                                {
                                    qaraDaslarListi.RemoveAt(qaraDasVurInd);
                                    VurulanQaraDasiKenaraQoy();
                                }
                            }
                            else
                                if (hardan.X > hara.X && hardan.Y > hara.Y)
                            {
                                int qaraDasVurInd = SecilenQaraDasdir(new Kordinat(hara.X + i, hara.Y + i));
                                if (qaraDasVurInd >= 0)
                                {
                                    qaraDaslarListi.RemoveAt(qaraDasVurInd);
                                    VurulanQaraDasiKenaraQoy();
                                }
                            }
                            else
                                if (hardan.X < hara.X && hardan.Y > hara.Y)
                            {
                                int qaraDasVurInd = SecilenQaraDasdir(new Kordinat(hara.X - i, hara.Y + i));
                                if (qaraDasVurInd >= 0)
                                {
                                    qaraDaslarListi.RemoveAt(qaraDasVurInd);
                                    VurulanQaraDasiKenaraQoy();
                                }
                            }

                        }
                        //ag damkanin yeniden vurmagi
                        bool[] boolList = AgDamkaVurur(k);
                        if (boolList[0] || boolList[1] || boolList[2] || boolList[3])
                        {                            
                            agDaslarListi[oynanilacagAgIndeksi].X = k.X;
                            agDaslarListi[oynanilacagAgIndeksi].Y = k.Y;
                            DaslariDuzmek();
                            AgDamkaninHereketi(k);
                            hardan = k;
                            yenidenVur = true;
                            novbe = 1; return;
                        }



                    }

                    agDaslarListi[oynanilacagAgIndeksi].X = k.X;
                    agDaslarListi[oynanilacagAgIndeksi].Y = k.Y;
                    DaslariDuzmek();

                    yenidenVur = false;
                    novbe = 2;

                }

                //  qara das oyna
                if (l.BackColor == Color.Azure && novbe == 2 && oynanilacagQaraIndeksi >= 0
                    )
                {
                    GedisiListeYaz(hardan, hara);
                    //rengi qaytarmaq
                    foreach (var item in labels)
                    {
                        if (item.BackColor == Color.Azure ||
                            item.BackColor == Color.LightGreen) item.BackColor = Color.Red;
                    }
                    //adi das ucun
                    if (qaraDaslarListi[oynanilacagQaraIndeksi].Damka == false)
                    {
                        //qara das damkiya cixir
                        if (k.X == 0) qaraDaslarListi[oynanilacagQaraIndeksi].Damka = true;

                        //vurmaq
                        if ((hardan.X + hara.X) % 2 == 0)
                        {
                            int a = SecilenAgDasdir(new Kordinat((hardan.X + hara.X) / 2, (hardan.Y + hara.Y) / 2));
                            if (a >= 0)
                            {
                                agDaslarListi.RemoveAt(a);
                                VurulanAgDasiKenaraQoy();
                            }

                            //yeniden vurmaq
                            if (QaraDasIreliVurur(k) || QaraDasGeriVurur(k))
                            {
                                qaraDaslarListi[oynanilacagQaraIndeksi].X = k.X;
                                qaraDaslarListi[oynanilacagQaraIndeksi].Y = k.Y;
                                DaslariDuzmek();

                                hardan = k;
                                yenidenVur = true;
                                novbe = 2; return;
                            }
                        }
                    }
                    //damka ucun
                    else
                    {
                        int a;
                        if (hardan.X < hara.X) { a = hara.X - hardan.X - 1; }
                        else { a = hardan.X - hara.X - 1; }
                        for (int i = 1; i <= a; i++)
                        {

                            if (hardan.X < hara.X && hardan.Y < hara.Y)
                            {
                                int agDasVurInd = SecilenAgDasdir(new Kordinat(hara.X - i, hara.Y - i));
                                if (agDasVurInd >= 0)
                                {
                                    agDaslarListi.RemoveAt(agDasVurInd);
                                    VurulanAgDasiKenaraQoy();
                                }
                            }
                            else
                                if (hardan.X > hara.X && hardan.Y < hara.Y)
                            {
                                int agDasVurInd = SecilenAgDasdir(new Kordinat(hara.X + i, hara.Y - i));
                                if (agDasVurInd >= 0)
                                {
                                    agDaslarListi.RemoveAt(agDasVurInd);
                                    VurulanAgDasiKenaraQoy();
                                }
                            }
                            else
                                if (hardan.X > hara.X && hardan.Y > hara.Y)
                            {
                                int agDasVurInd = SecilenAgDasdir(new Kordinat(hara.X + i, hara.Y + i));
                                if (agDasVurInd >= 0)
                                {
                                    agDaslarListi.RemoveAt(agDasVurInd);
                                    VurulanAgDasiKenaraQoy();
                                }
                            }
                            else
                                if (hardan.X < hara.X && hardan.Y > hara.Y)
                            {
                                int agDasVurInd = SecilenAgDasdir(new Kordinat(hara.X - i, hara.Y + i));
                                if (agDasVurInd >= 0)
                                {
                                    agDaslarListi.RemoveAt(agDasVurInd);
                                    VurulanAgDasiKenaraQoy();
                                }
                            }

                        }
                        //qara damkanin yeniden vurmasi
                        bool[] boolList = QaraDamkaVurur(k);
                        if (boolList[0] || boolList[1] || boolList[2] || boolList[3])
                        {
                            qaraDaslarListi[oynanilacagQaraIndeksi].X = k.X;
                            qaraDaslarListi[oynanilacagQaraIndeksi].Y = k.Y;
                            DaslariDuzmek();
                            QaraDamkaninHereketi(k);
                            hardan = k;
                            yenidenVur = true;
                            novbe = 2; return;
                        }
                    }

                    qaraDaslarListi[oynanilacagQaraIndeksi].X = k.X;
                    qaraDaslarListi[oynanilacagQaraIndeksi].Y = k.Y;
                    DaslariDuzmek();

                    yenidenVur = false;
                    novbe = 1;

                }
            }


            if (agDaslarListi.Count() == 0) MessageBox.Show("Qaralar uddu");
            if (qaraDaslarListi.Count() == 0) MessageBox.Show("Aglar  uddu");


        }

        //vurulan ag dasi kenara qoymaq
        private void VurulanAgDasiKenaraQoy()
        {
            PictureBox p = new PictureBox();
            p.Image = Resources.agSade;
            p.Width = p.Height = 40;
            p.SizeMode = PictureBoxSizeMode.StretchImage;
            pVurulanAgDasSekilleri.Controls.Add(p);
        }

        //vurulan qara dasi kenera qoymaq
        private void VurulanQaraDasiKenaraQoy()
        {
            PictureBox p = new PictureBox();
            p.Image = Resources.qaraSade;
            p.Width = p.Height = 40;
            p.SizeMode = PictureBoxSizeMode.StretchImage;
            pVurulanQaraDasSekilleri.Controls.Add(p);
        }

        //sade ag dasin ireli ve ya geri vurmasi
        bool AgDasIreliVurur(Kordinat k)
        {
            bool agdasvururmu = false;
            if (k.X < 6 && k.Y < 6)
            {
                Kordinat nk = new Kordinat(k.X + 1, k.Y + 1);
                if (SecilenQaraDasdir(nk) >= 0)
                    if (labels[k.X + 2, k.Y + 2].Image == null)
                    {
                        labels[k.X + 2, k.Y + 2].BackColor = Color.Azure;
                        agdasvururmu = true;
                    }
            }
            if (k.X < 6 && k.Y > 1)
            {
                Kordinat nk = new Kordinat(k.X + 1, k.Y - 1);
                if (SecilenQaraDasdir(nk) >= 0)
                    if (labels[k.X + 2, k.Y - 2].Image == null)
                    {
                        labels[k.X + 2, k.Y - 2].BackColor = Color.Azure;
                        agdasvururmu = true;
                    }
            }
            return agdasvururmu;

        }
        bool AgDasGeriVurur(Kordinat k)
        {
            bool agdasvururmu = false;
            if (k.X > 1 && k.Y < 6)
            {
                Kordinat nk = new Kordinat(k.X - 1, k.Y + 1);
                if (SecilenQaraDasdir(nk) >= 0)
                    if (labels[k.X - 2, k.Y + 2].Image == null)
                    {
                        labels[k.X - 2, k.Y + 2].BackColor = Color.Azure;
                        agdasvururmu = true;
                    }
            }
            if (k.X > 1 && k.Y > 1)
            {
                Kordinat nk = new Kordinat(k.X - 1, k.Y - 1);
                if (SecilenQaraDasdir(nk) >= 0)
                    if (labels[k.X - 2, k.Y - 2].Image == null)
                    {
                        labels[k.X - 2, k.Y - 2].BackColor = Color.Azure;
                        agdasvururmu = true;
                    }

            }

            return agdasvururmu;
        }


        //sade qara dasin ireli ve ya geri vurmasi
        bool QaraDasIreliVurur(Kordinat k)
        {
            bool qaradasvururmu = false;
            if (k.X > 1 && k.Y < 6)
            {
                Kordinat nk = new Kordinat(k.X - 1, k.Y + 1);
                if (SecilenAgDasdir(nk) >= 0)
                    if (labels[k.X - 2, k.Y + 2].Image == null)
                    {
                        labels[k.X - 2, k.Y + 2].BackColor = Color.Azure;
                        qaradasvururmu = true;
                    }
            }
            if (k.X > 1 && k.Y > 1)
            {
                Kordinat nk = new Kordinat(k.X - 1, k.Y - 1);
                if (SecilenAgDasdir(nk) >= 0)
                    if (labels[k.X - 2, k.Y - 2].Image == null)
                    {
                        labels[k.X - 2, k.Y - 2].BackColor = Color.Azure;
                        qaradasvururmu = true;
                    }
            }
            return qaradasvururmu;

        }
        bool QaraDasGeriVurur(Kordinat k)
        {
            bool qaradasvururmu = false;
            if (k.X < 7 && k.Y < 6)
            {
                Kordinat nk = new Kordinat(k.X + 1, k.Y + 1);
                if (SecilenAgDasdir(nk) >= 0)
                    if (labels[k.X + 2, k.Y + 2].Image == null)
                    {
                        labels[k.X + 2, k.Y + 2].BackColor = Color.Azure;
                        qaradasvururmu = true;
                    }
            }
            if (k.X < 7 && k.Y > 1)
            {
                Kordinat nk = new Kordinat(k.X + 1, k.Y - 1);
                if (SecilenAgDasdir(nk) >= 0)
                    if (labels[k.X + 2, k.Y - 2].Image == null)
                    {
                        labels[k.X + 2, k.Y - 2].BackColor = Color.Azure;
                        qaradasvururmu = true;
                    }

            }

            return qaradasvururmu;
        }


        //ag damka vurur
        bool[] AgDamkaVurur(Kordinat k)
        {
            Kordinat sagA = new Kordinat(k.X, k.Y);
            Kordinat sagY = new Kordinat(k.X, k.Y);
            Kordinat solA = new Kordinat(k.X, k.Y);
            Kordinat solY = new Kordinat(k.X, k.Y);

            bool sagAsgiVurur = false;
            bool sagYuxariVurur = false;
            bool solAsagiVurur = false;
            bool solYuxariVurur = false;

            //saga asagi
            for (int i = 0; i < 8; i++)
            {
                sagA.X++; sagA.Y++;
                if (sagA.X > 7 || sagA.Y > 7) break;// taxtadan kenara
                if (SecilenAgDasdir(sagA) >= 0) break; //qarsisinda ag das var
                if (SecilenQaraDasdir(sagA) >= 0)//qarsinda qara das var
                {
                    Kordinat nk = new Kordinat(sagA.X + 1, sagA.Y + 1);
                    if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var
                    if (sagA.X < 7 && sagA.Y < 7) sagAsgiVurur = true;
                }

            }

            //sag yuxari
            for (int i = 0; i < 8; i++)
            {
                sagY.X--; sagY.Y++;
                if (sagY.X < 0 || sagY.Y > 7) break;
                if (SecilenAgDasdir(sagY) >= 0) break;
                if (SecilenQaraDasdir(sagY) >= 0)
                {
                    Kordinat nk = new Kordinat(sagY.X - 1, sagY.Y + 1);
                    if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;
                    if (sagY.X > 0 && sagY.Y < 7) sagYuxariVurur = true;
                }

            }

            //sol asagi
            for (int i = 0; i < 8; i++)
            {
                solA.X++; solA.Y--;
                if (solA.X > 7 || solA.Y < 0) break;// taxtadan kenara
                if (SecilenAgDasdir(solA) >= 0) break; //qarsisinda ag das var
                if (SecilenQaraDasdir(solA) >= 0)//qarsinda qara das var
                {
                    Kordinat nk = new Kordinat(solA.X + 1, solA.Y - 1);
                    if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var
                    if (solA.X < 7 && solA.Y > 0) solAsagiVurur = true;
                }

            }

            //sol yuxari
            for (int i = 0; i < 8; i++)
            {
                solY.X--; solY.Y--;
                if (solY.X < 0 || solY.Y < 0) break;//taxtadan kenara
                if (SecilenAgDasdir(solY) >= 0) break;//qarsida ag das var
                if (SecilenQaraDasdir(solY) >= 0)//qarsida qara das car
                {
                    Kordinat nk = new Kordinat(solY.X - 1, solY.Y - 1);
                    if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;
                    if (solY.X > 0 && solY.Y > 0) solYuxariVurur = true;
                }

            }

            bool[] b = { sagAsgiVurur, sagYuxariVurur, solAsagiVurur, solYuxariVurur };

            return b;
        }

        //ag damkanin hereketi
        void AgDamkaninHereketi(Kordinat k)
        {
            Kordinat sagA = new Kordinat(k.X, k.Y);
            Kordinat sagY = new Kordinat(k.X, k.Y);
            Kordinat solA = new Kordinat(k.X, k.Y);
            Kordinat solY = new Kordinat(k.X, k.Y);

            bool[] vururmu = AgDamkaVurur(k);

            bool sagAsgiVurur = vururmu[0];
            bool sagYuxariVurur = vururmu[1];
            bool solAsagiVurur = vururmu[2];
            bool solYuxariVurur = vururmu[3];


            //her hasisa bir terefe damka vurursa vuruan terflere yer gostersin
            if (sagAsgiVurur || sagYuxariVurur || solAsagiVurur || solYuxariVurur)
            {
                //saga asagi
                if (sagAsgiVurur) for (int i = 0; i < 8; i++)
                    {
                        sagA.X++; sagA.Y++;
                        if (sagA.X > 7 || sagA.Y > 7) break;// taxtadan kenara
                        if (SecilenAgDasdir(sagA) >= 0) break; //qarsisinda ag das var
                        if (SecilenQaraDasdir(sagA) >= 0)//qarsinda qara das var
                        {
                            Kordinat nk = new Kordinat(sagA.X + 1, sagA.Y + 1);
                            if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var                        
                        }
                        else
                        {
                            labels[sagA.X, sagA.Y].BackColor = Color.Azure;
                        }
                    }

                //sag yuxari
                if (sagYuxariVurur) for (int i = 0; i < 8; i++)
                    {
                        sagY.X--; sagY.Y++;
                        if (sagY.X < 0 || sagY.Y > 7) break;
                        if (SecilenAgDasdir(sagY) >= 0) break;
                        if (SecilenQaraDasdir(sagY) >= 0)
                        {
                            Kordinat nk = new Kordinat(sagY.X - 1, sagY.Y + 1);
                            if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;

                        }
                        else
                        {
                            labels[sagY.X, sagY.Y].BackColor = Color.Azure;
                        }
                    }

                //sol asagi
                if (solAsagiVurur) for (int i = 0; i < 8; i++)
                    {
                        solA.X++; solA.Y--;
                        if (solA.X > 7 || solA.Y < 0) break;// taxtadan kenara
                        if (SecilenAgDasdir(solA) >= 0) break; //qarsisinda ag das var
                        if (SecilenQaraDasdir(solA) >= 0)//qarsinda qara das var
                        {
                            Kordinat nk = new Kordinat(solA.X + 1, solA.Y - 1);
                            if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var

                        }
                        else
                        {
                            labels[solA.X, solA.Y].BackColor = Color.Azure;
                        }
                    }

                //sol yuxari
                if (solYuxariVurur) for (int i = 0; i < 8; i++)
                    {
                        solY.X--; solY.Y--;
                        if (solY.X < 0 || solY.Y < 0) break;//taxtadan kenara
                        if (SecilenAgDasdir(solY) >= 0) break;//qarsida ag das var
                        if (SecilenQaraDasdir(solY) >= 0)//qarsida qara das car
                        {
                            Kordinat nk = new Kordinat(solY.X - 1, solY.Y - 1);
                            if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;

                        }
                        else
                        {
                            labels[solY.X, solY.Y].BackColor = Color.Azure;
                        }
                    }


            }
            //her hansi terefe vurmursa butun tereflere yer gostersin
            else
            {
                //saga asagi
                for (int i = 0; i < 8; i++)
                {
                    sagA.X++; sagA.Y++;
                    if (sagA.X > 7 || sagA.Y > 7) break;// taxtadan kenara
                    if (SecilenAgDasdir(sagA) >= 0) break; //qarsisinda ag das var
                    if (SecilenQaraDasdir(sagA) >= 0)//qarsinda qara das var
                    {
                        Kordinat nk = new Kordinat(sagA.X + 1, sagA.Y + 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var                        
                    }
                    else
                    {
                        labels[sagA.X, sagA.Y].BackColor = Color.Azure;
                    }
                }

                //sag yuxari
                for (int i = 0; i < 8; i++)
                {
                    sagY.X--; sagY.Y++;
                    if (sagY.X < 0 || sagY.Y > 7) break;
                    if (SecilenAgDasdir(sagY) >= 0) break;
                    if (SecilenQaraDasdir(sagY) >= 0)
                    {
                        Kordinat nk = new Kordinat(sagY.X - 1, sagY.Y + 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;

                    }
                    else
                    {
                        labels[sagY.X, sagY.Y].BackColor = Color.Azure;
                    }
                }

                //sol asagi
                for (int i = 0; i < 8; i++)
                {
                    solA.X++; solA.Y--;
                    if (solA.X > 7 || solA.Y < 0) break;// taxtadan kenara
                    if (SecilenAgDasdir(solA) >= 0) break; //qarsisinda ag das var
                    if (SecilenQaraDasdir(solA) >= 0)//qarsinda qara das var
                    {
                        Kordinat nk = new Kordinat(solA.X + 1, solA.Y - 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var

                    }
                    else
                    {
                        labels[solA.X, solA.Y].BackColor = Color.Azure;
                    }
                }

                //sol yuxari
                for (int i = 0; i < 8; i++)
                {
                    solY.X--; solY.Y--;
                    if (solY.X < 0 || solY.Y < 0) break;//taxtadan kenara
                    if (SecilenAgDasdir(solY) >= 0) break;//qarsida ag das var
                    if (SecilenQaraDasdir(solY) >= 0)//qarsida qara das car
                    {
                        Kordinat nk = new Kordinat(solY.X - 1, solY.Y - 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;

                    }
                    else
                    {
                        labels[solY.X, solY.Y].BackColor = Color.Azure;
                    }
                }
            }
        }

        //qara damka vurur
        bool[] QaraDamkaVurur(Kordinat k)
        {
            Kordinat sagA = new Kordinat(k.X, k.Y);
            Kordinat sagY = new Kordinat(k.X, k.Y);
            Kordinat solA = new Kordinat(k.X, k.Y);
            Kordinat solY = new Kordinat(k.X, k.Y);

            bool sagAsgiVurur = false;
            bool sagYuxariVurur = false;
            bool solAsagiVurur = false;
            bool solYuxariVurur = false;

            //saga asagi
            for (int i = 0; i < 8; i++)
            {
                sagA.X++; sagA.Y++;
                if (sagA.X > 7 || sagA.Y > 7) break;// taxtadan kenara
                if (SecilenQaraDasdir(sagA) >= 0) break; //qarsisinda qara das var
                if (SecilenAgDasdir(sagA) >= 0)//qarsinda ag das var
                {
                    Kordinat nk = new Kordinat(sagA.X + 1, sagA.Y + 1);
                    if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var
                    if (sagA.X < 7 && sagA.Y < 7) sagAsgiVurur = true;
                }
                
            }

            //sag yuxari
            for (int i = 0; i < 8; i++)
            {
                sagY.X--; sagY.Y++;
                if (sagY.X < 0 || sagY.Y > 7) break;
                if (SecilenQaraDasdir(sagY) >= 0) break;
                if (SecilenAgDasdir(sagY) >= 0)
                {
                    Kordinat nk = new Kordinat(sagY.X - 1, sagY.Y + 1);
                    if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;
                    if (sagY.X >0 && sagY.Y <7) sagYuxariVurur = true;
                }
                
            }

            //sol asagi
            for (int i = 0; i < 8; i++)
            {
                solA.X++; solA.Y--;
                if (solA.X > 7 || solA.Y < 0) break;// taxtadan kenara
                if (SecilenQaraDasdir(solA) >= 0) break; //qarsisinda qara das var
                if (SecilenAgDasdir(solA) >= 0)//qarsinda ag das var
                {
                    Kordinat nk = new Kordinat(solA.X + 1, solA.Y - 1);
                    if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var
                    if (solA.X < 7 && solA.Y > 0) solAsagiVurur = true;
                }
                
            }

            //sol yuxari
            for (int i = 0; i < 8; i++)
            {
                solY.X--; solY.Y--;
                if (solY.X < 0 || solY.Y < 0) break;//taxtadan kenara
                if (SecilenQaraDasdir(solY) >= 0) break;//qarsida ag das var
                if (SecilenAgDasdir(solY) >= 0)//qarsida qara das car
                {
                    Kordinat nk = new Kordinat(solY.X - 1, solY.Y - 1);
                    if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;
                    if (solY.X>0 && solY.Y > 0) solYuxariVurur = true;
                }
                
            }

            bool[] b = { sagAsgiVurur, sagYuxariVurur, solAsagiVurur, solYuxariVurur };
            return b;

        }

        //qara damkanin hereketi
        void QaraDamkaninHereketi(Kordinat k)
        {
            Kordinat sagA = new Kordinat(k.X, k.Y);
            Kordinat sagY = new Kordinat(k.X, k.Y);
            Kordinat solA = new Kordinat(k.X, k.Y);
            Kordinat solY = new Kordinat(k.X, k.Y);

            bool[] vururmu = QaraDamkaVurur(k);

            bool sagAsgiVurur = vururmu[0];
            bool sagYuxariVurur = vururmu[1];
            bool solAsagiVurur = vururmu[2];
            bool solYuxariVurur = vururmu[3];


            //her hasisa bir terefe damka vurursa vuruan terflere yer gostersin
            if (sagAsgiVurur || sagYuxariVurur || solAsagiVurur || solYuxariVurur)
            {

                //saga asagi
                if(sagAsgiVurur) for (int i = 0; i < 8; i++)
                {
                    sagA.X++; sagA.Y++;
                    if (sagA.X > 7 || sagA.Y > 7) break;// taxtadan kenara
                    if (SecilenQaraDasdir(sagA) >= 0) break; //qarsisinda qara das var
                    if (SecilenAgDasdir(sagA) >= 0)//qarsinda ag das var
                    {
                        Kordinat nk = new Kordinat(sagA.X + 1, sagA.Y + 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var
                    }
                    else
                    {
                        labels[sagA.X, sagA.Y].BackColor = Color.Azure;
                    }
                }

                //sag yuxari
                if (sagYuxariVurur) for (int i = 0; i < 8; i++)
                {
                    sagY.X--; sagY.Y++;
                    if (sagY.X < 0 || sagY.Y > 7) break;
                    if (SecilenQaraDasdir(sagY) >= 0) break;
                    if (SecilenAgDasdir(sagY) >= 0)
                    {
                        Kordinat nk = new Kordinat(sagY.X - 1, sagY.Y + 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;
                    }
                    else
                    {
                        labels[sagY.X, sagY.Y].BackColor = Color.Azure;
                    }
                }

                //sol asagi
                if (solAsagiVurur) for (int i = 0; i < 8; i++)
                {
                    solA.X++; solA.Y--;
                    if (solA.X > 7 || solA.Y < 0) break;// taxtadan kenara
                    if (SecilenQaraDasdir(solA) >= 0) break; //qarsisinda qara das var
                    if (SecilenAgDasdir(solA) >= 0)//qarsinda ag das var
                    {
                        Kordinat nk = new Kordinat(solA.X + 1, solA.Y - 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var
                    }
                    else
                    {
                        labels[solA.X, solA.Y].BackColor = Color.Azure;
                    }
                }

                //sol yuxari
                if (solYuxariVurur) for (int i = 0; i < 8; i++)
                {
                    solY.X--; solY.Y--;
                    if (solY.X < 0 || solY.Y < 0) break;//taxtadan kenara
                    if (SecilenQaraDasdir(solY) >= 0) break;//qarsida ag das var
                    if (SecilenAgDasdir(solY) >= 0)//qarsida qara das car
                    {
                        Kordinat nk = new Kordinat(solY.X - 1, solY.Y - 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;
                    }
                    else
                    {
                        labels[solY.X, solY.Y].BackColor = Color.Azure;
                    }
                }
            }
            //eger hec terefe vurmursa
            else
            {
                //saga asagi
                for (int i = 0; i < 8; i++)
                {
                    sagA.X++; sagA.Y++;
                    if (sagA.X > 7 || sagA.Y > 7) break;// taxtadan kenara
                    if (SecilenQaraDasdir(sagA) >= 0) break; //qarsisinda qara das var
                    if (SecilenAgDasdir(sagA) >= 0)//qarsinda ag das var
                    {
                        Kordinat nk = new Kordinat(sagA.X + 1, sagA.Y + 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var
                    }
                    else
                    {
                        labels[sagA.X, sagA.Y].BackColor = Color.Azure;
                    }
                }

                //sag yuxari
                for (int i = 0; i < 8; i++)
                {
                    sagY.X--; sagY.Y++;
                    if (sagY.X < 0 || sagY.Y > 7) break;
                    if (SecilenQaraDasdir(sagY) >= 0) break;
                    if (SecilenAgDasdir(sagY) >= 0)
                    {
                        Kordinat nk = new Kordinat(sagY.X - 1, sagY.Y + 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;
                    }
                    else
                    {
                        labels[sagY.X, sagY.Y].BackColor = Color.Azure;
                    }
                }

                //sol asagi
                for (int i = 0; i < 8; i++)
                {
                    solA.X++; solA.Y--;
                    if (solA.X > 7 || solA.Y < 0) break;// taxtadan kenara
                    if (SecilenQaraDasdir(solA) >= 0) break; //qarsisinda qara das var
                    if (SecilenAgDasdir(solA) >= 0)//qarsinda ag das var
                    {
                        Kordinat nk = new Kordinat(solA.X + 1, solA.Y - 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;//qarsida iki das var
                    }
                    else
                    {
                        labels[solA.X, solA.Y].BackColor = Color.Azure;
                    }
                }

                //sol yuxari
                for (int i = 0; i < 8; i++)
                {
                    solY.X--; solY.Y--;
                    if (solY.X < 0 || solY.Y < 0) break;//taxtadan kenara
                    if (SecilenQaraDasdir(solY) >= 0) break;//qarsida ag das var
                    if (SecilenAgDasdir(solY) >= 0)//qarsida qara das car
                    {
                        Kordinat nk = new Kordinat(solY.X - 1, solY.Y - 1);
                        if (SecilenAgDasdir(nk) >= 0 || SecilenQaraDasdir(nk) >= 0) break;
                    }
                    else
                    {
                        labels[solY.X, solY.Y].BackColor = Color.Azure;
                    }
                }

            }

        }

        int SecilenAgDasdir(Kordinat k)
        {
            int i = 0;
            foreach (var item in agDaslarListi)
            {

                if (item.X == k.X && item.Y == k.Y)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        int SecilenQaraDasdir(Kordinat k)
        {
            int i = 0;
            foreach (var item in qaraDaslarListi)
            {

                if (item.X == k.X && item.Y == k.Y)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
    }
}
