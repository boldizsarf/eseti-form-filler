using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eseti {
    public partial class Form3 : Form {

        // Appdata mappa
        string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public Form3() {
            InitializeComponent();
            pictureBox1.BackgroundImage = Image.FromFile(appdata + "/eseti/alap.eseti");
        }

        private void Form3_Load(object sender, EventArgs e) {
            if (File.Exists(appdata + "/eseti/dark.eseti")) {
                BackColor = Color.DimGray;
                ForeColor = Color.LightGray;
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e) {
            Hide();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
        }

        private void Form3_ResizeBegin(object sender, EventArgs e) {
            //Size = new Size(365, 527);
        }
    }
}
