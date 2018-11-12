using System;

using StreamKontrolUnit.ViewModels;

using Windows.UI.Xaml.Controls;

namespace StreamKontrolUnit.Views
{
    public sealed partial class WebViewPage : Page
    {
        private WebViewViewModel ViewModel
        {
            get { return DataContext as WebViewViewModel; }
        }

        public WebViewPage()
        {
            InitializeComponent();
            ViewModel.Initialize(webView);
        }
    }
}
