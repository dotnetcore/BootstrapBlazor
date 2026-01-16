// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">IReconnector 接口</para>
///  <para lang="en">IReconnector Interface</para>
/// </summary>
public interface IReconnector
{
    /// <summary>
    ///  <para lang="zh">获得/设置 正在尝试重试连接对话框的模板</para>
    ///  <para lang="en">Get/Set Reconnecting Template</para>
    /// </summary>
    RenderFragment? ReconnectingTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 连接失败对话框的模板</para>
    ///  <para lang="en">Get/Set Reconnect Failed Template</para>
    /// </summary>
    RenderFragment? ReconnectFailedTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 连接被拒绝对话框的模板</para>
    ///  <para lang="en">Get/Set Reconnect Rejected Template</para>
    /// </summary>
    RenderFragment? ReconnectRejectedTemplate { get; set; }
}
