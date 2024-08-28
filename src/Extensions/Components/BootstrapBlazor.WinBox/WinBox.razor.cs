// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
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
        WinBoxService.Register(this, Execute);
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

    private async Task Execute(WinBoxOption? option, string method)
    {
        if (method == "stack")
        {
            await InvokeVoidAsync("stack");
        }
        else if (option != null)
        {
            if (method == "show")
            {
                await Show(option);
            }
            else if (method == "setIcon")
            {
                await InvokeVoidAsync("execute", option.Id, method, option.Icon);
            }
            else if (method == "setTitle")
            {
                await InvokeVoidAsync("execute", option.Id, method, option.Title);
            }
            else
            {
                await InvokeVoidAsync("execute", option.Id, method);
            }
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

    private async Task CloseAsync(WinBoxOption option)
    {
        await InvokeVoidAsync("execute", option.Id, "close");
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
        StateHasChanged();
    }

    /// <summary>
    /// 弹窗关闭回调方法由 JavaScript 调用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnCreate(string id)
    {
        if (_cache.TryGetValue(id, out var option) && option.OnCreateAsync != null)
        {
            await option.OnCreateAsync();
        }
    }

    /// <summary>
    /// 弹窗关闭回调方法由 JavaScript 调用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnShow(string id)
    {
        if (_cache.TryGetValue(id, out var option) && option.OnShowAsync != null)
        {
            await option.OnShowAsync();
        }
    }

    /// <summary>
    /// 弹窗关闭回调方法由 JavaScript 调用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnHide(string id)
    {
        if (_cache.TryGetValue(id, out var option) && option.OnHideAsync != null)
        {
            await option.OnHideAsync();
        }
    }

    /// <summary>
    /// 弹窗关闭回调方法由 JavaScript 调用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnFocus(string id)
    {
        if (_cache.TryGetValue(id, out var option) && option.OnFocusAsync != null)
        {
            await option.OnFocusAsync();
        }
    }

    /// <summary>
    /// 弹窗关闭回调方法由 JavaScript 调用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnBlur(string id)
    {
        if (_cache.TryGetValue(id, out var option) && option.OnBlurAsync != null)
        {
            await option.OnBlurAsync();
        }
    }

    /// <summary>
    /// 弹窗关闭回调方法由 JavaScript 调用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnFullscreen(string id)
    {
        if (_cache.TryGetValue(id, out var option) && option.OnFullscreenAsync != null)
        {
            await option.OnFullscreenAsync();
        }
    }

    /// <summary>
    /// 弹窗关闭回调方法由 JavaScript 调用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnRestore(string id)
    {
        if (_cache.TryGetValue(id, out var option) && option.OnRestoreAsync != null)
        {
            await option.OnRestoreAsync();
        }
    }

    /// <summary>
    /// 弹窗关闭回调方法由 JavaScript 调用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnMaximize(string id)
    {
        if (_cache.TryGetValue(id, out var option) && option.OnMaximizeAsync != null)
        {
            await option.OnMaximizeAsync();
        }
    }

    /// <summary>
    /// 弹窗关闭回调方法由 JavaScript 调用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnMinimize(string id)
    {
        if (_cache.TryGetValue(id, out var option) && option.OnMinimizeAsync != null)
        {
            await option.OnMinimizeAsync();
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
