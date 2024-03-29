#pragma checksum "C:\Users\janis\source\repos\StreamControlUnit.WebDash\Pages\Chat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5dce8b8fa6e58dd70b0b6e437aad4cc68221e43d"
// <auto-generated/>
#pragma warning disable 1591
namespace StreamControlUnit.WebDash.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using System.Net.Http;
    using Microsoft.AspNetCore.Blazor.Layouts;
    using Microsoft.AspNetCore.Blazor.Routing;
    using Microsoft.JSInterop;
    using StreamControlUnit.WebDash;
    using StreamControlUnit.WebDash.Shared;
    using Blazor.Extensions;
    [Microsoft.AspNetCore.Blazor.Layouts.LayoutAttribute(typeof(MainLayout))]

    [Microsoft.AspNetCore.Blazor.Components.RouteAttribute("/chat")]
    public class Chat : Microsoft.AspNetCore.Blazor.Components.BlazorComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Blazor.RenderTree.RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            builder.AddMarkupContent(0, "<h1>Chat</h1>\n\n");
            builder.OpenElement(1, "form");
            builder.AddAttribute(2, "action", "#");
            builder.AddAttribute(3, "onsubmit", Microsoft.AspNetCore.Blazor.Components.BindMethods.GetEventHandlerValue<Microsoft.AspNetCore.Blazor.UIEventArgs>(SendAsync));
            builder.AddContent(4, "\nSaySomething\n    ");
            builder.OpenElement(5, "input");
            builder.AddAttribute(6, "disabled", disabled);
            builder.AddAttribute(7, "value", Microsoft.AspNetCore.Blazor.Components.BindMethods.GetValue(message));
            builder.AddAttribute(8, "onchange", Microsoft.AspNetCore.Blazor.Components.BindMethods.SetValueHandler(__value => message = __value, message));
            builder.CloseElement();
            builder.AddContent(9, "\n    ");
            builder.OpenElement(10, "button");
            builder.AddAttribute(11, "disabled", disabled);
            builder.AddContent(12, "Send");
            builder.CloseElement();
            builder.AddContent(13, "\n");
            builder.CloseElement();
            builder.AddContent(14, "\n\n");
            builder.OpenElement(15, "ul");
            builder.AddContent(16, "\n");
#line 14 "C:\Users\janis\source\repos\StreamControlUnit.WebDash\Pages\Chat.cshtml"
     foreach (var message in messages)
    {

#line default
#line hidden
            builder.AddContent(17, "        ");
            builder.OpenElement(18, "li");
            builder.AddContent(19, message);
            builder.CloseElement();
            builder.AddContent(20, "\n");
#line 17 "C:\Users\janis\source\repos\StreamControlUnit.WebDash\Pages\Chat.cshtml"
    }

#line default
#line hidden
            builder.CloseElement();
        }
        #pragma warning restore 1998
#line 20 "C:\Users\janis\source\repos\StreamControlUnit.WebDash\Pages\Chat.cshtml"
           
    string message;
    IList<string> messages = new List<string>();
    bool disabled = true;
    HubConnection connection;
    protected override async Task OnInitAsync()
    {
        connection = new HubConnectionBuilder().WithUrl("/hubs/chat").Build();
        connection.On<string>("SendAction", this.OnMessage);
        connection.On<string>("SendMessage", this.OnMessage);
        await connection.StartAsync();
        disabled = false;
    }
    Task OnMessage(string message)
    {
        messages.Add(message);
        StateHasChanged();
        return Task.CompletedTask;
    }
    async Task SendAsync()
    {
        await connection.InvokeAsync("Send", message);
        message = "";
    }

#line default
#line hidden
    }
}
#pragma warning restore 1591
