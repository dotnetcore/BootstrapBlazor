// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Layout 组件</para>
/// <para lang="en">Layout Component</para>
/// </summary>
public partial class Layout : IHandlerException, ITabHeader
{
    private bool IsSmallScreen { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Tab 标签头文本本地化回调方法</para>
    /// <para lang="en">Gets or sets Localization callback method for Tab header text</para>
    /// </summary>
    [Parameter]
    public Func<string?, string?>? OnTabHeaderTextLocalizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the tab 样式. 默认为 <see cref="TabStyle.Default"/>.</para>
    /// <para lang="en">Gets or sets the tab style. Default is <see cref="TabStyle.Default"/>.</para>
    /// </summary>
    [Parameter]
    public TabStyle TabStyle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 show the toolbar. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether show the toolbar. Default is false.</para>
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 模板 of the toolbar 按钮. 默认为 null.</para>
    /// <para lang="en">Gets or sets the template of the toolbar button. Default is null.</para>
    /// </summary>
    [Parameter]
    public RenderFragment<Tab>? ToolbarTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 show the full screen 按钮. 默认为 true.</para>
    /// <para lang="en">Gets or sets whether show the full screen button. Default is true.</para>
    /// </summary>
    [Parameter]
    public bool ShowFullscreenToolbarButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 the full screen toolbar 按钮 图标 string. 默认为 null.</para>
    /// <para lang="en">Gets or sets the full screen toolbar button icon string. Default is null.</para>
    /// </summary>
    [Parameter]
    public string? FullscreenToolbarButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the full screen toolbar 按钮 tooltip string. 默认为 null.</para>
    /// <para lang="en">Gets or sets the full screen toolbar button tooltip string. Default is null.</para>
    /// </summary>
    [Parameter]
    public string? FullscreenToolbarTooltipText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 show the full screen 按钮. 默认为 true.</para>
    /// <para lang="en">Gets or sets whether show the full screen button. Default is true.</para>
    /// </summary>
    [Parameter]
    public bool ShowRefreshToolbarButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 the refresh toolbar 按钮 图标 string. 默认为 null.</para>
    /// <para lang="en">Gets or sets the refresh toolbar button icon string. Default is null.</para>
    /// </summary>
    [Parameter]
    public string? RefreshToolbarButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the refresh toolbar 按钮 tooltip string. 默认为 null.</para>
    /// <para lang="en">Gets or sets the refresh toolbar button tooltip string. Default is null.</para>
    /// </summary>
    [Parameter]
    public string? RefreshToolbarTooltipText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the refresh toolbar 按钮 click event 回调. 默认为 null.</para>
    /// <para lang="en">Gets or sets the refresh toolbar button click event callback. Default is null.</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnToolbarRefreshCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭标签页前回调方法</para>
    /// <para lang="en">Gets or sets Callback method before closing tab</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">返回 false 时不关闭 <see cref="TabItem"/> 标签页</para>
    /// <para lang="en">Do not close <see cref="TabItem"/> tab when returning false</para>
    /// </remarks>
    [Parameter]
    public Func<TabItem, Task<bool>>? OnCloseTabItemAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏状态</para>
    /// <para lang="en">Gets or sets Sidebar status</para>
    /// </summary>
    [Parameter]
    public bool IsCollapsed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏状态</para>
    /// <para lang="en">Gets or sets Sidebar status</para>
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsCollapsedChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 菜单手风琴效果</para>
    /// <para lang="en">Gets or sets Menu Accordion effect</para>
    /// </summary>
    [Parameter]
    public bool IsAccordion { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 收起展开按钮模板</para>
    /// <para lang="en">Gets or sets Collapse/Expand button template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? CollapseBarTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 模板</para>
    /// <para lang="en">Gets or sets Header Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? Header { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 模板</para>
    /// <para lang="en">Gets or sets Footer Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? Footer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 MenuBar 图标</para>
    /// <para lang="en">Gets or sets MenuBar Icon</para>
    /// </summary>
    [Parameter]
    public string? MenuBarIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Side 模板</para>
    /// <para lang="en">Gets or sets Side Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? Side { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示分割栏 默认 false 不显示 仅在 左右布局时有效</para>
    /// <para lang="en">Gets or sets Whether to show split bar. Default false Effective only in Left-Right Layout</para>
    /// </summary>
    [Parameter]
    public bool ShowSplitBar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示分割栏 默认 false 不显示 仅在 左右布局时有效</para>
    /// <para lang="en">Gets or sets Whether to show split bar. Default false Effective only in Left-Right Layout</para>
    /// </summary>
    [Parameter]
    [ExcludeFromCodeCoverage]
    [Obsolete("已弃用，请使用 ShowSplitBar 单词拼写错误；Deprecated. Please use 'ShowSplitBar' instead. The word 'Splitebar' is misspelled.")]
    public bool ShowSplitebar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏最小宽度 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Sidebar minimum width. Default null</para>
    /// </summary>
    [Parameter]
    public int? SidebarMinWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏最大宽度 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Sidebar maximum width. Default null</para>
    /// </summary>
    [Parameter]
    public int? SidebarMaxWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 NotAuthorized 模板 默认 null NET6.0/7.0 有效</para>
    /// <para lang="en">Gets or sets NotAuthorized Template. Default null. Valid in NET6.0/7.0</para>
    /// </summary>
    [Parameter]
    public RenderFragment? NotAuthorized { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 NotFound 模板 默认 null NET6.0/7.0 有效</para>
    /// <para lang="en">Gets or sets NotFound Template. Default null. Valid in NET6.0/7.0</para>
    /// </summary>
    [Parameter]
    public RenderFragment? NotFound { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 NotFound 标签文本 默认 null NET6.0/7.0 有效</para>
    /// <para lang="en">Gets or sets NotFound Tab Text. Default null. Valid in NET6.0/7.0</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? NotFoundTabText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏宽度，支持百分比，设置 0 时关闭宽度功能 默认值 300</para>
    /// <para lang="en">Gets or sets Sidebar width. Supports percentage. Disable width function when set to 0. Default 300</para>
    /// </summary>
    [Parameter]
    public string? SideWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Main 模板</para>
    /// <para lang="en">Gets or sets Main Template</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment? Main { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏是否占满整个左侧 默认为 false</para>
    /// <para lang="en">Gets or sets Whether the sidebar fills the entire left side. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsFullSide { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为整页面布局 默认为 false</para>
    /// <para lang="en">Gets or sets Whether it is a full page layout. Default false</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">为真时增加 is-page 样式</para>
    /// <para lang="en">Add is-page style when true</para>
    /// </remarks>
    [Parameter]
    public bool IsPage { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏菜单集合</para>
    /// <para lang="en">Gets or sets Sidebar Menu Collection</para>
    /// </summary>
    [Parameter]
    public IEnumerable<MenuItem>? Menus { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否右侧使用 Tab 组件 默认为 false 不使用</para>
    /// <para lang="en">Gets or sets Whether to use Tab component on the right side. Default false</para>
    /// </summary>
    [Parameter]
    public bool UseTabSet { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否固定多标签 Header 默认 false</para>
    /// <para lang="en">Gets or sets Whether to fix multi-tab Header. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsFixedTabHeader { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否仅渲染 Active 标签</para>
    /// <para lang="en">Gets or sets Whether to render only Active Tab</para>
    /// </summary>
    [Parameter]
    public bool IsOnlyRenderActiveTab { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许拖动标签页 默认 true</para>
    /// <para lang="en">Gets or sets Whether to allow dragging tabs. Default true</para>
    /// </summary>
    [Parameter]
    public bool AllowDragTab { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否固定 Footer 组件</para>
    /// <para lang="en">Gets or sets Whether to fix Footer component</para>
    /// </summary>
    [Parameter]
    public bool IsFixedFooter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否固定 Header 组件</para>
    /// <para lang="en">Gets or sets Whether to fix Header component</para>
    /// </summary>
    [Parameter]
    public bool IsFixedHeader { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示收缩展开 Bar 默认 false</para>
    /// <para lang="en">Gets or sets Whether to show Collapse/Expand Bar. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowCollapseBar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Footer 模板 默认 false</para>
    /// <para lang="en">Gets or sets Whether to show Footer Template. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示返回顶端按钮 默认为 false 不显示</para>
    /// <para lang="en">Gets or sets Whether to show "Back to Top" button. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowGotoTop { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击菜单时回调委托方法 默认为 null</para>
    /// <para lang="en">Gets or sets Callback delegate method when menu is clicked. Default null</para>
    /// </summary>
    [Parameter]
    public Func<MenuItem, Task>? OnClickMenu { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 收缩展开回调委托</para>
    /// <para lang="en">Gets or sets Collapse/Expand Callback Delegate</para>
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnCollapsed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 默认标签页 关闭所有标签页时自动打开此地址 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Default Tab. Automatically open this address when closing all tabs. Default null</para>
    /// </summary>
    [Parameter]
    public string TabDefaultUrl { get; set; } = "";

    /// <summary>
    /// <para lang="zh">获得/设置 标签是否显示关闭按钮 默认 true</para>
    /// <para lang="en">Gets or sets Whether to show close button on tab. Default true</para>
    /// </summary>
    [Parameter]
    public bool ShowTabItemClose { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 标签是否显示扩展按钮 默认 true</para>
    /// <para lang="en">Gets or sets Whether to show extend buttons on tab. Default true</para>
    /// </summary>
    [Parameter]
    public bool ShowTabExtendButtons { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 点击标签页是否切换地址栏 默认 true</para>
    /// <para lang="en">Gets or sets Whether to switch address bar when clicking tab. Default true</para>
    /// </summary>
    [Parameter]
    public bool ClickTabToNavigation { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 授权回调方法多用于权限控制</para>
    /// <para lang="en">Gets or sets Authorization callback method, mostly used for permission control</para>
    /// </summary>
    [Parameter]
    public Func<string, Task<bool>>? OnAuthorizing { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 未授权导航地址 默认为 "/Account/Login" Cookie 模式登录页</para>
    /// <para lang="en">Gets or sets Unauthorized navigation address. Default "/Account/Login" Cookie mode login page</para>
    /// </summary>
    [Parameter]
    public string NotAuthorizeUrl { get; set; } = "/Account/Login";

    /// <summary>
    /// <para lang="zh">获得/设置 是否 enable tab context menu. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether enable tab context menu. Default is false.</para>
    /// </summary>
    [Parameter]
    public bool ShowTabContextMenu { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 模板 of before tab context menu. 默认为 null.</para>
    /// <para lang="en">Gets or sets the template of before tab context menu. Default is null.</para>
    /// </summary>
    [Parameter]
    public RenderFragment<Tab>? BeforeTabContextMenuTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 模板 of tab context menu. 默认为 null.</para>
    /// <para lang="en">Gets or sets the template of tab context menu. Default is null.</para>
    /// </summary>
    [Parameter]
    public RenderFragment<Tab>? TabContextMenuTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 图标 of tab item context menu refresh 按钮. 默认为 null.</para>
    /// <para lang="en">Gets or sets the icon of tab item context menu refresh button. Default is null.</para>
    /// </summary>
    [Parameter]
    public string? TabContextMenuRefreshIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 图标 of tab item context menu close 按钮. 默认为 null.</para>
    /// <para lang="en">Gets or sets the icon of tab item context menu close button. Default is null.</para>
    /// </summary>
    [Parameter]
    public string? TabContextMenuCloseIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 图标 of tab item context menu close other 按钮. 默认为 null.</para>
    /// <para lang="en">Gets or sets the icon of tab item context menu close other button. Default is null.</para>
    /// </summary>
    [Parameter]
    public string? TabContextMenuCloseOtherIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 图标 of tab item context menu close all 按钮. 默认为 null.</para>
    /// <para lang="en">Gets or sets the icon of tab item context menu close all button. Default is null.</para>
    /// </summary>
    [Parameter]
    public string? TabContextMenuCloseAllIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 before popup context menu 回调. 默认为 null.</para>
    /// <para lang="en">Gets or sets before popup context menu callback. Default is null.</para>
    /// </summary>
    [Parameter]
    public Func<TabItem, Task<bool>>? OnBeforeShowContextMenu { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 show the tab in header. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether show the tab in header. Default is false.</para>
    /// </summary>
    [Parameter]
    public bool ShowTabInHeader { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否跳过认证逻辑 默认 false</para>
    /// <para lang="en">Gets or sets Whether to skip authentication logic. Default false</para>
    /// </summary>
    [Parameter]
    public bool SkipAuthenticate { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? Navigation { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private bool SubscribedLocationChangedEvent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否已授权</para>
    /// <para lang="en">Gets or sets Whether authorized</para>
    /// </summary>
    private bool _authenticated;

    /// <summary>
    /// <para lang="zh">获得 组件样式</para>
    /// <para lang="en">Get Component Style</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("layout")
        .AddClass("has-sidebar", Side != null && IsFullSide)
        .AddClass("has-footer", ShowFooter && Footer != null)
        .AddClass("is-collapsed", IsCollapsed)
        .AddClass("is-fixed-tab", IsFixedTabHeader && UseTabSet)
        .AddClass("is-page", IsPage)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass("--bb-layout-header-height: 0px;", Header == null)
        .AddClass("--bb-layout-footer-height: 0px;", ShowFooter == false || Footer == null)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 页脚样式</para>
    /// <para lang="en">Get Footer Style</para>
    /// </summary>
    private string? FooterClassString => CssBuilder.Default("layout-footer")
        .AddClass("is-fixed", IsFixedFooter)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 页头样式</para>
    /// <para lang="en">Get Header Style</para>
    /// </summary>
    private string? HeaderClassString => CssBuilder.Default("layout-header")
        .AddClass("is-fixed", IsFixedHeader)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 侧边栏样式</para>
    /// <para lang="en">Get Sidebar Style</para>
    /// </summary>
    private string? SideClassString => CssBuilder.Default("layout-side")
        .AddClass("is-fixed-header", IsFixedHeader)
        .AddClass("is-fixed-footer", IsFixedFooter)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 侧边栏 Style 字符串</para>
    /// <para lang="en">Get Sidebar Style String</para>
    /// </summary>
    private string? SideStyleString => CssBuilder.Default()
        .AddClass($"--bb-layout-sidebar-width: {SideWidth.ConvertToPercentString()}", !string.IsNullOrEmpty(SideWidth) && SideWidth != "0")
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 排除地址支持通配符</para>
    /// <para lang="en">Gets or sets Exclude URLs support wildcards</para>
    /// </summary>
    [Parameter]
    public IEnumerable<string>? ExcludeUrls { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Gets or sets a collection of additional assemblies that should be searched for components that can match URIs.</para>
    /// <para lang="en">Gets or sets Gets or sets a collection of additional assemblies that should be searched for components that can match URIs.</para>
    /// </summary>
    [Parameter]
    public IEnumerable<Assembly>? AdditionalAssemblies { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 鼠标悬停提示文字信息</para>
    /// <para lang="en">Gets or sets Tooltip Text</para>
    /// </summary>
    [Parameter]
    public string? TooltipText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 更新回调方法 默认 null</para>
    /// <para lang="en">Gets or sets Update callback method. Default null</para>
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnUpdateAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 AuthorizeRouteView 参数</para>
    /// <para lang="en">Gets or sets AuthorizeRouteView Parameter</para>
    /// </summary>
    [Parameter]
    public object? Resource { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否开启全局异常捕获 默认 null 使用 <see cref="BootstrapBlazorOptions.EnableErrorLogger"/> 设置值</para>
    /// <para lang="en">Gets or sets Whether to enable global exception capture. Default null. Use <see cref="BootstrapBlazorOptions.EnableErrorLogger"/> value.</para>
    /// </summary>
    [Parameter]
    public bool? EnableErrorLogger { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否记录异常到 <see cref="ILogger"/> 默认 null 使用 <see cref="BootstrapBlazorOptions.EnableErrorLoggerILogger"/> 设置值</para>
    /// <para lang="en">Gets or sets Whether to log exceptions to <see cref="ILogger"/>. Default null. Use <see cref="BootstrapBlazorOptions.EnableErrorLoggerILogger"/> value.</para>
    /// </summary>
    [Parameter]
    public bool? EnableErrorLoggerILogger { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Error 提示弹窗 默认 null 使用 <see cref="BootstrapBlazorOptions.ShowErrorLoggerToast"/> 设置值</para>
    /// <para lang="en">Gets or sets Whether to show Error Toast. Default null. Use <see cref="BootstrapBlazorOptions.ShowErrorLoggerToast"/> value.</para>
    /// </summary>
    [Parameter]
    public bool? ShowErrorLoggerToast { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 错误日志 <see cref="Toast"/> 弹窗标题 默认 null</para>
    /// <para lang="en">Gets or sets Error Logger <see cref="Toast"/> Title. Default null.</para>
    /// </summary>
    [Parameter]
    public string? ErrorLoggerToastTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义错误处理回调方法</para>
    /// <para lang="en">Gets or sets Custom error handling callback method</para>
    /// </summary>
    [Parameter]
    public Func<ILogger, Exception, Task>? OnErrorHandleAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得 登录授权信息</para>
    /// <para lang="en">Get Login Authorization Information</para>
    /// </summary>
    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationStateTask { get; set; }

    [Inject, NotNull]
    private IServiceProvider? ServiceProvider { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Layout>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    private bool _init;
    private LayoutHeader? _layoutHeader;

    private ITabHeader? TabHeader => ShowTabInHeader ? this : null;

    private bool EnableLogger => EnableErrorLogger ?? Options.CurrentValue.EnableErrorLogger;

    private bool EnableILogger => EnableErrorLoggerILogger ?? Options.CurrentValue.EnableErrorLoggerILogger;

    private bool ShowToast => ShowErrorLoggerToast ?? Options.CurrentValue.ShowErrorLoggerToast;

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
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (SkipAuthenticate || AuthenticationStateTask == null)
        {
            _authenticated = true;
            _init = true;
            return;
        }

        // wasm 模式下 开启权限必须提供 AdditionalAssemblies 参数
        AdditionalAssemblies ??= [Assembly.GetEntryAssembly()!];

        var url = Navigation.ToBaseRelativePathWithoutQueryAndFragment();
        var context = RouteTableFactory.Create(AdditionalAssemblies, url);
        if (context.Handler != null)
        {
            _authenticated = await context.Handler.IsAuthorizedAsync(ServiceProvider, AuthenticationStateTask, Resource);

            // 检查当前 Url
            if (OnAuthorizing != null)
            {
                _authenticated = _authenticated && await OnAuthorizing(Navigation.Uri);
            }
        }
        _init = true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        TooltipText ??= Localizer[nameof(TooltipText)];
        MenuBarIcon ??= IconTheme.GetIconByKey(ComponentIcons.LayoutMenuBarIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(SetCollapsed));

    /// <summary>
    /// <para lang="zh">HandlerMain 方法</para>
    /// <para lang="en">HandlerMain Method</para>
    /// </summary>
    protected virtual RenderFragment HandlerMain() => builder =>
    {
        builder.AddContent(0, _errorContent ?? Main);
        _errorContent = null;
    };

    /// <summary>
    /// <para lang="zh">设置侧边栏收缩方法 客户端监控 window.onResize 事件回调此方法</para>
    /// <para lang="en">Set Sidebar Collapse Method. Client monitors window.onResize event to call this method</para>
    /// </summary>
    [JSInvokable]
    public void SetCollapsed(int width)
    {
        IsSmallScreen = width < 768;
    }

    /// <summary>
    /// <para lang="zh">调用 Update 回调方法</para>
    /// <para lang="en">Call Update Callback Method</para>
    /// </summary>
    /// <param name="key"></param>
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
    /// <para lang="zh">点击菜单时回调此方法</para>
    /// <para lang="en">Callback method when menu is clicked</para>
    /// </summary>
    private async Task ClickMenu(MenuItem item)
    {
        // 小屏幕时生效
        if (IsSmallScreen && !item.Items.Any())
        {
            IsCollapsed = false;
            await TriggerCollapseChanged();
        }

        if (OnClickMenu != null)
        {
            await OnClickMenu(item);
        }
    }

    private async Task TriggerCollapseChanged()
    {
        if (IsCollapsedChanged.HasDelegate)
        {
            await IsCollapsedChanged.InvokeAsync(IsCollapsed);
        }

        if (OnCollapsed != null)
        {
            await OnCollapsed(IsCollapsed);
        }
    }

    private async Task ToggleSidebar()
    {
        IsCollapsed = !IsCollapsed;

        await TriggerCollapseChanged();
    }

    private IErrorLogger? _errorLogger;

    private Task OnErrorLoggerInitialized(IErrorLogger logger)
    {
        _errorLogger = logger;
        _errorLogger.Register(this);
        return Task.CompletedTask;
    }

    /// <summary>
    /// <para lang="zh">上次渲染错误内容</para>
    /// <para lang="en">Last rendered error content</para>
    /// </summary>
    private RenderFragment? _errorContent;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="errorContent"></param>
    public virtual Task HandlerExceptionAsync(Exception ex, RenderFragment<Exception> errorContent)
    {
        _errorContent = errorContent(ex);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private string? GetTargetString() => IsFixedTabHeader ? ".tabs-body" : null;

    private RenderFragment RenderTabHeader() => builder =>
    {
        builder.OpenComponent<LayoutHeader>(0);
        builder.AddComponentReferenceCapture(1, instance => _layoutHeader = (LayoutHeader)instance);
        builder.CloseComponent();
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="renderFragment"></param>
    public void Render(RenderFragment renderFragment)
    {
        _layoutHeader?.Render(renderFragment);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string GetId() => $"{Id}_tab_header";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        if (disposing)
        {
            _errorLogger?.UnRegister(this);
            ErrorLogger?.UnRegister(this);
            if (SubscribedLocationChangedEvent)
            {
                Navigation.LocationChanged -= Navigation_LocationChanged;
            }
        }
    }
}
