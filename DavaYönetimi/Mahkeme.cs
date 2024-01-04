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

namespace HukukOtomasyon.DavaYönetimi
{
    public partial class Mahkeme : Form
    {
        public Mahkeme()
        {
            InitializeComponent();
            ValueFind();
        }

        private void ValueFind()
        {
            dataGridView1.DataSource =  MsSqlFindAll();
        }

        private List<MahkemeSınıfı> MsSqlFindAll()
        {
            try
            {
                List<MahkemeSınıfı> mahkeme = new List<MahkemeSınıfı>();


                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();
                using (SqlConnection connection = services.ConnectDatabase())
                {
                    SqlCommand cmd = new SqlCommand("SELECT Mahkeme.*, Hakim.H_Ad,Hakim.H_Soyad,Hakim.E_posta FROM Mahkeme JOIN Hakim ON Mahkeme.Mahkeme_Başkanı = Hakim.HakimID;", connection);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mahkeme.Add(new MahkemeSınıfı
                            {
                                MahkemeID = Convert.ToInt32(reader["MahkemeID"]),
                                Mahkeme_Adı = reader["Mahkeme_Adı"].ToString(),
                                Mahkeme_Türü = reader["Mahkeme_Türü"].ToString(),
                                Mahkeme_Adresi = reader["Mahkeme_Adresi"].ToString(),
                                H_Ad = reader["H_Ad"].ToString(),
                                H_Soyad = reader["H_Soyad"].ToString(),
                                E_posta = reader["E_posta"].ToString(),
                            });

                        }

                    }
                    return mahkeme;
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

        private void Mahkeme_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            YönetimSayfası yönetimSayfası = new YönetimSayfası();
            yönetimSayfası.Show();
            this.Hide();
        }
    }
}
