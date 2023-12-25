using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HukukOtomasyon
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MuvekkilEkle muvekkilEkle = new MuvekkilEkle();
            //muvekkilEkle.Show();
            MuvekkilIslemleri muvekkil = new MuvekkilIslemleri();
            muvekkil.Show();
            this.Hide();
        }
    }
}
