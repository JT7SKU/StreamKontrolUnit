using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SKU.Stream.Services.Models.GitHub
{
    public class GitHubConfiguration
    {
        [Required]
        [Display(Name ="Repository Owner")]
        [Remote(action: "VerityUser",controller:"GitHub")]
        public string RepositoryOwner { get; set; }

        [Required]
        [Display(Name = "Repository Name")]
        [Remote(action: "VerityRepository", controller: "GitHub", AdditionalFields =nameof(RepositoryOwner))]
        public string RepositoryName { get; set; }

        public string ExcludeUser { get; set; } = "janisku7";
        // Think adding way to not hardcode values here
        public string RepositoryCsv { get; set; } = "janisku7/JT7SKU";

        public string DisplayMode { get; set; } = "h-scroll";
        public int Width { get; set; } = 600;
        public int SpeedMs { get; set; } = 15000;

        public string Font { get; set; } = "Arial";
        public int FontSizePt { get; set; } = 14;
        public bool CaptionBold { get; set; } = true;
        public string CaptionColor { get; set; } = "lavender";
        public string TextColor { get; set; } = "darkpurple";
        public string BackgroundColor { get; set; } = "#666";
    }
}
