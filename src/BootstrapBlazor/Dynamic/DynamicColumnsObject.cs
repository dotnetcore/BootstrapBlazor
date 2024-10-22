// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 动态列类
/// </summary>
public class DynamicColumnsObject : IDynamicColumnsObject
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Dictionary<string, object?> Columns { get; set; }

    /// <summary>
    /// 获得/设置 行主键
    /// </summary>
    public Guid DynamicObjectPrimaryKey { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="columnsData"></param>
    public DynamicColumnsObject(Dictionary<string, object?> columnsData)
    {
        Columns = columnsData;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public DynamicColumnsObject() : this([]) { }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public virtual object? GetValue(string propertyName)
    {
        return Columns.TryGetValue(propertyName, out object? v) ? v : null;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    public virtual void SetValue(string propertyName, object? value)
    {
        Columns[propertyName] = value;
    }
}
