// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// Provides a base class for converting socket data into a specified entity type.
/// </summary>
/// <typeparam name="TEntity">The type of entity to convert the socket data into.</typeparam>
public class SocketDataConverter<TEntity>(SocketDataConverterCollections converters) : ISocketDataConverter<TEntity>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SocketDataConverter() : this(new())
    {

    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual bool TryConvertTo(ReadOnlyMemory<byte> data, [NotNullWhen(true)] out TEntity? entity)
    {
        var v = CreateEntity();
        var ret = Parse(data, v);
        entity = ret ? v : default;
        return ret;
    }

    /// <summary>
    /// 创建实体实例方法
    /// </summary>
    /// <returns></returns>
    protected virtual TEntity CreateEntity() => Activator.CreateInstance<TEntity>();

    /// <summary>
    /// 将字节数据转换为指定实体类型的实例。
    /// </summary>
    /// <param name="data"></param>
    /// <param name="entity"></param>
    protected virtual bool Parse(ReadOnlyMemory<byte> data, TEntity entity)
    {
        // 使用 SocketDataPropertyAttribute 特性获取数据转换规则
        var ret = false;
        if (entity != null)
        {
            var unuseProperties = new List<PropertyInfo>(32);

            // 通过 SocketDataPropertyConverterAttribute 特性获取属性转换器
            var properties = entity.GetType().GetProperties().Where(p => p.CanWrite).ToList();
            foreach (var p in properties)
            {
                var attr = p.GetCustomAttribute<SocketDataPropertyConverterAttribute>(false)
                    ?? GetPropertyConverterAttribute(p);
                if (attr != null)
                {
                    p.SetValue(entity, attr.ConvertTo(data));
                }
            }
            ret = true;
        }
        return ret;
    }

    private SocketDataPropertyConverterAttribute? GetPropertyConverterAttribute(PropertyInfo propertyInfo)
    {
        SocketDataPropertyConverterAttribute? attr = null;
        if (converters.TryGetPropertyConverter<TEntity>(propertyInfo, out var v))
        {
            attr = v;
        }
        return attr;
    }
}
