// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 动态类型上下文基类 <see cref="IDynamicObjectContext" />
    /// </summary>
    public abstract class DynamicObjectContext : IDynamicObjectContext
    {
        /// <summary>
        /// 获取动态类型各列信息
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<ITableColumn> GetColumns();

        /// <summary>
        /// 获得动态类数据方法
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<IDynamicObject> GetItems();

        /// <summary>
        /// 
        /// </summary>
        protected ConcurrentDictionary<string, List<CustomAttributeBuilder>> CustomerAttributeBuilderCache { get; } = new();

        /// <summary>
        /// 添加标签到指定列
        /// </summary>
        /// <param name="columnName">指定列名称</param>
        /// <param name="attributeType">Attribute 实例</param>
        /// <param name="types"></param>
        /// <param name="constructorArgs"></param>
        /// <param name="propertyInfos"></param>
        /// <param name="propertyValues"></param>
        public void AddAttribute(string columnName, Type attributeType, Type[] types, object?[] constructorArgs, PropertyInfo[]? propertyInfos = null, object?[]? propertyValues = null)
        {
            var attr = attributeType.GetConstructor(types);
            if (attr != null)
            {
                var cab = new CustomAttributeBuilder(attr, constructorArgs,
                    namedProperties: propertyInfos ?? Array.Empty<PropertyInfo>(),
                    propertyValues: propertyValues ?? Array.Empty<object?>());
                CustomerAttributeBuilderCache.AddOrUpdate(columnName,
                    key => new List<CustomAttributeBuilder> { cab },
                    (key, builders) =>
                    {
                        builders.Add(cab);
                        return builders;
                    });
            }
        }

        /// <summary>
        /// 对象配置方法
        /// </summary>
        protected internal virtual void OnConfigurating()
        {

        }

        /// <summary>
        /// 列创建回调方法 入口参数为 ITableColumn 实例 返回值为 CustomAttributeBuilder 集合
        /// </summary>
        protected internal virtual IEnumerable<CustomAttributeBuilder> OnColumnCreating(ITableColumn col) => CustomerAttributeBuilderCache.TryGetValue(col.GetFieldName(), out var builders)
            ? builders
            : Enumerable.Empty<CustomAttributeBuilder>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract Task<IDynamicObject> AddAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract Task<bool> SaveAsync(IDynamicObject item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public abstract Task<bool> DeleteAsync(IEnumerable<IDynamicObject> items);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Func<DynamicObjectContextArgs, Task>? OnChanged { get; set; }
    }
}
