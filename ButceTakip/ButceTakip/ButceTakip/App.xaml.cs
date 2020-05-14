using ButceTakip.DataManager;
using ButceTakip.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ButceTakip
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ButceTakip.db3"));
                }
                return database;
            }
        }
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjUyMzIwQDMxMzgyZTMxMmUzMEY2dDE3dHdsZEFUTEZVeW1GWEN2eFZrbmRlalRvb2pKdWxEQk5MQnlsNUE9");
            InitializeComponent();

            MainPage = new MainPage();
            //Views.Home = new Home();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
