// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Shared;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class LayoutPages
{
    private IEnumerable<SelectedItem> SideBarItems { get; set; } = new SelectedItem[]
    {
            new SelectedItem("left-right", "左右结构"),
            new SelectedItem("top-bottom", "上下结构")
    };

    private string? StyleString => CssBuilder.Default()
        .AddClass($"height: {Height * 100}px", Height > 0)
        .Build();

    private int Height { get; set; }

    /// <summary>
    /// 获得/设置 是否显示页脚
    /// </summary>
    private bool ShowFooter { get; set; }

    /// <summary>
    /// 获得/设置 是否固定 TabHeader
    /// </summary>
    private bool IsFixedTab { get; set; }

    /// <summary>
    /// 获得/设置 是否固定 Header
    /// </summary>
    private bool IsFixedHeader { get; set; }

    /// <summary>
    /// 获得/设置 是否固定页脚
    /// </summary>
    private bool IsFixedFooter { get; set; }

    /// <summary>
    /// 获得/设置 侧边栏是否外置
    /// </summary>
    private bool IsFullSide { get; set; }

    /// <summary>
    /// 获得/设置 是否开启多标签模式
    /// </summary>
    private bool UseTabSet { get; set; }

    [CascadingParameter]
    [NotNull]
    private PageLayout? RootPage { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? Navigator { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        IsFullSide = RootPage.IsFullSide;
        IsFixedHeader = RootPage.IsFixedHeader;
        IsFixedFooter = RootPage.IsFixedFooter;
        ShowFooter = RootPage.ShowFooter;
        UseTabSet = RootPage.UseTabSet;

        SideBarItems.ElementAt(IsFullSide ? 0 : 1).Active = true;
    }

    private Task OnFooterChanged(CheckboxState state, bool val) => UpdateAsync();

    private Task OnTabStateChanged(CheckboxState state, bool val) => UpdateAsync();

    private Task OnHeaderStateChanged(CheckboxState state, bool val) => UpdateAsync();

    private Task OnFooterStateChanged(CheckboxState state, bool val) => UpdateAsync();

    private async Task OnSideChanged(IEnumerable<SelectedItem> values, SelectedItem item)
    {
        IsFullSide = item.Value == "left-right";
        await UpdateAsync();
    }

    private Task OnUseTabSetChanged(bool val) => UpdateAsync();

    /// <summary>
    /// UpdateAsync 方法
    /// </summary>
    /// <returns></returns>
    public async Task UpdateAsync()
    {
        var parameters = new Dictionary<string, object?>()
        {
            [nameof(RootPage.IsFullSide)] = IsFullSide,
            [nameof(RootPage.IsFixedFooter)] = IsFixedFooter && ShowFooter,
            [nameof(RootPage.IsFixedHeader)] = IsFixedHeader,
            [nameof(RootPage.IsFixedTab)] = IsFixedTab,
            [nameof(RootPage.ShowFooter)] = ShowFooter,
            [nameof(RootPage.UseTabSet)] = UseTabSet
        };

        await RootPage.SetParametersAsync(ParameterView.FromDictionary(parameters));

        // 获得 Razor 示例代码
        RootPage.Update();
    }

    private Task OnNavigation()
    {
        Navigator.NavigateTo("layout-page1");
        return Task.CompletedTask;
    }
}
