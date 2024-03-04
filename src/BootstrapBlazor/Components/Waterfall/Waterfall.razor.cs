// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Waterfall 组件
/// </summary>
public partial class Waterfall
{
    /// <summary>
    /// 获得/设置 点击列表项回调方法
    /// </summary>
    [Parameter]
    public Func<WaterfallItem, Task>? OnClickItemAsync { get; set; }

    /// <summary>
    /// 获得/设置 请求数据回调方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<WaterfallItem?, Task<IEnumerable<WaterfallItem>>>? OnRequestAsync { get; set; }

    /// <summary>
    /// 获得/设置 模板 默认为 null
    /// </summary>
    [Parameter]
    public RenderFragment<(WaterfallItem Item, RenderFragment Context)>? Template { get; set; }

    /// <summary>
    /// 获得/设置 图片模板 默认为 null
    /// </summary>
    [Parameter]
    public RenderFragment<WaterfallItem>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 加载模板
    /// </summary>
    [Parameter]
    public RenderFragment? LoadTemplate { get; set; }

    private readonly List<WaterfallItem> _items = [];

    /// <summary>
    /// 获得/设置 每一项宽度 默认 216
    /// </summary>
    [Parameter]
    public int ItemWidth { get; set; } = 216;

    /// <summary>
    /// 获得/设置 每一项最小宽度 默认 316 用于显示 loading 图标
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
    /// <inheritdoc/>
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
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(OnloadAsync));

    /// <summary>
    /// 请求数据回调方法
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
    /// 点击图片回调方法
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
