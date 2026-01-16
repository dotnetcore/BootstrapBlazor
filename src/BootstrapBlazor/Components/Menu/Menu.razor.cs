// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Menu 组件基类</para>
/// <para lang="en">Menu Component Base</para>
/// </summary>
public partial class Menu
{
    /// <summary>
    /// <para lang="zh">获得 组件样式</para>
    /// <para lang="en">Get Component Style</para>
    /// </summary>
    protected string? ClassString => CssBuilder.Default("menu")
        .AddClass("is-bottom", IsBottom)
        .AddClass("is-vertical", IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? SideMenuClassString => CssBuilder.Default()
        .AddClass("accordion", IsAccordion)
        .Build();

    private string? ExpandString => (IsVertical && IsExpandAll) ? "true" : null;

    private string SideMenuId => $"{Id}_sub";

    /// <summary>
    /// <para lang="zh">用于提高性能存储当前 active 状态的菜单</para>
    /// <para lang="en">Used to improve performance by storing the current active menu</para>
    /// </summary>
    private MenuItem? ActiveMenu { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 菜单数据集合</para>
    /// <para lang="en">Get/Set Menu Data Collection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<MenuItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为手风琴效果 默认为 false</para>
    /// <para lang="en">Get/Set Whether it is accordion effect. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">启用此功能时 <see cref="IsExpandAll" /> 参数不生效</para>
    /// <para lang="en"><see cref="IsExpandAll" /> parameter does not take effect when this feature is enabled</para>
    /// </remarks>
    [Parameter]
    public bool IsAccordion { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否全部展开 默认为 false</para>
    /// <para lang="en">Get/Set Whether to expand all. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">手风琴效果 <see cref="IsAccordion" /> 时此参数不生效</para>
    /// <para lang="en">This parameter does not take effect when accordion effect <see cref="IsAccordion" /> is enabled</para>
    /// </remarks>
    [Parameter]
    public bool IsExpandAll { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧栏是否收起 默认 false 未收起</para>
    /// <para lang="en">Get/Set Whether sidebar is collapsed. Default false (Not collapsed)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsCollapsed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧栏垂直模式 默认 false</para>
    /// <para lang="en">Get/Set Sidebar vertical mode. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自动滚动到可视区域 默认 true <see cref="IsVertical"/> 开启时生效</para>
    /// <para lang="en">Get/Set Automatically scroll to visible area. Default true. Effective when <see cref="IsVertical"/> is enabled</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool IsScrollIntoView { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏垂直模式在底部 默认 false</para>
    /// <para lang="en">Get/Set Sidebar vertical mode at bottom. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsBottom { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 缩进大小 默认为 16 单位 px</para>
    /// <para lang="en">Get/Set Indent size. Default 16px</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int IndentSize { get; set; } = 16;

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁止导航 默认为 false 允许导航</para>
    /// <para lang="en">Get/Set Whether to disable navigation. Default false (Allow navigation)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool DisableNavigation { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 菜单项点击回调委托</para>
    /// <para lang="en">Get/Set Menu item click callback delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<MenuItem, Task>? OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 NavigationManager 实例</para>
    /// <para lang="en">Get/Set NavigationManager Instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    private NavigationManager? Navigator { get; set; }

    [Inject]
    [NotNull]
    private TabItemTextOptions? Options { get; set; }

    private bool _isExpandAll;
    private bool _isAccordion;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _isAccordion = IsAccordion;
        _isExpandAll = IsExpandAll;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= [];
        InitMenus(null, Items, GetUrl());
        if (!DisableNavigation)
        {
            Options.Text ??= ActiveMenu?.Text;
            Options.Icon = ActiveMenu?.Icon;
            Options.IsActive = true;
        }
    }

    private string GetUrl()
    {
        var url = Navigator.ToBaseRelativePath(Navigator.Uri);
        if (url.Contains('?'))
        {
            url = url[..url.IndexOf('?')];
        }
        if (url.Contains('#'))
        {
            url = url[..url.IndexOf('#')];
        }
        return url;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeUpdateAsync();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    private async Task InvokeUpdateAsync()
    {
        if (ShouldInvoke())
        {
            _isAccordion = IsAccordion;
            _isExpandAll = IsExpandAll;
            await InvokeVoidAsync("update", Id);
        }

        bool ShouldInvoke() => IsVertical && (_isAccordion != IsAccordion || _isExpandAll != IsExpandAll);
    }

    private void InitMenus(MenuItem? parent, IEnumerable<MenuItem> menus, string url)
    {
        foreach (var item in menus)
        {
            if (parent != null)
            {
                // 设置当前菜单父菜单
                item.Parent = parent;
            }

            // 设置当前菜单缩进
            item.SetIndent();

            if (!DisableNavigation)
            {
                // 未禁用导航时设置 active = false 使用地址栏激活菜单
                item.IsActive = false;
            }

            if (item.Items.Any())
            {
                // 递归子菜单
                InitMenus(item, item.Items, url);
            }
            else if (!DisableNavigation && (item.Url?.TrimStart('/').Equals(url, StringComparison.OrdinalIgnoreCase) ?? false))
            {
                // 未禁用导航时 使用地址栏激活菜单
                item.IsActive = true;

                // 设置父菜单展开
                item.SetCollapse(false);
            }

            if (item.IsActive)
            {
                ActiveMenu = item;
            }
        }
    }

    private async Task OnClickMenu(MenuItem item)
    {
        if (!item.IsDisabled)
        {
            if (!DisableNavigation && !item.Items.Any())
            {
                Options.Text = item.Text;
                Options.Icon = item.Icon;
                Options.IsActive = true;
            }

            // 回调委托
            if (OnClick != null)
            {
                await OnClick(item);
            }

            if (DisableNavigation)
            {
                if (IsVertical)
                {
                    if (ActiveMenu != null)
                    {
                        ActiveMenu.IsActive = false;
                    }
                    item.IsActive = true;
                    if (IsCollapsed)
                    {
                        item.CascadingSetActive();
                    }
                }
                else
                {
                    // 顶栏模式重新级联设置 active
                    ActiveMenu?.CascadingSetActive(false);
                    item.CascadingSetActive();
                }
                ActiveMenu = item;

                // 刷新 UI
                StateHasChanged();
            }
        }
    }
}
