// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using FreeSql.Internal.Model;

namespace BootstrapBlazor.DataAcces.FreeSql;

/// <summary>
/// 
/// </summary>
public static class FreeSqlExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static DynamicFilterInfo ToDynamicFilter(this QueryPageOptions option)
    {
        var ret = new DynamicFilterInfo() { Filters = new List<DynamicFilterInfo>() };

        foreach (var filter in option.Filters.Concat(option.Searchs))
        {
            var item = new DynamicFilterInfo() { Filters = new List<DynamicFilterInfo>() };
            var actions = filter.GetFilterConditions();
            foreach (var f in actions)
            {
                item.Logic = f.FilterLogic.ToDynamicFilterLogic();
                item.Filters.Add(new DynamicFilterInfo()
                {
                    Field = f.FieldKey,
                    Value = f.FieldValue,
                    Operator = f.FilterAction.ToDynamicFilterOperator()
                });
            }
            ret.Filters.Add(item);
        }
        return ret;
    }

    private static DynamicFilterLogic ToDynamicFilterLogic(this FilterLogic logic) => logic switch
    {
        FilterLogic.And => DynamicFilterLogic.And,
        _ => DynamicFilterLogic.Or
    };

    private static DynamicFilterOperator ToDynamicFilterOperator(this FilterAction action) => action switch
    {
        FilterAction.Equal => DynamicFilterOperator.Equal,
        FilterAction.NotEqual => DynamicFilterOperator.NotEqual,
        FilterAction.Contains => DynamicFilterOperator.Contains,
        FilterAction.NotContains => DynamicFilterOperator.NotContains,
        FilterAction.GreaterThan => DynamicFilterOperator.GreaterThan,
        FilterAction.GreaterThanOrEqual => DynamicFilterOperator.GreaterThanOrEqual,
        FilterAction.LessThan => DynamicFilterOperator.LessThan,
        FilterAction.LessThanOrEqual => DynamicFilterOperator.LessThanOrEqual,
        _ => throw new System.NotSupportedException()
    };
}
