using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libreria
{
    public partial class Presentacion : Form
    {
        public Presentacion()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Presentacion_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            timer1.Stop();
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }
    }
}
