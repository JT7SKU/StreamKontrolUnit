using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StreamKontolUnitX.ViewModels
{
    public class GitHubViewModel
    {
        public Command ConnectedCommand { get; }
        public Command DisconnectCommand { get; set; }
    }
}
