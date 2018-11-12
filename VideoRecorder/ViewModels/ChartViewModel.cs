using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

using VideoRecorder.Models;
using VideoRecorder.Services;

namespace VideoRecorder.ViewModels
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
