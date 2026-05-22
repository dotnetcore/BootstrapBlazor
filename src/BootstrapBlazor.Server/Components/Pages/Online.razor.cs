// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Pages;

/// <summary>
/// 在线人数统计
/// </summary>
public partial class Online : IDisposable
{
    [Inject]
    [NotNull]
    private IConnectionService? ConnectionService { get; set; }

    private readonly List<ConnectionItem> _items = [];
    private static readonly Comparison<ConnectionItem> ConnectionComparer = static (x, y) => x.ConnectionTime.CompareTo(y.ConnectionTime);
    private CancellationTokenSource? _cancellationTokenSource;

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

        if (firstRender)
        {
            _cancellationTokenSource ??= new CancellationTokenSource();
            using var _timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

            try
            {
                while (await _timer.WaitForNextTickAsync(_cancellationTokenSource.Token))
                {
                    BuildContext();
                    await InvokeAsync(StateHasChanged);
                }
            }
            catch (OperationCanceledException) { }
        }
    }

    private void BuildContext()
    {
        _items.Clear();
        _items.AddRange(ConnectionService.Connections);
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
