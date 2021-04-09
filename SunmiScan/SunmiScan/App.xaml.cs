using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SunmiScan
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ScanQrCodePage();
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
