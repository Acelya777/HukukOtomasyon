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

namespace HukukOtomasyon.Hakim
{
    public partial class HakimIslemleri : Form
    {
        public HakimIslemleri()
        {
            InitializeComponent();
            ValueFind();
        }

        private List<HakimModel> MsSqlFindAll()
        {
            List<HakimModel> hakimModels = new List<HakimModel>();
            try
            {
                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

                using (SqlConnection connection = services.ConnectDatabase())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Hakim", connection);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hakimModels.Add(new HakimModel
                            {
                                HakimID = Convert.ToInt32(reader["HakimID"]),
                                H_Ad = reader["H_Ad"].ToString(),
                                H_Soyad = reader["H_Soyad"].ToString(),
                                TC_Num = reader["TC_Num"].ToString(),
                                Sicil_Num = reader["Sicil_Num"].ToString(),
                                Telefon_Num = reader["Telefon_Num"].ToString(),
                                E_posta = reader["E_posta"].ToString(),
                                Adres = reader["Adres"].ToString(),

                            });

                        }

                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return hakimModels;
        }

        public void MsSqlUpdate(HakimModel hakimModel)
        {
            try
            {
                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

                using (SqlConnection connection = services.ConnectDatabase())
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Hakim SET H_Ad = @H_Ad, H_Soyad = @H_Soyad, TC_Num = @TC_Num ,Sicil_Num=@Sicil_Num,Telefon_Num=@Telefon_Num,E_posta = @E_posta, Adres = @Adres WHERE HakimID = @HakimID", connection))
                    {
                        cmd.Parameters.AddWithValue("@HakimID", hakimModel.HakimID);
                        cmd.Parameters.AddWithValue("@H_Ad", hakimModel.H_Ad);
                        cmd.Parameters.AddWithValue("@H_Soyad", hakimModel.H_Soyad);
                        cmd.Parameters.AddWithValue("@TC_Num", hakimModel.TC_Num);
                        cmd.Parameters.AddWithValue("@Sicil_Num", hakimModel.Sicil_Num);
                        cmd.Parameters.AddWithValue("@Telefon_Num", hakimModel.Telefon_Num);
                        cmd.Parameters.AddWithValue("@E_posta", hakimModel.E_posta);
                        cmd.Parameters.AddWithValue("@Adres", hakimModel.Adres);

                        connection.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                            Console.WriteLine("Güncelleme başarılı.");
                        else
                            Console.WriteLine("Güncelleme yapılamadı.");
                    }

                }



            }
            catch (Exception ex)
            {
             Console.WriteLine(ex.ToString());
            }
        }

        private void ValueFind()
        {
            dataGridView1.DataSource = MsSqlFindAll();
        }

        private void MsSqlDelete(int hakimID)
        {
            HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

            using (SqlConnection connection = services.ConnectDatabase())
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Dava WHERE hakimID = @hakimID;", connection))
                {
                    cmd.Parameters.AddWithValue("@hakimID", hakimID);
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM Hakim WHERE hakimID = @hakimID";
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Silme işlemi başarılı.");
                    }
                    else
                    {
                        MessageBox.Show("Silme işlemi başarısız.");
                    }
                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HakimEkle hakimEkle = new HakimEkle();
            hakimEkle.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow || row.Cells["HakimID"].Value == null) continue; // Yeni, boş veya ID'si olmayan satırları atla

                HakimModel guncelHakim = new HakimModel
                {
                    HakimID = Convert.ToInt32(row.Cells["HakimID"].Value),
                    H_Ad = Convert.ToString(row.Cells["H_Ad"].Value),
                    H_Soyad = Convert.ToString(row.Cells["H_Soyad"].Value),
                    TC_Num = Convert.ToString(row.Cells["TC_Num"].Value),
                    Sicil_Num = Convert.ToString(row.Cells["Sicil_Num"].Value),
                    Telefon_Num = Convert.ToString(row.Cells["Telefon_Num"].Value),
                    E_posta = Convert.ToString(row.Cells["E_posta"].Value),
                    Adres = Convert.ToString(row.Cells["Adres"].Value),
                };

                MsSqlUpdate(guncelHakim);
                HakimIslemleri hakimIslemleri = new HakimIslemleri();
                hakimIslemleri.Show();
                this.Hide();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Silme"].Index)
            {
                int avukatID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["hakimID"].Value);

                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

                using (SqlConnection connection = services.ConnectDatabase())
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Dava WHERE hakimID = @hakimID;", connection))
                    {
                        connection.Open();

                        cmd.Parameters.AddWithValue("@hakimID", avukatID);
                        cmd.CommandText = "DELETE FROM Hakim WHERE hakimID = @hakimID";


                        int affectedRows = cmd.ExecuteNonQuery();


                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Silme işlemi başarılı.");
                            HakimIslemleri hakimIslemleri = new HakimIslemleri();
                            hakimIslemleri.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Silme işlemi başarısız.");
                        }
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
