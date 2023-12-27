using HukukOtomasyon.Service;
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
using HukukOtomasyon.Service;
using System.Reflection.Emit;

namespace HukukOtomasyon
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        public bool Login(string username, string password)
        {
            HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

            SqlConnection connection = services.ConnectDatabase();

            using (SqlConnection cnn = connection)
            {
                try
                {
                    cnn.Open();
                    string query = "SELECT COUNT(1) FROM GirisBilgileri WHERE kullanıcıAdi=@username AND şifre=@password";
                    SqlCommand command = new SqlCommand(query, cnn);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 1)
                    {
                        Anasayfa anasayfa = new Anasayfa();
                        anasayfa.Show();
                        Giris giris = new Giris();
                        this.Hide();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Giriş bilgileri yanlış!");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username= textBox1.Text;

            string password =textBox2.Text;

            Login(username, password);

        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.BackColor = System.Drawing.Color.Transparent;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {

        }

    }
}
