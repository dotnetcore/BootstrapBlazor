using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// 增加 BootstrapBlazor 服务
builder.Services.AddBootstrapBlazorServices();

await builder.Build().RunAsync();
