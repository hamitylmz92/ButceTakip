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
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();
            List<MenuList> menuList = new List<MenuList> {
            new MenuList{Id=0,MenuAdi="Anasayfa",Resim="homeIcon.png"},
            new MenuList{Id=0,MenuAdi="Özet",Resim="ozetIcon.png"},
            new MenuList{Id=1,MenuAdi="Gelirler",Resim="GelirlerIcon.png"},
            new MenuList{Id=2,MenuAdi="Giderler",Resim="GiderlerIcon.png"},
            new MenuList{Id=3,MenuAdi="Kategoriler",Resim="KategoriIcon.png"},
            new MenuList{Id=4,MenuAdi="TestReport",Resim="chartIcon.png"}
            };

            lstMenu.ItemsSource = menuList;
        }
 
    }
}