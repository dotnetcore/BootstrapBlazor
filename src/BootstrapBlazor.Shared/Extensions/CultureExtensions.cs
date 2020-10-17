using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.Configuration
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
        public static IEnumerable<KeyValuePair<string, string>> GetSupportCultures(this IConfiguration configuration) => configuration.GetSection("SupportCultures").GetChildren()
            .SelectMany(c => new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>(c.Key, c.Value)
            });
    }
}
