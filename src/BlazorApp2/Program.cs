using ThingsGateway;
using ThingsGateway.Razor;

namespace BlazorApp2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services
         .AddRazorComponents(options =>
         {
             options.TemporaryRedirectionUrlValidityDuration = TimeSpan.FromMinutes(10);
         })
         .AddInteractiveServerComponents(options =>
         {
             options.RootComponents.MaxJSRootComponents = 500;
             options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(2);
             options.MaxBufferedUnacknowledgedRenderBatches = 20;
             options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(10);
         });
            builder.Services.AddBootstrapBlazor(
    option => option.JSModuleVersion = Random.Shared.Next(10000).ToString()
    );
            builder.Services.AddSingleton<IMenuService, DefaultMenuService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
       .AddInteractiveServerRenderMode();

            app.Run();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.ToString());
        }
    }
}
