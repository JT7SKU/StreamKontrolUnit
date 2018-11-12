using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Stream.Goals.Models;
using Stream.Goals.ViewModels;

namespace Stream.Goals.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        GoalDetailViewModel viewModel;

        public ItemDetailPage(GoalDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new GoalDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}