using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamKontolUnitX.Models
{
    public class GitHubMessage : ObservableObject
    {
        string user;
        public string User { get => user; set => SetProperty(ref user, value); }

        string message;
        public string Message { get => message; set => SetProperty(ref message, value); }
    }
}
