using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButceTakip.Models
{
    public class Kullanici
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sife { get; set; }
    }
}
