// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Reconnector 组件</para>
/// <para lang="en">Reconnector Component</para>
/// </summary>
public class Reconnector : ComponentBase, IReconnector
{
    /// <summary>
    /// <para lang="zh">获得/设置 正在尝试重试连接对话框的模板</para>
    /// <para lang="en">Get/Set Reconnecting Template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ReconnectingTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 连接失败对话框的模板</para>
    /// <para lang="en">Get/Set Reconnect Failed Template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ReconnectFailedTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 连接被拒绝对话框的模板</para>
    /// <para lang="en">Get/Set Reconnect Rejected Template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ReconnectRejectedTemplate { get; set; }

    [Inject]
    [NotNull]
    private IReconnectorProvider? Provider { get; set; }

    /// <summary>
    /// <para lang="zh">OnAfterRender 方法</para>
    /// <para lang="en">OnAfterRender Method</para>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        Provider.NotifyContentChanged(this);
    }
}
