// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Waterfall 组件</para>
/// <para lang="en">Waterfall component</para>
/// </summary>
public partial class Waterfall
{
    /// <summary>
    /// <para lang="zh">获得/设置 点击列表项回调方法</para>
    /// <para lang="en">Gets or sets 点击列表项callback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<WaterfallItem, Task>? OnClickItemAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 请求数据回调方法</para>
    /// <para lang="en">Gets or sets 请求datacallback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<WaterfallItem?, Task<IEnumerable<WaterfallItem>>>? OnRequestAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 模板 默认为 null</para>
    /// <para lang="en">Gets or sets template Default is为 null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<(WaterfallItem Item, RenderFragment Context)>? Template { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图片模板 默认为 null</para>
    /// <para lang="en">Gets or sets 图片template Default is为 null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<WaterfallItem>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 加载模板</para>
    /// <para lang="en">Gets or sets 加载template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? LoadTemplate { get; set; }

    private readonly List<WaterfallItem> _items = [];

    /// <summary>
    /// <para lang="zh">获得/设置 每一项宽度 默认 216</para>
    /// <para lang="en">Gets or sets 每一项width Default is 216</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int ItemWidth { get; set; } = 216;

    /// <summary>
    /// <para lang="zh">获得/设置 每一项最小宽度 默认 316 用于显示 loading 图标</para>
    /// <para lang="en">Gets or sets 每一项最小width Default is 316 用于display loading icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int ItemMinHeight { get; set; } = 316;

    private string? ClassString => CssBuilder.Default("bb-waterfall")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bb-waterfall-item-width: {ItemWidth}px;")
        .AddClass($"--bb-waterfall-item-min-height: {ItemMinHeight}px;")
        .Build();

    private bool _rendered;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_rendered)
        {
            _rendered = false;
            await InvokeVoidAsync("append", Id);
        }
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(OnloadAsync));

    /// <summary>
    /// <para lang="zh">请求数据回调方法</para>
    /// <para lang="en">请求datacallback method</para>
    /// </summary>
    [JSInvokable]
    public async Task OnloadAsync(WaterfallItem? item)
    {
        if (OnRequestAsync != null)
        {
            _items.Clear();
            _items.AddRange(await OnRequestAsync(item));
            _rendered = true;
            StateHasChanged();
        }
    }

    /// <summary>
    /// <para lang="zh">点击图片回调方法</para>
    /// <para lang="en">点击图片callback method</para>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnClickItem(WaterfallItem item)
    {
        if (OnClickItemAsync != null)
        {
            await OnClickItemAsync(item);
        }
    }
}
