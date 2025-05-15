// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Filter component
/// </summary>
public class Filter<TFilter> : FilterProvider where TFilter : IComponent
{
    /// <summary>
    /// 获得/设置 过滤器组件参数集合 Default is null
    /// </summary>
    [Parameter]
    public IDictionary<string, object>? FilterParameters { get; set; }

    /// <summary>
    /// 渲染自定义过滤器方法
    /// </summary>
    /// <returns></returns>
    protected override RenderFragment RenderFilter() => builder =>
    {
        var filterType = typeof(TFilter);
        builder.OpenComponent<TFilter>(0);
        if (filterType.IsSubclassOf(typeof(FilterBase)))
        {
            builder.AddAttribute(1, nameof(FilterBase.FieldKey), FieldKey);
            builder.AddAttribute(2, nameof(FilterBase.IsHeaderRow), IsHeaderRow);
        }
        if (filterType.IsSubclassOf(typeof(MultipleFilterBase)))
        {
            builder.AddAttribute(10, nameof(MultipleFilterBase.Count), Count);
        }

        if (FilterParameters != null)
        {
            builder.AddMultipleAttributes(100, FilterParameters);
        }
        builder.CloseComponent();
    };
}
