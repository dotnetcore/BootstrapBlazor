// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// Bridges existing server dispatches to read-only browser event streams.
/// </summary>
public sealed class SiteNotificationService(
    IDispatchService<GiteePostBody> commits,
    IDispatchService<bool> reboots,
    IDispatchService<MessageItem> messages,
    ILogger<SiteNotificationService> logger) : IHostedService
{
    private readonly Lock _gate = new();
    private readonly HashSet<Channel<SiteNotification>> _subscribers = [];
    private bool _isUpdating;

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        commits.Subscribe(OnCommit);
        reboots.Subscribe(OnReboot);
        messages.Subscribe(OnMessage);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        commits.UnSubscribe(OnCommit);
        reboots.UnSubscribe(OnReboot);
        messages.UnSubscribe(OnMessage);
        lock (_gate)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Writer.TryComplete();
            }
            _subscribers.Clear();
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Streams live notifications and the current update state until the browser disconnects.
    /// </summary>
    public async IAsyncEnumerable<SiteNotification> ListenAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var channel = Channel.CreateBounded<SiteNotification>(new BoundedChannelOptions(32)
        {
            FullMode = BoundedChannelFullMode.DropOldest,
            SingleReader = true
        }, _ => logger.LogWarning("Dropping an old site notification for a slow subscriber."));

        lock (_gate)
        {
            _subscribers.Add(channel);
            channel.Writer.TryWrite(new("reboot", IsUpdating: _isUpdating));
        }
        try
        {
            await foreach (var notification in channel.Reader.ReadAllAsync(cancellationToken))
            {
                yield return notification;
            }
        }
        finally
        {
            lock (_gate)
            {
                _subscribers.Remove(channel);
                channel.Writer.TryComplete();
            }
        }
    }

    private void Publish(SiteNotification notification)
    {
        lock (_gate)
        {
            if (notification.Kind == "reboot")
            {
                _isUpdating = notification.IsUpdating;
            }
            foreach (var subscriber in _subscribers)
            {
                subscriber.Writer.TryWrite(notification);
            }
        }
    }

    private Task OnMessage(DispatchEntry<MessageItem> entry)
    {
        if (entry.Entry != null)
        {
            Publish(new("message", entry.Entry.Message));
        }
        return Task.CompletedTask;
    }

    private Task OnReboot(DispatchEntry<bool> entry)
    {
        Publish(new("reboot", IsUpdating: entry.Entry));
        return Task.CompletedTask;
    }

    private Task OnCommit(DispatchEntry<GiteePostBody> entry)
    {
        if (entry.CanDispatch())
        {
            var payload = entry.Entry!;
            var commit = payload.HeadCommit ?? payload.Commits?.FirstOrDefault();
            var url = Uri.TryCreate(commit?.Url, UriKind.Absolute, out var uri)
                && (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp)
                ? uri.AbsoluteUri : null;
            Publish(new("commit", commit?.Message, Author: commit?.Author?.Name,
                Branch: payload.GetBranchName(), Url: url,
                Timestamp: commit?.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                TotalCount: payload.Commits?.Count ?? 1));
        }
        return Task.CompletedTask;
    }
}
