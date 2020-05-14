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
    public partial class Gelirler : ContentPage
    {
        public Gelirler()
        {
            InitializeComponent();
            DataGetir();
        }
        public async void DataGetir()
        {
            lstGelirler.ItemsSource = await App.Database.GetGelirIslemList();

            MessagingCenter.Subscribe<List<GelirIslem>>(this, "YeniGelir", async (obj) =>
            {
                lstGelirler.ItemsSource = obj;
            });
        }
        async void barBtnYeni_Clicked(object sender, EventArgs e)
        {
            GelirIslem gelir = new GelirIslem();
            await Navigation.PushModalAsync(new NavigationPage(new YeniGelir(gelir)));
        }

        async void lstGelirler_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            Syncfusion.ListView.XForms.SfListView view = (Syncfusion.ListView.XForms.SfListView)sender;
            GelirIslem gelir = (GelirIslem)view.SelectedItem;
            await Navigation.PushModalAsync(new NavigationPage(new YeniGelir(gelir)));
            view.SelectedItem = null;
        }

        async void btnSil_Clicked(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            bool respons = await DisplayAlert("Gelir", "Gelir silinsin mi?", "Tamam", "Vazgeç");

            if (respons)
            {
                GelirIslem gelir = (GelirIslem)btn.CommandParameter;
                await App.Database.DeleteGeliriAsync(gelir, gelir.Id);
                DataGetir();
            }
        }
    }
}