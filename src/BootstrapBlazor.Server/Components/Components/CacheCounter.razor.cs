// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 缓存数量组件
/// </summary>
public partial class CacheCounter : IDisposable
{
    [Inject, NotNull]
    private ICacheManager? CacheManager { get; set; }

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        try
        {
            await Task.Delay(5000, _cancellationTokenSource.Token);
            StateHasChanged();
        }
        catch { }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        GC.SuppressFinalize(this);
    }
}
