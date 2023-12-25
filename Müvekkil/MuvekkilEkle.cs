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
    public partial class MuvekkilEkle : Form
    {
        public MuvekkilEkle()
        {
            InitializeComponent();
            ComboBox();
        }

        public void ComboBox()
        {
            cinsiyet.Items.Add("Kadın");
            cinsiyet.Items.Add("Erkek");
        }

        private void MuvekkilAdd()
        {
            try { 
            HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

                using (SqlConnection connection = services.ConnectDatabase())
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Muvekkil(Ad, Soyad, TC_Num, Dogum_Tarihi, Cinsiyet, Telefon_Num, E_posta, Adres) VALUES (@Param1, @Param2,@Param3, @Param4,@Param5, @Param6,@Param7, @Param8)", connection))
                    {
                        cmd.Parameters.AddWithValue("@Param1", ad.Text);
                        cmd.Parameters.AddWithValue("@Param2", soyad.Text);
                        cmd.Parameters.AddWithValue("@Param3", TC.Text);
                        cmd.Parameters.AddWithValue("@Param4", dogumTarihi.Text);
                        cmd.Parameters.AddWithValue("@Param7", eposta.Text);
                        cmd.Parameters.AddWithValue("@Param8", adres.Text);

                        
                        string telefonNum = telNum.Text;
                        if (int.TryParse(telefonNum, out int result)) {
                            if (telefonNum.Length >= 2 && telefonNum.Substring(0, 2) == "05")
                            {
                                cmd.Parameters.AddWithValue("@Param6", telefonNum);
                            }
                            else
                            {
                                MessageBox.Show("İlk 2 hanesi 05 ile başlamalıdır!");
                            }
                        } else
                        {
                            MessageBox.Show("Sadece rakam giriniz!");
                        }

                        string seciliCinsiyet =  cinsiyet.SelectedItem as string;

                        if (seciliCinsiyet == "Kadın")
                        {
                            string secilenCinsiyet = "K";
                            cmd.Parameters.AddWithValue("@Param5", secilenCinsiyet);

                        }
                        else if (seciliCinsiyet == "Erkek")
                        {
                            string secilenCinsiyet = "E";
                            cmd.Parameters.AddWithValue("@Param5", secilenCinsiyet);

                        }
                        
                       
                        connection.Open();
                        int affectedRows = cmd.ExecuteNonQuery(); 

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Müvekkil başarıyla eklendi.");
                        }
                        else
                        {
                            MessageBox.Show("Müvekkil eklenirken bir hata oluştu.");
                        }

                    }
                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    
                }

}

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void MuvekkilEkle_Load(object sender, EventArgs e)
        {
            
        }

        private void ad_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MuvekkilAdd();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MuvekkilIslemleri muvekkilIslemleri = new MuvekkilIslemleri();
            muvekkilIslemleri.Show();
            this.Hide();
        }
    }
}
