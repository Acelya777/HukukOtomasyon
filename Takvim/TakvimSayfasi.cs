using HukukOtomasyon.DavaYönetimi;
using HukukOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HukukOtomasyon.Takvim
{
    public partial class TakvimSayfasi : Form
    {
        public TakvimSayfasi()
        {
            InitializeComponent();
            ValueFind();
        }
        private void ValueFind()
        {
            dataGridView1.DataSource = MsSqlFindAll();
        }
        private List<AvukatTakvim> MsSqlFindAll()
        {
            try
            {
                List<AvukatTakvim> avukatTakvims = new List<AvukatTakvim>();


                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();
                using (SqlConnection connection = services.ConnectDatabase())
                {
                    SqlCommand cmd = new SqlCommand("SELECT AvukatTakvim.*, Mahkeme.Mahkeme_Adı, Mahkeme.Mahkeme_Adresi, Avukat.A_Ad, Avukat.A_Soyad FROM AvukatTakvim " +
                        "JOIN Mahkeme ON AvukatTakvim.Mahkeme_Adi = Mahkeme.mahkemeID " +
                        "JOIN Avukat ON AvukatTakvim.AvukatID = Avukat.AvukatID; ", connection);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            avukatTakvims.Add(new AvukatTakvim
                            {
                                TakvimID = Convert.ToInt32(reader["TakvimID"]),
                                Başlik = reader["Başlik"].ToString(),
                                Aciklama = reader["Aciklama"].ToString(),
                                Başlangic_Tarihi_ve_Saati = reader["Başlangic_Tarihi_ve_Saati"].ToString(),
                                Bitis_Tarihi_ve_Saati = reader["Bitis_Tarihi_ve_Saati"].ToString(),
                                Mahkeme_Adı = reader["Mahkeme_Adı"].ToString(),
                                Mahkeme_Adresi = reader["Mahkeme_Adresi"].ToString(),
                                A_Ad = reader["A_Ad"].ToString(),
                                A_Soyad = reader["A_Soyad"].ToString(),
                            });

                        }

                    }
                    return avukatTakvims;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }
    }
}
