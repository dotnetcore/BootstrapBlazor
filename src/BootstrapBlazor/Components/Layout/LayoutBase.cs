// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Components;

/// <summary>
/// Layout 组件基类
/// </summary>
public abstract class LayoutBase : BootstrapComponentBase, IAsyncDisposable
{
    /// <summary>
    /// 
    /// </summary>
    protected bool IsSmallScreen { get; set; }

    /// <summary>
    /// 获得/设置 侧边栏状态
    /// </summary>
    [Parameter]
    public bool IsCollapsed { get; set; }

    /// <summary>
    /// 获得/设置 侧边栏状态
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsCollapsedChanged { get; set; }

    /// <summary>
    /// 获得/设置 Header 模板
    /// </summary>
    [Parameter]
    public RenderFragment? Header { get; set; }

    /// <summary>
    /// 获得/设置 Footer 模板
    /// </summary>
    [Parameter]
    public RenderFragment? Footer { get; set; }

    /// <summary>
    /// 获得/设置 Side 模板
    /// </summary>
    [Parameter]
    public RenderFragment? Side { get; set; }

    /// <summary>
    /// 获得/设置 NotAuthorized 模板
    /// </summary>
    [Parameter]
    public RenderFragment? NotAuthorized { get; set; }

    /// <summary>
    /// 获得/设置 NotFound 模板
    /// </summary>
    [Parameter]
    public RenderFragment? NotFound { get; set; }

    /// <summary>
    /// 获得/设置 NotFound 标签文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? NotFoundTabText { get; set; }

    /// <summary>
    /// 获得/设置 Footer 高度 支持百分比 默认宽度为 300px
    /// </summary>
    [Parameter]
    public string SideWidth { get; set; } = "300";

    /// <summary>
    /// 获得/设置 Main 模板
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment? Main { get; set; }

    /// <summary>
    /// 获得/设置 侧边栏是否占满整个左侧 默认为 false
    /// </summary>
    [Parameter]
    public bool IsFullSide { get; set; }

    /// <summary>
    /// 获得/设置 是否为正页面布局 默认为 false
    /// </summary>
    [Parameter]
    public bool IsPage { get; set; }

    /// <summary>
    /// 获得/设置 侧边栏菜单集合
    /// </summary>
    [Parameter]
    public IEnumerable<MenuItem>? Menus { get; set; }

    /// <summary>
    /// 获得/设置 是否右侧使用 Tab 组件 默认为 false 不使用
    /// </summary>
    [Parameter]
    public bool UseTabSet { get; set; }

    /// <summary>
    /// 获得/设置 是否仅渲染 Active 标签
    /// </summary>
    [Parameter]
    public bool IsOnlyRenderActiveTab { get; set; }

    /// <summary>
    /// 获得/设置 是否固定 Footer 组件
    /// </summary>
    [Parameter]
    public bool IsFixedFooter { get; set; }

    /// <summary>
    /// 获得/设置 是否固定 Header 组件
    /// </summary>
    [Parameter]
    public bool IsFixedHeader { get; set; }

    /// <summary>
    /// 获得/设置 是否显示收缩展开 Bar
    /// </summary>
    [Parameter]
    public bool ShowCollapseBar { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Footer 模板 默认 false
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; }

    /// <summary>
    /// 获得/设置 是否显示返回顶端按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowGotoTop { get; set; }

    /// <summary>
    /// 获得/设置 点击菜单时回调委托方法 默认为 null
    /// </summary>
    [Parameter]
    public Func<MenuItem, Task>? OnClickMenu { get; set; }

    /// <summary>
    /// 获得/设置 收缩展开回调委托
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnCollapsed { get; set; }

    /// <summary>
    /// 获得/设置 默认标签页 关闭所以标签页时自动打开此地址 默认 null 未设置
    /// </summary>
    [Parameter]
    public string TabDefaultUrl { get; set; } = "";

    /// <summary>
    /// 获得/设置 授权回调方法多用于权限控制
    /// </summary>
    [Parameter]
    public Func<string, Task<bool>>? OnAuthorizing { get; set; }

    /// <summary>
    /// 获得/设置 未授权导航地址 默认为 "/Account/Login" Cookie 模式登录页
    /// </summary>
    [Parameter]
    public string NotAuthorizeUrl { get; set; } = "/Account/Login";

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    protected NavigationManager? Navigation { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (OnAuthorizing != null)
        {
            Navigation.LocationChanged += Navigation_LocationChanged;
        }
    }

    private async void Navigation_LocationChanged(object? sender, LocationChangedEventArgs e)
    {
        if (OnAuthorizing != null)
        {
            var auth = await OnAuthorizing(e.Location);
            if (!auth)
            {
                Navigation.NavigateTo(NotAuthorizeUrl, true);
            }
        }
    }

    /// <summary>
    /// 点击 收缩展开按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    protected async Task CollapseMenu()
    {
        IsCollapsed = !IsCollapsed;
        if (IsCollapsedChanged.HasDelegate)
        {
            await IsCollapsedChanged.InvokeAsync(IsCollapsed);
        }

        if (OnCollapsed != null)
        {
            await OnCollapsed(IsCollapsed);
        }
    }

    /// <summary>
    /// 点击菜单时回调此方法
    /// </summary>
    /// <returns></returns>
    protected Func<MenuItem, Task> ClickMenu() => async item =>
    {
        // 小屏幕时生效
        if (IsSmallScreen && !item.Items.Any())
        {
            await CollapseMenu();
        }

        if (OnClickMenu != null)
        {
            await OnClickMenu(item);
        }
    };

    /// <summary>
    /// DisposeAsyncCore 方法
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual ValueTask DisposeAsyncCore(bool disposing)
    {
        if (disposing)
        {
            if (OnAuthorizing != null)
            {
                Navigation.LocationChanged -= Navigation_LocationChanged;
            }
        }
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore(true);
        GC.SuppressFinalize(this);
    }
}
