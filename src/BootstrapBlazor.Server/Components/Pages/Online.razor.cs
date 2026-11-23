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
    private DateTimeOffset _refreshedAt;
    private TimeSpan _timeoutInterval;
    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _refreshTask;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        BuildContext(DateTimeOffset.Now);
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
                    BuildContext(DateTimeOffset.Now);
                    StateHasChanged();
                });
            }
        }
        catch (OperationCanceledException) { }
    }

    private void BuildContext(DateTimeOffset now)
    {
        _refreshedAt = now;
        _timeoutInterval = Options.Value.ConnectionHubOptions.TimeoutInterval;
        _items.Clear();
        _items.AddRange(ConnectionService.Connections.Where(item => now - item.LastBeatTime < _timeoutInterval));
        _items.Sort(ConnectionComparer);
    }

    private string GetTimeUntilRemoval(ConnectionItem item)
    {
        var elapsed = _refreshedAt - item.LastBeatTime;
        if (elapsed < TimeSpan.Zero)
        {
            elapsed = TimeSpan.Zero;
        }

        var remaining = elapsed < _timeoutInterval ? _timeoutInterval - elapsed : TimeSpan.Zero;
        return remaining.ToString(remaining.Days > 0 ? @"d\.hh\:mm\:ss" : @"hh\:mm\:ss");
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
