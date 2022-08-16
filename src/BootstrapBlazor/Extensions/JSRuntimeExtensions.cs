// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// JSRuntime 扩展操作类
/// </summary>
[ExcludeFromCodeCoverage]
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
        catch (JSException) { }
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
        catch (JSException) { }
        catch (AggregateException) { }
        catch (InvalidOperationException) { }
        catch (TaskCanceledException) { }
        return ret;
    }

    /// <summary>
    /// IJSRuntime 扩展方法 动态加载脚本 脚本目录为 modules
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="path"></param>
    /// <param name="relative">是否为相对路径 默认 true</param>
    /// <returns></returns>
    public static async Task<JSModule> LoadModule(this IJSRuntime jsRuntime, string path, bool relative = true)
    {
        var fileName = relative ? $"./_content/BootstrapBlazor/modules/{path}" : path;
        var jSObjectReference = await jsRuntime.InvokeAsync<IJSObjectReference>(identifier: "import", fileName);
        return new JSModule(jSObjectReference);
    }

    /// <summary>
    /// IJSRuntime 扩展方法 动态加载脚本 脚本目录为 modules
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="jsRuntime"></param>
    /// <param name="path"></param>
    /// <param name="value"></param>
    /// <param name="relative">是否为相对路径 默认 true</param>
    /// <returns></returns>
    public static async Task<JSModule<TValue>> LoadModule<TValue>(this IJSRuntime jsRuntime, string path, TValue value, bool relative = true) where TValue : class
    {
        var fileName = relative ? $"./_content/BootstrapBlazor/modules/{path}" : path;
        var jSObjectReference = await jsRuntime.InvokeAsync<IJSObjectReference>(identifier: "import", fileName);
        return new JSModule<TValue>(jSObjectReference, value);
    }
}
