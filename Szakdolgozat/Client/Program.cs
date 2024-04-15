global using Szakdolgozat.Shared;
global using Szakdolgozat.Client.Services.MealService;
global using Szakdolgozat.Client.Services.CartService;
global using Szakdolgozat.Client.Services.AuthService;
global using Microsoft.AspNetCore.Components.Authorization;
global using System.Net.Http.Json;
global using Szakdolgozat.Client.Services.OrderService;
global using Szakdolgozat.Client.Services.CategoryService;
global using Szakdolgozat.Client.Services.MealVariantService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Szakdolgozat.Client;
using Szakdolgozat.Client.Services.MealTypeService;
using Blazored.LocalStorage;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService , OrderService>();
builder.Services.AddScoped<IMealTypeService, MealTypeService>();
builder.Services.AddScoped<IMealVariantService, MealVariantService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddRadzenComponents();



await builder.Build().RunAsync();
