using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfPatronage.Functions.Models
{
    public class ArkServerPostRequest
    {
        public string Key { get; set; }
        public string SteamID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
