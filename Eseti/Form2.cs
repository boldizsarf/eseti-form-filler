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

namespace Eseti {
    public partial class Form2 : Form {

        public string F2_Type { get; set; }

        // Appdata mappa
        string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public Form2() {
            InitializeComponent();
            if (File.Exists(appdata + "/eseti/dark.eseti")) {
                BackColor = Color.DimGray;
                ForeColor = Color.LightGray;
                textBox1.BackColor = Color.DimGray;
            }
        }

        private void Form2_Load(object sender, EventArgs e) {
            switch (F2_Type) {

                // Segítség
                case "help":
                    Text = "Segítség";
                    using (WebClient client = new WebClient()) {
                        textBox1.Text = client.DownloadString("http://air360.hu/app/eseti/help.eseti");
                    }
                    break;

                // Frissítési előzmények
                case "updates":
                    Text = "Frissítési előzmények";
                    using (WebClient client = new WebClient()) {
                        textBox1.Text = client.DownloadString("http://air360.hu/app/eseti/updates.eseti");
                    }
                    break;
            }
        }
    }
}
