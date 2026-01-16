// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">拨号按钮组件</para>
/// <para lang="en">DialButton component</para>
/// </summary>
public partial class DialButton
{
    /// <summary>
    /// <para lang="zh">数据项模板</para>
    /// <para lang="en">Data item template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">按钮模板</para>
    /// <para lang="en">Button template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ButtonTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">展开部分模板</para>
    /// <para lang="en">Expanded section template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<DialButtonItem>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">展开项集合</para>
    /// <para lang="en">Expanded item collection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<DialButtonItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮颜色</para>
    /// <para lang="en">Gets or sets the button color</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 动画延时 默认 400 单位 ms 毫秒</para>
    /// <para lang="en">Gets or sets the animation duration. Default is 400ms</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Duration { get; set; } = 400;

    /// <summary>
    /// <para lang="zh">获得/设置 位置</para>
    /// <para lang="en">Gets or sets the placement</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="DialMode"/> 为 <seealso cref="DialMode.Radial"/> 时扇形分布半径值 默认 75;</para>
    /// <para lang="en">Gets or sets the radial radius when <see cref="DialMode"/> is <seealso cref="DialMode.Radial"/>. Default is 75</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Radius { get; set; } = 75;

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗偏移量 默认 8px</para>
    /// <para lang="en">Gets or sets the offset. Default is 8px</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public float Offset { get; set; } = 8;

    /// <summary>
    /// <para lang="zh">获得/设置 Size 大小</para>
    /// <para lang="en">Gets or sets the Size</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示图标</para>
    /// <para lang="en">Gets or sets the icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用 默认为 false</para>
    /// <para lang="en">Gets or sets whether it is disabled. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动关闭弹窗 默认为 true</para>
    /// <para lang="en">Gets or sets whether to auto close. Default is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsAutoClose { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 OnClick 事件</para>
    /// <para lang="en">Gets or sets the OnClick event</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<DialButtonItem> OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 呈现方式 默认为 直线</para>
    /// <para lang="en">Gets or sets the presentation mode. Default is Linear</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public DialMode DialMode { get; set; }

    /// <summary>
    /// <para lang="zh">获得 按钮样式集合</para>
    /// <para lang="en">Gets the button style collection</para>
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default("dial-button")
        .AddClass("is-radial", DialMode == DialMode.Radial)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 按钮样式集合</para>
    /// <para lang="en">Gets the button style collection</para>
    /// </summary>
    /// <returns></returns>
    private string? ButtonClassString => CssBuilder.Default("btn")
        .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
        .Build();

    private string? IsAutoCloseString => IsAutoClose ? "true" : null;

    /// <summary>
    /// <para lang="zh">获得 按钮 disabled 属性</para>
    /// <para lang="en">Gets the button disabled attribute</para>
    /// </summary>
    private string? Disabled => IsDisabled ? "disabled" : null;

    private string? RadiusString => DialMode == DialMode.Radial ? Radius.ToString() : null;

    private readonly List<DialButtonItem> _buttonItems = [];

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
