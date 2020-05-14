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
    public partial class YeniGider : ContentPage
    {
        public GiderIslem _gider;
        public YeniGider(GiderIslem gider=null)
        {
            InitializeComponent();
            DataGetir();
            dtKayitTarihi.Date = DateTime.Now;
            if (gider == null)
                gider = new GiderIslem();
            _gider = gider;

            if (_gider.Id > 0)
            {
                txtAciklama.Text = _gider.Aciklama;
                dtKayitTarihi.Date = _gider.KayitTarihi;
                txtGider.Value = _gider.Deger;
            }
        }
        public async void DataGetir()
        {
            cmbKategori.DataSource = await App.Database.GetKategoriList(1);
        }
        async void brBtnVazgec_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void brBtnKaydet_Clicked(object sender, EventArgs e)
        {
            try
            {
                GiderIslem gider = new GiderIslem();
                gider.Aciklama = txtAciklama.Text;
                decimal deger = Convert.ToDecimal(txtGider.Value);
                gider.Deger = deger;
                gider.GelirGiderTuruId = Convert.ToInt32(cmbKategori.SelectedValue);
                gider.KayitTarihi = dtKayitTarihi.Date;
                gider.KullaniciId = 0;
                if(gider.Deger<=0 && gider.GelirGiderTuruId<=0)
                {
                     DisplayAlert("Uyarı","Kategori seçimi ve Gider tutarı giriniz","Tamam");
                    return;
                }

                if (_gider.Id > 0)
                {
                    gider.Id = _gider.Id;
                    await App.Database.UpdateModelAsync(gider);
                }
                else
                {
                    await App.Database.SaveModelAsync(gider);
                }

                List<GiderIslem> giderIslem = await App.Database.GetGiderIslemList();
                MessagingCenter.Send<List<GiderIslem>>(giderIslem, "YeniGider");
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {

               await DisplayAlert("Hata", ex.ToString(), "Tamam");
            }
        }
    }
}