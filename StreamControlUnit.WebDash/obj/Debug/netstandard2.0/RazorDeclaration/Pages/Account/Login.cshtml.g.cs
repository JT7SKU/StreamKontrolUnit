#pragma checksum "C:\Users\janis\source\repos\JT7SKU\SKU.StreamKontrolUnit\StreamControlUnit.WebDash\Pages\Account\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bf41fc9a4ec2122312bd1632427727ae4572ea38"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace StreamControlUnit.WebDash.Pages.Account
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using System.Net.Http;
    using Microsoft.AspNetCore.Blazor.Layouts;
    using Microsoft.AspNetCore.Blazor.Routing;
    using Microsoft.JSInterop;
    using StreamControlUnit.WebDash;
    using StreamControlUnit.WebDash.Shared;
    using StreamKontrolUnit.Shared;
    [Microsoft.AspNetCore.Components.Layouts.LayoutAttribute(typeof(MainLayout))]

    [Microsoft.AspNetCore.Components.RouteAttribute("/login")]
    public class Login : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder builder)
        {
        }
        #pragma warning restore 1998
#line 19 "C:\Users\janis\source\repos\JT7SKU\SKU.StreamKontrolUnit\StreamControlUnit.WebDash\Pages\Account\Login.cshtml"
               
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Token { get; set; } = "";

        private async Task SubmitForm()
        {
            var vm = new TokenViewModel
            {
                Email = Email,
                Password = Password
            };
            var response = await Http.PostJsonAsync<object>("https://localhost:5001/api/Token/Login", vm);

            Console.WriteLine(response);
        }
    

#line default
#line hidden
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591