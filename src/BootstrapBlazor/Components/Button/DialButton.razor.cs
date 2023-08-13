// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 拨号按钮组件
/// </summary>
public partial class DialButton
{
    /// <summary>
    /// 数据项模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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
    public IEnumerable<DialButtonItem>? Items { get; set; }

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
    public EventCallback<DialButtonItem> OnClick { get; set; }

    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default("dial-button")
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

    private string? DialButtonListClassString => CssBuilder.Default("dial-list")
        .Build();

    private string? IsAutoCloseString => IsAutoClose ? "true" : null;

    /// <summary>
    /// 获得 按钮 disabled 属性
    /// </summary>
    private string? Disabled => IsDisabled ? "disabled" : null;

    private List<DialButtonItem> _buttonItems = new();

    private IEnumerable<DialButtonItem> _list => _buttonItems.Concat(Items);

    private Placement _lastPlacement;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= Enumerable.Empty<DialButtonItem>();
    }

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

    private async Task OnClickItem(DialButtonItem item)
    {
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
