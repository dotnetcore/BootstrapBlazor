// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Server.Data;
using BootstrapBlazor.Server.Services;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;

namespace UnitTestDocs;

public class SiteNotificationTest
{
    [Theory]
    [InlineData("https://example.org/commit", "https://example.org/commit")]
    [InlineData("javascript:alert(1)", null)]
    public async Task Commit_ProjectsOnlyPublicData(string url, string? expectedUrl)
    {
        var commits = new DispatchService<GiteePostBody>();
        var service = CreateService(commits);
        await service.StartAsync(CancellationToken.None);
        try
        {
            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            await using var stream = service.ListenAsync(timeout.Token).GetAsyncEnumerator(TestContext.Current.CancellationToken);
            Assert.True(await stream.MoveNextAsync());

            commits.Dispatch(new()
            {
                Entry = new()
                {
                    Password = "private-password",
                    Sign = "private-signature",
                    Ref = "refs/heads/main",
                    Commits =
                    [
                        new()
                        {
                            Message = "public message",
                            Url = url,
                            Author = new() { Name = "public author", Email = "private@example.invalid" }
                        }
                    ]
                }
            });

            Assert.True(await stream.MoveNextAsync());
            Assert.Equal("commit", stream.Current.Kind);
            Assert.Equal("public message", stream.Current.Message);
            Assert.Equal("public author", stream.Current.Author);
            Assert.Equal("main", stream.Current.Branch);
            Assert.Equal(expectedUrl, stream.Current.Url);
            var json = JsonSerializer.Serialize(stream.Current);
            Assert.DoesNotContain("private-password", json);
            Assert.DoesNotContain("private-signature", json);
            Assert.DoesNotContain("private@example.invalid", json);
        }
        finally
        {
            await service.StopAsync(CancellationToken.None);
        }
        Assert.Equal(0, commits.SubscriberCount);
    }

    [Fact]
    public async Task Reconnect_ReceivesCurrentUpdateStateAndSubsequentEnd()
    {
        var reboots = new DispatchService<bool>();
        var service = CreateService(reboots: reboots);
        await service.StartAsync(CancellationToken.None);
        try
        {
            reboots.Dispatch(new() { Entry = true });
            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            await using var first = service.ListenAsync(timeout.Token).GetAsyncEnumerator(TestContext.Current.CancellationToken);
            await using var second = service.ListenAsync(timeout.Token).GetAsyncEnumerator(TestContext.Current.CancellationToken);
            Assert.True(await first.MoveNextAsync());
            Assert.True(first.Current.IsUpdating);
            Assert.True(await second.MoveNextAsync());
            Assert.True(second.Current.IsUpdating);

            reboots.Dispatch(new() { Entry = false });
            Assert.True(await first.MoveNextAsync());
            Assert.False(first.Current.IsUpdating);
            Assert.True(await second.MoveNextAsync());
            Assert.False(second.Current.IsUpdating);
        }
        finally
        {
            await service.StopAsync(CancellationToken.None);
        }
    }

    [Fact]
    public async Task Cancellation_ReleasesReaderWithoutAffectingOtherSubscribers()
    {
        var messages = new DispatchService<MessageItem>();
        var service = CreateService(messages: messages);
        await service.StartAsync(CancellationToken.None);
        try
        {
            using var cancellation = new CancellationTokenSource();
            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            await using var disconnected = service.ListenAsync(cancellation.Token).GetAsyncEnumerator(TestContext.Current.CancellationToken);
            await using var connected = service.ListenAsync(timeout.Token).GetAsyncEnumerator(TestContext.Current.CancellationToken);
            Assert.True(await disconnected.MoveNextAsync());
            Assert.True(await connected.MoveNextAsync());

            var pending = disconnected.MoveNextAsync().AsTask();
            cancellation.Cancel();
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => pending);

            messages.Dispatch(new() { Entry = new() { Message = "still connected" } });
            Assert.True(await connected.MoveNextAsync());
            Assert.Equal("still connected", connected.Current.Message);
        }
        finally
        {
            await service.StopAsync(CancellationToken.None);
        }
    }

    private static SiteNotificationService CreateService(
        DispatchService<GiteePostBody>? commits = null,
        DispatchService<bool>? reboots = null,
        DispatchService<MessageItem>? messages = null) =>
        new(commits ?? new(), reboots ?? new(), messages ?? new(), NullLogger<SiteNotificationService>.Instance);

    private sealed class DispatchService<T> : IDispatchService<T>
    {
        private readonly List<Func<DispatchEntry<T>, Task>> _callbacks = [];

        public int SubscriberCount => _callbacks.Count;

        public void Subscribe(Func<DispatchEntry<T>, Task> callback) => _callbacks.Add(callback);

        public void UnSubscribe(Func<DispatchEntry<T>, Task> callback) => _callbacks.Remove(callback);

        public void Dispatch(DispatchEntry<T> payload)
        {
            foreach (var callback in _callbacks)
            {
                callback(payload).GetAwaiter().GetResult();
            }
        }
    }
}
