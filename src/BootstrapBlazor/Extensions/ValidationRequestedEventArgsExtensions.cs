// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if !NET11_0_OR_GREATER
using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ValidationRequestedEventArgs 异步验证兼容扩展</para>
/// <para lang="en">Async validation compatibility extensions for ValidationRequestedEventArgs</para>
/// </summary>
public static class ValidationRequestedEventArgsExtensions
{
    /// <summary>
    /// <para lang="zh">注册由当前 EditContext.ValidateAsync 调用执行并等待的异步验证</para>
    /// <para lang="en">Registers an async validator to be invoked and awaited by the current EditContext.ValidateAsync call</para>
    /// </summary>
    /// <param name="args"><para lang="zh">验证请求事件参数</para><para lang="en">The validation request event arguments</para></param>
    /// <param name="validator"><para lang="zh">异步验证委托</para><para lang="en">The async validation delegate</para></param>
    /// <remarks>
    /// <para lang="zh">必须在 OnValidationRequested 处理程序中同步注册。旧框架共享事件参数，不能在处理程序中调用其他上下文的同步 Validate 方法。</para>
    /// <para lang="en">Register synchronously inside OnValidationRequested. Older frameworks share event arguments, so do not call another context's synchronous Validate method inside the handler.</para>
    /// </remarks>
    /// <exception cref="InvalidOperationException"><para lang="zh">当前不处于异步验证注册阶段</para><para lang="en">There is no current async validation registration scope</para></exception>
    public static void AddAsyncValidator(this ValidationRequestedEventArgs args, Func<CancellationToken, Task> validator)
    {
        EditContextExtensions.AddAsyncValidator(validator);
    }
}
#endif
