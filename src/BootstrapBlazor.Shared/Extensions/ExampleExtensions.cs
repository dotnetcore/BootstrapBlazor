using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 示例代码获取服务
    /// </summary>
    public static class ExampleExtensions
    {
        /// <summary>
        /// 注入版本获取服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddExampleService(this IServiceCollection services)
        {
            services.AddTransient<ExampleService>();
            return services;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class ExampleService
    {
        private HttpClient Client { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="client"></param>
        /// <param name="navigator"></param>
        public ExampleService(HttpClient client, NavigationManager navigator)
        {
            Client = client;
            Client.Timeout = TimeSpan.FromSeconds(5);
            Client.BaseAddress = new Uri(navigator.BaseUri);
        }

        /// <summary>
        /// 获得组件版本号方法
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCodeAsync(string CodeFile)
        {
            var content = "";
            try
            {
                var folder = CodeFile.Split('.').FirstOrDefault();
                if (!string.IsNullOrEmpty(folder))
                {
                    content = await Client.GetStringAsync($"_content/BootstrapBlazor.Docs/docs/{folder}/{CodeFile}");
                }
            }
            catch (Exception ex)
            {
                content = ex.ToString();
            }
            return content;
        }
    }
}
