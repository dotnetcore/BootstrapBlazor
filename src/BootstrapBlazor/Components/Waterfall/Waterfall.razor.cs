﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Waterfall 组件
/// </summary>
public partial class Waterfall
{
    /// <summary>
    /// 获得/设置 数据源
    /// </summary>
    [Parameter]
    [NotNull]
    public List<string>? Items { get; set; }

    private string? ClassString => CssBuilder.Default("bb-waterfall")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= [];
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
    public async Task OnloadAsync()
    {
        await InvokeVoidAsync("append", Id, Items);
    }
}
