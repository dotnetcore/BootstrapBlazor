// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SubCascader 组件</para>
/// <para lang="en">SubCascader component</para>
/// </summary>
public partial class SubCascader
{
    /// <summary>
    /// <para lang="zh">获得/设置 组件数据源</para>
    /// <para lang="en">Gets or sets the component data source</para>
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public IEnumerable<CascaderItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选择项点击回调委托</para>
    /// <para lang="en">Gets or sets the selected item click callback delegate</para>
    /// </summary>
    [Parameter]
    public Func<CascaderItem, Task>? OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子菜单指示图标</para>
    /// <para lang="en">Gets or sets the submenu indicator icon</para>
    /// </summary>
    [Parameter]
    public string? SubMenuIcon { get; set; }

    [CascadingParameter]
    [NotNull]
    private List<CascaderItem>? SelectedItems { get; set; }

    private string? GetClassString(string classString, CascaderItem item) => CssBuilder.Default(classString)
        .AddClass("active", SelectedItems.Contains(item))
        .Build();

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        Items ??= Enumerable.Empty<CascaderItem>();
    }

    private async Task OnClickItem(CascaderItem item)
    {
        if (OnClick != null)
        {
            await OnClick(item);
        }
    }
}
