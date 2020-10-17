using System.IO;
using System.Reflection;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// 路径操作类
    /// </summary>
    internal static class PathHelpers
    {
        /// <summary>
        /// 获取当前应用程序所在路径
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationRoot()
            => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
