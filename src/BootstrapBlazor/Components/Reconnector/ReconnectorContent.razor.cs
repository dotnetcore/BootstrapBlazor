﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ReconnectorContent 组件
/// </summary>
public partial class ReconnectorContent
{
    /// <summary>
    /// 获得/设置 ReconnectingTemplate 模板
    /// </summary>
    [Parameter]
    public RenderFragment? ReconnectingTemplate { get; set; }

    /// <summary>
    /// 获得/设置 ReconnectFailedTemplate 模板
    /// </summary>
    [Parameter]
    public RenderFragment? ReconnectFailedTemplate { get; set; }

    /// <summary>
    /// 获得/设置 ReconnectRejectedTemplate 模板
    /// </summary>
    [Parameter]
    public RenderFragment? ReconnectRejectedTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否自动尝试重连 默认 true
    /// </summary>
    [Parameter]
    public bool AutoReconnect { get; set; } = true;

    /// <summary>
    /// 获得/设置 自动重连间隔 默认 5000 毫秒 最小值为 1000 毫秒
    /// </summary>
    [Parameter]
    public int ReconnectInterval { get; set; } = 5000;

    [Inject]
    [NotNull]
    private IReconnectorProvider? Provider { get; set; }

    /// <summary>
    /// SetParametersAsync 方法
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        Provider.Register(ContentChanged);
        return base.SetParametersAsync(parameters);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        if (AutoReconnect)
        {
            await InvokeVoidAsync("reconnect", Math.Max(1000, ReconnectInterval));
        }
    }

    /// <summary>
    /// ContentChanged 方法
    /// </summary>
    /// <param name="reconnectingTemplate"></param>
    /// <param name="reconnectFailedTemplate"></param>
    /// <param name="reconnectRejectedTemplate"></param>
    private void ContentChanged(RenderFragment? reconnectingTemplate, RenderFragment? reconnectFailedTemplate, RenderFragment? reconnectRejectedTemplate)
    {
        ReconnectingTemplate = reconnectingTemplate;
        ReconnectFailedTemplate = reconnectFailedTemplate;
        ReconnectRejectedTemplate = reconnectRejectedTemplate;
        InvokeAsync(StateHasChanged);
    }
}
