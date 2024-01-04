using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HukukOtomasyon.Models
{
    public class AvukatTakvim
    {
        public int TakvimID { get; set; }
        public string Başlik { get; set;}
        public string Aciklama { get; set;}
        public string Başlangic_Tarihi_ve_Saati { get; set;}
        public string Bitis_Tarihi_ve_Saati { get; set; }
        public string Mahkeme_Adı { get; set; }
        public string Mahkeme_Adresi { get; set; }
        public string A_Ad { get; set; }
        public string A_Soyad { get; set; }


    }
}
