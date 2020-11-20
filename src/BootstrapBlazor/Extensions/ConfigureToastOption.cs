using BootstrapBlazor.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigureOptions<TOption> : ConfigureFromConfigurationOptions<TOption> where TOption : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public ConfigureOptions(IConfiguration config)
            : base(config.GetSection(typeof(TOption).Name))
        {

        }
    }
}
