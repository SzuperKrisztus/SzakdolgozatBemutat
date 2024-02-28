global using Szakdolgozat.Shared;
global using Szakdolgozat.Client.Services.MealService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Szakdolgozat.Client;
using Szakdolgozat.Client.Services.CategoryService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
await builder.Build().RunAsync();
