// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 动态类型注册中心
    /// </summary>
    internal static class DynamicObjectRegister
    {
        private static ConcurrentDictionary<Type, List<AutoGenerateColumnAttribute>> TableColumnsCache { get; } = new();
        private static ConcurrentDictionary<Type, AutoGenerateClassAttribute> classAttrDic = new();

        /// <summary>
        /// 注册 AutoGenerateClassAttribute
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>
        public static void AddAutoGenerateClassAttribute(Type type, AutoGenerateClassAttribute info)
        {
            classAttrDic[type] = info;
        }

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
        /// 释放缓存资源
        /// </summary>
        /// <param name="modelType"></param>
        public static void Release(Type modelType)
        {
            TableColumnsCache.TryRemove(modelType, out _);
        }

        /// <summary>
        /// 获得指定类型的所有列
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ITableColumn> GetColumns<TItem>() => TableColumnsCache.TryGetValue(typeof(TItem), out var cols) ? cols : Enumerable.Empty<ITableColumn>();

        /// <summary>
        /// 获取类型上面的 AutoGenerateClassAttribute
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AutoGenerateClassAttribute GetTypeAttribute(Type type)
        {
            return classAttrDic[type];
        }
    }
}
