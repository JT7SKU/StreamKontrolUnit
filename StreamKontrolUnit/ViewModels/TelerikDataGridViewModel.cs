using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

using StreamKontrolUnit.Models;
using StreamKontrolUnit.Services;

namespace StreamKontrolUnit.ViewModels
{
    public class TelerikDataGridViewModel : ViewModelBase
    {
        public ObservableCollection<SampleOrder> Source
        {
            get
            {
                // TODO WTS: Replace this with your actual data
                return SampleDataService.GetGridSampleData();
            }
        }
    }
}
