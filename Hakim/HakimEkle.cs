using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HukukOtomasyon.Hakim
{
    public partial class HakimEkle : Form
    {
        public HakimEkle()
        {
            InitializeComponent();
        }

        private void HakimAdd()
        {
            try
            {
                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

                using (SqlConnection connection = services.ConnectDatabase())
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Hakim(H_Ad, H_Soyad, TC_Num,Sicil_Num, Telefon_Num, E_posta, Adres) VALUES (@Param1, @Param2,@Param3, @Param4,@Param5, @Param6,@Param7)", connection))
                    {
                        cmd.Parameters.AddWithValue("@Param1", ad.Text);
                        cmd.Parameters.AddWithValue("@Param2", soyad.Text);
                        cmd.Parameters.AddWithValue("@Param3", TC.Text);
                        cmd.Parameters.AddWithValue("@Param6", eposta.Text);
                        cmd.Parameters.AddWithValue("@Param7", adres.Text);

                        string telefonNum = telNum.Text;
                        string sicilNum = SicilNum.Text;
                        if (int.TryParse(telefonNum, out int result)&&
                            int.TryParse(sicilNum, out int result1))
                        {
                            cmd.Parameters.AddWithValue("@Param4", SicilNum.Text);
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
                            MessageBox.Show("Hakim başarıyla eklendi.");
                        }
                        else
                        {
                            MessageBox.Show("Hakim eklenirken bir hata oluştu.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HakimIslemleri hakimIslemleri = new HakimIslemleri();
            hakimIslemleri.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HakimAdd();
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }
    }
}
