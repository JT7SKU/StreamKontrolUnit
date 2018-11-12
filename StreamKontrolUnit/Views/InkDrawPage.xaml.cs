using System;

using StreamKontrolUnit.Services.Ink;
using StreamKontrolUnit.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StreamKontrolUnit.Views
{
    // For more information regarding Windows Ink documentation and samples see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/ink.md
    public sealed partial class InkDrawPage : Page
    {
        private InkDrawViewModel ViewModel
        {
            get { return DataContext as InkDrawViewModel; }
        }

        public InkDrawPage()
        {
            InitializeComponent();
            Loaded += (sender, eventArgs) =>
            {
                SetCanvasSize();

                var strokeService = new InkStrokesService(inkCanvas.InkPresenter);
                var selectionRectangleService = new InkSelectionRectangleService(inkCanvas, selectionCanvas, strokeService);

                ViewModel.Initialize(
                    strokeService,
                    new InkLassoSelectionService(inkCanvas, selectionCanvas, strokeService, selectionRectangleService),
                    new InkPointerDeviceService(inkCanvas),
                    new InkCopyPasteService(strokeService),
                    new InkUndoRedoService(inkCanvas, strokeService),
                    new InkFileService(inkCanvas, strokeService),
                    new InkZoomService(canvasScroll));
            };
        }

        private void SetCanvasSize()
        {
            inkCanvas.Width = Math.Max(canvasScroll.ViewportWidth, 1000);
            inkCanvas.Height = Math.Max(canvasScroll.ViewportHeight, 1000);
        }
    }
}
