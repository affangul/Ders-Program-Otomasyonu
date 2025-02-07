using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ders_Programı
{
    public partial class Arasayfa : Form
    {
        public Arasayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // bilgi düzenleme
        {
            Ana ana = new Ana();
            ana.Show();
        }

        private void button2_Click(object sender, EventArgs e) // ders programı oluşturma
        {
            DPOluşturma DPO = new DPOluşturma();
            DPO.Show();
        }

        private void button3_Click(object sender, EventArgs e) // ders programı çıktı
        {
            DersProgramı dp = new DersProgramı();
            dp.Show();
        }

        private void button4_Click(object sender, EventArgs e) // çıkış
        {
            this.Close();
        }
    }
}
