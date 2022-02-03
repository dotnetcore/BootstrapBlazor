// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// JSRuntime 扩展操作类
/// </summary>
internal static class JSRuntimeExtensions
{
    /// <summary>
    /// 调用 JSInvoke 方法
    /// </summary>
    /// <param name="jsRuntime">IJSRuntime 实例</param>
    /// <param name="el">Element 实例或者组件 Id</param>
    /// <param name="func">Javascript 方法</param>
    /// <param name="args">Javascript 参数</param>
    public static async ValueTask InvokeVoidAsync(this IJSRuntime jsRuntime, object? el = null, string? func = null, params object[] args)
    {
        var paras = new List<object>();
        if (el != null)
        {
            paras.Add(el);
        }

        if (args != null)
        {
            paras.AddRange(args);
        }

        try
        {
            await jsRuntime.InvokeVoidAsync($"$.{func}", paras.ToArray()).ConfigureAwait(false);
        }
#if NET6_0_OR_GREATER
        catch (JSDisconnectedException) { }
#endif
        catch (AggregateException) { }
        catch (InvalidOperationException) { }
        catch (TaskCanceledException) { }
    }

    /// <summary>
    /// 调用 JSInvoke 方法
    /// </summary>
    /// <param name="jsRuntime">IJSRuntime 实例</param>
    /// <param name="el">Element 实例或者组件 Id</param>
    /// <param name="func">Javascript 方法</param>
    /// <param name="args">Javascript 参数</param>
    public static async ValueTask<TValue?> InvokeAsync<TValue>(this IJSRuntime jsRuntime, object? el = null, string? func = null, params object[] args)
    {
        TValue? ret = default;
        var paras = new List<object>();
        if (el != null)
        {
            paras.Add(el);
        }

        if (args != null)
        {
            paras.AddRange(args);
        }

        try
        {
            ret = await jsRuntime.InvokeAsync<TValue>($"$.{func}", paras.ToArray()).ConfigureAwait(false);
        }
#if NET6_0_OR_GREATER
        catch (JSDisconnectedException) { }
#endif
        catch (AggregateException) { }
        catch (InvalidOperationException) { }
        catch (TaskCanceledException) { }
        return ret;
    }
}
