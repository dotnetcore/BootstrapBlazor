// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// WebClient 服务类
/// </summary>
/// <param name="runtime"></param>
/// <param name="navigation"></param>
/// <param name="versionService"></param>
public class WebClientService(IJSRuntime runtime, NavigationManager navigation, IVersionService versionService) : IAsyncDisposable
{
    /// <summary>
    /// 获得/设置 模态弹窗返回值任务实例
    /// </summary>
    private TaskCompletionSource? ReturnTask { get; set; }

    private NavigationManager Navigation { get; } = navigation;

    private JSModule? Module { get; set; }

    private DotNetObjectReference<WebClientService>? Interop { get; set; }

    private ClientInfo? Client { get; set; }

    /// <summary>
    /// 获得 ClientInfo 实例方法
    /// </summary>
    /// <returns></returns>
    public async Task<ClientInfo> GetClientInfo()
    {
        ReturnTask = new TaskCompletionSource();
        Client = new ClientInfo()
        {
            RequestUrl = Navigation.Uri
        };
        Module ??= await runtime.LoadModule("./_content/BootstrapBlazor/modules/client.js", versionService.GetVersion());
        Interop ??= DotNetObjectReference.Create(this);
        await Module.InvokeVoidAsync("ping", "ip.axd", Interop, nameof(SetData));

        // 等待 SetData 方法执行完毕
        await ReturnTask.Task;
        return Client;
    }

    /// <summary>
    /// SetData 方法由 JS 调用
    /// </summary>
    /// <param name="client"></param>
    [JSInvokable]
    public void SetData(ClientInfo client)
    {
        Client = client;
        Client.RequestUrl = Navigation.Uri;
        ReturnTask?.TrySetResult();
    }

    private static WebClientDeviceType ParseDeviceType(string device)
    {
        var ret = WebClientDeviceType.PC;
        if (Enum.TryParse<WebClientDeviceType>(device, true, out var d))
        {
            ret = d;
        }
        return ret;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            // 销毁 DotNetObjectReference 实例
            Interop?.Dispose();

            // 销毁 JSModule
            if (Module != null)
            {
                await Module.DisposeAsync();
                Module = null;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// 客户端请求信息实体类
/// </summary>
public class ClientInfo
{
    /// <summary>
    /// 获得/设置 操作日志主键ID
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 获得/设置 客户端IP
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// 获得/设置 客户端地点
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 获得/设置 客户端浏览器
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// 获得/设置 客户端操作系统
    /// </summary>
    public string? OS { get; set; }

    /// <summary>
    /// 获得/设置 客户端设备类型
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WebClientDeviceType Device { get; set; }

    /// <summary>
    /// 获得/设置 客户端浏览器语言
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// 获取/设置 请求网址
    /// </summary>
    public string? RequestUrl { get; set; }

    /// <summary>
    /// 获得/设置 客户端 UserAgent
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// 获得/设置 浏览器引擎信息
    /// </summary>
    public string? Engine { get; set; }
}
