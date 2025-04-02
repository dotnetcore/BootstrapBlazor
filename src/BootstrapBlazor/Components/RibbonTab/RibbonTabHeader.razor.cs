// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// Header 组件
/// </summary>
public partial class RibbonTabHeader
{
    /// <summary>
    /// 获得/设置 数据源
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public IEnumerable<RibbonTabItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否显示悬浮小箭头 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowFloatButton { get; set; }

    /// <summary>
    /// 获得/设置 是否开启 Url 锚点
    /// </summary>
    [Parameter]
    public bool IsSupportAnchor { get; set; }

    /// <summary>
    /// 获得/设置 点击命令按钮回调方法
    /// </summary>
    [Parameter]
    public Func<RibbonTabItem, Task>? OnItemClickAsync { get; set; }

    /// <summary>
    /// 获得/设置 点击标签 Menu 回调方法
    /// </summary>
    [Parameter]
    public Func<RibbonTabItem, Task>? OnMenuClickAsync { get; set; }

    /// <summary>
    /// 编码锚点回调方法 第一参数是当前地址 Url 第二个参数是当前选项 Text 属性 返回值为地址全路径
    /// </summary>
    [Parameter]
    public Func<string, string?, string?>? EncodeAnchorCallback { get; set; }

    /// <summary>
    /// 解码锚点回调方法
    /// </summary>
    [Parameter]
    public Func<string, string?>? DecodeAnchorCallback { get; set; }

    /// <summary>
    /// 获得/设置 组件是否悬浮状态改变时回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnFloatChanged { get; set; }

    /// <summary>
    /// 获得/设置 选项卡向上箭头图标
    /// </summary>
    [Parameter]
    public string? RibbonArrowUpIcon { get; set; }

    /// <summary>
    /// 获得/设置 选项卡向下箭头图标
    /// </summary>
    [Parameter]
    public string? RibbonArrowDownIcon { get; set; }

    /// <summary>
    /// 获得/设置 选项卡可固定图标
    /// </summary>
    [Parameter]
    public string? RibbonArrowPinIcon { get; set; }

    /// <summary>
    /// 获得/设置 右侧按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment? RightButtonsTemplate { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private bool _isFloat;

    private bool _isExpand;

    private string? HeaderClassString => CssBuilder.Default("ribbon-header")
        .AddClass("is-float", _isFloat)
        .AddClass("is-expand", _isFloat && _isExpand)
        .Build();

    private string? ArrowIconClassString => CssBuilder.Default()
        .AddClass(RibbonArrowUpIcon, !_isFloat)
        .AddClass(RibbonArrowDownIcon, _isFloat && !_isExpand)
        .AddClass(RibbonArrowPinIcon, _isFloat && _isExpand)
        .Build();

    private static string? GetClassString(RibbonTabItem item) => CssBuilder.Default()
        .AddClass("active", item.IsActive)
        .AddClass(item.CssClass)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RibbonArrowUpIcon ??= IconTheme.GetIconByKey(ComponentIcons.RibbonTabArrowUpIcon);
        RibbonArrowDownIcon ??= IconTheme.GetIconByKey(ComponentIcons.RibbonTabArrowDownIcon);
        RibbonArrowPinIcon ??= IconTheme.GetIconByKey(ComponentIcons.RibbonTabArrowPinIcon);

        Items ??= [];

        if (IsSupportAnchor)
        {
            var hash = DecodeAnchorCallback?.Invoke(NavigationManager.Uri) ?? HttpUtility.UrlDecode(NavigationManager.Uri.Split('#').LastOrDefault());
            if (!string.IsNullOrEmpty(hash))
            {
                var item = Items.FirstOrDefault(i => i.Text == hash);
                if (item != null)
                {
                    ResetActiveTabItem();
                    item.IsActive = true;
                }
            }
        }
        else if (!Items.Any(i => i.IsActive))
        {
            var item = Items.FirstOrDefault();
            if (item != null)
            {
                item.IsActive = true;
            }
        }
    }

    private async Task OnClick(RibbonTabItem item)
    {
        if (OnItemClickAsync != null)
        {
            await OnItemClickAsync(item);
        }
    }

    private async Task OnClickTabItemAsync(TabItem item)
    {
        if (IsSupportAnchor)
        {
            var url = EncodeAnchorCallback?.Invoke(NavigationManager.Uri, item.Text) ?? $"{NavigationManager.Uri.Split('#').FirstOrDefault()}#{HttpUtility.UrlEncode(item.Text)}";
            if (!string.IsNullOrEmpty(url))
            {
                NavigationManager.NavigateTo(url);
            }
        }

        ResetActiveTabItem();
        var tab = Items.First(i => i.Text == item.Text);
        tab.IsActive = true;
        if (OnMenuClickAsync != null)
        {
            await OnMenuClickAsync(tab);
        }
        if (_isFloat)
        {
            _isExpand = true;
            StateHasChanged();
        }
    }

    private void ResetActiveTabItem()
    {
        var activeItem = Items.FirstOrDefault(item => item.IsActive);
        if (activeItem != null)
        {
            activeItem.IsActive = false;
        }
    }

    private async Task OnToggleFloat()
    {
        _isFloat = !_isFloat;
        if (!_isFloat)
        {
            _isExpand = false;
        }
        if (OnFloatChanged != null)
        {
            await OnFloatChanged(_isFloat);
        }
    }

    private static RenderFragment? RenderTemplate(RibbonTabItem item) => item.Component?.Render() ?? item.Template;

    /// <summary>
    /// SetExpand 方法
    /// </summary>
    public void SetExpand()
    {
        _isExpand = false;
        StateHasChanged();
    }

    /// <summary>
    /// 重新渲染组件
    /// </summary>
    public void Render() => StateHasChanged();
}
