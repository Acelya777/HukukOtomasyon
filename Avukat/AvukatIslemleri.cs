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

namespace HukukOtomasyon.Avukat
{
    public partial class AvukatIslemleri : Form
    {
        public AvukatIslemleri()
        {
            InitializeComponent();
            ValueFind();
        }

        private List<AvukatModel> MsSqlFindAl()
        {
            List<AvukatModel> avukatModels = new List<AvukatModel>();
            try
            {
                
                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();
                using (SqlConnection connection = services.ConnectDatabase())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Avukat", connection);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            avukatModels.Add(new AvukatModel
                            {
                                AvukatID = Convert.ToInt32(reader["AvukatID"]),
                                A_Ad = reader["A_Ad"].ToString(),
                                A_Soyad = reader["A_Soyad"].ToString(),
                                TC_Num = reader["TC_Num"].ToString(),
                                Baro_Num = reader["Baro_Num"].ToString(),
                                Telefon_Num = reader["Telefon_Num"].ToString(),
                                E_posta = reader["E_posta"].ToString(),
                                Adres = reader["Adres"].ToString(),
                            });

                        }

                    }
                }
                
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return avukatModels;
        }

        private void ValueFind()
        {
            dataGridView1.DataSource = MsSqlFindAl();
        }

        private void MsSqlUpdate(AvukatModel avukatModel)
        {
            HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

            using (SqlConnection connection = services.ConnectDatabase())
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Avukat SET A_Ad = @A_Ad, A_Soyad = @A_Soyad, TC_Num = @TC_Num ,Baro_Num =@Baro_Num,E_posta = @E_posta, Adres = @Adres WHERE AvukatID = @AvukatID", connection))
                {
                    cmd.Parameters.AddWithValue("@AvukatID", avukatModel.AvukatID);
                    cmd.Parameters.AddWithValue("@A_Ad", avukatModel.A_Ad);
                    cmd.Parameters.AddWithValue("@A_Soyad", avukatModel.A_Soyad);
                    cmd.Parameters.AddWithValue("@TC_Num", avukatModel.TC_Num);
                    cmd.Parameters.AddWithValue("@Baro_Num", avukatModel.Baro_Num);
                    cmd.Parameters.AddWithValue("@E_posta", avukatModel.E_posta);
                    cmd.Parameters.AddWithValue("@Adres", avukatModel.Adres);

                    connection.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        Console.WriteLine("Güncelleme başarılı.");
                    else
                        Console.WriteLine("Güncelleme yapılamadı.");
                }
            }

        }

        public void MsSqlDelete(int  avukatID)
        {
            HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

            using (SqlConnection connection = services.ConnectDatabase())
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Dava WHERE AvukatID = @AvukatID;", connection))
                {
                    cmd.Parameters.AddWithValue("@avukatID", avukatID);
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM Avukat WHERE AvukatID = @AvukatID";
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

        private void button1_Click(object sender, EventArgs e)
        {
            AvukatEkle avukatEkle = new AvukatEkle();
            avukatEkle.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow || row.Cells["AvukatID"].Value == null) continue;

                AvukatModel guncelAvukat = new AvukatModel
                {
                    AvukatID = Convert.ToInt32(row.Cells["AvukatID"].Value),
                    A_Ad = Convert.ToString(row.Cells["A_Ad"].Value),
                    A_Soyad = Convert.ToString(row.Cells["A_Soyad"].Value),
                    TC_Num = Convert.ToString(row.Cells["TC_Num"].Value),
                    Baro_Num = Convert.ToString(row.Cells["Baro_Num"].Value),
                    E_posta = Convert.ToString(row.Cells["E_posta"].Value),
                    Adres = Convert.ToString(row.Cells["Adres"].Value),
                };
                MsSqlUpdate(guncelAvukat);
                AvukatIslemleri avukatIslemleri= new AvukatIslemleri();
                avukatIslemleri.Show();
                this.Hide();
            } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string avukatID = avukatId.Text;
            Console.WriteLine(avukatID.ToString());
            int avukatIdInt;

            if (int.TryParse(avukatID, out avukatIdInt))
            {

                MsSqlDelete(avukatIdInt);
                AvukatIslemleri avukatIslemleri = new AvukatIslemleri();
                avukatIslemleri.Show();
                this.Hide();
            }
            else
            {
                // Dönüşüm başarısız, hata mesajı göster
                MessageBox.Show("Giriş dizesi doğru biçimde değil. Lütfen geçerli bir MuvekkilID giriniz.");
            }
        }

        private void avukatID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
