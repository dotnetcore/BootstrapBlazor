using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// 增加 BootstrapBlazor 服务
builder.Services.AddBootstrapBlazorServices(options =>
{
    // 统一设置 Toast 组件自动消失时间
    //options.Themes.AddRange(themes);
});

await builder.Build().RunAsync();
