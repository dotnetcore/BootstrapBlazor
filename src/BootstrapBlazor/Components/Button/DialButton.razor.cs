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
    public RenderFragment<DialButtonItem>? ItemTemplate { get; set; }

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
    /// 获得/设置 动画延时 默认 400 单位 ms 毫秒
    /// </summary>
    [Parameter]
    public int Duration { get; set; } = 400;

    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="DialMode"/> 为 <seealso cref="DialMode.Radial"/> 时扇形分布半径值 默认 75;
    /// </summary>
    [Parameter]
    public int Radius { get; set; } = 75;

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
    /// 获得/设置 呈现方式 默认为 直线
    /// </summary>
    [Parameter]
    public DialMode DialMode { get; set; }

    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default("dial-button")
        .AddClass("is-radial", DialMode == DialMode.Radial)
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

    private string? IsAutoCloseString => IsAutoClose ? "true" : null;

    /// <summary>
    /// 获得 按钮 disabled 属性
    /// </summary>
    private string? Disabled => IsDisabled ? "disabled" : null;

    private string? RadiusString => DialMode == DialMode.Radial ? Radius.ToString() : null;

    private readonly List<DialButtonItem> _buttonItems = new();

    private IEnumerable<DialButtonItem> _list => _buttonItems.Concat(Items);

    private string? DurationString => Duration == 400 ? null : $"{Duration}";

    private bool _shouldRender;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= Enumerable.Empty<DialButtonItem>();
        Duration = Math.Max(400, Duration);
        _shouldRender = true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender && _shouldRender)
        {
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
