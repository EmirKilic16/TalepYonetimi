using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalepYonetimi.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        public DateTime? SonGuncellemeTarihi { get; set; }
        public bool AktifMi { get; set; } = true;
        public bool SilindiMi { get; set; }
    }
}
