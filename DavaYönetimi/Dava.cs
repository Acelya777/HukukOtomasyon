using HukukOtomasyon.Models;
using HukukOtomasyon.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HukukOtomasyon.DavaYönetimi
{
    public partial class Dava : Form
    {
        public Dava()
        {
            InitializeComponent();
            ValueFind();
        }
        private void ValueFind()
        {
            dataGridView1.DataSource = MsSqlFindAll();
        }
        private List<DavaSınıfı> MsSqlFindAll()
        {
            try
            {
                List<DavaSınıfı> davaSınıfı = new List<DavaSınıfı>();


                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();
                using (SqlConnection connection = services.ConnectDatabase())
                {
                    SqlCommand cmd = new SqlCommand("SELECT Dava.*, Muvekkil.Ad,Muvekkil.Soyad FROM Dava JOIN Muvekkil ON Muvekkil.MuvekkilID = Dava.MuvekkilID;", connection);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            davaSınıfı.Add(new DavaSınıfı
                            {
                                DavaID = Convert.ToInt32(reader["DavaID"]),
                                MuvekkilID = reader["MuvekkilID"].ToString(),
                                Dava_Türü = reader["Dava_Türü"].ToString(),
                                Dava_Açılış_Tarihi = reader["Dava_Açılış_Tarihi"].ToString(),
                                Duruşma_Tarihler = reader["Duruşma_Tarihler"].ToString(),
                                Ad = reader["Ad"].ToString(),
                                Soyad = reader["Soyad"].ToString(),

                            });

                        }

                    }
                    return davaSınıfı;
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
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Dosya"].Index) 
            {
                int davaId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MuvekkilID"].Value);

                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

                using (SqlConnection connection = services.ConnectDatabase())
                {
                    SqlCommand cmd = new SqlCommand("SELECT DosyaVerisi FROM Dosya WHERE MuvekkilID = @MuvekkilID", connection);
                    cmd.Parameters.AddWithValue("@MuvekkilID", davaId);
                    connection.Open();

                    string base64Data = (string)cmd.ExecuteScalar();
                    if (!string.IsNullOrEmpty(base64Data))
                    {
                        byte[] imageBytes = Convert.FromBase64String(base64Data);
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                        }
                    }

                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            YönetimSayfası yönetimSayfası = new YönetimSayfası();
            yönetimSayfası.Show();
            this.Hide();
        }
    }
}
