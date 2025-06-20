﻿using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClassCompass.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient for your API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5004/") });

await builder.Build().RunAsync();
