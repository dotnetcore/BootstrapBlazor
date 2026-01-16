// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SlideButton 组件</para>
/// <para lang="en">SlideButton component</para>
/// </summary>
public partial class SlideButton
{
    /// <summary>
    /// <para lang="zh">获得/设置 数据项模板</para>
    /// <para lang="en">Gets or sets the data item template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? SlideButtonItems { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮模板</para>
    /// <para lang="en">Gets or sets the button template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ButtonTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 展开按钮项模板</para>
    /// <para lang="en">Gets or sets the expanded button item template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? ButtonItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 展开部分模板</para>
    /// <para lang="en">Gets or sets the expanded body template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 展开项集合</para>
    /// <para lang="en">Gets or sets the expanded items collection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 展开项 Header 文本</para>
    /// <para lang="en">Gets or sets the header text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? HeaderText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮颜色</para>
    /// <para lang="en">Gets or sets the button color</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 展开项显示位置</para>
    /// <para lang="en">Gets or sets the placement</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; }

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
    /// <para lang="zh">获得/设置 显示文本</para>
    /// <para lang="en">Gets or sets the text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

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
    public EventCallback<SelectedItem> OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标题 默认 false 不显示</para>
    /// <para lang="en">Gets or sets whether to show header. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowHeader { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 部分模板</para>
    /// <para lang="en">Gets or sets the header template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得 按钮样式集合</para>
    /// <para lang="en">Gets the button style collection</para>
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default("slide-button")
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

    private string? SlideListClassString => CssBuilder.Default("slide-list invisible")
        .AddClass("is-horizontal", Placement.ToDescriptionString().StartsWith("left") || Placement.ToDescriptionString().StartsWith("right"))
        .Build();

    private string? IsAutoCloseString => IsAutoClose ? "true" : null;

    /// <summary>
    /// <para lang="zh">获得 按钮 disabled 属性</para>
    /// <para lang="en">Gets the button disabled attribute</para>
    /// </summary>
    private string? Disabled => IsDisabled ? "disabled" : null;

    private SelectedItem? _selectedItem;

    private readonly List<SlideButtonItem> _buttonItems = [];

    private Placement _lastPlacement;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= [];
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
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(item);
        }
    }
}
