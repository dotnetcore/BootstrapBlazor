// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class LayoutPages
{
    private List<SelectedItem> SideBarItems { get; } =
    [
        new("left-right", "左右结构"),
        new("top-bottom", "上下结构")
    ];

    [NotNull]
    private SelectedItem? ActiveItem { get; set; }

    private string? StyleString => CssBuilder.Default()
        .AddClass($"height: {Height * 100}px", Height > 0)
        .Build();

    private double Height { get; set; }

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
    protected override void OnInitialized()
    {
        base.OnInitialized();

        IsFullSide = RootPage.IsFullSide;
        IsFixedHeader = RootPage.IsFixedHeader;
        IsFixedFooter = RootPage.IsFixedFooter;
        ShowFooter = RootPage.ShowFooter;
        UseTabSet = RootPage.UseTabSet;

        ActiveItem = IsFullSide ? SideBarItems[0] : SideBarItems[1];
    }

    private Task OnFooterChanged(CheckboxState state, bool val)
    {
        ShowFooter = val;
        Update();
        return Task.CompletedTask;
    }

    private Task OnTabStateChanged(CheckboxState state, bool val)
    {
        IsFixedTab = val;
        Update();
        return Task.CompletedTask;
    }

    private Task OnHeaderStateChanged(CheckboxState state, bool val)
    {
        IsFixedHeader = val;
        Update();
        return Task.CompletedTask;
    }

    private Task OnFooterStateChanged(CheckboxState state, bool val)
    {
        IsFixedFooter = val;
        Update();
        return Task.CompletedTask;
    }

    private Task OnSideChanged(IEnumerable<SelectedItem> values, SelectedItem item)
    {
        ActiveItem.Active = false;
        item.Active = true;
        ActiveItem = item;
        IsFullSide = item.Value == "left-right";
        Update();
        return Task.CompletedTask;
    }

    private Task OnUseTabSetChanged(bool val)
    {
        UseTabSet = val;
        Update();
        return Task.CompletedTask;
    }

    /// <summary>
    /// UpdateAsync 方法
    /// </summary>
    /// <returns></returns>
    public void Update()
    {
        RootPage.IsFullSide = IsFullSide;
        RootPage.IsFixedFooter = IsFixedFooter && ShowFooter;
        RootPage.IsFixedHeader = IsFixedHeader;
        RootPage.IsFixedTab = IsFixedTab;
        RootPage.ShowFooter = ShowFooter;
        RootPage.UseTabSet = UseTabSet;
        StateHasChanged();
        RootPage.Update();
    }

    private Task OnNavigation()
    {
        Navigator.NavigateTo("layout-page1");
        return Task.CompletedTask;
    }
}
