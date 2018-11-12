using System;

using CommonServiceLocator;

using GalaSoft.MvvmLight.Ioc;

using StreamKontrolUnit.Services;
using StreamKontrolUnit.Views;

namespace StreamKontrolUnit.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (SimpleIoc.Default.IsRegistered<NavigationServiceEx>())
            {
                return;
            }

            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<MainViewModel, MainPage>();
            Register<WebViewViewModel, WebViewPage>();
            Register<MediaPlayerViewModel, MediaPlayerPage>();
            Register<InkDrawViewModel, InkDrawPage>();
            Register<InkSmartCanvasViewModel, InkSmartCanvasPage>();
            Register<InkDrawPictureViewModel, InkDrawPicturePage>();
            Register<ImageGalleryViewModel, ImageGalleryPage>();
            Register<ImageGalleryDetailViewModel, ImageGalleryDetailPage>();
            Register<CameraViewModel, CameraPage>();
            Register<MapViewModel, MapPage>();
            Register<MasterDetailViewModel, MasterDetailPage>();
            Register<DataGridViewModel, DataGridPage>();
            Register<ChartViewModel, ChartPage>();
            Register<TelerikDataGridViewModel, TelerikDataGridPage>();
            Register<TabbedViewModel, TabbedPage>();
            Register<SettingsViewModel, SettingsPage>();
            Register<SchemeActivationSampleViewModel, SchemeActivationSamplePage>();
            Register<ShareTargetViewModel, ShareTargetPage>();
        }

        public ShareTargetViewModel ShareTargetViewModel => ServiceLocator.Current.GetInstance<ShareTargetViewModel>();

        public SchemeActivationSampleViewModel SchemeActivationSampleViewModel => ServiceLocator.Current.GetInstance<SchemeActivationSampleViewModel>();

        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public TabbedViewModel TabbedViewModel => ServiceLocator.Current.GetInstance<TabbedViewModel>();

        public TelerikDataGridViewModel TelerikDataGridViewModel => ServiceLocator.Current.GetInstance<TelerikDataGridViewModel>();

        public ChartViewModel ChartViewModel => ServiceLocator.Current.GetInstance<ChartViewModel>();

        public DataGridViewModel DataGridViewModel => ServiceLocator.Current.GetInstance<DataGridViewModel>();

        public MasterDetailViewModel MasterDetailViewModel => ServiceLocator.Current.GetInstance<MasterDetailViewModel>();

        public MapViewModel MapViewModel => ServiceLocator.Current.GetInstance<MapViewModel>();

        public CameraViewModel CameraViewModel => ServiceLocator.Current.GetInstance<CameraViewModel>();

        public ImageGalleryDetailViewModel ImageGalleryDetailViewModel => ServiceLocator.Current.GetInstance<ImageGalleryDetailViewModel>();

        public ImageGalleryViewModel ImageGalleryViewModel => ServiceLocator.Current.GetInstance<ImageGalleryViewModel>();

        public InkDrawPictureViewModel InkDrawPictureViewModel => ServiceLocator.Current.GetInstance<InkDrawPictureViewModel>();

        public InkSmartCanvasViewModel InkSmartCanvasViewModel => ServiceLocator.Current.GetInstance<InkSmartCanvasViewModel>();

        public InkDrawViewModel InkDrawViewModel => ServiceLocator.Current.GetInstance<InkDrawViewModel>();

        // A Guid is generated as a unique key for each instance as reusing the same VM instance in multiple MediaPlayerElement instances can cause playback errors
        public MediaPlayerViewModel MediaPlayerViewModel => ServiceLocator.Current.GetInstance<MediaPlayerViewModel>(Guid.NewGuid().ToString());

        public WebViewViewModel WebViewViewModel => ServiceLocator.Current.GetInstance<WebViewViewModel>();

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
