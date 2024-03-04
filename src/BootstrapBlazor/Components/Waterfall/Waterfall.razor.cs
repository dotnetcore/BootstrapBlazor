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
    [EditorRequired]
    [NotNull]
    public Func<WaterfallItem?, Task<IEnumerable<WaterfallItem>>>? OnRequestAsync { get; set; }

    /// <summary>
    /// 获得/设置 模板 默认为 null
    /// </summary>
    [Parameter]
    public RenderFragment<RenderFragment>? Template { get; set; }

    /// <summary>
    /// 获得/设置 图片模板 默认为 null
    /// </summary>
    [Parameter]
    public RenderFragment<WaterfallItem>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 每一项宽度 默认 216
    /// </summary>
    [Parameter]
    public int ItemWidth { get; set; } = 216;

    private string? ClassString => CssBuilder.Default("bb-waterfall")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bb-waterfall-item-width: {ItemWidth}px;")
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(OnloadAsync));

    /// <summary>
    /// 请求数据回调方法
    /// </summary>
    [JSInvokable]
    public async Task<IEnumerable<WaterfallItem>> OnloadAsync(WaterfallItem? item)
    {
        var items = await OnRequestAsync(item);
        return items;
    }

    private async Task OnClickItem(WaterfallItem item)
    {
        if (OnClickItemAsync != null)
        {
            await OnClickItemAsync(item);
        }
    }
}
