// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 
/// </summary>
public class CustomDynamicColumnsObjectData : DynamicColumnsObject
{
    /// <summary>
    /// 
    /// </summary>
    public string? Fix { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public CustomDynamicColumnsObjectData() : this("", []) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fix"></param>
    /// <param name="data"></param>
    public CustomDynamicColumnsObjectData(string? fix, Dictionary<string, object?> data)
    {
        Fix = fix;
        Columns = data;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public override object? GetValue(string propertyName)
    {
        return propertyName == nameof(Fix) ? Fix : base.GetValue(propertyName);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    public override void SetValue(string propertyName, object? value)
    {
        if (propertyName == nameof(Fix))
            Fix = value?.ToString();
        Columns[propertyName] = value;
    }
}
