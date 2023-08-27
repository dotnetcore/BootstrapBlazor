// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// QueryPageOptions 扩展方法
/// </summary>
public static class QueryPageOptionsExtensions
{
    /// <summary>
    /// 将 QueryPageOptions 过滤条件转换为 FilterKeyValueAction
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static FilterKeyValueAction ToFilter(this QueryPageOptions option)
    {
        var filter = new FilterKeyValueAction() { Filters = new() };

        // 处理模糊搜索
        if (option.Searches.Any())
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FilterLogic = FilterLogic.Or,
                Filters = option.Searches.Select(i => i.GetFilterConditions()).ToList()
            });
        }

        // 处理自定义搜索
        if (option.CustomerSearches.Any())
        {
            filter.Filters.AddRange(option.CustomerSearches.Select(i => i.GetFilterConditions()));
        }

        // 处理高级搜索
        if (option.AdvanceSearches.Any())
        {
            filter.Filters.AddRange(option.AdvanceSearches.Select(i => i.GetFilterConditions()));
        }

        // 处理表格过滤条件
        if (option.Filters.Any())
        {
            filter.Filters.AddRange(option.Filters.Select(i => i.GetFilterConditions()));
        }

        return filter;
    }

    /// <summary>
    /// 是否包含过滤条件
    /// </summary>
    /// <param name="filterKeyValueAction"></param>
    /// <returns></returns>
    public static bool HasFilters(this FilterKeyValueAction filterKeyValueAction) => filterKeyValueAction.Filters != null && filterKeyValueAction.Filters.Any();
}
