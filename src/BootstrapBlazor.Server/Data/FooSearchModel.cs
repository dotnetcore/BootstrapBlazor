// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared;

/// <summary>
/// 
/// </summary>
public class FooSearchModel : ITableSearchModel
{
    /// <summary>
    /// 
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Count { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTimeRangeValue? SearchDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public EnumEducation? Education { get; set; }

    /// <summary>
    /// 获得 搜索条件集合
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public IEnumerable<IFilterAction> GetSearches()
    {
        var ret = new List<IFilterAction>();
        if (!string.IsNullOrEmpty(Name))
        {
            ret.Add(new SearchFilterAction(nameof(Foo.Name), Name));
        }

        if (!string.IsNullOrEmpty(Count))
        {
            if (Count == "1")
            {
                ret.Add(new SearchFilterAction(nameof(Foo.Count), 30, FilterAction.LessThan));
            }
            else if (Count == "2")
            {
                ret.Add(new SearchFilterAction(nameof(Foo.Count), 30, FilterAction.GreaterThanOrEqual));
                ret.Add(new SearchFilterAction(nameof(Foo.Count), 70, FilterAction.LessThan));
            }
            else if (Count == "3")
            {
                ret.Add(new SearchFilterAction(nameof(Foo.Count), 70, FilterAction.GreaterThanOrEqual));
                ret.Add(new SearchFilterAction(nameof(Foo.Count), 100, FilterAction.LessThan));
            }
        }

        if (SearchDate != null)
        {
            ret.Add(new SearchFilterAction(nameof(Foo.DateTime), SearchDate.Start, FilterAction.GreaterThanOrEqual));
            ret.Add(new SearchFilterAction(nameof(Foo.DateTime), SearchDate.End, FilterAction.LessThanOrEqual));
        }

        if (Education != null)
        {
            ret.Add(new SearchFilterAction(nameof(Foo.Education), Education, FilterAction.Equal));
        }
        return ret;
    }

    /// <summary>
    /// 重置操作
    /// </summary>
    public void Reset()
    {
        Name = null;
        Count = null;
        SearchDate = null;
        Education = null;
    }
}
