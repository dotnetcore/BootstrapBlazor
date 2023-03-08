// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// Tab 组件基类
/// </summary>
public partial class Tab : IHandlerException, IDisposable
{
    private bool FirstRender { get; set; } = true;

    private static string? GetContentClassString(TabItem item) => CssBuilder.Default("tabs-body-content")
        .AddClass("d-none", !item.IsActive)
        .Build();

    private string? WrapClassString => CssBuilder.Default("tabs-nav-wrap")
        .AddClass("extend", ShouldShowExtendButtons())
        .Build();

    private string? GetClassString(TabItem item) => CssBuilder.Default("tabs-item")
        .AddClass("active", item.IsActive)
        .AddClass("is-closeable", ShowClose)
        .Build();

    private static string? GetIconClassString(string icon) => CssBuilder.Default()
        .AddClass(icon)
        .Build();

    private ElementReference TabElement { get; set; }

    private string? ClassString => CssBuilder.Default("tabs")
        .AddClass("tabs-card", IsCard)
        .AddClass("tabs-border-card", IsBorderCard)
        .AddClass($"tabs-{Placement.ToDescriptionString()}", Placement == Placement.Top || Placement == Placement.Right || Placement == Placement.Bottom || Placement == Placement.Left)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"height: {Height}px;", Height > 0)
        .Build();

    private readonly List<TabItem> _items = new(50);

    /// <summary>
    /// 获得/设置 TabItem 集合
    /// </summary>
    public IEnumerable<TabItem> Items => _items;

    /// <summary>
    /// 获得/设置 是否为排除地址 默认为 false
    /// </summary>
    private bool Excluded { get; set; }

    /// <summary>
    /// 获得/设置 是否为卡片样式
    /// </summary>
    [Parameter]
    public bool IsCard { get; set; }

    /// <summary>
    /// 获得/设置 是否为带边框卡片样式 默认 false
    /// </summary>
    [Parameter]
    public bool IsBorderCard { get; set; }

    /// <summary>
    /// 获得/设置 是否仅渲染 Active 标签
    /// </summary>
    [Parameter]
    public bool IsOnlyRenderActiveTab { get; set; }

    /// <summary>
    /// 获得/设置 懒加载 TabItem, 首次不渲染
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
    public Func<TabItem, Task>? OnClickTab { get; set; }

    /// <summary>
    /// 获得/设置 NotFound 标签文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? NotFoundTabText { get; set; }

    /// <summary>
    /// 获得/设置 关闭当前 TabItem 菜单文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseCurrentTabText { get; set; }

    /// <summary>
    /// 获得/设置 关闭所有 TabItem 菜单文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseAllTabsText { get; set; }

    /// <summary>
    /// 获得/设置 关闭其他 TabItem 菜单文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseOtherTabsText { get; set; }

    /// <summary>
    /// 获得/设置 按钮模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment? ButtonTemplate { get; set; }

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
    private IOptionsMonitor<TabItemMenuBindOptions>? TabItemMenuBinder { get; set; }

    private ConcurrentDictionary<TabItem, bool> LazyTabCache { get; } = new();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ErrorLogger?.Register(this);
    }

    /// <summary>
    /// OnParametersSet 方法
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

        AdditionalAssemblies ??= new[] { Assembly.GetEntryAssembly()! };

        if (ClickTabToNavigation)
        {
            AddTabByUrl();
        }
    }

    private void AddTabByUrl()
    {
        var requestUrl = Navigator.ToBaseRelativePath(Navigator.Uri);

        // 判断是否排除
        var urls = ExcludeUrls ?? Enumerable.Empty<string>();
        if (requestUrl == "")
        {
            Excluded = urls.Any(u => u == "" || u == "/");
        }
        else
        {
            Excluded = urls.Any(u => u != "/" && requestUrl.StartsWith(u.TrimStart('/'), StringComparison.OrdinalIgnoreCase));
        }
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

    /// <summary>
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            FirstRender = false;
        }

        await JSRuntime.InvokeVoidAsync(TabElement, "bb_tab");
    }

    private bool ShouldShowExtendButtons() => ShowExtendButtons && (Placement == Placement.Top || Placement == Placement.Bottom);

    /// <summary>
    /// 点击 TabItem 时回调此方法
    /// </summary>
    private async Task OnClickTabItem(TabItem item)
    {
        Items.ToList().ForEach(i => i.SetActive(false));
        if (OnClickTab != null)
        {
            await OnClickTab(item);
        }

        if (!ClickTabToNavigation)
        {
            item.SetActive(true);
            StateHasChanged();
        }
    }

    /// <summary>
    /// 切换到上一个标签方法
    /// </summary>
    public Task ClickPrevTab()
    {
        var item = Items.FirstOrDefault(i => i.IsActive);
        if (item != null)
        {
            var index = _items.IndexOf(item);
            if (index > -1)
            {
                index--;
                if (index < 0)
                {
                    index = _items.Count - 1;
                }

                if (!ClickTabToNavigation)
                {
                    item.SetActive(false);
                }

                item = Items.ElementAt(index);
                if (ClickTabToNavigation)
                {
                    Navigator.NavigateTo(item.Url);
                }
                else
                {
                    item.SetActive(true);
                    StateHasChanged();
                }
            }
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 切换到下一个标签方法
    /// </summary>
    public Task ClickNextTab()
    {
        var item = Items.FirstOrDefault(i => i.IsActive);
        if (item != null)
        {
            var index = _items.IndexOf(item);
            if (index < _items.Count)
            {
                if (!ClickTabToNavigation)
                {
                    item.SetActive(false);
                }

                index++;
                if (index + 1 > _items.Count)
                {
                    index = 0;
                }

                item = Items.ElementAt(index);

                if (ClickTabToNavigation)
                {
                    Navigator.NavigateTo(item.Url);
                }
                else
                {
                    item.SetActive(true);
                    StateHasChanged();
                }
            }
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 关闭当前标签页方法
    /// </summary>
    public async Task CloseCurrentTab()
    {
        var tab = _items.FirstOrDefault(t => t.IsActive);
        if (tab is { Closable: true })
        {
            await RemoveTab(tab);
        }
    }

    private void OnClickCloseAllTabs() => _items.RemoveAll(t => t.Closable);

    /// <summary>
    /// 关闭所有标签页方法
    /// </summary>
    public void CloseAllTabs()
    {
        OnClickCloseAllTabs();
        StateHasChanged();
    }

    private void OnClickCloseOtherTabs() => _items.RemoveAll(t => t is { Closable: true, IsActive: false });

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
    internal void AddItem(TabItem item) => _items.Add(item);

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
        var parameters = new Dictionary<string, object?>();
        var context = RouteTableFactory.Create(AdditionalAssemblies, url);
        if (context.Handler != null)
        {
            // 检查 Options 优先
            var option = context.Handler.GetCustomAttribute<TabItemOptionAttribute>(false)
                ?? TabItemMenuBinder.CurrentValue.Binders
                    .FirstOrDefault(i => i.Key.Equals(url, StringComparison.OrdinalIgnoreCase))
                    .Value;
            if (option != null)
            {
                parameters.Add(nameof(TabItem.Icon), option.Icon);
                parameters.Add(nameof(TabItem.Closable), option.Closable);
                parameters.Add(nameof(TabItem.IsActive), true);
                parameters.Add(nameof(TabItem.Text), option.Text);
            }
            else if (Options.Valid())
            {
                parameters.Add(nameof(TabItem.Icon), Options.Icon);
                parameters.Add(nameof(TabItem.Closable), Options.Closable);
                parameters.Add(nameof(TabItem.IsActive), Options.IsActive);
                parameters.Add(nameof(TabItem.Text), Options.Text);
            }
            parameters.Add(nameof(TabItem.Url), url);

            parameters.Add(nameof(TabItem.ChildContent), new RenderFragment(builder =>
            {
                builder.OpenComponent<BootstrapBlazorAuthorizeView>(0);
                builder.AddAttribute(1, nameof(BootstrapBlazorAuthorizeView.Type), context.Handler);
                builder.AddAttribute(1, nameof(BootstrapBlazorAuthorizeView.Parameters), context.Parameters);
                builder.AddAttribute(2, nameof(BootstrapBlazorAuthorizeView.NotAuthorized), NotAuthorized);
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
    }

    /// <summary>
    /// 添加 TabItem 方法
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="index"></param>
    public void AddTab(Dictionary<string, object?> parameters, int? index = null)
    {
        AddTabItem(parameters, index);
        StateHasChanged();
    }

    private void AddTabItem(Dictionary<string, object?> parameters, int? index = null)
    {
        var item = TabItem.Create(parameters);
        item.TabSet = this;
        if (item.IsActive)
        {
            _items.ForEach(i => i.SetActive(false));
        }

        if (index.HasValue)
        {
            _items.Insert(index.Value, item);
        }
        else
        {
            _items.Add(item);
        }
    }

    /// <summary>
    /// 移除 TabItem 方法
    /// </summary>
    /// <param name="item"></param>
    public async Task RemoveTab(TabItem item)
    {
        if (OnCloseTabItemAsync != null && !await OnCloseTabItemAsync(item))
        {
            return;
        }

        var index = _items.IndexOf(item);
        _items.Remove(item);

        // 删除的 TabItem 是当前 Tab
        // 查找后面的 Tab
        var activeItem = _items.FirstOrDefault(i => i.IsActive)
                         ?? (index < _items.Count ? _items[index] : _items.LastOrDefault());
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

        StateHasChanged();
    }

    /// <summary>
    /// 设置指定 TabItem 为激活状态
    /// </summary>
    /// <param name="index"></param>
    public void ActiveTab(int index)
    {
        var item = _items.ElementAtOrDefault(index);
        if (item != null)
        {
            ActiveTab(item);
        }
    }

    /// <summary>
    /// 获得当前活动 Tab
    /// </summary>
    /// <returns></returns>
    public TabItem? GetActiveTab() => _items.FirstOrDefault(s => s.IsActive);

    private void ActiveTabItem(TabItem item)
    {
        _items.ForEach(i => i.SetActive(false));
        item.SetActive(true);
    }

    private RenderFragment RenderTabItemContent(TabItem item) => builder =>
    {
        if (item.IsActive)
        {
            var content = _errorContent ?? item.ChildContent;
            builder.AddContent(0, content);
            _errorContent = null;
            if (IsLazyLoadTabItem)
            {
                LazyTabCache.AddOrUpdate(item, _ => true, (_, _) => true);
            }
        }
        else if (!IsLazyLoadTabItem || item.AlwaysLoad || LazyTabCache.TryGetValue(item, out var init) && init)
        {
            builder.AddContent(0, item.ChildContent);
        }
    };

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
    /// Dispose 方法
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            ErrorLogger?.UnRegister(this);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
