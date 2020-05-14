
using Plugin.Messaging;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ButceTakip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
        }

        async void btnGelir_Clicked(object sender, EventArgs e)
        {
            try
            {
                loadingIn.IsRunning = true;
                await Navigation.PushModalAsync(new NavigationPage(new GelirGider(true)));

                //var sendEmail = CrossMessaging.Current.EmailMessenger;
                //if(sendEmail.CanSendEmail)
                //{
                 
                //    if(sendEmail.CanSendEmailAttachments && sendEmail.CanSendEmailBodyAsHtml)
                //    {
                //        var emailWithHtmlBody = new EmailMessageBuilder()
                //            .To("abdulhamit.ylmz@gmail.com")
                //            .Cc("hmd62392@gmail.com")
                //            .Subject("Xamarin Test")
                //            .BodyAsHtml("<b>Deneme</b> mailidir")
                //            .Build();
                //        sendEmail.SendEmail(emailWithHtmlBody);
                //    }
                //}


            }
            finally
            {
                loadingIn.IsRunning = false;
            }
            
        }

        async void btnGider_Clicked(object sender, EventArgs e)
        {
            try
            {
                loadingIn.IsRunning = true;
                await Navigation.PushModalAsync(new NavigationPage(new GelirGider(false)));
            }
            finally
            {
                loadingIn.IsRunning = false;
            }
            
        }

        async void btnKategori_Clicked(object sender, EventArgs e)
        {
            try
            {
                loadingIn.IsRunning = true;
                await Navigation.PushModalAsync(new NavigationPage(new Kategoriler()));
            }
            finally
            {
                loadingIn.IsRunning = false;
            }
            
        }

        async void btnOzet_Clicked(object sender, EventArgs e)
        {
            try
            {
                loadingIn.IsRunning = true;
                await Navigation.PushModalAsync(new NavigationPage(new Default()));
            }
            finally 
            {
                loadingIn.IsRunning = false;
            }
            
        }

        async void btnRapor_Clicked(object sender, EventArgs e)
        {
            try
            {
                loadingIn.IsRunning = true;
                await Navigation.PushModalAsync(new NavigationPage(new Raporlar()));
            }
            finally
            {
                loadingIn.IsRunning = false;
            }
        }
    }
}