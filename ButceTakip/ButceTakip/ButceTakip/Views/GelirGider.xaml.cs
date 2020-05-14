using ButceTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ButceTakip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GelirGider : ContentPage
    {
        public List<GiderIslem> lstGider;
        public List<GelirIslem> lstGelir;
        //true:Gelir  false:Gider
        public static bool sayfaTuru;
        public GelirGider(bool value)
        {
            InitializeComponent();
            sayfaTuru = value;
            DateTime dtAyIlkGun = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Ay ilk günü
            DateTime dtAySonGun = dtAyIlkGun.AddMonths(1).AddDays(-1);// Ay son günü
            dtIlk.Date = dtAyIlkGun;
            dtSon.Date = dtAySonGun;
            dtIlk.DateSelected += dt_DateSelected;
            dtSon.DateSelected += dt_DateSelected;
          
            if (value)
                GelirDataGetir();
            else
                GiderDataGetir();
        }
        public async void GelirDataGetir()
        {
            Title = "Gelirler";
            lstGelir = await App.Database.GetGelirIslemList();
            lstGelirGider.ItemsSource = lstGelir.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date); //await App.Database.GetGiderIslemList();
            psGelirGider.ItemsSource = lstGelir.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date);
            MessagingCenter.Subscribe<List<GelirIslem>>(this, "YeniGelir", async (obj) =>
            {
                lstGelirGider.ItemsSource = obj.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date);
            });
            lblToplamDeger.Text = lstGelir.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date).Sum(x => x.Deger).ToString("c");
        }
        public async void GiderDataGetir()
        {
            Title = "Giderler";
            lstGider = await App.Database.GetGiderIslemList();
            lstGelirGider.ItemsSource = lstGider.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date); //await App.Database.GetGiderIslemList();
            psGelirGider.ItemsSource = lstGider.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date);
            MessagingCenter.Subscribe<List<GiderIslem>>(this, "YeniGider", async (obj) =>
            {
                lstGelirGider.ItemsSource = obj.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date);
            });
            lblToplamDeger.Text = lstGider.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date).Sum(x => x.Deger).ToString("c");
        }
        async void barBtnYeni_Clicked(object sender, EventArgs e)
        {
            if (sayfaTuru)
                await Navigation.PushModalAsync(new NavigationPage(new YeniGelir(null)));
            else
                await Navigation.PushModalAsync(new NavigationPage(new YeniGider(null)));
        }
        async void lstGelirGider_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            if (sayfaTuru)
            {
                Syncfusion.ListView.XForms.SfListView view = (Syncfusion.ListView.XForms.SfListView)sender;
                GelirIslem gelir = (GelirIslem)view.SelectedItem;
                await Navigation.PushModalAsync(new NavigationPage(new YeniGelir(gelir)));
                view.SelectedItem = null;
            }
            else
            {
                Syncfusion.ListView.XForms.SfListView view = (Syncfusion.ListView.XForms.SfListView)sender;
                GiderIslem gider = (GiderIslem)view.SelectedItem;
                await Navigation.PushModalAsync(new NavigationPage(new YeniGider(gider)));
                view.SelectedItem = null;
            }
        }

        async void btnSil_Clicked(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            bool respons = await DisplayAlert("Silme İşlemi", "Kayıt silinsin mi?", "Tamam", "Vazgeç");
            
            if (respons)
            {
                if (sayfaTuru)
                {
                    GelirIslem gelir = (GelirIslem)btn.CommandParameter;
                    await App.Database.DeleteGeliriAsync(gelir, gelir.Id);
                    GelirDataGetir();
                }
                else
                {
                    GiderIslem gider = (GiderIslem)btn.CommandParameter;
                    await App.Database.DeleteGideriAsync(gider, gider.Id);
                    GiderDataGetir();
                }
               
                
            }
        }
        private void dt_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                if (sayfaTuru)
                    GelirDataGetir();
                else
                    GiderDataGetir();
            }
            catch (Exception ex)
            {
                DisplayAlert("Hata:", ex.ToString(), "Tamam");
            }
        }


    }
}