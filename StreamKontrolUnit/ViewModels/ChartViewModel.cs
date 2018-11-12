using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

using StreamKontrolUnit.Models;
using StreamKontrolUnit.Services;

namespace StreamKontrolUnit.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {
        public ChartViewModel()
        {
        }

        public ObservableCollection<DataPoint> Source
        {
            get
            {
                // TODO WTS: Replace this with your actual data
                return SampleDataService.GetChartSampleData();
            }
        }
    }
}
