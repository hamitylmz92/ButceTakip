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
    public partial class YeniGelir : ContentPage
    {
        public GelirIslem _gelir;
        public YeniGelir(GelirIslem gelir=null)
        {
            InitializeComponent();
            DataGetir();
            dtKayitTarihi.Date = DateTime.Now;
            if (gelir == null)
                gelir = new GelirIslem();
            _gelir = gelir;

            if (_gelir.Id > 0)
            {
                txtAciklama.Text = _gelir.Aciklama;
                dtKayitTarihi.Date = _gelir.KayitTarihi;
                txtGelir.Value = _gelir.Deger;
            }
        }
        public async void DataGetir()
        {
            cmbKategori.DataSource = await App.Database.GetKategoriList(0);
        }

        private void cmbKategori_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }

        async void brBtnVazgec_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void brBtnKaydet_Clicked(object sender, EventArgs e)
        {
            try
            {
                GelirIslem gelir = new GelirIslem();
                gelir.Aciklama = txtAciklama.Text;
                decimal deger=Convert.ToDecimal(txtGelir.Value);
                gelir.Deger = deger;
                gelir.GelirGiderTuruId = Convert.ToInt32(cmbKategori.SelectedValue);
                gelir.KayitTarihi = dtKayitTarihi.Date;
                gelir.KullaniciId = 0;

                if (gelir.Deger <= 0 && gelir.GelirGiderTuruId <= 0)
                {
                    DisplayAlert("Uyarı", "Kategori seçimi ve Gelir tutarı giriniz", "Tamam");
                    return;
                }

                if (_gelir.Id > 0)
                {
                    gelir.Id = _gelir.Id;
                    await App.Database.UpdateModelAsync(gelir);
                }
                else
                {
                    await App.Database.SaveModelAsync(gelir);
                }

                List<GelirIslem> gelirIslem = await App.Database.GetGelirIslemList();
                MessagingCenter.Send<List<GelirIslem>>(gelirIslem, "YeniGelir");
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {

                DisplayAlert("Hata", ex.ToString(), "Tamam");
            }
            

        }
    }
}