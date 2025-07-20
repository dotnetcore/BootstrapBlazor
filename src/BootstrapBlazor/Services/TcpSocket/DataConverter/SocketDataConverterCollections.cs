// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class SocketDataConverterCollections
{
    readonly ConcurrentDictionary<Type, ISocketDataConverter> _converters = new();
    readonly ConcurrentDictionary<MemberInfo, SocketDataPropertyConverterAttribute> _propertyConverters = new();

    /// <summary>
    /// 增加数据类型转换器方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="converter"></param>
    public void AddOrUpdateTypeConverter<TEntity>(ISocketDataConverter<TEntity> converter)
    {
        var type = typeof(TEntity);
        _converters.AddOrUpdate(type, t => converter, (t, v) => converter);
    }

    /// <summary>
    /// 添加属性类型转化器方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="propertyExpression"></param>
    /// <param name="attribute"></param>
    public void AddOrUpdatePropertyConverter<TEntity>(Expression<Func<TEntity, object?>> propertyExpression, SocketDataPropertyConverterAttribute attribute)
    {
        if (propertyExpression.Body is MemberExpression memberExpression)
        {
            if(attribute.Type == null)
            {
                attribute.Type = memberExpression.Type;
            }
            _propertyConverters.AddOrUpdate(memberExpression.Member, m => attribute, (m, v) => attribute);
        }
    }

    /// <summary>
    /// 获得指定数据类型转换器方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public bool TryGetTypeConverter<TEntity>([NotNullWhen(true)] out ISocketDataConverter<TEntity>? converter)
    {
        converter = null;
        var ret = false;
        if (_converters.TryGetValue(typeof(TEntity), out var v) && v is ISocketDataConverter<TEntity> c)
        {
            converter = c;
            ret = true;
        }
        return ret;
    }

    /// <summary>
    /// 获得指定数据类型属性转换器方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public bool TryGetPropertyConverter<TEntity>(Expression<Func<TEntity, object?>> propertyExpression, [NotNullWhen(true)] out SocketDataPropertyConverterAttribute? converterAttribute)
    {
        converterAttribute = null;
        var ret = false;
        if (propertyExpression.Body is MemberExpression memberExpression && TryGetPropertyConverter<TEntity>(memberExpression.Member, out var v))
        {
            converterAttribute = v;
            ret = true;
        }
        return ret;
    }

    /// <summary>
    /// 获得指定数据类型属性转换器方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public bool TryGetPropertyConverter<TEntity>(MemberInfo memberInfo, [NotNullWhen(true)] out SocketDataPropertyConverterAttribute? converterAttribute)
    {
        converterAttribute = null;
        var ret = false;
        if (_propertyConverters.TryGetValue(memberInfo, out var v))
        {
            converterAttribute = v;
            ret = true;
        }
        return ret;
    }
}
