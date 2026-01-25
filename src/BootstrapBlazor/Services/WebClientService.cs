// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">WebClient 服务类</para>
/// <para lang="en">WebClient Service Class</para>
/// </summary>
/// <param name="ipLocatorFactory"></param>
/// <param name="options"></param>
/// <param name="runtime"></param>
/// <param name="navigation"></param>
/// <param name="logger"></param>
public class WebClientService(IIpLocatorFactory ipLocatorFactory,
    IOptionsMonitor<BootstrapBlazorOptions> options,
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
    /// <para lang="zh">获得 ClientInfo 实例方法</para>
    /// <para lang="en">Get ClientInfo Instance Method</para>
    /// </summary>
    public async Task<ClientInfo> GetClientInfo()
    {
        _taskCompletionSource = new TaskCompletionSource();
        _client = new ClientInfo()
        {
            RequestUrl = navigation.Uri
        };

        try
        {
            _jsModule ??= await runtime.LoadModuleByName("client");
            if (_jsModule != null)
            {
                _interop ??= DotNetObjectReference.Create(this);
                await _jsModule.InvokeVoidAsync("ping", "ip.axd", _interop, nameof(SetData));
                // <para lang="zh">等待 SetData 方法执行完毕</para>
                // <para lang="en">Wait for SetData method to complete</para>
                await _taskCompletionSource.Task.WaitAsync(TimeSpan.FromSeconds(3));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{GetClientInfo} throw exception", nameof(GetClientInfo));
        }

        // <para lang="zh">补充 IP 地址信息</para>
        // <para lang="en">Supplement IP address information</para>
        if (options.CurrentValue.WebClientOptions.EnableIpLocator && string.IsNullOrEmpty(_client.City))
        {
            _provider ??= ipLocatorFactory.Create(options.CurrentValue.IpLocatorOptions.ProviderName);
            _client.City = await _provider.Locate(_client.Ip);
        }
        return _client;
    }

    /// <summary>
    /// <para lang="zh">SetData 方法由 JS 调用</para>
    /// <para lang="en">SetData method called by JS</para>
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
    /// <para lang="zh">Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously</para>
    /// <para lang="en">Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            // <para lang="zh">销毁 DotNetObjectReference 实例</para>
            // <para lang="en">Dispose DotNetObjectReference instance</para>
            _interop?.Dispose();

            // <para lang="zh">销毁 JSModule</para>
            // <para lang="en">Dispose JSModule</para>
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
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// <para lang="zh">客户端请求信息实体类</para>
/// <para lang="en">Client Request Information Entity Class</para>
/// </summary>
public class ClientInfo
{
    /// <summary>
    /// <para lang="zh">获得/设置 链接 Id</para>
    /// <para lang="en">Gets or sets Connection Id</para>
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 客户端IP</para>
    /// <para lang="en">Gets or sets Client IP</para>
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 客户端地点</para>
    /// <para lang="en">Gets or sets Client City</para>
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 客户端浏览器</para>
    /// <para lang="en">Gets or sets Client Browser</para>
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 客户端操作系统</para>
    /// <para lang="en">Gets or sets Client OS</para>
    /// </summary>
    public string? OS { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 客户端设备类型</para>
    /// <para lang="en">Gets or sets Client Device Type</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WebClientDeviceType Device { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 客户端浏览器语言</para>
    /// <para lang="en">Gets or sets Client Browser Language</para>
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// <para lang="zh">获取/设置 请求网址</para>
    /// <para lang="en">Gets or sets Request URL</para>
    /// </summary>
    public string? RequestUrl { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 客户端 UserAgent</para>
    /// <para lang="en">Gets or sets Client UserAgent</para>
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 浏览器引擎信息</para>
    /// <para lang="en">Gets or sets Browser Engine Info</para>
    /// </summary>
    public string? Engine { get; set; }
}
