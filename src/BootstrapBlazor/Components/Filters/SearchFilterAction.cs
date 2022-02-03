// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IFilterAction 类默认实现类
/// </summary>
public class SearchFilterAction : IFilterAction
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public FilterAction Action { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="action"></param>
    public SearchFilterAction(string name, object value, FilterAction action = FilterAction.Contains)
    {
        Name = name;
        Value = value;
        Action = action;
    }

    /// <summary>
    /// 获取所有过滤条件集合
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerable<FilterKeyValueAction> GetFilterConditions() => new List<FilterKeyValueAction>()
        {
            new()
            {
                FieldKey = Name,
                FieldValue = Value,
                FilterAction = Action,
            }
        };
}
