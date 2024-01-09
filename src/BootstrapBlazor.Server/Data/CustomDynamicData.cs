// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    public Dictionary<string, string> Columns { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fix"></param>
    /// <param name="data"></param>
    public CustomDynamicData(string fix, Dictionary<string, string> data)
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
        else if (Columns.ContainsKey(binder.Name))
        {
            result = Columns[binder.Name];
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
