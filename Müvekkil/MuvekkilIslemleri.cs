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

namespace HukukOtomasyon
{
    public partial class MuvekkilIslemleri : Form
    {
        public MuvekkilIslemleri()
        {
            InitializeComponent();
            ValueFind();


        }
        
        private List<Muvekkil> MsSqlFindAll()
        {

            try
            {
                List<Muvekkil> muvekkiller = new List<Muvekkil>();
                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

                using (SqlConnection connection = services.ConnectDatabase())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Muvekkil", connection);
                    
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            muvekkiller.Add(new Muvekkil
                            {
                                MuvekkilID = Convert.ToInt32(reader["MuvekkilID"]),
                                Ad = reader["Ad"].ToString(),
                                Soyad = reader["Soyad"].ToString(),
                                TC_Num = reader["TC_Num"].ToString(),
                                Dogum_Tarihi = reader["Dogum_Tarihi"].ToString(),
                                Cinsiyet = reader["Cinsiyet"].ToString(),
                                Telefon_Num = reader["Telefon_Num"].ToString(),
                                E_posta = reader["E_posta"].ToString(),
                                Adres = reader["Adres"].ToString(),
                                
                        });
                            
                        }

                    }
                    

                    return muvekkiller;

                    //// DataGridView'a veriyi atayalım
                    //dataGridView1.DataSource = dataTable;
                }
                
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        private void ValueFind()
        {
            dataGridView1.DataSource = MsSqlFindAll();
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


        public void MsSqlUpdate(Muvekkil muvekkil)
        {
            try
            {
                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

                using (SqlConnection connection = services.ConnectDatabase())
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Muvekkil SET Ad = @Ad, Soyad = @Soyad, TC_Num = @TC_Num ,Cinsiyet=@Cinsiyet,Telefon_Num=@Telefon_Num,E_posta = @E_posta, Adres = @Adres WHERE MuvekkilId = @MuvekkilId", connection))
                    {
                        cmd.Parameters.AddWithValue("@MuvekkilID", muvekkil.MuvekkilID);
                        cmd.Parameters.AddWithValue("@Ad", muvekkil.Ad);
                        cmd.Parameters.AddWithValue("@Soyad", muvekkil.Soyad);
                        cmd.Parameters.AddWithValue("@TC_Num", muvekkil.TC_Num);
                        //cmd.Parameters.AddWithValue("@Dogum_Tarihi", muvekkil.Dogum_Tarihi);
                        cmd.Parameters.AddWithValue("@Cinsiyet", muvekkil.Cinsiyet);
                        cmd.Parameters.AddWithValue("@Telefon_Num", muvekkil.Telefon_Num);
                        cmd.Parameters.AddWithValue("@E_posta", muvekkil.E_posta);
                        cmd.Parameters.AddWithValue("@Adres", muvekkil.Adres);

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
        public void MsSqlDelete(int MuvekkilID)
        {
            HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

            using (SqlConnection connection = services.ConnectDatabase())
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Dava WHERE MuvekkilID = @MuvekkilID; DELETE FROM MuvekkilGelirGider WHERE MuvekkilID = @MuvekkilID;", connection))
                {
                    cmd.Parameters.AddWithValue("@MuvekkilID", MuvekkilID);
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM Muvekkil WHERE MuvekkilID = @MuvekkilID";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow || row.Cells["MuvekkilID"].Value == null) continue; // Yeni, boş veya ID'si olmayan satırları atla

                Muvekkil guncelMuvekkil = new Muvekkil
                {
                    MuvekkilID = Convert.ToInt32(row.Cells["MuvekkilID"].Value),
                    Ad = Convert.ToString(row.Cells["Ad"].Value),
                    Soyad = Convert.ToString(row.Cells["Soyad"].Value),
                    TC_Num = Convert.ToString(row.Cells["TC_Num"].Value),
                    //Dogum_Tarihi = Convert.ToString(row.Cells["Dogum_Tarihi"].Value),
                    Cinsiyet = Convert.ToString(row.Cells["Cinsiyet"].Value),
                    Telefon_Num = Convert.ToString(row.Cells["Telefon_Num"].Value),
                    E_posta = Convert.ToString(row.Cells["E_posta"].Value),
                    Adres = Convert.ToString(row.Cells["Adres"].Value),
                };

                MsSqlUpdate(guncelMuvekkil);
                
            }
            MuvekkilIslemleri muvekkilIslemleri = new MuvekkilIslemleri();
            muvekkilIslemleri.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string muvekkilID = textBox1.Text;
            Console.WriteLine(muvekkilID.ToString());
            int muvekkilIdInt;

            if (int.TryParse(muvekkilID, out muvekkilIdInt))
            {
                
                MsSqlDelete(muvekkilIdInt);
                MuvekkilIslemleri muvekkilIslemleri= new MuvekkilIslemleri();
                muvekkilIslemleri.Show();
                this.Hide();
            }
            else
            {
                // Dönüşüm başarısız, hata mesajı göster
                MessageBox.Show("Giriş dizesi doğru biçimde değil. Lütfen geçerli bir MuvekkilID giriniz.");
            }
        }
    }
}
