using HukukOtomasyon.Models;
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

namespace HukukOtomasyon.Odeme
{
    public partial class OdemeSayfasi : Form
    {
        public OdemeSayfasi()
        {
            InitializeComponent();
            ValueFind();
        }

        private List<OdemeSınıfı> MsSqlFindAll()
        {
            List<OdemeSınıfı> odemeler = new List<OdemeSınıfı>();
            try
            {
                HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();

                using (SqlConnection connection = services.ConnectDatabase())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM OdemeTablosu", connection);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            odemeler.Add(new OdemeSınıfı
                            {
                                OdemeID = Convert.ToInt32(reader["OdemeID"]),
                                MuvekkilID = reader["MuvekkilID"].ToString(),
                                İşlem_Türü = reader["İşlem_Türü"].ToString(),
                                İşlem_Açıklaması = reader["İşlem_Açıklaması"].ToString(),
                                İşlem_Tarihi = reader["İşlem_Tarihi"].ToString(),
                                Tutar = reader["Tutar"].ToString(),
                            });

                        }
                    }
                    return odemeler;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        private void ValueFind()
        {
            dataGridView1.DataSource = MsSqlFindAll();
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["DelColumn"].Index)
            {
                
                int odemeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["OdemeID"].Value);
               
                   
                    HukukOtomasyon.Service.Services services = new HukukOtomasyon.Service.Services();
                    // SQL sorgusunu çalıştır
                    using (SqlConnection connection = services.ConnectDatabase())
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM OdemeTablosu WHERE OdemeID = @OdemeID;", connection))
                        {
                        cmd.Parameters.AddWithValue("@OdemeID", odemeID);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show($"İşlem gerçekleştirildi!");
                            OdemeSayfasi odemeSayfasi = new OdemeSayfasi();
                            odemeSayfasi.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show($"ID {odemeID} bulunamadı veya işleme alınamadı.");
                        }
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
    }
}
