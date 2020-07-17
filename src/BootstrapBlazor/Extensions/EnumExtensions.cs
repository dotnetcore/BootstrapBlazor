using System;
using System.ComponentModel;
using System.Reflection;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 枚举类型扩展方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取 DescriptionAttribute 标签方法
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToDescriptionString<TEnum>(this TEnum val)
        {
            if (val == null) throw new ArgumentNullException(nameof(val));

            var attributes = typeof(TEnum).GetField(val.ToString())?.GetCustomAttribute<DescriptionAttribute>();
            return attributes?.Description ?? string.Empty;
        }
    }
}
