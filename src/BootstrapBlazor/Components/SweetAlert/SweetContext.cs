// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

internal class SweetContext
{
    /// <summary>
    /// 获得/设置 弹窗返回值
    /// </summary>
    public bool Value { get; set; }

    /// <summary>
    /// 获得/设置 弹窗任务上下文
    /// </summary>
    [NotNull]
#if NET7_0_OR_GREATER
    public required TaskCompletionSource<bool>? ConfirmTask { get; init; }
#else
    public TaskCompletionSource<bool>? ConfirmTask { get; set; }
#endif
}
