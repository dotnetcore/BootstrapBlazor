using Longbow.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 后台任务扩展方法
    /// </summary>
    internal static class TasksExtensions
    {
        /// <summary>
        /// 添加示例后台任务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddBlazorBackgroundTask(this IServiceCollection services)
        {
            services.AddTaskServices();
            services.AddHostedService<BlazorBackgroundServices>();
            return services;
        }
    }

    /// <summary>
    /// 后台任务服务类
    /// </summary>
    internal class BlazorBackgroundServices : BackgroundService
    {
        IWebHostEnvironment? _env;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public BlazorBackgroundServices(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.Run(() =>
        {
            TaskServicesManager.GetOrAdd("清除文件", token =>
            {
                if (_env != null)
                {
                    var webSiteUrl = $"images{Path.DirectorySeparatorChar}uploader{Path.DirectorySeparatorChar}";
                    var filePath = Path.Combine(_env.WebRootPath, webSiteUrl);
                    if (Directory.Exists(filePath))
                    {
                        Directory.EnumerateFiles(filePath).Take(10).ToList().ForEach(file =>
                        {
                            try
                            {
                                if (token.IsCancellationRequested) return;
                                File.Delete(file);
                            }
                            catch { }
                        });
                    }

                }
                return Task.CompletedTask;
            }, TriggerBuilder.Build(Cron.Minutely(10)));
        });
    }
}
