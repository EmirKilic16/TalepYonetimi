using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalepYonetimi.Domain.Entities
{
    public class Talep : BaseEntity
    {
        public string Baslik {  get; set; }

        public string Aciklama { get; set; }

        public int KullaniciId { get; set; } 
        public Kullanici Kullanici { get; set; }

    }
}
