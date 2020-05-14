using ButceTakip.Models;
using Plugin.LocalNotifications;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ButceTakip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Default : ContentPage
    {
        public decimal toplamGelir =0;
        public decimal toplamGider = 0;
        public List<Kategori> kList;
        public Default()
        {
            InitializeComponent();
            DateTime dtAyIlkGun = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Ay ilk günü
            DateTime dtAySonGun = dtAyIlkGun.AddMonths(1).AddDays(-1);// Ay son günü
            dtIlk.Date = dtAyIlkGun;
            dtSon.Date = dtAySonGun;
            DataGetir();

            CrossLocalNotifications.Current.Show("Butçe Takip","Bu bir deneme",10,DateTime.Now.AddMinutes(1));
        }
        public async void DataGetir()
        {
            List<GelirIslem> gelirList= await App.Database.GetGelirIslemList();
            List<GiderIslem> giderList = await App.Database.GetGiderIslemList();
            toplamGelir = gelirList.Where(x=>x.KayitTarihi>=dtIlk.Date && x.KayitTarihi<=dtSon.Date).Sum(x => x.Deger);
            toplamGider = giderList.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date).Sum(x => x.Deger);

            txtGelir.Text = toplamGelir.ToString("c");
            txtGider.Text = toplamGider.ToString("c");
            txtGenel.Text = (toplamGelir - toplamGider).ToString("c");

            kList = await App.Database.GetKategoriList();
            if (kList == null)
                kList = new List<Kategori>();

            lstGenel.ItemsSource = GenelList(
                gelirList.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date).ToList(),
                giderList.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date).ToList()
                );
        }

        private void btnSil_Clicked(object sender, EventArgs e)
        {

        }
        public ObservableCollection<GelirGiderIslem> GenelList(List<GelirIslem> gelirList, List<GiderIslem> giderList)
        {
            ObservableCollection<GelirGiderIslem> genelList = new ObservableCollection<GelirGiderIslem>();

            foreach (var item in gelirList)
            {
                GelirGiderIslem gg = new GelirGiderIslem();
                gg.Aciklama = item.Aciklama;
                gg.Deger = item.Deger;
                gg.KayitTarihi = item.KayitTarihi;
                gg.KayitTarihiStr = item.KayitTarihi.ToShortDateString();
                gg.KullaniciId = item.KullaniciId;
                gg.GelirGiderTuruId = item.GelirGiderTuruId;
                gg.KategoriAdi = kList.Find(x => x.Id == gg.GelirGiderTuruId).KategoriAdi;// await KategoriAdiGetir(gg.GelirGiderTuruId);
                gg.Resim = "GelirlerIcon.png";
                gg.Renk = "ForestGreen";
                gg.GelirGiderTuruAdi = "Gelir";
                genelList.Add(gg);
            }

            foreach (var item in giderList)
            {
                GelirGiderIslem gg = new GelirGiderIslem();
                gg.Aciklama = item.Aciklama;
                gg.Deger = item.Deger;
                gg.KayitTarihi = item.KayitTarihi;
                gg.KayitTarihiStr = item.KayitTarihi.ToShortDateString();
                gg.KullaniciId = item.KullaniciId;
                gg.GelirGiderTuruId = item.GelirGiderTuruId;
                gg.KategoriAdi = kList.Find(x => x.Id == gg.GelirGiderTuruId).KategoriAdi;
                gg.Resim = "GiderlerIcon.png";
                gg.Renk = "DarkRed";
                gg.GelirGiderTuruAdi = "Gider";
                genelList.Add(gg);
            }
            return genelList.OrderByDescending(x=>x.KayitTarihi).ToObservableCollection();
        }

        private void dt_Selected(object sender, DateChangedEventArgs e)
        {
            DataGetir();
        }
        public class GelirGiderIslem
        {
            public int Id { get; set; }
            public int GelirGiderTuruId { get; set; }
            public string Aciklama { get; set; }
            public decimal Deger { get; set; }
            public DateTime KayitTarihi { get; set; }
            public int KullaniciId { get; set; }
            public string GelirGiderTuruAdi { get; set; }
            public string Renk { get; set; }
            public string Resim { get; set; }
            public string KategoriAdi { get; set; }
            public string KayitTarihiStr { get; set; }
        }



        async void btnGelir_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<List<GelirIslem>>(this, "YeniGelir", async (obj) =>
            {
                DataGetir();
            });
            await Navigation.PushModalAsync(new NavigationPage(new YeniGelir(null)));
        }

        async void btnGider_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<List<GiderIslem>>(this, "YeniGider", async (obj) =>
            {
                DataGetir();
            });
            await Navigation.PushModalAsync(new NavigationPage(new YeniGider(null)));
        }
    }
}