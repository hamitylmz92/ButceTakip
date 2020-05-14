using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButceTakip.Models
{
    public class GiderIslem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int GelirGiderTuruId { get; set; }
        public string Aciklama { get; set; }
        public decimal Deger { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int KullaniciId { get; set; }
    }
}
