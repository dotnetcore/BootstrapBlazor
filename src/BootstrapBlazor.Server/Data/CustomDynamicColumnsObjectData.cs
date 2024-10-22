// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
