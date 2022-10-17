// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared;

/// <summary>
/// 
/// </summary>
public class BootstrapBlazorDynamicObjectData : IDynamicObject
{
    /// <summary>
    /// 
    /// </summary>
    public string Fix { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, string> Dynamic { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid DynamicObjectPrimaryKey { get => new(Fix); set { } }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fix"></param>
    /// <param name="data"></param>
    public BootstrapBlazorDynamicObjectData(string fix, Dictionary<string, string> data)
    {
        Fix = fix;
        Dynamic = data;
    }

    /// <summary>
    /// 
    /// </summary>
    public BootstrapBlazorDynamicObjectData() : this("", new()) { }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public object? GetValue(string propertyName)
    {
        if (propertyName == nameof(Fix))
            return Fix;
        return Dynamic.TryGetValue(propertyName, out string? v) ? v : "";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    public void SetValue(string propertyName, object? value)
    {
        if (value is not string str)
            return;
        if (propertyName == nameof(Fix))
            Fix = str;
        Dynamic[propertyName] = str;
    }
}
