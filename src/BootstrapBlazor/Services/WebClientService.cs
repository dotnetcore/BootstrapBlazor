// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// WebClient 服务类
/// </summary>
/// <param name="ipLocatorFactory"></param>
/// <param name="options"></param>
/// <param name="runtime"></param>
/// <param name="navigation"></param>
/// <param name="logger"></param>
public class WebClientService(IIpLocatorFactory ipLocatorFactory,
    IOptions<BootstrapBlazorOptions> options,
    IJSRuntime runtime,
    NavigationManager navigation,
    ILogger<WebClientService> logger) : IAsyncDisposable
{
    private TaskCompletionSource? _taskCompletionSource;
    private JSModule? _jsModule;
    private DotNetObjectReference<WebClientService>? _interop;
    private ClientInfo? _client;
    private IIpLocatorProvider? _provider;

    /// <summary>
    /// 获得 ClientInfo 实例方法
    /// </summary>
    /// <returns></returns>
    public async Task<ClientInfo> GetClientInfo()
    {
        _taskCompletionSource = new TaskCompletionSource();
        _client = new ClientInfo()
        {
            RequestUrl = navigation.Uri
        };
        _jsModule ??= await runtime.LoadModule("./_content/BootstrapBlazor/modules/client.js");
        _interop ??= DotNetObjectReference.Create(this);
        await _jsModule.InvokeVoidAsync("ping", "ip.axd", _interop, nameof(SetData));

        // 等待 SetData 方法执行完毕
        try
        {
            await _taskCompletionSource.Task.WaitAsync(TimeSpan.FromSeconds(1));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "method GetClientInfo failed");
        }

        // 补充 IP 地址信息
        if (string.IsNullOrEmpty(_client.City))
        {
            _provider ??= ipLocatorFactory.Create(options.Value.IpLocatorOptions.ProviderName);
            _client.City = await _provider.Locate(_client.Ip);
        }
        return _client;
    }

    /// <summary>
    /// SetData 方法由 JS 调用
    /// </summary>
    /// <param name="client"></param>
    [JSInvokable]
    public void SetData(ClientInfo client)
    {
        _client = client;
        _client.RequestUrl = navigation.Uri;
        _taskCompletionSource?.TrySetResult();
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
            _interop?.Dispose();

            // 销毁 JSModule
            if (_jsModule != null)
            {
                await _jsModule.DisposeAsync();
                _jsModule = null;
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
