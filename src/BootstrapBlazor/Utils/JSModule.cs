// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">模块加载器</para>
/// <para lang="en">Module loader</para>
/// </summary>
/// <param name="jSObjectReference"></param>
public class JSModule(IJSObjectReference? jSObjectReference) : IAsyncDisposable
{
    /// <summary>
    /// <para lang="zh">InvokeVoidAsync 方法</para>
    /// <para lang="en">InvokeVoidAsync method</para>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual ValueTask InvokeVoidAsync(string identifier, params object?[]? args) => InvokeVoidAsync(identifier, CancellationToken.None, args);

    /// <summary>
    /// <para lang="zh">InvokeVoidAsync 方法</para>
    /// <para lang="en">InvokeVoidAsync method</para>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="timeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual ValueTask InvokeVoidAsync(string identifier, TimeSpan timeout, params object?[]? args)
    {
        using CancellationTokenSource? cancellationTokenSource = ((timeout == Timeout.InfiniteTimeSpan) ? null : new CancellationTokenSource(timeout));
        CancellationToken cancellationToken = cancellationTokenSource?.Token ?? CancellationToken.None;
        return InvokeVoidAsync(identifier, cancellationToken, args);
    }

    /// <summary>
    /// <para lang="zh">InvokeVoidAsync 方法</para>
    /// <para lang="en">InvokeVoidAsync method</para>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual async ValueTask InvokeVoidAsync(string identifier, CancellationToken cancellationToken = default, params object?[]? args)
    {
        var paras = new List<object?>();
        if (args != null)
        {
            paras.AddRange(args);
        }
        try
        {
            if (jSObjectReference != null)
            {
                await jSObjectReference.InvokeVoidAsync(identifier, cancellationToken, [.. paras]);
            }
        }
        catch (JSException)
        {
#if DEBUG
            System.Console.WriteLine($"identifier: {identifier} args: {string.Join(" ", args!)}");
            throw;
#endif
        }
        catch (JSDisconnectedException) { }
        catch (OperationCanceledException) { }
        catch (ObjectDisposedException) { }
    }

    /// <summary>
    /// <para lang="zh">InvokeAsync 方法</para>
    /// <para lang="en">InvokeAsync method</para>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual ValueTask<TValue?> InvokeAsync<TValue>(string identifier, params object?[]? args) => InvokeAsync<TValue?>(identifier, CancellationToken.None, args);

    /// <summary>
    /// <para lang="zh">InvokeAsync 方法</para>
    /// <para lang="en">InvokeAsync method</para>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="timeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual ValueTask<TValue?> InvokeAsync<TValue>(string identifier, TimeSpan timeout, params object?[]? args)
    {
        using CancellationTokenSource? cancellationTokenSource = ((timeout == Timeout.InfiniteTimeSpan) ? null : new CancellationTokenSource(timeout));
        CancellationToken cancellationToken = cancellationTokenSource?.Token ?? CancellationToken.None;
        return InvokeAsync<TValue?>(identifier, cancellationToken, args);
    }

    /// <summary>
    /// <para lang="zh">InvokeAsync 方法</para>
    /// <para lang="en">InvokeAsync method</para>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual async ValueTask<TValue?> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken = default, params object?[]? args)
    {
        var paras = new List<object?>();
        if (args != null)
        {
            paras.AddRange(args!);
        }

        TValue? ret = default;
        try
        {
            if (jSObjectReference != null)
            {
                ret = await jSObjectReference.InvokeAsync<TValue?>(identifier, cancellationToken, [.. paras]);
            }
        }
        catch (JSException)
        {
#if DEBUG
            System.Console.WriteLine($"identifier: {identifier} args: {string.Join(" ", args!)}");
            throw;
#endif
        }
        catch (JSDisconnectedException) { }
        catch (OperationCanceledException) { }
        catch (ObjectDisposedException) { }

        return ret;
    }

    /// <summary>
    /// <para lang="zh">Dispose 方法</para>
    /// <para lang="en">Dispose method</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsyncCore(bool disposing)
    {
        if (disposing)
        {
            try
            {
                if (jSObjectReference != null)
                {
                    await jSObjectReference.DisposeAsync();
                }
            }
            catch { }
        }
    }

    /// <summary>
    /// <para lang="zh">Dispose 方法</para>
    /// <para lang="en">Dispose method</para>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore(true);
        GC.SuppressFinalize(this);
    }
}
