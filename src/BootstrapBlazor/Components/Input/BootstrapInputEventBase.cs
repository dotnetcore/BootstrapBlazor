// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">输入框基类</para>
/// <para lang="en">Input Base Class</para>
/// </summary>
public abstract class BootstrapInputEventBase<TValue> : BootstrapInputBase<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否在文本框输入值时触发 bind-value:event="oninput" 默认 false</para>
    /// <para lang="en">Get/Set Whether to trigger bind-value:event="oninput" when entering value in text box. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">设置 <see cref="UseInputEvent"/> 参数后，Formatter 与 FormatString 均失效</para>
    /// <para lang="en">After setting the <see cref="UseInputEvent"/> parameter, both Formatter and FormatString become invalid</para>
    /// </remarks>
    [Parameter]
    public bool UseInputEvent { get; set; }

    /// <summary>
    /// <para lang="zh">event 字符串</para>
    /// <para lang="en">event String</para>
    /// </summary>
    protected string EventString => UseInputEvent ? "oninput" : "onchange";
}
