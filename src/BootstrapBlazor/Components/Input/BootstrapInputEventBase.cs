// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
