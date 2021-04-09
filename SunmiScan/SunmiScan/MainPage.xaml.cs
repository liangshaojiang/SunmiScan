using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SunmiScan
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btn_scan.Clicked += async (sender, e) =>
            {

#if __ANDROID__
	// Initialize the scanner first so it can track the current context
	MobileBarcodeScanner.Initialize (Application);
#endif

                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan(ZXing.Mobile.MobileBarcodeScanningOptions.Default);

                if (result != null) {
                    Console.WriteLine("Scanned Barcode: " + result.Text);
                    lab_text.Text = result.Text;
                   // Toast.MakeText(this, "" + singleCheck, ToastLength.Long).Show();
                }
                  
            };




        }
    }
}
