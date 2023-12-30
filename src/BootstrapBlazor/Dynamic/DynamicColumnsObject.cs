// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
