using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace SunmiScan
{
    /// <summary>
    /// ZXingCustomScanPage
    /// </summary>
    public class ZXingCustomScanPage : ContentPage
    {
        private ZXingScannerView _zxing;
        private ZXingScanOverlay _overlay;

        public ZXingCustomScanPage(ZXingScanOverlay overlay = null) : base()
        {
            _overlay = overlay ?? new ZXingScanOverlay();

            Title = "扫一扫";

            _zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
            };

            // 返回结果
            _zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _zxing.IsAnalyzing = false;

                    await Navigation.PopModalAsync();//安卓可能报错

                    OnScanResult?.Invoke(result);
                });

            // 闪光灯
            _overlay.Options.FlashTappedAction = () =>
            {
                _zxing.IsTorchOn = !_zxing.IsTorchOn;
            };

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            grid.Children.Add(_zxing);
            grid.Children.Add(_overlay);

            Content = grid;
        }

        // 扫描结果
        public Action<ZXing.Result> OnScanResult { get; set; }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            _zxing.IsScanning = true;

            if (_overlay != null && _overlay.Options.ShowScanAnimation)
                await _overlay.ScanAnimationAsync();
        }

        protected override void OnDisappearing()
        {
            _zxing.IsScanning = false;

            base.OnDisappearing();
        }
    }
}