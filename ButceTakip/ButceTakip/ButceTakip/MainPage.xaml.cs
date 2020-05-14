using ButceTakip.Models;
using ButceTakip.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ButceTakip
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            masterPage.lstMenu.SelectionChanged += lstMenu_SelectionChanged;
           
            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }


        }
        void lstMenu_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            Syncfusion.ListView.XForms.SfListView view = (Syncfusion.ListView.XForms.SfListView)sender;
            MenuList menu = (MenuList)view.SelectedItem;

            if (menu.MenuAdi == "Özet")
            {
                Default pg = new Default();
                GetPage(pg);
            }else if (menu.MenuAdi == "Anasayfa")
            {
                Home pg = new Home();
                
                GetPage(pg);
            }
            else if (menu.MenuAdi == "Gelirler")
            {
                GelirGider pg = new GelirGider(true);
                GetPage(pg);
            }
            else if (menu.MenuAdi == "Giderler")
            {
                GelirGider pg = new GelirGider(false);
                GetPage(pg);
            }
            else if (menu.MenuAdi == "Kategoriler")
            {
                Kategoriler pg = new Kategoriler();
                GetPage(pg);
            }else if (menu.MenuAdi == "TestReport")
            {
                TestReport pg = new TestReport();
                GetPage(pg);
            }
            view.SelectedItem = null;
        }
        public void GetPage(Page pg)
        {
           
            Detail = new NavigationPage((Page)pg);
          
            masterPage.lstMenu.SelectedItem = null;
            IsPresented = false;
        }

    }
}
