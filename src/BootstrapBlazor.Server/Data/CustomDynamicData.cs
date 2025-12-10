// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Dynamic;

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 
/// </summary>
public class CustomDynamicData : System.Dynamic.DynamicObject
{
    /// <summary>
    /// 获得/设置 固定列
    /// </summary>
    public string Fix { get; set; } = "";

    /// <summary>
    /// 存储每列值信息 Key 列名 Value 为列值
    /// </summary>
    public Dictionary<string, object?> Columns { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fix"></param>
    /// <param name="data"></param>
    public CustomDynamicData(string fix, Dictionary<string, object?> data)
    {
        Fix = fix;
        Columns = data;
    }

    /// <summary>
    /// 
    /// </summary>
    public CustomDynamicData() : this("", []) { }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="binder"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        if (binder.Name == nameof(Fix))
        {
            result = Fix;
        }
        else if (Columns.TryGetValue(binder.Name, out object? value))
        {
            result = value;
        }
        else
        {
            // When property name not found, return empty
            result = "";
        }
        return true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="binder"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        var ret = false;
        var v = value?.ToString() ?? string.Empty;
        if (binder.Name == nameof(Fix))
        {
            Fix = v;
            ret = true;
        }
        else if (Columns.ContainsKey(binder.Name))
        {
            Columns[binder.Name] = v;
            ret = true;
        }
        return ret;
    }
}
