using System;

using StreamKontrolUnit.ViewModels;

using Windows.UI.Xaml.Controls;

namespace StreamKontrolUnit.Views
{
    public sealed partial class DataGridPage : Page
    {
        private DataGridViewModel ViewModel
        {
            get { return DataContext as DataGridViewModel; }
        }

        // TODO WTS: Change the grid as appropriate to your app, adjust the column definitions on DataGridPage.xaml.
        // For more details see the documentation at https://github.com/Microsoft/WindowsCommunityToolkit/blob/master/docs/controls/DataGrid.md
        public DataGridPage()
        {
            InitializeComponent();
        }
    }
}
