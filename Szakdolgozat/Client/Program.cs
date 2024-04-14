global using Szakdolgozat.Shared;
global using Szakdolgozat.Client.Services.MealService;
global using Szakdolgozat.Client.Services.CartService;
global using Szakdolgozat.Client.Services.AuthService;
global using Microsoft.AspNetCore.Components.Authorization;
global using System.Net.Http.Json;
global using Szakdolgozat.Client.Services.OrderService;
global using Szakdolgozat.Client.Services.CategoryService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Szakdolgozat.Client;
using Blazored.LocalStorage;
using Szakdolgozat.Client.Services.MealTypeService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService , OrderService>();
builder.Services.AddScoped<IMealTypeService, MealTypeService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


await builder.Build().RunAsync();
