﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// WinBox 组件
/// </summary>
public partial class WinBox
{
    /// <summary>
    /// DialogServices 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private WinBoxService? WinBoxService { get; set; }

    private WinBoxOption? _option;

    private readonly ConcurrentDictionary<string, WinBoxOption> _cache = new();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 Dialog 弹窗事件
        WinBoxService.Register(this, Show);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_option != null && _render)
        {
            _render = false;
            await InvokeVoidAsync("show", _option.Id, Interop, _option);
        }
    }

    private bool _render;
    private async Task Show(WinBoxOption option)
    {
        var id = ComponentIdGenerator.Generate(option);

        _cache.TryAdd(id, option);
        option.Id = id;
        if (option.ContentTemplate != null)
        {
            _render = true;
            _option = option;
            StateHasChanged();
        }
        else
        {
            await InvokeVoidAsync("show", option.Id, Interop, option);
        }
    }

    /// <summary>
    /// 弹窗关闭回调方法由 JavaScript 调用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnClose(string id)
    {
        if (_cache.TryRemove(id, out var option) && option.OnCloseAsync != null)
        {
            await option.OnCloseAsync();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        if (disposing)
        {
            WinBoxService.UnRegister(this);
        }
    }
}
