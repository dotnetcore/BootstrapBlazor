// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Dynamic;

namespace BootstrapBlazor.Shared;

/// <summary>
/// 
/// </summary>
public class CustomDynamicData : DynamicObject
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
    /// <param name="fix"></param>
    /// <param name="data"></param>
    public CustomDynamicData(string fix, Dictionary<string, string> data)
    {
        Fix = fix;
        Dynamic = data;
    }

    /// <summary>
    /// 
    /// </summary>
    public CustomDynamicData() : this("", new()) { }

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
            return true;
        }
        else if (Dynamic.ContainsKey(binder.Name))
        {
            result = Dynamic[binder.Name];
            return true;
        }
        else
        {
            // When property name not found, return empty
            result = "";
            return true;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="binder"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        if (value is not string str)
        {
            return false;
        }
        else if (binder.Name == nameof(Fix))
        {
            Fix = str;
            return true;
        }
        else if (Dynamic.ContainsKey(binder.Name))
        {
            Dynamic[binder.Name] = str;
            return true;
        }
        else
        {
            return true;
        }
    }
}
