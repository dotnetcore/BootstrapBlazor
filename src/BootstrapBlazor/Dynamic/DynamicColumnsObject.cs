// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh"><see cref="IDynamicColumnsObject"/> 实现类</para>
/// <para lang="en"><see cref="IDynamicColumnsObject"/> implementation class</para>
/// </summary>
public class DynamicColumnsObject : IDynamicColumnsObject
{
    /// <summary>
    /// <para lang="zh">获得/设置 列集合</para>
    /// <para lang="en">Gets or sets the column collection</para>
    /// </summary>
    public Dictionary<string, object?> Columns { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 行主键</para>
    /// <para lang="en">Gets or sets the row primary key</para>
    /// </summary>
    public Guid DynamicObjectPrimaryKey { get; set; }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    /// <param name="columnsData"></param>
    public DynamicColumnsObject(Dictionary<string, object?> columnsData)
    {
        Columns = columnsData;
    }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    public DynamicColumnsObject() : this([]) { }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyName"></param>
    public virtual object? GetValue(string propertyName)
    {
        return Columns.TryGetValue(propertyName, out object? v) ? v : null;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual void SetValue(string propertyName, object? value)
    {
        Columns[propertyName] = value;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual void Cancel() { }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual void Accept() { }
}
