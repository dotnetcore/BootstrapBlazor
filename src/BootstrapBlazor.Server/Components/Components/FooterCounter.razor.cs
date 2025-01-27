// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// FooterCounter 组件
/// </summary>
public partial class FooterCounter : IDisposable
{
    private string? Runtime { get; set; }

    private CancellationTokenSource _disposeTokenSource = new();

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
        UpdateRuntime();
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        try
        {
            await Task.Delay(1000, _disposeTokenSource.Token);
            UpdateRuntime();
            StateHasChanged();
        }
        catch { }
    }

    private void UpdateRuntime()
    {
        var ts = DateTimeOffset.Now - Cache.GetStartTime();
        Runtime = ts.ToString("dd\\.hh\\:mm\\:ss");
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _disposeTokenSource.Cancel();
            _disposeTokenSource.Dispose();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
