using SQLite;

namespace ButceTakip.Models
{
    public class Gider
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public float Tutar { get; set; }

    }
}
