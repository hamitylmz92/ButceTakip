using ButceTakip.Models;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ButceTakip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestReport : ContentPage
    {
        public List<GiderIslem> lstGider;
        public TestReport()
        {
            InitializeComponent();
            DataGetir();
            
        }

        public async void DataGetir()
        {
            SfChart chart = new SfChart();
            chart.Title.Text = "Chart";

            //Initializing primary axis
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title.Text = "Name";
            chart.PrimaryAxis = primaryAxis;
            
            //Initializing secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title.Text = "Height (in cm)";
            chart.SecondaryAxis = secondaryAxis;

            //Initializing column series
            ColumnSeries series = new ColumnSeries();
            //ViewModel viewModel = new ViewModel();
            series.ItemsSource = await App.Database.GetGiderIslemList();// lstGelir;// viewModel.Data;
            series.XBindingPath = "Aciklama";
            series.YBindingPath = "Deger";


            series.Label = "Değerler";

            series.DataMarker = new ChartDataMarker();
            series.EnableTooltip = true;
            chart.Legend = new ChartLegend();

            chart.Series.Add(series);
            this.Content = chart;
        }

        //public class ViewModel
        //{
        //    public List<Person> Data { get; set; }

        //    public ViewModel()
        //    {
        //        Data = new List<Person>()
        //    {
        //        new Person { Name = "David", Height = 180 },
        //        new Person { Name = "Michael", Height = 170 },
        //        new Person { Name = "Steve", Height = 160 },
        //        new Person { Name = "Joel", Height = 182 }
        //    };
        //    }
        //}
        //public class Person
        //{
        //    public string Name { get; set; }

        //    public double Height { get; set; }
        //}


    }
}