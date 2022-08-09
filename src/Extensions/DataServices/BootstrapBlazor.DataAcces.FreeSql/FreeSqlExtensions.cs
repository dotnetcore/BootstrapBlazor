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

        foreach (var filter in option.Filters)
        {
            // Filter 之间默认为 and
            ret.Filters.Add(filter.ToDynamicFilter());
        }

        foreach (var search in option.CustomerSearchs)
        {
            // 自定义搜索条件 之间默认为 and
            ret.Filters.Add(search.ToDynamicFilter());
        }

        foreach (var search in option.AdvanceSearchs)
        {
            // 高级搜索条件 之间默认为 and
            ret.Filters.Add(search.ToDynamicFilter());
        }

        if (option.Searchs.Any())
        {
            // Searchs 之间默认为 or
            var searchTextFilter = new DynamicFilterInfo()
            {
                Logic = DynamicFilterLogic.Or,
                Filters = new List<DynamicFilterInfo>()
            };
            foreach (var search in option.Searchs)
            {
                searchTextFilter.Filters.Add(search.ToDynamicFilter());
            }
            ret.Filters.Add(searchTextFilter);
        }
        return ret;
    }

    private static DynamicFilterInfo ToDynamicFilter(this IFilterAction filter)
    {
        // TableFilter 最多仅两个条件
        var actions = filter.GetFilterConditions();

        var item = new DynamicFilterInfo() { Filters = new List<DynamicFilterInfo>() };
        if (actions.Any())
        {
            var f = actions.First();
            item.Filters.Add(new DynamicFilterInfo()
            {
                Field = f.FieldKey,
                Value = f.FieldValue,
                Operator = f.FilterAction.ToDynamicFilterOperator()
            });

            if (actions.Count() > 1)
            {
                var c = actions.ElementAt(1);
                item.Logic = c.FilterLogic.ToDynamicFilterLogic();
                item.Filters.Add(new DynamicFilterInfo()
                {
                    Field = c.FieldKey,
                    Value = c.FieldValue,
                    Operator = c.FilterAction.ToDynamicFilterOperator()
                });
            }
        }
        return item;
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
