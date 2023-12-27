using HukukOtomasyon.Avukat;
using HukukOtomasyon.DavaYönetimi;
using HukukOtomasyon.Hakim;
using HukukOtomasyon.Odeme;
using HukukOtomasyon.Takvim;
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

        private void Anasayfa_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AvukatIslemleri avukatIslemleri = new AvukatIslemleri();
            avukatIslemleri.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HakimIslemleri hakimIslemleri=new HakimIslemleri();
            hakimIslemleri.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            YönetimSayfası yönetimSayfası = new YönetimSayfası();
            yönetimSayfası.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OdemeSayfasi odeme = new OdemeSayfasi();
            odeme.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TakvimSayfasi takvim = new TakvimSayfasi();
            takvim.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
