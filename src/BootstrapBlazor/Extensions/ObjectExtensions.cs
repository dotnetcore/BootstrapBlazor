using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

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
                        // 20200608 tian_teng@outlook.com 支持字段和只读属性
                        type.GetFields().ToList().ForEach(f =>
                        {
                            var v = f.GetValue(item);
                            valType.GetField(f.Name)?.SetValue(ret, v);
                        });
                        type.GetProperties().ToList().ForEach(p =>
                        {
                            if (p.CanWrite)
                            {
                                var v = p.GetValue(item);
                                valType.GetProperty(p.Name)?.SetValue(ret, v);
                            }
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

        private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo> _propertyCache = new ConcurrentDictionary<(Type, string), PropertyInfo>();

        /// <summary>
        /// 通过 FieldName 获得属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetValueByFieldName(this object obj, string fieldName)
        {
            var type = obj.GetType();
            var key = (type, fieldName);
            var pi = _propertyCache.GetOrAdd(key, key =>
            {
                var p = key.ModelType.GetProperties().FirstOrDefault(p => p.Name.Equals(key.FieldName, StringComparison.OrdinalIgnoreCase));
                return p;
            });
            if (pi == null) _propertyCache.TryRemove(key, out pi);
            return pi.GetValue(obj)?.ToString() ?? "";
        }
    }
}
