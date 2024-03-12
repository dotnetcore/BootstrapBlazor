// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 输入框基类
/// </summary>
public abstract class BootstrapInputEventBase<TValue> : BootstrapInputBase<TValue>
{
    /// <summary>
    /// 获得/设置 是否在文本框输入值时触发 bind-value:event="oninput" 默认 false
    /// </summary>
    /// <remarks>设置 <see cref="UseInputEvent"/> 参数后，Formatter 与 FormatString 均失效</remarks>
    [Parameter]
    public bool UseInputEvent { get; set; }

    /// <summary>
    /// event 字符串
    /// </summary>
    protected string EventString => UseInputEvent ? "oninput" : "onchange";
}
