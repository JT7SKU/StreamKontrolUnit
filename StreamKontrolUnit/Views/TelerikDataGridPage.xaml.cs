using System;

using StreamKontrolUnit.ViewModels;

using Windows.UI.Xaml.Controls;

namespace StreamKontrolUnit.Views
{
    public sealed partial class TelerikDataGridPage : Page
    {
        private TelerikDataGridViewModel ViewModel
        {
            get { return DataContext as TelerikDataGridViewModel; }
        }

        // TODO WTS: Change the grid as appropriate to your app, adjust the column definitions on TelerikDataGridPage.xaml.
        // For help see http://docs.telerik.com/windows-universal/controls/raddatagrid/gettingstarted
        // You may also want to extend the grid to work with the RadDataForm http://docs.telerik.com/windows-universal/controls/raddataform/dataform-gettingstarted
        public TelerikDataGridPage()
        {
            InitializeComponent();
        }
    }
}
