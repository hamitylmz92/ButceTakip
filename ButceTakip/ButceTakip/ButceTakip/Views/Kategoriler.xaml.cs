
using ButceTakip.Helper;
using ButceTakip.Models;
using Syncfusion.DataSource;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ButceTakip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Kategoriler : ContentPage
    {
        public List<Kategori> kategoriList;
        public Kategoriler()
        {
            InitializeComponent();
            DataGetir();
        }
        public async void DataGetir()
        {
            List<Kategori> kList=await App.Database.GetKategoriList();
            foreach (var item in kList)
            {
                if (item.Tipi == 0)
                {
                    item.TipAciklama = "Gelir";
                    item.Color = "ForestGreen";
                }
                else
                {
                    item.TipAciklama = "Gider";
                    item.Color = "DarkRed";
                }
            }
            lstKategori.ItemsSource = kList;

            MessagingCenter.Subscribe<List<Kategori>>(this, "AddItem", async (obj) =>
            {
                lstKategori.ItemsSource = obj;
            });

            //lstKategori.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            //{
            //    PropertyName = "TipAciklama",

            //});
            //Image img = new Image();
            //img.Source = "user.png";
            //lstKategori.GroupHeaderTemplate = new DataTemplate(() =>
            //{

            //    var grid = new Grid { BackgroundColor = Color.FromHex("#bf3f3f") };
            //    var column0 = new ColumnDefinition { Width = 30 };
            //    var column1 = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
            //    grid.ColumnDefinitions.Add(column0);
            //    grid.ColumnDefinitions.Add(column1);

            //    var image = new Image();
            //    Binding binding = new Binding("IsExpand");
            //    binding.Converter = new BoolToImageConverter();
            //    image.SetBinding(Image.SourceProperty, binding);

            //    //image.HeightRequest = 30;
            //    //image.WidthRequest = 30;
            //    //image.VerticalOptions = LayoutOptions.Center;
            //    //image.HorizontalOptions = LayoutOptions.Center;

            //    var label = new Label
            //    {
            //        FontAttributes = FontAttributes.Bold,
            //        FontSize = 25,
            //        VerticalOptions = LayoutOptions.Center,
            //        HorizontalOptions = LayoutOptions.Start,
            //        Margin = new Thickness(20, 0, 0, 0),
            //    };

            //    label.SetBinding(Label.TextProperty, new Binding("Key"));

            //    grid.Children.Add(img, 0, 0);
            //    grid.Children.Add(label, 1, 0);
            //    return grid;
            //});

            kategoriList = kList;
            //lstKategori.CollapseAll();
        }
        async void barBtnYeni_Clicked(object sender, EventArgs e)
        {
            Kategori kat = new Kategori();
            await Navigation.PushModalAsync(new NavigationPage(new YeniKategori(kat)));
        }

        async void lstKategori_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            Syncfusion.ListView.XForms.SfListView view = (Syncfusion.ListView.XForms.SfListView)sender;
            Kategori kategori = (Kategori)view.SelectedItem;
            await Navigation.PushModalAsync(new NavigationPage(new YeniKategori(kategori)));
            view.SelectedItem = null;
        }

        async void btnSil_Clicked(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            bool respons = await DisplayAlert("Kategori", "Kategori silinsin mi?", "Tamam", "Vazgeç");

            if (respons)
            {
                Kategori kat = (Kategori)btn.CommandParameter;
                await App.Database.DeleteKategoriAsync(kat, kat.Id);
                DataGetir();
            }
        }

        private void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (sender as SearchBar);
            List<Kategori> klist = kategoriList.Where(
                x => x.KategoriAdi.Contains(searchBar.Text.ToUpper()) || 
                x.KategoriAdi.Contains(searchBar.Text.ToLower())
                ).ToList();
            lstKategori.ItemsSource = klist;
        }
    }
}