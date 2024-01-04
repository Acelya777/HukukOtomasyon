using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HukukOtomasyon.Models
{
    public class MahkemeSınıfı
    {
        public int MahkemeID { get; set; }
        public String Mahkeme_Adı { get; set; }
        public String Mahkeme_Türü { get; set; }
        public String Mahkeme_Adresi { get; set; }
        public String H_Ad {  get; set; }
        public String H_Soyad { get; set; }
        public String E_posta { get; set; }
    }
}
