using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TalepYonetimi.Domain.Entities
{
    public class Kullanici : BaseEntity
    {
        public string Ad {  get; set; }
       
        public string Soyad { get; set; }

        public string Eposta { get; set; }

        public string SifreHash { get; set; }

        public ICollection<Talep> Talepler { get; set; } = new List<Talep>();


    }
}
