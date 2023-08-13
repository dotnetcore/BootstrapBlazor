// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// SlideButton 组件
/// </summary>
public partial class SlideButton
{
    /// <summary>
    /// 数据项模板
    /// </summary>
    [Parameter]
    public RenderFragment? SlideButtonItems { get; set; }

    /// <summary>
    /// 按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment? ButtonTemplate { get; set; }

    /// <summary>
    /// 展开部分模板
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// 展开项集合
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 展开项 Header 文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? HeaderText { get; set; }

    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; }

    /// <summary>
    /// 获得/设置 弹窗便宜量 默认 8px
    /// </summary>
    [Parameter]
    public float Offset { get; set; } = 8;

    /// <summary>
    /// 获得/设置 Size 大小
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// 获得/设置 显示图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 显示文本
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认为 false
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 是否自动关闭弹窗 默认为 true
    /// </summary>
    [Parameter]
    public bool IsAutoClose { get; set; } = true;

    /// <summary>
    /// 获得/设置 OnClick 事件
    /// </summary>
    [Parameter]
    public EventCallback<SelectedItem> OnClick { get; set; }

    /// <summary>
    /// 获得/设置 是否显示标题 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowHeader { get; set; }

    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default("slide-button")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    /// <returns></returns>
    private string? ButtonClassString => CssBuilder.Default("btn")
        .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
        .Build();

    private string? SlideListClassString => CssBuilder.Default("slide-list d-none")
        .AddClass("is-horizontal", Placement.ToDescriptionString().StartsWith("left") || Placement.ToDescriptionString().StartsWith("right"))
        .Build();

    private string? IsAutoCloseString => IsAutoClose ? "true" : null;

    /// <summary>
    /// 获得 按钮 disabled 属性
    /// </summary>
    private string? Disabled => IsDisabled ? "disabled" : null;

    private SelectedItem? _selectedItem;

    private List<SlideButtonItem> _buttonItems = new();

    private Placement _lastPlacement;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= Enumerable.Empty<SelectedItem>();
    }

    private string? GetItemClass(SelectedItem item) => CssBuilder.Default("slide-item")
        .AddClass("active", _selectedItem?.Value == item.Value)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _lastPlacement = Placement;
        }

        await base.OnAfterRenderAsync(firstRender);

        if (_lastPlacement != Placement)
        {
            _lastPlacement = Placement;
            await InvokeVoidAsync("update", Id);
        }
    }

    private async Task OnClickItem(SelectedItem item)
    {
        _selectedItem = item;
        if (IsAutoClose)
        {
            await InvokeVoidAsync("close", Id);
        }
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(item);
        }
    }
}
