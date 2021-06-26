// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 动态属性注册中心
    /// </summary>
    public static class DynamicColumnCenter
    {
        private static readonly Dictionary<Type, List<PropertyInfo>> typePropDic = new Dictionary<Type, List<PropertyInfo>>();
        private static readonly Dictionary<Type, AutoGenerateClassAttribute> classAttrDic = new Dictionary<Type, AutoGenerateClassAttribute>();

        /// <summary>
        /// 给指定类型，添加动态属性
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>

        public static void AddProperty(Type type, PropertyInfo info)
        {
            if (!typePropDic.ContainsKey(type))
            {
                typePropDic[type] = new List<PropertyInfo>();
            }
            typePropDic[type].Add(info);
        }

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
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ITableColumn> GetProperties<TItem>()
        {
            var type = typeof(TItem);
            return Enumerable.Empty<ITableColumn>();
        }

        /// <summary>
        /// 获取指定了类型的所有属性信息
        /// </summary>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties(Type type)
        {
            return typePropDic[type].ToArray();
        }

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
