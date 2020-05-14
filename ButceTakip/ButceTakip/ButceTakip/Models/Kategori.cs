using SQLite;
using System;
namespace ButceTakip.Models
{
    public class Kategori
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string KategoriAdi { get; set; }
        public string Aciklama { get; set; }
        public int KullaniciId { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int Tipi { get; set; }
        public string TipAciklama { get; set; }
        public string Color { get; set; }
    }
}
