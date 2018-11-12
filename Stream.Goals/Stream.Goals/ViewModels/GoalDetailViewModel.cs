using System;

using Stream.Goals.Models;

namespace Stream.Goals.ViewModels
{
    public class GoalDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public GoalDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
