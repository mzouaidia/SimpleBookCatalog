using Syncfusion.Blazor;
using SimpleBookCatalog.App;
using SimpleBookCatalog.App.Services;
using SimpleBookCatalog.App.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped(hc => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetConnectionString("ApiAddressProd")!)
    //BaseAddress = new Uri(builder.Configuration.GetConnectionString("ApiAddressLocal")!)
});

builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();