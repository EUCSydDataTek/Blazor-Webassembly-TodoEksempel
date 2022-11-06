using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Todo_BlazorWebassembly_Eksempel;
using Todo_BlazorWebassembly_Eksempel.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ITodoService,TodoApiService>();

builder.Services.AddHttpClient<ITodoService, TodoApiService>(config => config.BaseAddress = AppConfig.Todo_BaseAddress);

await builder.Build().RunAsync();
