using StreamKontrolUnit.Helpers;
using StreamKontrolUnit.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StreamKontrolUnit.Views
{
    public sealed partial class CameraPage : Page
    {
        private CameraViewModel ViewModel
        {
            get { return DataContext as CameraViewModel; }
        }

        public CameraPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await cameraControl.InitializeCameraAsync();
        }

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            await cameraControl.CleanupCameraAsync();
        }
    }
}
