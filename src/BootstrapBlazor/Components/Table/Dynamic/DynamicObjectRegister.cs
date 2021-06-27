// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 动态类型注册中心
    /// </summary>
    internal static class DynamicObjectRegister
    {
        private static ConcurrentDictionary<Type, List<AutoGenerateColumnAttribute>> TableColumnsCache { get; } = new();

        private static ConcurrentDictionary<Type, List<List<object?>>> ValuesCache { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TItem> GenerateDynamicObject<TItem>() where TItem : IDynamicObject, new()
        {
            var ret = new List<TItem>();
            if (ValuesCache.TryGetValue(typeof(TItem), out var vals))
            {
                ret.AddRange(vals.Select(v => new TItem()
                {
                    Values = v
                }));
            }
            return ret;
        }

        /// <summary>
        /// 增加数值集合方法
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static void AddValues<TItem>(List<object?> values) => AddValues(typeof(TItem), values);

        /// <summary>
        /// 增加数值集合方法
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="values"></param>
        public static void AddValues(Type modelType, List<object?> values)
        {
            if (!ValuesCache.TryGetValue(modelType, out var vals))
            {
                vals = new List<List<object?>>();
                ValuesCache.TryAdd(modelType, vals);
            }
            vals.Add(values);
        }

        /// <summary>
        /// 增加动态列
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="colName"></param>
        /// <param name="colType"></param>
        public static void AddColumn<TItem>(string colName, Type colType) => AddColumn(typeof(TItem), colName, colType);

        /// <summary>
        /// 增加动态列
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="colName"></param>
        /// <param name="colType"></param>
        public static void AddColumn(Type modelType, string colName, Type colType)
        {
            if (!TableColumnsCache.TryGetValue(modelType, out var cols))
            {
                cols = new List<AutoGenerateColumnAttribute>();
                TableColumnsCache.TryAdd(modelType, cols);
            }

            InternalRemoveColumn(cols, colName);

            cols.Add(new AutoGenerateColumnAttribute()
            {
                Text = colName,
                FieldName = colName,
                PropertyType = colType
            });
        }

        /// <summary>
        /// 移除动态列
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="colName"></param>
        public static void RemoveColumn(Type modelType, string colName)
        {
            if (TableColumnsCache.TryGetValue(modelType, out var cols))
            {
                InternalRemoveColumn(cols, colName);
            }
        }

        private static void InternalRemoveColumn(List<AutoGenerateColumnAttribute> cols, string colName)
        {
            var col = cols.FirstOrDefault(c => c.FieldName == colName);
            if (col != null)
            {
                cols.Remove(col);
            }
        }

        /// <summary>
        /// 获得指定类型的所有列
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ITableColumn> GetColumns<TItem>() => TableColumnsCache.TryGetValue(typeof(TItem), out var cols) ? cols : Enumerable.Empty<ITableColumn>();

        /// <summary>
        /// 释放缓存资源
        /// </summary>
        /// <param name="modelType"></param>
        public static void Release(Type modelType)
        {
            TableColumnsCache.TryRemove(modelType, out _);
            ValuesCache.TryRemove(modelType, out _);
        }
    }
}
