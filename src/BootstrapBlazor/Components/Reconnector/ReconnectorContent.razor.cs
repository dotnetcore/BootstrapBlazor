// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
            await InvokeVoidAsync("reconnect");
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
