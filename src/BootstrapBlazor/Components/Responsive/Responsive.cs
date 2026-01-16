// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">响应监听 组件</para>
/// <para lang="en">Responsive Component</para>
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "responsive", JSObjectReference = true)]
public class Responsive : BootstrapModuleComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 浏览器断点阈值改变时触发 默认 null</para>
    /// <para lang="en">Get/Set Trigger when browser breakpoint threshold changes. Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<BreakPoint, Task>? OnBreakPointChanged { get; set; }

    private BreakPoint _breakPoint = BreakPoint.None;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(OnResize));

    /// <summary>
    /// <para lang="zh">JSInvoke 回调方法</para>
    /// <para lang="en">JSInvoke Callback Method</para>
    /// </summary>
    /// <param name="breakPoint"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnResize(BreakPoint breakPoint)
    {
        if (OnBreakPointChanged != null && breakPoint != _breakPoint)
        {
            _breakPoint = breakPoint;
            await OnBreakPointChanged(breakPoint);
        }
    }
}
