// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Pages;

/// <summary>
/// 在线人数统计
/// </summary>
public partial class Online : IDisposable
{
    [Inject]
    [NotNull]
    private IConnectionService? ConnectionService { get; set; }

    [Inject]
    [NotNull]
    private IOptions<BootstrapBlazorOptions>? Options { get; set; }

    private readonly List<ConnectionItem> _items = [];
    private static readonly Comparison<ConnectionItem> ConnectionComparer = static (x, y) => x.ConnectionTime.CompareTo(y.ConnectionTime);
    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _refreshTask;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        BuildContext();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && _refreshTask == null)
        {
            _cancellationTokenSource ??= new CancellationTokenSource();
            _refreshTask = RefreshAsync(_cancellationTokenSource.Token);
        }
    }

    private async Task RefreshAsync(CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

        try
        {
            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                await InvokeAsync(() =>
                {
                    BuildContext();
                    StateHasChanged();
                });
            }
        }
        catch (OperationCanceledException) { }
    }

    private void BuildContext()
    {
        _items.Clear();
        var now = DateTimeOffset.Now;
        var timeout = Options.Value.ConnectionHubOptions.TimeoutInterval;
        _items.AddRange(ConnectionService.Connections.Where(item => item.LastBeatTime.Add(timeout) > now));
        _items.Sort(ConnectionComparer);
    }

    private static string GetDurString(ConnectionItem item)
    {
        var dur = item.LastBeatTime - item.ConnectionTime;
        return dur.ToString("dd\\.hh\\:mm\\:ss");
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
            _refreshTask = null;
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
