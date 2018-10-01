// Készítette: Fodor Boldizsár
// Weblap: air360.hu

// Importálások
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Eseti {
    public partial class Form1 : Form {

        // Form3 felvétele
        Form3 form3 = new Form3();

        // Appdata mappa
        string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        // Adatok betöltése
        public Form1() {
            InitializeComponent();
            if (!(Directory.Exists(appdata + "/eseti"))) {
                Directory.CreateDirectory(appdata + "/eseti");
            }
            Thread.Sleep(100);
            if (!(File.Exists(appdata + "/eseti/alap.eseti"))) {
                using (var client = new WebClient()) {
                    client.DownloadFile("http://air360.hu/app/eseti/files/alap.eseti", appdata + "/eseti/alap.eseti");
                }
            }

            LaucnherUpdate();

            Thread.Sleep(100);
            pictureBox1.BackgroundImage = Image.FromFile(appdata + "/eseti/alap.eseti");
            

            textBox10.Enabled = false;
            textBox13.Enabled = false;
            Opacity = 0;

            if (File.Exists(appdata + "/eseti/dev.eseti")) {
                fejlesztőiMódToolStripMenuItem.Checked = true;
            }
            else {
                fejlesztőiMódToolStripMenuItem.Checked = false;
            }

            if (File.Exists(appdata + "/eseti/dark.eseti")) {
                File.WriteAllText(appdata + "/eseti/dark.eseti", "true");
                sötétMódToolStripMenuItem.Checked = true;
                BackColor = Color.DimGray;
                ForeColor = Color.LightGray;
                menuStrip1.BackColor = Color.DimGray;
                menuStrip1.ForeColor = Color.LightGray;
                textBox1.BackColor = Color.DimGray;
                textBox2.BackColor = Color.DimGray;
                textBox3.BackColor = Color.DimGray;
                textBox4.BackColor = Color.DimGray;
                textBox5.BackColor = Color.DimGray;
                textBox6.BackColor = Color.DimGray;
                textBox7.BackColor = Color.DimGray;
                textBox8.BackColor = Color.DimGray;
                textBox9.BackColor = Color.DimGray;
                textBox10.BackColor = Color.DimGray;
                textBox11.BackColor = Color.DimGray;
                textBox12.BackColor = Color.DimGray;
                radioButton1.BackColor = Color.DimGray;
                radioButton2.BackColor = Color.DimGray;
                radioButton3.BackColor = Color.DimGray;
                radioButton4.BackColor = Color.DimGray;
                checkBox1.BackColor = Color.DimGray;
                checkBox2.BackColor = Color.DimGray;
                checkBox3.BackColor = Color.DimGray;
                checkBox4.BackColor = Color.DimGray;
                checkBox5.BackColor = Color.DimGray;
            } else {
                File.Delete(appdata + "/eseti/dark.eseti");
                sötétMódToolStripMenuItem.Checked = false;
                BackColor = Color.White;
                ForeColor = Color.Black;
                menuStrip1.BackColor = Color.White;
                menuStrip1.ForeColor = Color.Black;
                textBox1.BackColor = Color.White;
                textBox2.BackColor = Color.White;
                textBox3.BackColor = Color.White;
                textBox4.BackColor = Color.White;
                textBox5.BackColor = Color.White;
                textBox6.BackColor = Color.White;
                textBox7.BackColor = Color.White;
                textBox8.BackColor = Color.White;
                textBox9.BackColor = Color.White;
                textBox10.BackColor = Color.White;
                textBox11.BackColor = Color.White;
                textBox12.BackColor = Color.White;
                radioButton1.BackColor = Color.White;
                radioButton2.BackColor = Color.White;
                radioButton3.BackColor = Color.White;
                radioButton4.BackColor = Color.White;
                checkBox1.BackColor = Color.White;
                checkBox2.BackColor = Color.White;
                checkBox3.BackColor = Color.White;
                checkBox4.BackColor = Color.White;
                checkBox5.BackColor = Color.White;
            }

            if (File.Exists(appdata + "/eseti/live.eseti")) {
                élőElőnézetToolStripMenuItem.Checked = true;
                
            } else {
                élőElőnézetToolStripMenuItem.Checked = false;
            }
        }

        // Form1 betöltése
        private void Form1_Load(object sender, EventArgs e) {
            timerload.Start();
        }

        // Fade & update értesítés
        private void timerload_Tick(object sender, EventArgs e) {
            double x = 0.01;
            if (Opacity <= 0.95) {
                Opacity += x;
            }
            else if (Opacity + x > 0.95) {
                timerload.Stop();
                Opacity = 100;
                if (File.Exists(appdata + "/eseti/new.eseti")) {
                    string msg;
                    using (WebClient client = new WebClient()) {
                        msg = client.DownloadString("http://air360.hu/app/eseti/last.eseti");
                    }
                    MessageBox.Show(msg, "Az új verzió újdonságai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Az új verzióban az előnézetet az ENTER gomb lenyomásával tudja legenerálni.", "!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Az új verzióban az előnézetet az előnézetre való kattintással tudja kinagyítani. Bezárni az új ablakban lévő előnézetre való kattintással tudja bezárni.", "!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    File.Delete(appdata + "/eseti/new.eseti");
                }
            }
        }

        // Változók felvétele
        string local = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "/eseti");
        string type = "prew";
        string spath = null;
        string rd_btn = null;

        // Változások érzékelése

        private void Changed(object sender, EventArgs e) {
            if (élőElőnézetToolStripMenuItem.Checked) {
                type = "prew";
                generateImg();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) {
            type = "prew";
            textBox10.Enabled = true;
            rd_btn = "4";
            generateImg();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {
            type = "prew";
            textBox10.Enabled = false;
            rd_btn = "3";
            generateImg();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            type = "prew";
            textBox10.Enabled = false;
            rd_btn = "2";
            generateImg();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            type = "prew";
            textBox10.Enabled = false;
            rd_btn = "1";
            generateImg();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            type = "prew";
            generateImg();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            type = "prew";
            generateImg();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            type = "prew";
            generateImg();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e) {
            type = "prew";
            generateImg();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e) {
            type = "prew";
            generateImg();
        }
        

        // Mentés JPG -be
        private void mentésJPGFájlbaToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = local;
            saveFileDialog1.Title = "Mentés JPG fájlba";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "jpg";
            saveFileDialog1.Filter = "JPG képfájl (*.jpg)|*.jpg";
            saveFileDialog1.FilterIndex = 1;

            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                type = "jpg";
                spath = saveFileDialog1.FileName;
                generateImg();
            }
        }

        // Mentés PDF -be
        private void mentésPDFFájlbaToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = local;
            saveFileDialog1.Title = "Mentés PDF fájlba";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "PDF fájl (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 1;

            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                type = "pdf";
                spath = saveFileDialog1.FileName;
                generateImg();
            }
        }

        // Nyomtatvány generálása
        public Task generateImg() {
            form3.pictureBox1.BackgroundImage.Dispose();
            pictureBox1.BackgroundImage.Dispose();
            Image bitmap = Bitmap.FromFile(appdata + "/eseti/alap.eseti");
            Graphics graphicsImage = Graphics.FromImage(bitmap);

            Color black = Color.Black;

            string kerelmezo = textBox1.Text;
            string cim = textBox2.Text;
            string telefon = textBox3.Text;
            string email = textBox4.Text;
            string l_cim = textBox5.Text;
            string adoszam = textBox6.Text;
            string koordinatak = textBox7.Text;
            string idopont = textBox8.Text;
            string magassag = textBox9.Text;
            string felelos = textBox11.Text;
            string kero = textBox12.Text;
            string time = DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + ".";

            if (checkBox6.Checked == true) {
                time = textBox13.Text;
            }

            graphicsImage.DrawString(kerelmezo, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(470, 513));
            graphicsImage.DrawString(cim, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(270, 573));
            graphicsImage.DrawString(telefon, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(445, 633));
            graphicsImage.DrawString(email, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(1425, 633));
            graphicsImage.DrawString(l_cim, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(941, 690));
            graphicsImage.DrawString(adoszam, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(645, 747));
            graphicsImage.DrawString(koordinatak, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(150, 860));
            graphicsImage.DrawString(idopont, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(150, 1055));
            graphicsImage.DrawString(magassag, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(1045, 1137));
            graphicsImage.DrawString(felelos, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(150, 1410));
            graphicsImage.DrawString(kero, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(1157, 1493));
            graphicsImage.DrawString(time, new Font("arial", 10, FontStyle.Regular), new SolidBrush(black), new Point(250, 1870));
            
            switch (rd_btn) {
                case "1":
                    graphicsImage.DrawString("X", new Font("arial", 10, FontStyle.Bold), new SolidBrush(black), new Point(605, 1217));
                    break;
                case "2":
                    graphicsImage.DrawString("X", new Font("arial", 10, FontStyle.Bold), new SolidBrush(black), new Point(1249, 1217));
                    break;
                case "3":
                    graphicsImage.DrawString("X", new Font("arial", 10, FontStyle.Bold), new SolidBrush(black), new Point(1920, 1217));
                    break;
                case "4":
                    graphicsImage.DrawString("X", new Font("arial", 10, FontStyle.Bold), new SolidBrush(black), new Point(360, 1273));
                    graphicsImage.DrawString(textBox10.Text, new Font("arial", 10, FontStyle.Bold), new SolidBrush(black), new Point(1537, 1273));
                    break;
                case null:
                    break;
            }
            

            if (checkBox1.Checked == true) {
                graphicsImage.DrawString("X", new Font("arial", 10, FontStyle.Bold), new SolidBrush(black), new Point(157, 1605));
            }

            if (checkBox2.Checked == true) {
                graphicsImage.DrawString("X", new Font("arial", 10, FontStyle.Bold), new SolidBrush(black), new Point(157, 1657));
            }

            if (checkBox3.Checked == true) {
                graphicsImage.DrawString("X", new Font("arial", 10, FontStyle.Bold), new SolidBrush(black), new Point(157, 1709));
            }

            if (checkBox4.Checked == true) {
                graphicsImage.DrawString("X", new Font("arial", 10, FontStyle.Bold), new SolidBrush(black), new Point(157, 1761));
            }

            if (checkBox5.Checked == true) {
                graphicsImage.DrawString("X", new Font("arial", 10, FontStyle.Bold), new SolidBrush(black), new Point(157, 1813));
            }
            
            switch (type) {
                case "prew":
                    bitmap.Save(appdata + "/eseti/kesz.eseti");
                    bitmap.Dispose();
                    graphicsImage.Dispose();
                    if (pictureBox1.Enabled) {
                        pictureBox1.BackgroundImage = Image.FromFile(appdata + "/eseti/kesz.eseti");
                    }
                    form3.pictureBox1.BackgroundImage = Image.FromFile(appdata + "/eseti/kesz.eseti");
                    break;
                case "jpg":
                    bitmap.Save(appdata + "/eseti/kesz.eseti");
                    bitmap.Save(spath);
                    bitmap.Dispose();
                    graphicsImage.Dispose();
                    pictureBox1.BackgroundImage = Image.FromFile(appdata + "/eseti/kesz.eseti");
                    break;
                case "pdf":
                    PdfDocument doc = new PdfDocument();
                    doc.Pages.Add(new PdfPage());
                    XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);
                    XImage img = XImage.FromFile(appdata + "/eseti/kesz.eseti");
                    xgr.DrawImage(img, 0, 0);
                    doc.Save(spath);
                    doc.Close();
                    break;
            }
            return Task.CompletedTask;
        }

        // Sablon mentése
        private void mentésPresetkéntToolStripMenuItem_Click(object sender, EventArgs e) {
            string text1 = textBox1.Text;

            string text2 = textBox2.Text;

            string text3 = textBox3.Text;

            string text4 = textBox4.Text;

            string text5 = textBox5.Text;

            string text6 = textBox6.Text;

            string text7 = textBox7.Text;

            string text8 = textBox8.Text;

            string text9 = textBox9.Text;

            string rdn_btn_save = rd_btn;
            if (rdn_btn_save == null)
                rdn_btn_save = "null";

            string text10 = textBox10.Text;

            string text11 = textBox11.Text;

            string text12 = textBox12.Text;

            string check1 = "0";
            if (checkBox1.Checked == true)
                check1 = "1";

            string check2 = "0";
            if (checkBox2.Checked == true)
                check2 = "1";

            string check3 = "0";
            if (checkBox3.Checked == true)
                check3 = "1";

            string check4 = "0";
            if (checkBox4.Checked == true)
                check4 = "1";

            string check5 = "0";
            if (checkBox5.Checked == true)
                check5 = "1";

            string text = text1 + Environment.NewLine +
                text2 + Environment.NewLine +
                text3 + Environment.NewLine + 
                text4 + Environment.NewLine +
                text5 + Environment.NewLine + 
                text6 + Environment.NewLine + 
                text7 + Environment.NewLine + 
                text8 + Environment.NewLine +
                text9 + Environment.NewLine +
                rdn_btn_save + Environment.NewLine +
                text10 + Environment.NewLine +
                text11 + Environment.NewLine +
                text12 + Environment.NewLine +
                check1 + Environment.NewLine +
                check2 + Environment.NewLine +
                check3 + Environment.NewLine +
                check4 + Environment.NewLine +
                check5;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = local;
            saveFileDialog1.Title = "Mentés sablonként";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "es";
            saveFileDialog1.Filter = "Eseti sablon fájl (*.es)|*.es";
            saveFileDialog1.FilterIndex = 1;

            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                spath = saveFileDialog1.FileName;
                File.WriteAllText(spath, text);
            }
        }

        // Sablon betöltése
        private void presetBetöltéseToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = local;
            openFileDialog1.Title = "Sablon betöltése";
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "es";
            openFileDialog1.Filter = "Eseti sablon fájl (*.es)|*.es";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                spath = openFileDialog1.FileName;
                var lines = File.ReadAllLines(spath);
                textBox1.Text = lines[0];
                textBox2.Text = lines[1];
                textBox3.Text = lines[2];
                textBox4.Text = lines[3];
                textBox5.Text = lines[4];
                textBox6.Text = lines[5];
                textBox7.Text = lines[6];
                textBox8.Text = lines[7];
                textBox9.Text = lines[8];

                switch (lines[9]) {
                    case "null":
                        break;
                    case "1":
                        radioButton1.Checked = true;
                        break;
                    case "2":
                        radioButton2.Checked = true;
                        break;
                    case "3":
                        radioButton3.Checked = true;
                        break;
                    case "4":
                        radioButton4.Checked = true;
                        textBox10.Text = lines[10];
                        break;
                }

                textBox11.Text = lines[11];
                textBox12.Text = lines[12];

                if (lines[13] == "1") {
                    checkBox1.Checked = true;
                }

                if (lines[14] == "1") {
                    checkBox2.Checked = true;
                }

                if (lines[15] == "1") {
                    checkBox3.Checked = true;
                }

                if (lines[16] == "1") {
                    checkBox4.Checked = true;
                }

                if (lines[17] == "1") {
                    checkBox5.Checked = true;
                }

            }
        }

        // Sötét mód
        private void sötétMódToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!(File.Exists(appdata + "/eseti/dark.eseti"))) {
                File.WriteAllText(appdata + "/eseti/dark.eseti", "true");
                sötétMódToolStripMenuItem.Checked = true;
                BackColor = Color.DimGray;
                ForeColor = Color.LightGray;
                menuStrip1.BackColor = Color.DimGray;
                menuStrip1.ForeColor = Color.LightGray;
                textBox1.BackColor = Color.DimGray;
                textBox2.BackColor = Color.DimGray;
                textBox3.BackColor = Color.DimGray;
                textBox4.BackColor = Color.DimGray;
                textBox5.BackColor = Color.DimGray;
                textBox6.BackColor = Color.DimGray;
                textBox7.BackColor = Color.DimGray;
                textBox8.BackColor = Color.DimGray;
                textBox9.BackColor = Color.DimGray;
                textBox10.BackColor = Color.DimGray;
                textBox11.BackColor = Color.DimGray;
                textBox12.BackColor = Color.DimGray;
                radioButton1.BackColor = Color.DimGray;
                radioButton2.BackColor = Color.DimGray;
                radioButton3.BackColor = Color.DimGray;
                radioButton4.BackColor = Color.DimGray;
                checkBox1.BackColor = Color.DimGray;
                checkBox2.BackColor = Color.DimGray;
                checkBox3.BackColor = Color.DimGray;
                checkBox4.BackColor = Color.DimGray;
                checkBox5.BackColor = Color.DimGray;
            } else {
                File.Delete(appdata + "/eseti/dark.eseti");
                sötétMódToolStripMenuItem.Checked = false;
                BackColor = Color.White;
                ForeColor = Color.Black;
                menuStrip1.BackColor = Color.White;
                menuStrip1.ForeColor = Color.Black;
                textBox1.BackColor = Color.White;
                textBox2.BackColor = Color.White;
                textBox3.BackColor = Color.White;
                textBox4.BackColor = Color.White;
                textBox5.BackColor = Color.White;
                textBox6.BackColor = Color.White;
                textBox7.BackColor = Color.White;
                textBox8.BackColor = Color.White;
                textBox9.BackColor = Color.White;
                textBox10.BackColor = Color.White;
                textBox11.BackColor = Color.White;
                textBox12.BackColor = Color.White;
                radioButton1.BackColor = Color.White;
                radioButton2.BackColor = Color.White;
                radioButton3.BackColor = Color.White;
                radioButton4.BackColor = Color.White;
                checkBox1.BackColor = Color.White;
                checkBox2.BackColor = Color.White;
                checkBox3.BackColor = Color.White;
                checkBox4.BackColor = Color.White;
                checkBox5.BackColor = Color.White;
            }
        }

        // Fejlesztői mód
        private void fejlesztőiMódToolStripMenuItem_Click(object sender, EventArgs e) {
            if (File.Exists(appdata + "/eseti/dev.eseti")) {
                File.Delete(appdata + "/eseti/dev.eseti");
                fejlesztőiMódToolStripMenuItem.Checked = false;
            } else {
                File.WriteAllText(appdata + "/eseti/dev.eseti", "true");
                fejlesztőiMódToolStripMenuItem.Checked = true;
            }
        }

        // Minta
        private void button1_Click(object sender, EventArgs e) {
            spath = appdata + "/eseti/minta.es";
            var lines = File.ReadAllLines(spath);
            textBox1.Text = lines[0];
            textBox2.Text = lines[1];
            textBox3.Text = lines[2];
            textBox4.Text = lines[3];
            textBox5.Text = lines[4];
            textBox6.Text = lines[5];
            textBox7.Text = lines[6];
            textBox8.Text = lines[7];
            textBox9.Text = lines[8];

            switch (lines[9]) {
                case "null":
                    break;
                case "1":
                    radioButton1.Checked = true;
                    break;
                case "2":
                    radioButton2.Checked = true;
                    break;
                case "3":
                    radioButton3.Checked = true;
                    break;
                case "4":
                    radioButton4.Checked = true;
                    textBox10.Text = lines[10];
                    break;
            }

            textBox11.Text = lines[11];
            textBox12.Text = lines[12];

            if (lines[13] == "1") {
                checkBox1.Checked = true;
            }

            if (lines[14] == "1") {
                checkBox2.Checked = true;
            }

            if (lines[15] == "1") {
                checkBox3.Checked = true;
            }

            if (lines[16] == "1") {
                checkBox4.Checked = true;
            }

            if (lines[17] == "1") {
                checkBox5.Checked = true;
            }
        }

        // Launcher update
        public Task LaucnherUpdate() {
            string laucher_folder = File.ReadAllText(appdata + "/eseti/launcher.eseti");
            if (File.Exists(appdata + "/eseti/launcher_ver.eseti")) {
                string verloc = appdata + "/eseti/launcher_ver.eseti";
                int latest_ver = 0;
                int current_ver = int.Parse(File.ReadAllText(verloc).Trim());
                using (WebClient client = new WebClient()) {
                    latest_ver = int.Parse(client.DownloadString("http://air360.hu/app/eseti/latest_launcher.eseti").Trim());
                }
                if (latest_ver > current_ver) {
                    File.Delete(laucher_folder + "/Eseti.exe");
                    using (var client = new WebClient()) {
                        client.DownloadFile("http://air360.hu/app/eseti/Eseti.exe", laucher_folder + "/Eseti.exe");
                    }
                } else {

                }
            } else {
                File.Delete(laucher_folder + "/Eseti.exe");
                using (var client = new WebClient()) {
                    client.DownloadFile("http://air360.hu/app/eseti/Eseti.exe", laucher_folder + "/Eseti.exe");
                }
            }
            return Task.CompletedTask;
        }

        // Légtér térkép
        private void button2_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://terkep.legter.hu/#6.7/47.179/19.344");
        }

        // Keltezés checkbox
        private void checkBox6_CheckedChanged(object sender, EventArgs e) {
            if (checkBox6.Checked == true) {
                textBox13.Enabled = true;
            } else {
                textBox13.Enabled = false;
            }
            type = "prew";
            generateImg();
        }
        
        // Segítség
        public void segítségToolStripMenuItem_Click(object sender, EventArgs e) {
            Form2 form2 = new Form2();
            form2.F2_Type = "help";
            form2.Show();
        }

        // Frissítési előzmények
        private void frissítésiElőzményekToolStripMenuItem_Click(object sender, EventArgs e) {
            Form2 form2 = new Form2();
            form2.F2_Type = "updates";
            form2.Show();
        }

        // Előnézet nagyítása
        private void pictureBox1_DoubleClick(object sender, EventArgs e) {
            form3.Show();
        }

        // Ne lehessen nagyítani
        private void Form1_ResizeEnd(object sender, EventArgs e) {
            Size = new Size(823, 710);
        }

        // Előnézet elrejtése
        private void checkBox7_CheckedChanged(object sender, EventArgs e) {
            if (checkBox7.Checked == true) {
                type = "prew";
                generateImg();
                pictureBox1.Enabled = true;
                pictureBox1.Show();
                pictureBox1.BackgroundImage = Image.FromFile(appdata + "/eseti/kesz.eseti");
            } else {
                type = "prew";
                generateImg();
                pictureBox1.Hide();
                pictureBox1.Enabled = false;
                pictureBox1.BackgroundImage.Dispose();
            }
        }

        // Előnézet enterre
        private void Prew1(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                type = "prew";
                generateImg();
            }
        }

        // Élő előnézet kapcsoló
        private void élőElőnézetToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!(File.Exists(appdata + "/eseti/live.eseti"))) {
                DialogResult result = MessageBox.Show("Figyelem! Ennek a bállításnak a bekapcsolásával a program több erőforrást használhat! Folytatja?",
                "!!!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
                if (result == DialogResult.Yes) {
                    File.WriteAllText(appdata + "/eseti/live.eseti", "true");
                    élőElőnézetToolStripMenuItem.Checked = true;
                }
            } else {
                File.Delete(appdata + "/eseti/live.eseti");
                élőElőnézetToolStripMenuItem.Checked = false;
            }
        }
    }
}
