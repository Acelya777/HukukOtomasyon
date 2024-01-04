using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HukukOtomasyon.DavaYönetimi
{
    public partial class YönetimSayfası : Form
    {
        public YönetimSayfası()
        {
            InitializeComponent();
        }

        private void YönetimSayfası_Load(object sender, EventArgs e)
        {

        }

        private void OnMahkame(object sender, EventArgs e)
        {
            Mahkeme mahkeme = new Mahkeme();
            mahkeme.Show();
            this.Hide();
        }

        private void OnDosya(object sender, EventArgs e)
        {
        }

        private void OnDava(object sender, EventArgs e)
        {
            Dava dava = new Dava();
            dava.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }
    }
}
