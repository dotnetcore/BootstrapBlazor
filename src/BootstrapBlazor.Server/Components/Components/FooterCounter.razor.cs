// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// FooterCounter 组件
/// </summary>
public partial class FooterCounter
{
    private string? Runtime { get; set; }

    private readonly CancellationTokenSource _disposeTokenSource = new();

    private ConnectionHubOptions _options = default!;

    [Inject]
    [NotNull]
    private IOptions<BootstrapBlazorOptions>? BootstrapBlazorOptions { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _options = BootstrapBlazorOptions.Value.ConnectionHubOptions;

        var ts = DateTimeOffset.Now - Cache.GetStartTime();
        Runtime = ts.ToString("dd\\.hh\\:mm\\:ss");
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            try
            {
                while (_disposeTokenSource is { IsCancellationRequested: false })
                {
                    await Task.Delay(30000, _disposeTokenSource.Token);
                    var ts = DateTimeOffset.Now - Cache.GetStartTime();
                    await InvokeVoidAsync("updateFooterCounter", Id, ts.TotalSeconds);
                }
            }
            catch { }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync()
    {
        var ts = DateTimeOffset.Now - Cache.GetStartTime();
        return InvokeVoidAsync("init", Id, ts.TotalSeconds);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(true);

        if (disposing)
        {
            _disposeTokenSource.Cancel();
            _disposeTokenSource.Dispose();
        }
    }
}
