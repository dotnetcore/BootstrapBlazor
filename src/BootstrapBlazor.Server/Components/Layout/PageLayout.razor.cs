// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// 
/// </summary>
public sealed partial class PageLayout
{
    private string? Theme { get; set; }

    private string? LayoutClassString => CssBuilder.Default("layout-demo")
        .AddClass(Theme)
        .Build();

    private IEnumerable<MenuItem>? Menus { get; set; }

    /// <summary>
    /// 获得/设置 是否固定页头
    /// </summary>
    public bool IsFixedHeader { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否固定页脚
    /// </summary>
    public bool IsFixedFooter { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否固定页脚
    /// </summary>
    public bool IsFixedTabHeader { get; set; } = false;

    /// <summary>
    /// 获得/设置 侧边栏是否外置
    /// </summary>
    public bool IsFullSide { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示页脚
    /// </summary>
    public bool ShowFooter { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否开启多标签模式
    /// </summary>
    public bool UseTabSet { get; set; } = true;

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        // 模拟异步获取菜单数据
        await Task.Delay(10);

        Menus = new List<MenuItem>
        {
            new() { Text = "返回文档", Icon = "fa-fw fa-solid fa-house", Url = "introduction" },
            new() { Text = "后台模拟器", Icon = "fa-fw fa-solid fa-desktop", Url = "layout-page" },
            new() { Text = "示例网页", Icon = "fa-fw fa-solid fa-laptop", Url = "layout-demo/text=Parameter1" }
        };
    }

    /// <summary>
    /// 更新组件方法
    /// </summary>
    public void Update() => StateHasChanged();
}
