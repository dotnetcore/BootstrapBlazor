using Microsoft.AspNetCore.Components;
using System;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Object 扩展方法
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 泛型 Clone 方法
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TItem Clone<TItem>(this TItem item)
        {
            var ret = item;
            if (item != null)
            {
                var type = item.GetType();
                if (typeof(ICloneable).IsAssignableFrom(type))
                {
                    var clv = type.GetMethod("Clone")?.Invoke(type, null);
                    if (clv != null)
                    {
                        ret = (TItem)clv;
                    }
                }
                if (type.IsClass)
                {
                    ret = Activator.CreateInstance<TItem>();
                    var valType = ret?.GetType();
                    if (valType != null)
                    {
                        type.GetProperties().ToList().ForEach(p =>
                        {
                            var v = p.GetValue(item);
                            valType.GetProperty(p.Name)?.SetValue(ret, v);
                        });
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ConvertToPercentString(this string? val)
        {
            var ret = "";
            if (!string.IsNullOrEmpty(val))
            {
                if (val.EndsWith('%'))
                {
                    ret = val;
                }
                else if (val.EndsWith("px", StringComparison.OrdinalIgnoreCase))
                {
                    ret = val;
                }
                else if (int.TryParse(val, out var d))
                {
                    ret = $"{d}px";
                }
            }
            return ret;
        }
    }
}
