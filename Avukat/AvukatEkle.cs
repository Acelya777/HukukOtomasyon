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
    public partial class AvukatEkle : Form
    {
        public AvukatEkle()
        {
            InitializeComponent();
        }

        public void AvukatAdd()
        {
            try
            {
                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();
                using (SqlConnection connection = services.ConnectDatabase())
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Avukat(A_Ad, A_Soyad, TC_Num, Baro_Num, Telefon_Num, E_posta, Adres) VALUES (@Param1, @Param2,@Param3, @Param4,@Param5, @Param6,@Param7)", connection))
                    {
                        cmd.Parameters.AddWithValue("@Param1", ad.Text);
                        cmd.Parameters.AddWithValue("@Param2", soyad.Text);
                        cmd.Parameters.AddWithValue("@Param3", TC.Text);               
                        cmd.Parameters.AddWithValue("@Param6", eposta.Text);
                        cmd.Parameters.AddWithValue("@Param7", adres.Text);

                        string telefonNum = telNum.Text;
                        string baroNum = baro_Num.Text;
                        if (int.TryParse(telefonNum, out int result)&&
                            int.TryParse(baroNum, out int result1))
                        {
                            cmd.Parameters.AddWithValue("@Param4", baroNum);
                            if (telefonNum.Length >= 2 && telefonNum.Substring(0, 2) == "05")
                            {
                                cmd.Parameters.AddWithValue("@Param5", telefonNum);
                            }
                            else
                            {
                                MessageBox.Show("İlk 2 hanesi 05 ile başlamalıdır!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Telefon ve Sicil Numaraları rakam olmalı!");
                        }

                        connection.Open();
                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Avukat başarıyla eklendi.");
                        }
                        else
                        {
                            MessageBox.Show("Avukat eklenirken bir hata oluştu.");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            AvukatIslemleri avukatIslemleri = new AvukatIslemleri();
            avukatIslemleri.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                AvukatAdd();
            AvukatIslemleri avukatIslemleri = new AvukatIslemleri();
            avukatIslemleri.Show();
            this.Hide();
        }
    }
}
