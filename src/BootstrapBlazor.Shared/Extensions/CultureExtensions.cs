using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Culture 扩展操作类
    /// </summary>
    public static class CultureExtensions
    {
        /// <summary>
        /// 获得配置文件中配置的 Culture 信息
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IEnumerable<CultureInfo> GetSupportCultures(this IConfiguration configuration)
        {
            var ret = configuration.GetSection("SupportCultures").GetChildren()
                .Select(c => c.Value);

            if (!ret.Any())
            {
                ret = new List<string>()
                {
                    "en-US",
                    "zh-CN"
                };
            }

            return ret.Select(c => new CultureInfo(c));
        }
    }

    /// <summary>
    /// ICulture
    /// </summary>
    public interface ICultureStorage
    {
        /// <summary>
        /// 
        /// </summary>
        public CultureStorageMode Mode { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CultureStorageMode
    {
        /// <summary>
        /// 
        /// </summary>
        Webapi,

        /// <summary>
        /// 
        /// </summary>
        LocalStorage
    }
}
