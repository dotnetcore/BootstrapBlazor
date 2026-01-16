// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Dialog 组件服务</para>
/// <para lang="en">Dialog Service</para>
/// </summary>
public class DialogService : BootstrapServiceBase<DialogOption>
{
    /// <summary>
    /// <para lang="zh">显示 Dialog 方法</para>
    /// <para lang="en">Show Dialog</para>
    /// </summary>
    /// <param name="option"><para lang="zh">弹窗配置信息实体类</para><para lang="en">Dialog Option</para></param>
    /// <param name="dialog"><para lang="zh">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</para><para lang="en">Specific Dialog Component. Default is null, use <see cref="BootstrapBlazorRoot"/> component built-in dialog component</para></param>
    /// <returns></returns>
    public Task Show(DialogOption option, Dialog? dialog = null) => Invoke(option, dialog);
}
