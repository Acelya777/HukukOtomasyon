using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HukukOtomasyon.Service
{
    internal class Services
    {
        public SqlConnection ConnectDatabase()
        {
            string connectionString = @"Server= DESKTOP-DOQA6MR;Database=Hukuk; Trusted_Connection=True ";
            SqlConnection cnn = new SqlConnection(connectionString);
     
            return cnn;
        }
    }
}
