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
    public partial class MuvekkilIslemleri : Form
    {
        public MuvekkilIslemleri()
        {
            InitializeComponent();
            MsSqlFindAll();
        }
        private void MsSqlFindAll()
        {
            // Tabloya veri eklemek için örnek bir DataTable kullanalım
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Ad", typeof(string));
            dataTable.Columns.Add("Soyad", typeof(string));

            // Örnek veri ekleyelim
            dataTable.Rows.Add(1, "John", "Doe");
            dataTable.Rows.Add(2, "Jane", "Doe");
            dataTable.Rows.Add(3, "Alice", "Smith");

            // DataGridView'a veriyi atayalım
            dataGridView1.DataSource = dataTable;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MuvekkilEkle muvekkilEkle = new MuvekkilEkle();
            muvekkilEkle.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }
    }
}
