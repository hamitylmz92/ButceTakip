using ButceTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ButceTakip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Giderler : ContentPage
    {
        public List<GiderIslem> lstGider;
        public static int dtStartVal=0;
        public Giderler()
        {
            InitializeComponent();
            DateTime dtAyIlkGun = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Ay ilk günü
            DateTime dtAySonGun = dtAyIlkGun.AddMonths(1).AddDays(-1);// Ay son günü
            dtIlk.Date = dtAyIlkGun;
            dtSon.Date = dtAySonGun;
            dtIlk.DateSelected += dt_DateSelected;
            dtSon.DateSelected += dt_DateSelected;
            DataGetir();
        }
        public async void DataGetir()
        {
            lstGider= await App.Database.GetGiderIslemList();
            lstGiderler.ItemsSource = lstGider.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date); //await App.Database.GetGiderIslemList();
            psGider.ItemsSource = lstGider.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date);
            MessagingCenter.Subscribe<List<GiderIslem>>(this, "YeniGider", async (obj) =>
            {
                lstGiderler.ItemsSource = obj.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date);
            });
            lblToplamGider.Text = lstGider.Where(x => x.KayitTarihi >= dtIlk.Date && x.KayitTarihi <= dtSon.Date).Sum(x => x.Deger).ToString("c");

        }
        async void barBtnYeni_Clicked(object sender, EventArgs e)
        {
            GiderIslem gider = new GiderIslem();
            await Navigation.PushModalAsync(new NavigationPage(new YeniGider(gider)));
        }
        async void lstGiderler_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            Syncfusion.ListView.XForms.SfListView view = (Syncfusion.ListView.XForms.SfListView)sender;
            GiderIslem gider = (GiderIslem)view.SelectedItem;
            await Navigation.PushModalAsync(new NavigationPage(new YeniGider(gider)));
            view.SelectedItem = null;
        }

        async void btnSil_Clicked(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            bool respons = await DisplayAlert("Gider", "Gider silinsin mi?", "Tamam", "Vazgeç");

            if (respons)
            {
                GiderIslem gider = (GiderIslem)btn.CommandParameter;
                await App.Database.DeleteGideriAsync(gider, gider.Id);
                DataGetir();
            }
        }

        private void dt_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                DataGetir();
            }
            catch (Exception ex)
            {
                DisplayAlert("Hata:", ex.ToString(), "Tamam");
            }
        }
    }
}