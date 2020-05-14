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
    public partial class YeniKategori : ContentPage
    {
        public Kategori _kategori;
        public YeniKategori(Kategori kategori)
        {
            InitializeComponent();
            dtKayitTarihi.Date = DateTime.Now;
            _kategori = kategori;

            if(_kategori.Id>0)
            {
                txtAciklama.Text = _kategori.Aciklama;
                txtKategoriAdi.Text = _kategori.KategoriAdi;
                dtKayitTarihi.Date = _kategori.KayitTarihi;
                if (_kategori.Tipi == 0)
                    rbtnGelir.IsChecked = true;
                else
                    rbtnGider.IsChecked = true;
            }
        }

        async void brBtnVazgec_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void brBtnKaydet_Clicked(object sender, EventArgs e)
        {
            Kategori k = new Kategori();
            k.Aciklama = txtAciklama.Text;
            k.KategoriAdi = txtKategoriAdi.Text;
            k.KayitTarihi = dtKayitTarihi.Date;

            if (rbtnGelir.IsChecked.Value)
                k.Tipi = 0;
            else
                k.Tipi = 1;

            if (_kategori.Id > 0)
            {
                k.Id = _kategori.Id;
                await App.Database.UpdateModelAsync(k);
            }
            else
            {
                await App.Database.SaveModelAsync(k);

            }
            List<Kategori> ktgrList = await App.Database.GetKategoriList();
            foreach (var item in ktgrList)
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
            MessagingCenter.Send<List<Kategori>>(ktgrList, "AddItem");
            await Navigation.PopModalAsync();
        }

        private void rbtnGelir_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {

        }
    }
}