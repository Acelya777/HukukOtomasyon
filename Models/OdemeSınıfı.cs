using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HukukOtomasyon.Models
{
    public class OdemeSınıfı
    {
        public int OdemeID { get; set; }
        public string MuvekkilID {  get; set; }
        public string İşlem_Türü {  get; set; }
        public string İşlem_Açıklaması { get; set; }
        public string İşlem_Tarihi {  get; set; }
        public string Tutar {  get; set; }
    }
}
