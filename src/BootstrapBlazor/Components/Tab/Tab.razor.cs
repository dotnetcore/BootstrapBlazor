// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// Tab component
/// </summary>
public partial class Tab : IHandlerException
{
    private bool FirstRender { get; set; } = true;

    private string? WrapClassString => CssBuilder.Default("tabs-nav-wrap")
        .AddClass("extend", ShouldShowExtendButtons())
        .Build();

    private static string? GetItemWrapClassString(TabItem item) => CssBuilder.Default("tabs-item-wrap")
        .AddClass("active", item is { IsActive: true, IsDisabled: false })
        .Build();

    private string? GetClassString(TabItem item) => CssBuilder.Default("tabs-item")
        .AddClass("disabled", item.IsDisabled)
        .AddClass(item.CssClass)
        .AddClass("is-closeable", ShowClose)
        .Build();

    private static string? GetIconClassString(string icon) => CssBuilder.Default()
        .AddClass(icon)
        .Build();

    private string? ClassString => CssBuilder.Default("tabs")
        .AddClass("tabs-card", IsCard)
        .AddClass("tabs-border-card", IsBorderCard)
        .AddClass($"tabs-{Placement.ToDescriptionString()}", Placement == Placement.Top || Placement == Placement.Right || Placement == Placement.Bottom || Placement == Placement.Left)
        .AddClass("tabs-vertical", Placement == Placement.Left || Placement == Placement.Right)
        .AddClass("tabs-chrome", TabStyle == TabStyle.Chrome)
        .AddClass("tabs-capsule", TabStyle == TabStyle.Capsule)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"height: {Height}px;", Height > 0)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    private readonly List<TabItem> _items = new(50);

    private readonly List<TabItem> _draggedItems = new(50);

    /// <summary>
    /// Gets the collection of tab items.
    /// </summary>
    public IEnumerable<TabItem> Items => TabItems;

    private List<TabItem> TabItems => _dragged ? _draggedItems : _items;

    /// <summary>
    /// Gets or sets the excluded link. Default is false.
    /// </summary>
    private bool Excluded { get; set; }

    /// <summary>
    /// Gets or sets whether card style. Default is false.
    /// </summary>
    [Parameter]
    public bool IsCard { get; set; }

    /// <summary>
    /// Gets or sets whether border card style. Default is false.
    /// </summary>
    [Parameter]
    public bool IsBorderCard { get; set; }

    /// <summary>
    /// 获得/设置 是否仅渲染 Active 标签 默认 false
    /// </summary>
    [Parameter]
    public bool IsOnlyRenderActiveTab { get; set; }

    /// <summary>
    /// 获得/设置 懒加载 TabItem, 首次不渲染 默认 false
    /// </summary>
    [Parameter]
    public bool IsLazyLoadTabItem { get; set; }

    /// <summary>
    /// 获得/设置 组件高度 默认值为 0 高度自动
    /// </summary>
    [Parameter]
    public int Height { get; set; }

    /// <summary>
    /// 获得/设置 组件标签显示位置 默认显示在 Top 位置
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Top;

    /// <summary>
    /// 获得/设置 是否显示关闭按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowClose { get; set; }

    /// <summary>
    /// 获得/设置 是否显示全屏按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowFullScreen { get; set; }

    /// <summary>
    /// Gets or sets whether show the full screen button on context menu. Default is true.
    /// </summary>
    [Parameter]
    public bool ShowContextMenuFullScreen { get; set; } = true;

    /// <summary>
    /// 关闭标签页回调方法
    /// </summary>
    /// <remarks>返回 false 时不关 <see cref="TabItem"/> 标签页</remarks>
    [Parameter]
    public Func<TabItem, Task<bool>>? OnCloseTabItemAsync { get; set; }

    /// <summary>
    /// 获得/设置 是否显示扩展功能按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowExtendButtons { get; set; }

    /// <summary>
    /// 获得/设置 是否显示前后导航按钮 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowNavigatorButtons { get; set; } = true;

    /// <summary>
    /// Gets or sets whether auto reset tab item index. Default is true.
    /// </summary>
    [Parameter]
    public bool IsLoopSwitchTabItem { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示活动标签 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowActiveBar { get; set; } = true;

    /// <summary>
    /// 获得/设置 点击 TabItem 时是否自动导航 默认为 false 不导航
    /// </summary>
    [Parameter]
    public bool ClickTabToNavigation { get; set; }

    /// <summary>
    /// 获得/设置 TabItems 模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 NotAuthorized 模板 默认 null NET6.0/7.0 有效
    /// </summary>
    [Parameter]
    public RenderFragment? NotAuthorized { get; set; }

    /// <summary>
    /// 获得/设置 NotFound 模板 默认 null NET6.0/7.0 有效
    /// </summary>
    [Parameter]
    public RenderFragment? NotFound { get; set; }

    /// <summary>
    /// 获得/设置 NotFound 标签文本 默认 null NET6.0/7.0 有效
    /// </summary>
    [Parameter]
    public string? NotFoundTabText { get; set; }

    /// <summary>
    /// 获得/设置 TabItems 模板
    /// </summary>
    [Parameter]
    public RenderFragment? Body { get; set; }

    /// <summary>
    /// 获得/设置 Gets or sets a collection of additional assemblies that should be searched for components that can match URIs.
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<Assembly>? AdditionalAssemblies { get; set; }

    /// <summary>
    /// 获得/设置 排除地址支持通配符
    /// </summary>
    [Parameter]
    public IEnumerable<string>? ExcludeUrls { get; set; }

    /// <summary>
    /// 获得/设置 默认标签页 关闭所有标签页时自动打开此地址 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? DefaultUrl { get; set; }

    /// <summary>
    /// 获得/设置 点击 TabItem 时回调方法
    /// </summary>
    [Parameter]
    public Func<TabItem, Task>? OnClickTabItemAsync { get; set; }

    /// <summary>
    /// 获得/设置 关闭当前 TabItem 菜单文本
    /// </summary>
    [Parameter]
    public string? CloseCurrentTabText { get; set; }

    /// <summary>
    /// 获得/设置 关闭所有 TabItem 菜单文本
    /// </summary>
    [Parameter]
    public string? CloseAllTabsText { get; set; }

    /// <summary>
    /// 获得/设置 关闭其他 TabItem 菜单文本
    /// </summary>
    [Parameter]
    public string? CloseOtherTabsText { get; set; }

    /// <summary>
    /// 获得/设置 按钮模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment<Tab>? ButtonTemplate { get; set; }

    /// <summary>
    /// Gets or sets the template of the toolbar button. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment<Tab>? ToolbarTemplate { get; set; }

    /// <summary>
    /// 获得/设置 标签页前置模板 默认 null
    /// <para>在向前移动标签页按钮前</para>
    /// </summary>
    [Parameter]
    public RenderFragment<Tab>? BeforeNavigatorTemplate { get; set; }

    /// <summary>
    /// 获得/设置 标签页后置模板 默认 null
    /// <para>在向后移动标签页按钮前</para>
    /// </summary>
    [Parameter]
    public RenderFragment<Tab>? AfterNavigatorTemplate { get; set; }

    /// <summary>
    /// 获得/设置 上一个标签图标
    /// </summary>
    [Parameter]
    public string? PreviousIcon { get; set; }

    /// <summary>
    /// 获得/设置 下一个标签图标
    /// </summary>
    [Parameter]
    public string? NextIcon { get; set; }

    /// <summary>
    /// 获得/设置 下拉菜单标签图标
    /// </summary>
    [Parameter]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// 获得/设置 关闭标签图标
    /// </summary>
    [Parameter]
    public string? CloseIcon { get; set; }

    /// <summary>
    /// 获得/设置 导航菜单集合 默认 null
    /// </summary>
    /// <remarks>使用自定义布局时，需要 Tab 导航标签显示为菜单项时设置，已内置 <see cref="Layout.Menus"/> 默认 null</remarks>
    [Parameter]
    public IEnumerable<MenuItem>? Menus { get; set; }

    /// <summary>
    /// 获得/设置 是否允许拖放标题栏更改栏位顺序，默认为 false
    /// </summary>
    [Parameter]
    public bool AllowDrag { get; set; }

    /// <summary>
    /// 获得/设置 拖动标签页结束回调方法
    /// </summary>
    [Parameter]
    public Func<TabItem, Task>? OnDragItemEndAsync { get; set; }

    /// <summary>
    /// Gets or sets the tab style. Default is <see cref="TabStyle.Default"/>.
    /// </summary>
    [Parameter]
    public TabStyle TabStyle { get; set; }

    /// <summary>
    /// Gets or sets whether show the toolbar. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    /// Gets or sets whether show the full screen button. Default is true.
    /// </summary>
    [Parameter]
    public bool ShowFullscreenToolbarButton { get; set; } = true;

    /// <summary>
    /// Gets or sets the full screen toolbar button icon string. Default is null.
    /// </summary>
    [Parameter]
    public string? FullscreenToolbarButtonIcon { get; set; }

    /// <summary>
    /// Gets or sets the full screen toolbar button tooltip string. Default is null.
    /// </summary>
    [Parameter]
    public string? FullscreenToolbarTooltipText { get; set; }

    /// <summary>
    /// Gets or sets whether show the full screen button. Default is true.
    /// </summary>
    [Parameter]
    public bool ShowRefreshToolbarButton { get; set; } = true;

    /// <summary>
    /// Gets or sets the refresh toolbar button icon string. Default is null.
    /// </summary>
    [Parameter]
    public string? RefreshToolbarButtonIcon { get; set; }

    /// <summary>
    /// Gets or sets the refresh toolbar button tooltip string. Default is null.
    /// </summary>
    [Parameter]
    public string? RefreshToolbarTooltipText { get; set; }

    /// <summary>
    /// Gets or sets the refresh toolbar button click event callback. Default is null.
    /// </summary>
    [Parameter]
    public Func<Task>? OnToolbarRefreshCallback { get; set; }

    /// <summary>
    /// Gets or sets the previous tab navigation link tooltip text. Default is null.
    /// </summary>
    [Parameter]
    public string? PrevTabNavLinkTooltipText { get; set; }

    /// <summary>
    /// Gets or sets the next tab navigation link tooltip text. Default is null.
    /// </summary>
    [Parameter]
    public string? NextTabNavLinkTooltipText { get; set; }

    /// <summary>
    /// Gets or sets the close tab navigation link tooltip text. Default is null.
    /// </summary>
    [Parameter]
    public string? CloseTabNavLinkTooltipText { get; set; }

    /// <summary>
    /// Gets or sets whether enable tab context menu. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowContextMenu { get; set; }

    /// <summary>
    /// Gets or sets the template of before context menu. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment<Tab>? BeforeContextMenuTemplate { get; set; }

    /// <summary>
    /// Gets or sets the template of context menu. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment<Tab>? ContextMenuTemplate { get; set; }

    /// <summary>
    /// Gets or sets the icon of tab item context menu refresh button. Default is null.
    /// </summary>
    [Parameter]
    public string? ContextMenuRefreshIcon { get; set; }

    /// <summary>
    /// Gets or sets the icon of tab item context menu close button. Default is null.
    /// </summary>
    [Parameter]
    public string? ContextMenuCloseIcon { get; set; }

    /// <summary>
    /// Gets or sets the icon of tab item context menu close other button. Default is null.
    /// </summary>
    [Parameter]
    public string? ContextMenuCloseOtherIcon { get; set; }

    /// <summary>
    /// Gets or sets the icon of tab item context menu close all button. Default is null.
    /// </summary>
    [Parameter]
    public string? ContextMenuCloseAllIcon { get; set; }

    /// <summary>
    /// Gets or sets the icon of tab item context menu full screen button. Default is null.
    /// </summary>
    [Parameter]
    public string? ContextMenuFullScreenIcon { get; set; }

    /// <summary>
    /// Gets or sets before popup context menu callback. Default is null.
    /// </summary>
    [Parameter]
    public Func<TabItem, Task<bool>>? OnBeforeShowContextMenu { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="ITabHeader"/> instance. Default is null.
    /// </summary>
    [Parameter]
    public ITabHeader? TabHeader { get; set; }

    [CascadingParameter]
    private Layout? Layout { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Tab>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? Navigator { get; set; }

    [Inject]
    [NotNull]
    private TabItemTextOptions? Options { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<TabItemBindOptions>? TabItemMenuBinder { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject, NotNull]
    private DialogService? DialogService { get; set; }

    [Inject]
    [NotNull]
    private FullScreenService? FullScreenService { get; set; }

    private ContextMenuZone? _contextMenuZone;

    private ConcurrentDictionary<TabItem, bool> LazyTabCache { get; } = new();

    private bool HandlerNavigation { get; set; }

    private bool InvokeUpdate { get; set; }

    private Placement LastPlacement { get; set; }

    private string? DraggableString => AllowDrag ? "true" : null;

    private readonly ConcurrentDictionary<TabItem, TabItemContent> _cache = [];

    private bool IsPreventDefault => _contextMenuZone != null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ErrorLogger?.Register(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override void OnParametersSet()
    {
        if (ShowExtendButtons)
        {
            IsBorderCard = true;
        }

        CloseOtherTabsText ??= Localizer[nameof(CloseOtherTabsText)];
        CloseAllTabsText ??= Localizer[nameof(CloseAllTabsText)];
        CloseCurrentTabText ??= Localizer[nameof(CloseCurrentTabText)];
        NotFoundTabText ??= Localizer[nameof(NotFoundTabText)];
        RefreshToolbarTooltipText ??= Localizer[nameof(RefreshToolbarTooltipText)];
        FullscreenToolbarTooltipText ??= Localizer[nameof(FullscreenToolbarTooltipText)];
        PrevTabNavLinkTooltipText ??= Localizer[nameof(PrevTabNavLinkTooltipText)];
        NextTabNavLinkTooltipText ??= Localizer[nameof(NextTabNavLinkTooltipText)];
        CloseTabNavLinkTooltipText ??= Localizer[nameof(CloseTabNavLinkTooltipText)];

        PreviousIcon ??= IconTheme.GetIconByKey(ComponentIcons.TabPreviousIcon);
        NextIcon ??= IconTheme.GetIconByKey(ComponentIcons.TabNextIcon);
        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.TabDropdownIcon);
        CloseIcon ??= IconTheme.GetIconByKey(ComponentIcons.TabCloseIcon);
        RefreshToolbarButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TabRefreshButtonIcon);

        ContextMenuRefreshIcon ??= IconTheme.GetIconByKey(ComponentIcons.TabContextMenuRefreshIcon);
        ContextMenuCloseIcon ??= IconTheme.GetIconByKey(ComponentIcons.TabContextMenuCloseIcon);
        ContextMenuCloseOtherIcon ??= IconTheme.GetIconByKey(ComponentIcons.TabContextMenuCloseOtherIcon);
        ContextMenuCloseAllIcon ??= IconTheme.GetIconByKey(ComponentIcons.TabContextMenuCloseAllIcon);
        ContextMenuFullScreenIcon ??= IconTheme.GetIconByKey(ComponentIcons.TabContextMenuFullScreenIcon);

        if (AdditionalAssemblies is null)
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly is not null)
            {
                AdditionalAssemblies = [entryAssembly];
            }
        }

        if (Placement != Placement.Top && TabStyle == TabStyle.Chrome)
        {
            TabStyle = TabStyle.Default;
        }

        if (ClickTabToNavigation)
        {
            if (!HandlerNavigation)
            {
                HandlerNavigation = true;
                Navigator.LocationChanged += Navigator_LocationChanged;
            }
            AddTabByUrl();
        }
        else
        {
            RemoveLocationChanged();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            LastPlacement = Placement;
            FirstRender = false;
        }
        else if (LastPlacement != Placement)
        {
            LastPlacement = Placement;
            InvokeUpdate = true;
        }

        if (InvokeUpdate)
        {
            InvokeUpdate = false;
            await InvokeVoidAsync("update", Id);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(DragItemCallback));

    private void RemoveLocationChanged()
    {
        if (HandlerNavigation)
        {
            Navigator.LocationChanged -= Navigator_LocationChanged;
            HandlerNavigation = false;
        }
    }

    private void Navigator_LocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
    {
        AddTabByUrl();
        InvokeUpdate = true;
        StateHasChanged();
    }

    private void AddTabByUrl()
    {
        var requestUrl = Navigator.ToBaseRelativePath(Navigator.Uri);

        // 判断是否排除
        var routes = ExcludeUrls ?? [];
        Excluded = requestUrl == ""
            ? routes.Any(u => u is "" or "/")
            : routes.Any(u => u != "/" && requestUrl.StartsWith(u.TrimStart('/'), StringComparison.OrdinalIgnoreCase));
        if (!Excluded)
        {
            // 地址相同参数不同需要重新渲染 TabItem
            var tab = Items.FirstOrDefault(tab => tab.Url.TrimStart('/').Equals(requestUrl, StringComparison.OrdinalIgnoreCase));
            if (tab != null)
            {
                ActiveTabItem(tab);
            }
            else
            {
                AddTabItem(requestUrl);
            }
        }
    }

    private bool ShouldShowExtendButtons() => ShowExtendButtons && (Placement == Placement.Top || Placement == Placement.Bottom);

    /// <summary>
    /// 点击 TabItem 时回调此方法
    /// </summary>
    private async Task OnClickTabItem(TabItem item)
    {
        if (OnClickTabItemAsync != null)
        {
            await OnClickTabItemAsync(item);
        }

        if (!ClickTabToNavigation)
        {
            TabItems.ForEach(i => i.SetActive(false));
            item.SetActive(true);
            InvokeUpdate = true;
            StateHasChanged();
        }
    }

    /// <summary>
    /// 切换到上一个标签方法
    /// </summary>
    public void ClickPrevTab()
    {
        var item = Items.FirstOrDefault(i => i.IsActive);
        if (item != null)
        {
            var index = TabItems.IndexOf(item);
            if (index > -1)
            {
                index--;
                if (index < 0)
                {
                    if (IsLoopSwitchTabItem)
                    {
                        index = TabItems.Count - 1;
                    }
                    else
                    {
                        return;
                    }
                }

                if (!ClickTabToNavigation)
                {
                    item.SetActive(false);
                }

                item = TabItems[index];
                if (ClickTabToNavigation)
                {
                    Navigator.NavigateTo(item.Url);
                }
                else
                {
                    item.SetActive(true);
                    InvokeUpdate = true;
                }
            }
        }
    }

    /// <summary>
    /// 切换到下一个标签方法
    /// </summary>
    public void ClickNextTab()
    {
        var item = TabItems.Find(i => i.IsActive);
        if (item != null)
        {
            var index = TabItems.IndexOf(item);
            if (index < TabItems.Count)
            {
                index++;
                if (index + 1 > TabItems.Count)
                {
                    if (IsLoopSwitchTabItem)
                    {
                        index = 0;
                    }
                    else
                    {
                        return;
                    }
                }

                if (!ClickTabToNavigation)
                {
                    item.SetActive(false);
                }

                item = TabItems[index];

                if (ClickTabToNavigation)
                {
                    Navigator.NavigateTo(item.Url);
                }
                else
                {
                    item.SetActive(true);
                    InvokeUpdate = true;
                }
            }
        }
    }

    /// <summary>
    /// 关闭当前标签页方法
    /// </summary>
    public async Task CloseCurrentTab()
    {
        var tab = TabItems.Find(t => t.IsActive);
        if (tab is { Closable: true })
        {
            await RemoveTab(tab);
        }
    }

    private void OnClickCloseAllTabs()
    {
        TabItems.RemoveAll(t => t.Closable);
        if (TabItems.Count > 0)
        {
            var activeItem = TabItems.Find(i => i.IsActive);
            if (activeItem == null)
            {
                activeItem = TabItems[0];
                activeItem.SetActive(true);
            }
        }
        InvokeUpdate = true;
    }

    /// <summary>
    /// 关闭所有标签页方法
    /// </summary>
    public void CloseAllTabs()
    {
        OnClickCloseAllTabs();
        StateHasChanged();
    }

    private void OnClickCloseOtherTabs()
    {
        TabItems.RemoveAll(t => t is { Closable: true, IsActive: false });
        InvokeUpdate = true;
    }

    /// <summary>
    /// 关闭其他标签页方法
    /// </summary>
    public void CloseOtherTabs()
    {
        OnClickCloseOtherTabs();
        StateHasChanged();
    }

    /// <summary>
    /// 添加 TabItem 方法 由 TabItem 方法加载时调用
    /// </summary>
    /// <param name="item">TabItemBase 实例</param>
    internal void AddItem(TabItem item) => TabItems.Add(item);

    /// <summary>
    /// 通过 Url 添加 TabItem 标签方法
    /// </summary>
    /// <param name="url"></param>
    /// <param name="text"></param>
    /// <param name="icon"></param>
    /// <param name="active"></param>
    /// <param name="closable"></param>
    public void AddTab(string url, string text, string? icon = null, bool active = true, bool closable = true)
    {
        Options.Text = text;
        Options.Icon = icon;
        Options.IsActive = active;
        Options.Closable = closable;

        AddTabItem(url);
        StateHasChanged();
    }

    private void AddTabItem(string url)
    {
        var parameters = new Dictionary<string, object?>
        {
            { nameof(TabItem.Url), url }
        };
        var context = RouteTableFactory.Create(AdditionalAssemblies, url);
        if (context.Handler != null)
        {
            // 检查 Options 优先
            var option = context.Handler.GetCustomAttribute<TabItemOptionAttribute>(false)
                ?? TabItemMenuBinder.CurrentValue.Binders
                    .FirstOrDefault(i => i.Key.TrimStart('/').Equals(url.TrimStart('/'), StringComparison.OrdinalIgnoreCase))
                    .Value;
            if (option != null)
            {
                // TabItemOptionAttribute
                SetTabItemParameters(option.Text, option.Icon, option.Closable, true);
            }
            else if (Options.Valid())
            {
                // TabItemTextOptions
                SetTabItemParameters(Options.Text, Options.Icon, Options.Closable, Options.IsActive);
                Options.Reset();
            }
            else
            {
                var menu = GetMenuItem(url) ?? new MenuItem() { Text = url.Split("/").FirstOrDefault() };
                SetTabItemParameters(menu.Text, menu.Icon, true, true);
            }

            parameters.Add(nameof(TabItem.ChildContent), new RenderFragment(builder =>
            {
                builder.OpenComponent<BootstrapBlazorAuthorizeView>(0);
                builder.AddAttribute(1, nameof(BootstrapBlazorAuthorizeView.Type), context.Handler);
                builder.AddAttribute(2, nameof(BootstrapBlazorAuthorizeView.Parameters), context.Parameters);
                builder.AddAttribute(3, nameof(BootstrapBlazorAuthorizeView.NotAuthorized), NotAuthorized);
                builder.AddAttribute(4, nameof(BootstrapBlazorAuthorizeView.Resource), Layout?.Resource);
                builder.CloseComponent();
            }));
        }
        else
        {
            parameters.Add(nameof(TabItem.Text), NotFoundTabText);
            parameters.Add(nameof(TabItem.ChildContent), new RenderFragment(builder =>
            {
                builder.AddContent(0, NotFound);
            }));
        }

        AddTabItem(parameters);

        void SetTabItemParameters(string? text, string? icon, bool closable, bool active)
        {
            parameters.Add(nameof(TabItem.Text), text);
            parameters.Add(nameof(TabItem.Icon), icon);
            parameters.Add(nameof(TabItem.Closable), closable);
            parameters.Add(nameof(TabItem.IsActive), active);
        }
    }

    /// <summary>
    /// 添加 TabItem 方法
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="index"></param>
    public void AddTab(Dictionary<string, object?> parameters, int? index = null)
    {
        AddTabItem(parameters, index);
        InvokeUpdate = true;
        StateHasChanged();
    }

    private void AddTabItem(Dictionary<string, object?> parameters, int? index = null)
    {
        var item = TabItem.Create(parameters);
        item.TabSet = this;
        if (item.IsActive)
        {
            TabItems.ForEach(i => i.SetActive(false));
        }

        if (index.HasValue)
        {
            TabItems.Insert(index.Value, item);
        }
        else
        {
            TabItems.Add(item);
        }
    }

    /// <summary>
    /// 移除 TabItem 方法
    /// </summary>
    /// <param name="item"></param>
    public async Task RemoveTab(TabItem item)
    {
        Options.Reset();

        if (OnCloseTabItemAsync != null && !await OnCloseTabItemAsync(item))
        {
            return;
        }

        var index = TabItems.IndexOf(item);
        TabItems.Remove(item);
        InvokeUpdate = true;

        // 删除的 TabItem 是当前 Tab
        // 查找后面的 Tab
        var activeItem = TabItems.Find(i => i.IsActive)
                         ?? (index < TabItems.Count ? TabItems[index] : TabItems.LastOrDefault());
        if (activeItem != null)
        {
            if (ClickTabToNavigation)
            {
                Navigator.NavigateTo(activeItem.Url);
            }
            else
            {
                activeItem.SetActive(true);
                StateHasChanged();
            }
        }
        else
        {
            // 无标签
            StateHasChanged();
        }
    }

    /// <summary>
    /// 设置指定 TabItem 为激活状态
    /// </summary>
    /// <param name="item"></param>
    public void ActiveTab(TabItem item)
    {
        ActiveTabItem(item);
        InvokeUpdate = true;
        StateHasChanged();
    }

    /// <summary>
    /// 设置指定 TabItem 为激活状态
    /// </summary>
    /// <param name="index"></param>
    public void ActiveTab(int index)
    {
        var item = TabItems.ElementAtOrDefault(index);
        if (item != null)
        {
            ActiveTab(item);
        }
    }

    /// <summary>
    /// 获得当前活动 Tab
    /// </summary>
    /// <returns></returns>
    public TabItem? GetActiveTab() => TabItems.Find(s => s.IsActive);

    private void ActiveTabItem(TabItem item)
    {
        TabItems.ForEach(i => i.SetActive(false));
        item.SetActive(true);
    }

    /// <summary>
    /// 设置 TabItem 禁用状态
    /// </summary>
    /// <param name="item"></param>
    /// <param name="disabled"></param>
    public void SetDisabledItem(TabItem item, bool disabled)
    {
        item.SetDisabledWithoutRender(disabled);
        if (disabled)
        {
            item.SetActive(false);
        }
        if (TabItems.Find(i => i.IsActive) == null)
        {
            var tabItem = TabItems.Find(i => !i.IsDisabled);
            tabItem?.SetActive(true);
        }
        StateHasChanged();
    }

    private RenderFragment RenderTabItemContent(TabItem item) => builder =>
    {
        if (item.IsDisabled)
        {
            return;
        }

        if (item.IsActive)
        {
            builder.AddContent(0, item.RenderContent(_cache));
            if (IsLazyLoadTabItem)
            {
                LazyTabCache.AddOrUpdate(item, _ => true, (_, _) => true);
            }
        }
        else if (!IsLazyLoadTabItem || item.AlwaysLoad || LazyTabCache.TryGetValue(item, out var init) && init)
        {
            builder.AddContent(0, item.RenderContent(_cache));
        }
    };

    /// <summary>
    /// HandlerException 错误处理方法
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="errorContent"></param>
    public Task HandlerException(Exception ex, RenderFragment<Exception> errorContent) => DialogService.ShowErrorHandlerDialog(errorContent(ex));

    private IEnumerable<MenuItem>? _menuItems;
    private MenuItem? GetMenuItem(string url)
    {
        _menuItems ??= (Menus ?? Layout?.Menus).GetAllItems();
        return _menuItems?.FirstOrDefault(i => !string.IsNullOrEmpty(i.Url) && (i.Url.TrimStart('/').Equals(url.TrimStart('/'), StringComparison.OrdinalIgnoreCase)));
    }

    private bool _dragged;
    /// <summary>
    /// 拖动 TabItem 回调方法有 JS 调用
    /// </summary>
    /// <param name="originIndex"></param>
    /// <param name="currentIndex"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task DragItemCallback(int originIndex, int currentIndex)
    {
        var firstColumn = Items.ElementAtOrDefault(originIndex);
        var targetColumn = Items.ElementAtOrDefault(currentIndex);
        if (firstColumn != null && targetColumn != null)
        {
            if (_draggedItems.Count == 0)
            {
                _draggedItems.AddRange(_items);
            }
            _draggedItems.Remove(firstColumn);
            _draggedItems.Insert(currentIndex, firstColumn);
            _dragged = true;

            if (OnDragItemEndAsync != null)
            {
                await OnDragItemEndAsync(firstColumn);
            }
            InvokeUpdate = true;
            StateHasChanged();
        }
    }

    private string GetIdByTabItem(TabItem item) => ComponentIdGenerator.Generate(item);

    private async Task OnRefreshAsync()
    {
        // refresh the active tab item
        var item = TabItems.FirstOrDefault(i => i.IsActive);

        if (item is not null)
        {
            await Refresh(item);
        }
    }

    /// <summary>
    /// Refresh the tab item method
    /// </summary>
    /// <param name="item"></param>
    public async Task Refresh(TabItem item)
    {
        item.Refresh(_cache);

        if (OnToolbarRefreshCallback != null)
        {
            await OnToolbarRefreshCallback();
        }
    }

    private async Task OnRefresh(ContextMenuItem item, object? context)
    {
        if (context is TabItem tabItem)
        {
            await Refresh(tabItem);
        }
    }

    private async Task OnClose(ContextMenuItem item, object? context)
    {
        if (context is TabItem tabItem)
        {
            await RemoveTab(tabItem);
        }
    }

    private Task OnCloseOther(ContextMenuItem item, object? context)
    {
        if (context is TabItem tabItem)
        {
            ActiveTab(tabItem);
        }
        CloseOtherTabs();
        return Task.CompletedTask;
    }

    private Task OnCloseAll(ContextMenuItem item, object? context)
    {
        CloseAllTabs();
        return Task.CompletedTask;
    }

    private async Task OnFullScreen(ContextMenuItem item, object? context)
    {
        if (context is TabItem tabItem)
        {
            await FullScreenService.ToggleById(GetIdByTabItem(tabItem));
        }
    }

    private async Task OnContextMenu(MouseEventArgs e, TabItem item)
    {
        if (_contextMenuZone != null)
        {
            var show = true;
            if (OnBeforeShowContextMenu != null)
            {
                show = await OnBeforeShowContextMenu(item);
            }

            if (show)
            {
                await _contextMenuZone.OnContextMenu(e, item);
            }
        }
    }

    private RenderFragment RenderTabList() => builder =>
    {
        if (!Items.Any() && !string.IsNullOrEmpty(DefaultUrl))
        {
            if (ClickTabToNavigation)
            {
                Navigator.NavigateTo(DefaultUrl);
            }
            else
            {
                AddTabItem(DefaultUrl);
            }
        }

        if (FirstRender)
        {
            if (!Items.Any(t => t.IsActive))
            {
                Items.FirstOrDefault(i => i.IsDisabled == false)?.SetActive(true);
            }
        }

        if (ShowContextMenu)
        {
            builder.OpenComponent<ContextMenuZone>(0);
            builder.AddAttribute(10, nameof(ContextMenuZone.ChildContent), RenderContextMenuZoneContent());
            builder.AddComponentReferenceCapture(20, instance => _contextMenuZone = (ContextMenuZone)instance);
            builder.CloseComponent();
        }
        else
        {
            builder.AddContent(150, RenderTabItems());
        }

        if (TabStyle == TabStyle.Default && (IsCard || IsBorderCard))
        {
            builder.OpenElement(200, "div");
            builder.AddAttribute(210, "class", "tabs-item-fix");
            builder.CloseElement();
        }
    };

    private RenderFragment RenderContextMenuZoneContent() => builder =>
    {
        builder.AddContent(0, RenderTabItems());
        builder.AddContent(10, RenderContextMenu);
    };

    private RenderFragment RenderTabItems() => builder =>
    {
        foreach (var item in Items)
        {
            if (item.HeaderTemplate != null)
            {
                builder.OpenElement(0, "div");
                builder.SetKey(item);
                builder.AddAttribute(10, "class", GetItemWrapClassString(item));
                builder.AddAttribute(20, "draggable", DraggableString);
                builder.AddContent(30, item.HeaderTemplate(item));
                builder.CloseElement();
            }
            else if (item.IsDisabled)
            {
                builder.AddContent(40, RenderDisabledHeaderItem(item));
            }
            else
            {
                builder.AddContent(50, RenderHeaderItem(item));
            }
        }
    };

    /// <summary>
    /// Sets the <see cref="ITabHeader"/> instance.
    /// </summary>
    /// <param name="tabHeader"></param>
    public void SetTabHeader(ITabHeader tabHeader) => TabHeader = tabHeader;

    private string? HeaderId => TabHeader?.GetId();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        if (disposing)
        {
            RemoveLocationChanged();
            ErrorLogger?.UnRegister(this);
        }
    }
}
