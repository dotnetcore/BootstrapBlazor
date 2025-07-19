// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class SocketDataConverterCollections
{
    Dictionary<Type, ISocketDataConverter> _converters = new(32);

    /// <summary>
    /// 增加数据类型转换器方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="converter"></param>
    public void Add<TEntity>(ISocketDataConverter<TEntity> converter)
    {
        var type = typeof(TEntity);
        _converters.Add(type, converter);
    }

    /// <summary>
    /// 获得指定数据类型转换器方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public ISocketDataConverter<TEntity>? GetConverter<TEntity>()
    {
        ISocketDataConverter<TEntity>? converter = null;
        var type = typeof(TEntity);
        if (_converters.TryGetValue(type, out var v) && v is ISocketDataConverter<TEntity> c)
        {
            converter = c;
        }
        return converter;
    }
}
