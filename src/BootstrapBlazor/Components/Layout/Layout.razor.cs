// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// Layout 组件
/// </summary>
public partial class Layout : IHandlerException
{
    private bool IsSmallScreen { get; set; }

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
    /// 获得/设置 菜单手风琴效果
    /// </summary>
    [Parameter]
    public bool IsAccordion { get; set; }

    /// <summary>
    /// 获得/设置 收起展开按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment? CollapseBarTemplate { get; set; }

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
    /// 获得/设置 MenuBar 图标
    /// </summary>
    [Parameter]
    public string? MenuBarIcon { get; set; }

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
    /// 获得/设置 侧边栏宽度，支持百分比，设置 0 时关闭宽度功能 默认值 300
    /// </summary>
    [Parameter]
    public string? SideWidth { get; set; }

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
    /// 获得/设置 是否允许拖动标签页 默认 true
    /// </summary>
    [Parameter]
    public bool AllowDragTab { get; set; } = true;

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
    /// 获得/设置 是否显示收缩展开 Bar 默认 false
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

    [Inject]
    [NotNull]
    private NavigationManager? Navigation { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private bool SubscribedLocationChangedEvent { get; set; }

    /// <summary>
    /// 获得/设置 是否已授权
    /// </summary>
    private bool IsAuthenticated { get; set; }

    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("layout")
        .AddClass("has-sidebar", Side != null && IsFullSide)
        .AddClass("is-page", IsPage)
        .AddClass("has-footer", ShowFooter)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 页脚样式
    /// </summary>
    private string? FooterClassString => CssBuilder.Default("layout-footer")
        .AddClass("is-fixed", IsFixedFooter)
        .AddClass("is-collapsed", IsCollapsed)
        .Build();

    /// <summary>
    /// 获得 页头样式
    /// </summary>
    private string? HeaderClassString => CssBuilder.Default("layout-header")
        .AddClass("is-fixed", IsFixedHeader)
        .Build();

    /// <summary>
    /// 获得 侧边栏样式
    /// </summary>
    private string? SideClassString => CssBuilder.Default("layout-side")
        .AddClass("is-collapsed", IsCollapsed)
        .AddClass("is-fixed-header", IsFixedHeader)
        .AddClass("is-fixed-footer", IsFixedFooter)
        .Build();

    /// <summary>
    /// 获得 侧边栏 Style 字符串
    /// </summary>
    private string? SideStyleString => CssBuilder.Default()
        .AddClass($"width: {SideWidth.ConvertToPercentString()}", !string.IsNullOrEmpty(SideWidth) && SideWidth != "0")
        .Build();

    /// <summary>
    /// 获得 Main 样式
    /// </summary>
    private string? MainClassString => CssBuilder.Default("layout-main")
        .AddClass("is-collapsed", IsCollapsed)
        .Build();

    /// <summary>
    /// 获得 展开收缩 Bar 样式
    /// </summary>
    private string? CollapseBarClassString => CssBuilder.Default("layout-header-bar")
        .AddClass("is-collapsed", IsCollapsed)
        .Build();

    /// <summary>
    /// 获得/设置 排除地址支持通配符
    /// </summary>
    [Parameter]
    public IEnumerable<string>? ExcludeUrls { get; set; }

    /// <summary>
    /// 获得/设置 Gets or sets a collection of additional assemblies that should be searched for components that can match URIs.
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<Assembly>? AdditionalAssemblies { get; set; }

    /// <summary>
    /// 获得/设置 鼠标悬停提示文字信息
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TooltipText { get; set; }

    /// <summary>
    /// 获得/设置 更新回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnUpdateAsync { get; set; }

    /// <summary>
    /// 获得/设置 AuthorizeRouteView 参数
    /// </summary>
    [Parameter]
    public object? Resource { get; set; }

    /// <summary>
    /// 获得 登录授权信息
    /// </summary>
    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationStateTask { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Layout>? Localizer { get; set; }

    [Inject]
    private IAuthorizationPolicyProvider? AuthorizationPolicyProvider { get; set; }

    [Inject]
    private IAuthorizationService? AuthorizationService { get; set; }

    private bool IsInit { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (OnAuthorizing != null)
        {
            SubscribedLocationChangedEvent = true;
            Navigation.LocationChanged += Navigation_LocationChanged;
        }

        ErrorLogger?.Register(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        // 需要认证并且未认证
        if (AuthenticationStateTask != null)
        {
            // wasm 模式下 开启权限必须提供 AdditionalAssemblies 参数
            AdditionalAssemblies ??= new[] { Assembly.GetEntryAssembly()! };

            var url = Navigation.ToBaseRelativePath(Navigation.Uri);
            var context = RouteTableFactory.Create(AdditionalAssemblies, url);
            if (context.Handler != null)
            {
                IsAuthenticated = await context.Handler.IsAuthorizedAsync(AuthenticationStateTask, AuthorizationPolicyProvider, AuthorizationService, Resource);

                // 检查当前 Url
                if (OnAuthorizing != null)
                {
                    IsAuthenticated = IsAuthenticated && await OnAuthorizing(Navigation.Uri);
                }
            }
        }
        else
        {
            IsAuthenticated = true;
        }

        IsInit = true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        TooltipText ??= Localizer[nameof(TooltipText)];
        SideWidth ??= "300";

        MenuBarIcon ??= IconTheme.GetIconByKey(ComponentIcons.LayoutMenuBarIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(SetCollapsed));

    /// <summary>
    /// HandlerMain 方法
    /// </summary>
    /// <returns></returns>
    protected virtual RenderFragment HandlerMain() => builder =>
    {
        builder.AddContent(0, _errorContent ?? Main);
        _errorContent = null;
    };

    /// <summary>
    /// 设置侧边栏收缩方法 客户端监控 window.onResize 事件回调此方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public void SetCollapsed(int width)
    {
        IsSmallScreen = width < 768;
    }

    /// <summary>
    /// 调用 Update 回调方法
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task UpdateAsync(string key)
    {
        if (OnUpdateAsync != null)
        {
            await OnUpdateAsync(key);
        }
    }

    private void Navigation_LocationChanged(object? sender, LocationChangedEventArgs e)
    {
        if (OnAuthorizing != null)
        {
            InvokeAsync(async () =>
            {
                var auth = await OnAuthorizing(e.Location);
                if (!auth)
                {
                    Navigation.NavigateTo(NotAuthorizeUrl, true);
                }
            });
        }
    }

    /// <summary>
    /// 点击 收缩展开按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    private async Task CollapseMenu()
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
    private Func<MenuItem, Task> ClickMenu() => async item =>
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
    /// 上次渲染错误内容
    /// </summary>
    private RenderFragment? _errorContent;

    /// <summary>
    /// HandlerException 错误处理方法
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="errorContent"></param>
    public virtual Task HandlerException(Exception ex, RenderFragment<Exception> errorContent)
    {
        _errorContent = errorContent(ex);
        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        if (disposing)
        {
            ErrorLogger?.UnRegister(this);
            if (SubscribedLocationChangedEvent)
            {
                Navigation.LocationChanged -= Navigation_LocationChanged;
            }
        }
    }
}
