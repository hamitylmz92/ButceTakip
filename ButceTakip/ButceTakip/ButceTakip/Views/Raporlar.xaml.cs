using ButceTakip.Helper;
using Syncfusion.SfDataGrid.XForms;
using Syncfusion.SfDataGrid.XForms.Exporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ButceTakip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Raporlar : ContentPage
    {
        public Raporlar()
        {
            InitializeComponent();

            DataGetir();


        }
        public async void DataGetir()
        {
            dtGelir.ItemsSource= await App.Database.GetGelirIslemList();
            dtGelir.GridStyle = new Dark();

            dtGider.ItemsSource = await App.Database.GetGiderIslemList();
            dtGider.GridStyle = new Dark();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DataGetir();
        }

        private void ExportExcel_Clicked(object sender, EventArgs e)
        {
            ExcelExport(dtGelir, "GelirData.xlsx");
        }

        private void ExportPdf_Clicked(object sender, EventArgs e)
        {
            PdfExport(dtGelir, "GelirData.pdf");
        }
   
        private void btnGiderPdf_Clicked(object sender, EventArgs e)
        {
            PdfExport(dtGider, "GiderData.pdf");
        }

        private void btnGiderExcel_Clicked(object sender, EventArgs e)
        {
            ExcelExport(dtGider, "GiderData.xlsx");
        }
        public void PdfExport(SfDataGrid grid, string fileName)
        {
            try
            {
                DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();
              
                MemoryStream stream = new MemoryStream();
                
                var exportToPdf = pdfExport.ExportToPdf(grid, new DataGridPdfExportOption()
                {
                    FitAllColumnsInOnePage = true,
                });
                exportToPdf.Save(stream);
                exportToPdf.Close(true);
                //if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                //    Xamarin.Forms.DependencyService.Get<ISaveWindowsPhone>().Save("DataGrid.pdf", "application/pdf", stream);
                //else
                Xamarin.Forms.DependencyService.Get<ISave>().Save(fileName, "application/pdf", stream);
            }
            catch (Exception ex)
            {
                string hata = ex.ToString();
                DisplayAlert("Hata", ex.ToString(), "Tamam");
            }
        }
        public void ExcelExport(SfDataGrid grid, string filename)
        {
            try
            {
                DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
                DataGridExcelExportingOption options = new DataGridExcelExportingOption();
                options.BottomTableSummaryStyle = new ExportCellStyle()
                {
                    BackgroundColor = Xamarin.Forms.Color.Yellow,
                    BorderColor = Xamarin.Forms.Color.Red,
                    ForegroundColor = Xamarin.Forms.Color.Green,
                };

                
                
                options.ExportMode = ExportMode.Text;
                var excelEngine = excelExport.ExportToExcel(grid, options);
                var workbook = excelEngine.Excel.Workbooks[0];
                
                MemoryStream stream = new MemoryStream();
                workbook.SaveAs(stream);
                workbook.Close();
                excelEngine.Dispose();
                //Xamarin.Forms.DependencyService.Get<ISave>().Save("DataGrid.xlsx", "application/msexcel", stream);
                Xamarin.Forms.DependencyService.Get<ISave>().Save(filename, "application/msexcel", stream);
            }
            catch (Exception ex)
            {
                string hata = ex.ToString();
                DisplayAlert("Hata", ex.ToString(), "Tamam");
            }
        }
    }
    #region Grid style ve Convert

    public class DatetimeToStringConverter : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var datetime = (DateTime)value;
            //put your custom formatting here
            return datetime.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class Dark : DataGridStyle
    {
        public Dark()
        {
        }

        //public override Color GetHeaderBackgroundColor()
        //{
        //    return Color.FromRgb(35, 169, 242);
        //}

        //public override Color GetHeaderForegroundColor()
        //{
        //    return Color.FromRgb(35, 169, 242);
        //}
        public override GridLinesVisibility GetGridLinesVisibility()
        {
            return GridLinesVisibility.Both;
        }

    }
    #endregion
}