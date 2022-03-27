// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 可关闭弹窗基类
/// </summary>
public class ClosableDialogBase : BootstrapComponentBase
{
    /// <summary>
    /// 关闭弹窗回调委托
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<Task>? OnClose { get; set; }

    /// <summary>
    /// 保存按钮回调委托
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<Task>? OnSave { get; set; }
}
