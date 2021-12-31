// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class WebClientService : IDisposable
{
    /// <summary>
    /// 获得/设置 操作日志主键ID
    /// </summary>
    public string? Id { get; private set; }

    /// <summary>
    /// 获得/设置 客户端IP
    /// </summary>
    public string? Ip { get; private set; }

    /// <summary>
    /// 获得/设置 客户端地点
    /// </summary>
    public string? City { get; private set; }

    /// <summary>
    /// 获得/设置 客户端浏览器
    /// </summary>
    public string? Browser { get; private set; }

    /// <summary>
    /// 获得/设置 客户端操作系统
    /// </summary>
    public string? OS { get; private set; }

    /// <summary>
    /// 获得/设置 客户端设备类型
    /// </summary>
    public string? Device { get; private set; }

    /// <summary>
    /// 获得/设置 客户端浏览器语言
    /// </summary>
    public string? Language { get; private set; }

    /// <summary>
    /// 获取/设置 请求网址
    /// </summary>
    public string? RequestUrl { get; private set; }

    /// <summary>
    /// 获得/设置 客户端 UserAgent
    /// </summary>
    public string? UserAgent { get; private set; }

    /// <summary>
    /// 获得/设置 浏览器引擎信息
    /// </summary>
    public string? Engine { get; private set; }

    /// <summary>
    /// 获得/设置 模态弹窗返回值任务实例
    /// </summary>
    private TaskCompletionSource<bool>? ReturnTask { get; set; }

    private readonly IJSRuntime _runtime;

    private readonly NavigationManager _navigation;

    private JSInterop<WebClientService>? Interop { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="runtime"></param>
    /// <param name="navigation"></param>
    public WebClientService(IJSRuntime runtime, NavigationManager navigation) => (_runtime, _navigation) = (runtime, navigation);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<bool> RetrieveRemoteInfo()
    {
        Interop ??= new JSInterop<WebClientService>(_runtime);
        await Interop.InvokeVoidAsync(this, null, "webClient", "ip.axd", nameof(SetData));
        RequestUrl = _navigation.Uri;

        // 等待 SetData 方法执行完毕
        ReturnTask = new TaskCompletionSource<bool>();
        return await ReturnTask.Task;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ip"></param>
    /// <param name="os"></param>
    /// <param name="browser"></param>
    /// <param name="device"></param>
    /// <param name="language"></param>
    /// <param name="engine"></param>
    /// <param name="agent"></param>
    [JSInvokable]
    public void SetData(string id, string ip, string os, string browser, string device, string language, string engine, string agent)
    {
        Id = id;
        Ip = ip;
        OS = os;
        Browser = browser;
        Device = device;
        Language = language;
        Engine = engine;
        UserAgent = agent;
        ReturnTask?.TrySetResult(true);
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Interop?.Dispose();
            Interop = null;
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
