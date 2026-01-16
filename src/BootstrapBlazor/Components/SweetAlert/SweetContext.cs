// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

internal class SweetContext
{
    /// <summary>
    /// <para lang="zh">获得/设置 弹窗返回值</para>
    /// <para lang="en">Get/Set Dialog Return Value</para>
    /// </summary>
    public bool Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗任务上下文</para>
    /// <para lang="en">Get/Set Dialog Task Context</para>
    /// </summary>
    [NotNull]
    public TaskCompletionSource<bool>? ConfirmTask { get; init; }
}
