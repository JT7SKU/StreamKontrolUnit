using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MECodex
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            => services.AddCors(
                options => options.AddDefaultPolicy(
                    builder=>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()));


        public void Configure(IBlazorApplicationBuilder app) => 
            app.AddComponent<App>(nameof(app));
        
    }
}
