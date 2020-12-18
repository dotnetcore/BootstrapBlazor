// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：Apache-2.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Tab 组件基类
    /// </summary>
    public sealed partial class Tab
    {
        static ConcurrentDictionary<string, Type> RouteTable { get; set; } = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// 
        /// </summary>
        private bool FirstRender { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string? GetContentClassString(TabItem item) => CssBuilder.Default("tabs-body-content")
            .AddClass("d-none", !item.IsActive)
            .Build();

        private string? WrapClassString => CssBuilder.Default("tabs-nav-wrap")
            .AddClass("extend", ShouldShowExtendButtons())
            .Build();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        private string? GetClassString(bool active) => CssBuilder.Default("tabs-item")
            .AddClass("is-active", active)
            .AddClass("is-closeable", ShowClose)
            .Build();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        private string? GetIconClassString(string icon) => CssBuilder.Default("fa fa-fw")
            .AddClass(icon)
            .Build();

        /// <summary>
        /// 获得/设置 Tab 组件 DOM 实例
        /// </summary>
        private ElementReference TabElement { get; set; }

        /// <summary>
        /// 获得 Tab 组件样式
        /// </summary>
        private string? ClassString => CssBuilder.Default("tabs")
            .AddClass("tabs-card", IsCard)
            .AddClass("tabs-border-card", IsBorderCard)
            .AddClass($"tabs-{Placement.ToDescriptionString()}", Placement == Placement.Top || Placement == Placement.Right || Placement == Placement.Bottom || Placement == Placement.Left)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 Tab 组件 Style
        /// </summary>
        private string? StyleString => CssBuilder.Default()
            .AddClass($"height: {Height}px;", Height > 0)
            .Build();

        private readonly List<TabItem> _items = new List<TabItem>(50);

        /// <summary>
        /// 获得/设置 TabItem 集合
        /// </summary>
        public IEnumerable<TabItem> Items => _items;

        /// <summary>
        /// 获得/设置 是否为卡片样式
        /// </summary>
        [Parameter]
        public bool IsCard { get; set; }

        /// <summary>
        /// 获得/设置 是否为带边框卡片样式
        /// </summary>
        [Parameter]
        public bool IsBorderCard { get; set; }

        /// <summary>
        /// 获得/设置 组件高度 默认值为 0 高度自动
        /// </summary>
        [Parameter]
        public int Height { get; set; }

        /// <summary>
        /// 获得/设置 默认首页 Tab 显示文本 默认为 Index
        /// </summary>
        [Parameter]
        [NotNull]
        public string? DefaultIndexText { get; set; } = "Index";

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
        /// 获得/设置 是否显示扩展功能按钮 默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool ShowExtendButtons { get; set; }

        /// <summary>
        /// 获得/设置 点击 TabItem 时是否自动导航 默认为 false 不导航
        /// </summary>
        [Parameter]
        public bool ClickTabToNavigator { get; set; }

        /// <summary>
        /// 获得/设置 TabItems 模板
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 Gets or sets a collection of additional assemblies that should be searched for components that can match URIs.
        /// </summary>
        [Parameter]
        public IEnumerable<Assembly>? AdditionalAssemblies { get; set; }

        /// <summary>
        /// 获得/设置 点击 TabItem 时回调方法
        /// </summary>
        [Parameter]
        public Func<TabItem, Task>? OnClickTab { get; set; }

        [Inject]
        [NotNull]
        private NavigationManager? Navigator { get; set; }

        [Inject]
        [NotNull]
        private MenuTabBoundleOptions? Options { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (ShowExtendButtons) IsBorderCard = true;

            await InitRouteTable();
        }

        private Task InitRouteTable() => Task.Run(() =>
        {
            if (ClickTabToNavigator)
            {
                var apps = AdditionalAssemblies == null ? new[] { Assembly.GetEntryAssembly()! } : new[] { Assembly.GetEntryAssembly()! }.Concat(AdditionalAssemblies);
                var componentTypes = apps.SelectMany(a => a.ExportedTypes.Where(t => typeof(IComponent).IsAssignableFrom(t)));
                foreach (var componentType in componentTypes)
                {
                    var routeAttributes = componentType.GetCustomAttributes<RouteAttribute>(false);
                    foreach (var template in routeAttributes.Select(t => t.Template))
                    {
                        RouteTable.TryAdd(template.Trim('/'), componentType);
                    }
                }
                Navigator.LocationChanged += Navigator_LocationChanged;

                InvokeAsync(() => AddTabByUrl(Navigator.ToBaseRelativePath(Navigator.Uri)));
            }
        });

        private void Navigator_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            var requestUrl = Navigator.ToBaseRelativePath(e.Location);

            var tab = Items.FirstOrDefault(tab => tab.Url == requestUrl);
            if (tab != null)
            {
                ActiveTab(tab);
            }
            else
            {
                AddTabByUrl(requestUrl);
            }
        }

        private void AddTabByUrl(string url)
        {
            if (RouteTable.TryGetValue(url, out var comp))
            {
                var item = new TabItem();
                var parameters = new Dictionary<string, object>
                {
                    [nameof(TabItem.Text)] = Options.TabItemText ?? string.Empty,
                    [nameof(TabItem.Url)] = url,
                    [nameof(TabItem.IsActive)] = true,
                    [nameof(TabItem.ChildContent)] = new RenderFragment(builder =>
                    {
                        builder.OpenComponent(0, comp);
                        builder.CloseComponent();
                    })
                };
                var _ = item.SetParametersAsync(ParameterView.FromDictionary(parameters));
                Add(item);
            }
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            FirstRender = false;

            await JSRuntime.InvokeVoidAsync(TabElement, "bb_tab");
        }

        private bool ShouldShowExtendButtons() => ShowExtendButtons && (Placement == Placement.Top || Placement == Placement.Bottom);

        /// <summary>
        /// 点击 TabItem 时回调此方法
        /// </summary>
        private async Task OnClickTabItem(TabItem item)
        {
            Items.ToList().ForEach(i => i.SetActive(false));
            if (OnClickTab != null) await OnClickTab(item);
            if (ClickTabToNavigator)
            {
                Navigator.NavigateTo(item.Url ?? "");
            }
            else
            {
                item.SetActive(true);
            }
        }

        /// <summary>
        /// 点击上一个标签页时回调此方法
        /// </summary>
        private void ClickPrevTab()
        {
            var item = Items.FirstOrDefault(i => i.IsActive);
            if (item != null)
            {
                var index = _items.IndexOf(item);
                if (index > -1)
                {
                    index--;
                    if (index < 0) index = _items.Count - 1;
                    if (!ClickTabToNavigator) item.SetActive(false);

                    item = Items.ElementAt(index);
                    if (ClickTabToNavigator)
                    {
                        Navigator.NavigateTo(item.Url!);
                    }
                    else
                    {
                        item.SetActive(true);
                    }
                }
            }
        }

        /// <summary>
        /// 点击下一个标签页时回调此方法
        /// </summary>
        private void ClickNextTab()
        {
            var item = Items.FirstOrDefault(i => i.IsActive);
            if (item != null)
            {
                var index = _items.IndexOf(item);
                if (index < _items.Count)
                {
                    if (!ClickTabToNavigator) item.SetActive(false);

                    index++;
                    if (index + 1 > _items.Count) index = 0;
                    item = Items.ElementAt(index);

                    if (ClickTabToNavigator)
                    {
                        Navigator.NavigateTo(item.Url!);
                    }
                    else
                    {
                        item.SetActive(true);
                    }
                }
            }
        }

        /// <summary>
        /// 关闭所有标签页方法
        /// </summary>
        private void CloseAllTab()
        {
            _items.RemoveAll(t => t.Closable);
        }

        /// <summary>
        /// 关闭当前标签页方法
        /// </summary>
        private async Task CloseCurrentTab()
        {
            var tab = _items.FirstOrDefault(t => t.IsActive);
            if (tab != null && tab.Closable) await Remove(tab);
        }

        /// <summary>
        /// 关闭其他标签页方法
        /// </summary>
        private void CloseOtherTab()
        {
            _items.RemoveAll(t => t.Closable && !t.IsActive);
        }

        /// <summary>
        /// 添加 TabItem 方法 由 TabItem 方法加载时调用
        /// </summary>
        /// <param name="item">TabItemBase 实例</param>
        internal void AddItem(TabItem item) => _items.Add(item);

        /// <summary>
        /// 添加 TabItem 方法
        /// </summary>
        /// <param name="item"></param>
        public Task Add(TabItem item)
        {
            var check = _items.Contains(item);
            if (item.IsActive || !check) _items.ForEach(i => i.SetActive(false));
            if (!check)
            {
                _items.Add(item);
                item.SetActive(true);
            }
            StateHasChanged();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 添加 TabItem 方法
        /// </summary>
        /// <param name="parameters"></param>
        public void AddTab(Dictionary<string, object> parameters)
        {
            var item = TabItem.Create(parameters);
            _items.Add(item);
            StateHasChanged();
        }

        /// <summary>
        /// 移除 TabItem 方法
        /// </summary>
        /// <param name="item"></param>
        public Task Remove(TabItem item)
        {
            var index = _items.IndexOf(item);
            _items.Remove(item);
            var activeItem = _items.FirstOrDefault(i => i.IsActive);
            if (activeItem == null)
            {
                // 删除的 TabItem 是当前 Tab
                if (index < _items.Count)
                {
                    // 查找后面的 Tab
                    activeItem = _items[index];
                }
                else
                {
                    // 查找前面的 Tab
                    activeItem = _items.LastOrDefault();
                }
                if (activeItem != null) activeItem.SetActive(true);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 设置指定 TabItem 为激活状态
        /// </summary>
        /// <param name="item"></param>
        public Task ActiveTab(TabItem item)
        {
            _items.ForEach(i => i.SetActive(false));
            item.SetActive(true);

            StateHasChanged();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ClickTabToNavigator) Navigator.LocationChanged -= Navigator_LocationChanged;
            }

            base.Dispose(disposing);
        }
    }
}
