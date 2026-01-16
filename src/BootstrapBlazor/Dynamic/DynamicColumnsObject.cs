// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">动态列类</para>
/// <para lang="en">动态列类</para>
/// </summary>
public class DynamicColumnsObject : IDynamicColumnsObject
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Dictionary<string, object?> Columns { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 行主键</para>
    /// <para lang="en">Gets or sets 行主键</para>
    /// </summary>
    public Guid DynamicObjectPrimaryKey { get; set; }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">构造函数</para>
    /// </summary>
    /// <param name="columnsData"></param>
    public DynamicColumnsObject(Dictionary<string, object?> columnsData)
    {
        Columns = columnsData;
    }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">构造函数</para>
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
