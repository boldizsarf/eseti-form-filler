using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Diagnostics;
using System.Reflection;

namespace EsetiInstaller {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            string local = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            
            if (File.Exists(appdata + "/eseti/dev.eseti")) {
                Show();
            } else {
                Hide();
                try {
                    //if (File.Exists(local + "/Eseti.exe")) {
                    if (!(Directory.Exists(appdata + "/eseti"))) {
                        textBox1.Text += Environment.NewLine + appdata + "/eseti generálása";
                        Directory.CreateDirectory(appdata + "/eseti");

                        Thread.Sleep(100);

                        textBox1.Text += Environment.NewLine + "Tömörített frissítés letöltése";
                        using (var client = new WebClient()) {
                            client.DownloadFile("http://air360.hu/app/eseti/latest.zip", appdata + "/eseti/latest.zip");
                        }

                        Thread.Sleep(100);

                        string zip = appdata + "/eseti/latest.zip";
                        string kibont = appdata + "/eseti/";

                        textBox1.Text += Environment.NewLine + "Frissítés kicsomagolása";
                        ZipFile.ExtractToDirectory(zip, kibont);

                        Thread.Sleep(100);

                        textBox1.Text += Environment.NewLine + "Tömörített frissítés törlése";
                        File.Delete(appdata + "/eseti/latest.zip");

                        textBox1.Text += Environment.NewLine + "Launcher elérési út kiírása";
                        File.WriteAllText(appdata + "/eseti/launcher.eseti", local);

                        textBox1.Text += Environment.NewLine + "Program indítása";

                        File.WriteAllText(appdata + "/eseti/launcher_ver.eseti", "3");


                        Thread.Sleep(100);

                        Process.Start(appdata + "/eseti/Eseti.exe");
                        Bezar();
                    }
                    else {
                        if (Directory.GetFiles(appdata + "/eseti").Length == 0) {
                            textBox1.Text += Environment.NewLine + "A program nem található";
                            textBox1.Text += Environment.NewLine + appdata + "/eseti törlése";
                            Directory.Delete(appdata + "/eseti");
                            Thread.Sleep(1000);
                        }
                        string verloc = appdata + "/eseti/ver.eseti";
                        int latest_ver = 0;
                        int current_ver = int.Parse(File.ReadAllText(verloc).Trim());

                        using (WebClient client = new WebClient()) {
                            textBox1.Text += Environment.NewLine + "Frissítések keresése";
                            latest_ver = int.Parse(client.DownloadString("http://air360.hu/app/eseti/latest.eseti").Trim());
                        }

                        if (latest_ver > current_ver) {

                            textBox1.Text += Environment.NewLine + "Régi verzió törlése";
                            DirectoryInfo de = new DirectoryInfo(appdata + "/eseti/de");
                            DirectoryInfo eappdata = new DirectoryInfo(appdata + "/eseti");
                            foreach (FileInfo file in de.GetFiles()) {
                                file.Delete();
                            }
                            foreach (FileInfo file in eappdata.GetFiles()) {
                                file.Delete();
                            }
                            foreach (DirectoryInfo dir in eappdata.GetDirectories()) {
                                dir.Delete(true);
                            }

                            Directory.CreateDirectory(appdata + "/eseti");

                            Thread.Sleep(100);

                            textBox1.Text += Environment.NewLine + "Frissítés letöltése";
                            using (var client = new WebClient()) {
                                client.DownloadFile("http://air360.hu/app/eseti/latest.zip", appdata + "/eseti/latest.zip");
                            }

                            Thread.Sleep(100);

                            textBox1.Text += Environment.NewLine + "Frissítés telepítése";
                            string zip = appdata + "/eseti/latest.zip";
                            string kibont = appdata + "/eseti/";

                            ZipFile.ExtractToDirectory(zip, kibont);

                            Thread.Sleep(100);

                            File.Delete(appdata + "/eseti/latest.zip");

                            textBox1.Text += Environment.NewLine + "Launcher elérési út kiírása";
                            File.WriteAllText(appdata + "/eseti/launcher.eseti", local);

                            textBox1.Text += Environment.NewLine + "Program indítása";


                            File.WriteAllText(appdata + "/eseti/launcher_ver.eseti", "3");


                            Thread.Sleep(100);

                            Process.Start(appdata + "/eseti/Eseti.exe");

                            Bezar();
                        } else {
                            textBox1.Text += Environment.NewLine + "Launcher elérési út kiírása";
                            File.WriteAllText(appdata + "/eseti/launcher.eseti", local);
                            textBox1.Text += Environment.NewLine + "Program indítása";

                            File.WriteAllText(appdata + "/eseti/launcher_ver.eseti", "3");

                            Thread.Sleep(100);

                            Process.Start(appdata + "/eseti/Eseti.exe");
                            Bezar();
                        }
                    }
                    //} else {
                    //  textBox1.Text += Environment.NewLine + "A fájl nevének 'Eseti.exe' -nek kell lennie!";
                    // DialogResult menu_res = MessageBox.Show("A fájl nevének 'Eseti.exe' -nek kell lennie!", "Hiba", MessageBoxButtons.OK);
                    // if (menu_res == DialogResult.OK) {
                    //     Bezar();
                    // } else {
                    //     Bezar();
                    // }
                    // }
                }
                catch (Exception err) {
                    textBox1.Text += Environment.NewLine + "Hiba: " + err;
                    MessageBox.Show(err.ToString());
                    Bezar();
                }
            }

            textBox1.Text += "Eseti launcher v1.2 | Eseti fejlesztői konzol v1.0";
            
        }
        public Task Bezar() {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (!(File.Exists(appdata + "/eseti/launcher_ver.eseti"))) {

            }

            if (!(File.Exists(appdata + "/eseti/dev.eseti"))) {
                Close();
            }

            return Task.CompletedTask;
        }

        private void button2_Click(object sender, EventArgs e) {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            MessageBox.Show("Ehhez a művelethez be kell zárnod a programot!");
            DirectoryInfo de = new DirectoryInfo(appdata + "/eseti/de");
            DirectoryInfo eappdata = new DirectoryInfo(appdata + "/eseti");
            foreach (FileInfo file in de.GetFiles()) {
                file.Delete();
                textBox1.Text += Environment.NewLine + file.FullName + " törlése";
            }
            foreach (FileInfo file in eappdata.GetFiles()) {
                file.Delete();
                textBox1.Text += Environment.NewLine + file.FullName + " törlése";
            }
            foreach (DirectoryInfo dir in eappdata.GetDirectories()) {
                dir.Delete(true);
                textBox1.Text += Environment.NewLine + dir.FullName + " törlése";
            }
            Directory.Delete(appdata + "/eseti");
            textBox1.Text += Environment.NewLine + "Program eltávolítva";
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }

        int bezar_req_yes_no = 0;

        private void textBox2_KeyDown(object sender, KeyEventArgs e) {

            string local = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (e.KeyCode == Keys.Enter) {
                string cmd = textBox2.Text;

                if (cmd.Length > 0) {
                    string awnser = "";
                    textBox2.Text = "";
                    

                    // Start
                    if (cmd == "start") {
                        try {
                            //if (File.Exists(local + "/Eseti.exe")) {
                            if (!(Directory.Exists(appdata + "/eseti"))) {
                                textBox1.Text += Environment.NewLine + appdata + "/eseti generálása";
                                Directory.CreateDirectory(appdata + "/eseti");

                                Thread.Sleep(100);

                                textBox1.Text += Environment.NewLine + "Tömörített frissítés letöltése";
                                using (var client = new WebClient()) {
                                    client.DownloadFile("http://air360.hu/app/eseti/latest.zip", appdata + "/eseti/latest.zip");
                                }

                                Thread.Sleep(100);

                                string zip = appdata + "/eseti/latest.zip";
                                string kibont = appdata + "/eseti/";

                                textBox1.Text += Environment.NewLine + "Frissítés kicsomagolása";
                                ZipFile.ExtractToDirectory(zip, kibont);

                                Thread.Sleep(100);

                                textBox1.Text += Environment.NewLine + "Tömörített frissítés törlése";
                                File.Delete(appdata + "/eseti/latest.zip");

                                textBox1.Text += Environment.NewLine + "Launcher elérési út kiírása";
                                File.WriteAllText(appdata + "/eseti/launcher.eseti", local);

                                textBox1.Text += Environment.NewLine + "Program indítása";

                                File.WriteAllText(appdata + "/eseti/launcher_ver.eseti", "3");


                                Thread.Sleep(100);

                                Process.Start(appdata + "/eseti/Eseti.exe");
                                Bezar();
                            } else {
                                if (Directory.GetFiles(appdata + "/eseti").Length == 0) {
                                    textBox1.Text += Environment.NewLine + "A program nem található";
                                    textBox1.Text += Environment.NewLine + appdata + "/eseti törlése";
                                    Directory.Delete(appdata + "/eseti");
                                    Thread.Sleep(1000);
                                }
                                string verloc = appdata + "/eseti/ver.eseti";
                                int latest_ver = 0;
                                int current_ver = int.Parse(File.ReadAllText(verloc).Trim());

                                using (WebClient client = new WebClient()) {
                                    textBox1.Text += Environment.NewLine + "Frissítések keresése";
                                    latest_ver = int.Parse(client.DownloadString("http://air360.hu/app/eseti/latest.eseti").Trim());
                                }

                                if (latest_ver > current_ver) {

                                    textBox1.Text += Environment.NewLine + "Régi verzió törlése";
                                    DirectoryInfo de = new DirectoryInfo(appdata + "/eseti/de");
                                    DirectoryInfo eappdata = new DirectoryInfo(appdata + "/eseti");
                                    foreach (FileInfo file in de.GetFiles()) {
                                        file.Delete();
                                    }
                                    foreach (FileInfo file in eappdata.GetFiles()) {
                                        file.Delete();
                                    }
                                    foreach (DirectoryInfo dir in eappdata.GetDirectories()) {
                                        dir.Delete(true);
                                    }

                                    Directory.CreateDirectory(appdata + "/eseti");

                                    Thread.Sleep(100);

                                    textBox1.Text += Environment.NewLine + "Frissítés letöltése";
                                    using (var client = new WebClient()) {
                                        client.DownloadFile("http://air360.hu/app/eseti/latest.zip", appdata + "/eseti/latest.zip");
                                    }

                                    Thread.Sleep(100);

                                    textBox1.Text += Environment.NewLine + "Frissítés telepítése";
                                    string zip = appdata + "/eseti/latest.zip";
                                    string kibont = appdata + "/eseti/";

                                    ZipFile.ExtractToDirectory(zip, kibont);

                                    Thread.Sleep(100);

                                    File.Delete(appdata + "/eseti/latest.zip");

                                    textBox1.Text += Environment.NewLine + "Launcher elérési út kiírása";
                                    File.WriteAllText(appdata + "/eseti/launcher.eseti", local);

                                    textBox1.Text += Environment.NewLine + "Program indítása";


                                    File.WriteAllText(appdata + "/eseti/launcher_ver.eseti", "3");


                                    Thread.Sleep(100);

                                    Process.Start(appdata + "/eseti/Eseti.exe");

                                    Bezar();
                                } else {
                                    textBox1.Text += Environment.NewLine + "Launcher elérési út kiírása";
                                    File.WriteAllText(appdata + "/eseti/launcher.eseti", local);
                                    textBox1.Text += Environment.NewLine + "Program indítása";

                                    File.WriteAllText(appdata + "/eseti/launcher_ver.eseti", "3");

                                    Thread.Sleep(100);

                                    Process.Start(appdata + "/eseti/Eseti.exe");
                                    Bezar();
                                }
                            }
                            //} else {
                            //  textBox1.Text += Environment.NewLine + "A fájl nevének 'Eseti.exe' -nek kell lennie!";
                            // DialogResult menu_res = MessageBox.Show("A fájl nevének 'Eseti.exe' -nek kell lennie!", "Hiba", MessageBoxButtons.OK);
                            // if (menu_res == DialogResult.OK) {
                            //     Bezar();
                            // } else {
                            //     Bezar();
                            // }
                            // }
                        } catch (Exception err) {
                            textBox1.Text += Environment.NewLine + "Hiba: " + err;
                            MessageBox.Show(err.ToString());
                            Bezar();
                        }
                    }

                    // date & time
                    else if (cmd == "time" || cmd == "date") {
                        awnser += DateTime.Now.ToString();
                    }

                    // print
                    else if (cmd.StartsWith("print")) {
                        string printedtext = cmd.Replace("print", null);
                        if (cmd == "print" || cmd == "print ") {
                            return;
                        } else {
                            printedtext = cmd.Replace("print ", "");
                            awnser += printedtext;
                        }
                    }

                    //Bezárás

                    else if (cmd == "close") {
                        awnser += "Biztos be akarod zárni a fejlesztői konzolt? (i/n)";
                        bezar_req_yes_no = 1;
                    }

                    else if (bezar_req_yes_no == 1) {
                        if (cmd == "i") {
                            bezar_req_yes_no = 0;
                            Close();
                        } else {
                            bezar_req_yes_no = 0;
                            return;
                        }
                    }

                    // help
                    else if (cmd == "help") {
                        awnser += "Parancsok";
                        awnser += Environment.NewLine + "-------------------------------------------";
                        awnser += Environment.NewLine + "start";
                        awnser += Environment.NewLine + "print";
                        awnser += Environment.NewLine + "time";
                        awnser += Environment.NewLine + "date";
                        awnser += Environment.NewLine + "clear";
                        awnser += Environment.NewLine + "close";
                    }

                    // Unknown command!
                    else {
                        awnser += "Nincs ilyen parancs (" + cmd + ")! 'help' a parancsok listázásohoz.";
                    }

                    // Awnser
                    if (!(textBox1.Text == "")) {
                        textBox1.Text += Environment.NewLine + awnser;
                    } else {
                        textBox1.Text += awnser;
                    }

                    // clear
                    if (cmd == "clear") {
                        textBox1.Text = "";
                    }

                }
            }
        }
    }
}
