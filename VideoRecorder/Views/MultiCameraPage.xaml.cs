using VideoRecorder.Helpers;
using VideoRecorder.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VideoRecorder.Views
{
    public sealed partial class MultiCameraPage : Page
    {
        private MultiCameraViewModel ViewModel
        {
            get { return DataContext as MultiCameraViewModel; }
        }

        public MultiCameraPage()
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
