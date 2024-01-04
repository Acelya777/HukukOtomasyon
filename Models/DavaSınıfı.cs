using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HukukOtomasyon.Models
{
    public class DavaSınıfı
    {
        public int DavaID { get; set; }
        public string MuvekkilID { get; set; }
        public String Dava_Türü { get; set; }
        public String Dava_Açılış_Tarihi { get; set; }
        public String Duruşma_Tarihler { get; set; }
        public String Ad {  get; set; }
        public String Soyad { get; set; }
    }
}
