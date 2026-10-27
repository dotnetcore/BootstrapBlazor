// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Security.Claims;

namespace UnitTest.Services;

public class ConnectionHubTest
{
    [Fact]
    public void ConnectionItem_JsonRoundTrip()
    {
        var item = new ConnectionItem
        {
            Id = "connection_id",
            ClientInfo = new ClientInfo { Id = "connection_id", Ip = "::1" },
            ConnectionTime = new DateTimeOffset(2026, 10, 27, 10, 0, 0, TimeSpan.FromHours(8)),
            LastBeatTime = new DateTimeOffset(2026, 10, 27, 10, 1, 0, TimeSpan.FromHours(8))
        };

        var json = System.Text.Json.JsonSerializer.Serialize(item);
        var result = System.Text.Json.JsonSerializer.Deserialize<ConnectionItem>(json);

        Assert.NotNull(result);
        Assert.Equal(item.Id, result.Id);
        Assert.Equal(item.ClientInfo?.Id, result.ClientInfo?.Id);
        Assert.Equal(item.ClientInfo?.Ip, result.ClientInfo?.Ip);
        Assert.Equal(item.ConnectionTime, result.ConnectionTime);
        Assert.Equal(item.LastBeatTime, result.LastBeatTime);
    }

    [Fact]
    public async Task Callback_Ok()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddBootstrapBlazor();

        var authorizationContext = new MockAuthenticationStateProvider();
        context.Services.AddScoped<AuthenticationStateProvider>(p => authorizationContext);

        var options = context.Services.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        options.Value.ConnectionHubOptions = new()
        {
            Enable = true,
            EnableIpLocator = true,
            TimeoutInterval = TimeSpan.FromMilliseconds(1000),
            BeatInterval = TimeSpan.FromMilliseconds(200)
        };

        var service = context.Services.GetRequiredService<IConnectionService>();
        var cut = context.Render<ConnectionHub>();
        await cut.InvokeAsync(async () =>
        {
            await cut.Instance.Callback(new ClientInfo { Id = "test_id", Ip = "::1" });
        });
        Assert.Equal(1, service.Count);

        await service.AddOrUpdateAsync(new ClientInfo() { Id = "test_id" }, TestContext.Current.CancellationToken);
        Assert.Equal(1, service.Count);

        await service.AddOrUpdateAsync(new ClientInfo() { Id = "test_id" }, TestContext.Current.CancellationToken);
        Assert.Equal(1, service.Count);

        var throttleFactory = context.Services.GetRequiredService<IThrottleDispatcherFactory>();
        throttleFactory.Clear("test_id");
        authorizationContext.SetAuthorized("mock_user");
        await cut.InvokeAsync(async () =>
        {
            await cut.Instance.Callback(new ClientInfo { Id = "test_id", Ip = "::1" });
        });
        Assert.True(service.TryGetValue("test_id", out var item));
        Assert.NotNull(item?.ClientInfo);
        Assert.Equal("mock_user", item.ClientInfo.UserName);

        Assert.True(item?.ConnectionTime < DateTimeOffset.Now);
        cut.Dispose();

        options.Value.ConnectionHubOptions.Enable = false;
        authorizationContext.SetAnonymous();
        throttleFactory.Clear("test_id");
        cut = context.Render<ConnectionHub>();
        await cut.InvokeAsync(async () =>
        {
            await cut.Instance.Callback(new ClientInfo { Id = "test_id", Ip = "::1" });
        });
        Assert.True(service.TryGetValue("test_id", out item));
        Assert.Null(item.ClientInfo?.UserName);

        // 设置 EnableIpLocator 为 false
        options.Value.ConnectionHubOptions.Enable = true;
        options.Value.ConnectionHubOptions.EnableIpLocator = false;
        throttleFactory.Clear("test_id");
        cut = context.Render<ConnectionHub>();
        await cut.InvokeAsync(async () =>
        {
            await cut.Instance.Callback(new ClientInfo { Id = "test_id", Ip = "::1" });
        });
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Callback_AwaitsAddOrUpdateAsync(bool useAuthenticationStateProvider)
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddBootstrapBlazor();

        var service = new MockConnectionService();
        context.Services.AddSingleton<IConnectionService>(service);
        context.Services.RemoveAll<AuthenticationStateProvider>();
        if (useAuthenticationStateProvider)
        {
            context.Services.AddScoped<AuthenticationStateProvider>(_ => new MockAuthenticationStateProvider());
        }
        var options = context.Services.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        options.Value.ConnectionHubOptions.Enable = true;

        var cut = context.Render<ConnectionHub>();
        var client = new ClientInfo() { Id = "test_async", UserName = "untrusted_user" };
        var callback = cut.InvokeAsync(() => cut.Instance.Callback(client));
        try
        {
            var received = await service.Received.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            Assert.Same(client, received);
            Assert.Null(received.UserName);
            Assert.Equal("http://localhost/", received.RequestUrl);
            Assert.False(callback.IsCompleted);
        }
        finally
        {
            service.Completion.TrySetResult();
        }
        await callback;
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Callback_EmptyId(string? id)
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddBootstrapBlazor();

        var service = context.Services.GetRequiredService<IConnectionService>();
        var cut = context.Render<ConnectionHub>();
        await cut.InvokeAsync(() => cut.Instance.Callback(new ClientInfo() { Id = id }));

        Assert.Empty(service.Connections);
    }

    [Fact]
    public async Task Callback_AddOrUpdateAsyncError()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddBootstrapBlazor();
        context.Services.AddScoped<AuthenticationStateProvider>(_ => new MockAuthenticationStateProvider());

        var logger = new RecordingLoggerProvider();
        context.Services.AddLogging(builder => builder.AddProvider(logger));
        var service = new MockConnectionService();
        context.Services.AddSingleton<IConnectionService>(service);
        var exception = new InvalidOperationException("Heartbeat update failed");
        service.Completion.SetException(exception);

        var cut = context.Render<ConnectionHub>();
        var client = new ClientInfo() { Id = "test_error" };
        await cut.InvokeAsync(() => cut.Instance.Callback(client));

        Assert.Same(client, await service.Received.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken));
        var entry = await logger.Logged.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
        Assert.Equal(LogLevel.Error, entry.Level);
        Assert.Same(exception, entry.Exception);
        Assert.Equal("Unable to process the connection heartbeat.", entry.Message);
    }

    [Fact]
    public async Task Callback_InvalidClient()
    {
        using var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddBootstrapBlazor();

        var logger = new RecordingLoggerProvider();
        context.Services.AddLogging(builder => builder.AddProvider(logger));
        var cut = context.Render<ConnectionHub>();
        await cut.InvokeAsync(() => cut.Instance.Callback(null!));

        var entry = await logger.Logged.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
        Assert.Equal(LogLevel.Error, entry.Level);
        Assert.IsType<NullReferenceException>(entry.Exception);
        Assert.Equal("Unable to process the connection heartbeat.", entry.Message);
    }

    [Fact]
    public async Task ExpirationScanFrequency_Ok()
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();

        using var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        options.Value.ConnectionHubOptions = new()
        {
            Enable = true,
            ExpirationScanFrequency = TimeSpan.FromMilliseconds(20),
            TimeoutInterval = TimeSpan.FromMilliseconds(200),
            BeatInterval = TimeSpan.FromMilliseconds(100)
        };

        var service = provider.GetRequiredService<IConnectionService>();
        await service.AddOrUpdateAsync(new ClientInfo() { Id = "test_id" }, TestContext.Current.CancellationToken);
        Assert.Equal(1, service.Count);
        Assert.Single(service.Connections);

        var deadline = DateTime.UtcNow.AddSeconds(5);
        while (service.Connections.Count > 0 && DateTime.UtcNow < deadline)
        {
            await Task.Delay(20, TestContext.Current.CancellationToken);
        }
        Assert.Equal(0, service.Count);
        Assert.Empty(service.Connections);
        Assert.False(service.TryGetValue("test_id", out var item));
        Assert.Null(item);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task ExpirationScanFrequency_Cancel(bool cancelBeforeDispose)
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();

        using var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        options.Value.ConnectionHubOptions.Enable = true;

        var service = provider.GetRequiredService<IConnectionService>();
        var source = GetCancellationTokenSource(service);
        var token = source.Token;
        Assert.False(token.IsCancellationRequested);

        if (cancelBeforeDispose)
        {
            source.Cancel();
        }
        provider.Dispose();
        Assert.True(token.IsCancellationRequested);
        await Task.Delay(50, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task ExpirationScanFrequency_Error()
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();
        var logger = new RecordingLoggerProvider();
        services.AddLogging(builder => builder.AddProvider(logger));

        using var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        options.Value.ConnectionHubOptions = new()
        {
            Enable = true,
            ExpirationScanFrequency = TimeSpan.FromMilliseconds(20),
            TimeoutInterval = TimeSpan.MaxValue
        };
        var service = provider.GetRequiredService<IConnectionService>();
        await service.AddOrUpdateAsync(new ClientInfo() { Id = "test_id" }, TestContext.Current.CancellationToken);

        var entry = await logger.Logged.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
        Assert.Equal(LogLevel.Error, entry.Level);
        Assert.IsType<ArgumentOutOfRangeException>(entry.Exception);
        Assert.Equal("Unable to remove expired connections.", entry.Message);

        // 异常后清理任务继续运行
        options.Value.ConnectionHubOptions.TimeoutInterval = TimeSpan.Zero;
        var deadline = DateTime.UtcNow.AddSeconds(5);
        while (service.Connections.Count > 0 && DateTime.UtcNow < deadline)
        {
            await Task.Delay(20, TestContext.Current.CancellationToken);
        }
        Assert.Empty(service.Connections);
    }

    [Fact]
    public void ConnectionService_DisposeFalse()
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();

        using var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<IConnectionService>();
        var source = GetCancellationTokenSource(service);
        var dispose = service.GetType().GetMethod("Dispose", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(dispose);

        dispose.Invoke(service, [false]);

        Assert.False(source.IsCancellationRequested);
        Assert.False(source.Token.IsCancellationRequested);
    }

    [Fact]
    public async Task ConnectionHubOptions_Ok()
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();

        using var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<IConnectionService>();
        await service.AddOrUpdateAsync(new ClientInfo() { Id = "test_id" }, TestContext.Current.CancellationToken);
        Assert.Equal(1, service.Count);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task AddOrUpdateAsync_EmptyId(string? id)
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();

        using var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<IConnectionService>();
        await service.AddOrUpdateAsync(new ClientInfo() { Id = id }, TestContext.Current.CancellationToken);

        Assert.Equal(0, service.Count);
        Assert.Empty(service.Connections);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task AddOrUpdateAsync_Cancel(bool existingConnection)
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();

        using var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<IConnectionService>();
        var client = new ClientInfo() { Id = "test_id", Ip = "::1" };
        if (existingConnection)
        {
            await service.AddOrUpdateAsync(client, TestContext.Current.CancellationToken);
        }

        service.TryGetValue("test_id", out var original);
        var lastBeatTime = original?.LastBeatTime;
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();

        var exception = await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            service.AddOrUpdateAsync(new ClientInfo() { Id = "test_id", Ip = "127.0.0.1" }, cancellation.Token));
        Assert.Equal(cancellation.Token, exception.CancellationToken);
        Assert.Equal(existingConnection, service.TryGetValue("test_id", out var item));
        if (existingConnection)
        {
            Assert.NotNull(item);
            Assert.Same(client, item.ClientInfo);
            Assert.Equal(lastBeatTime, item.LastBeatTime);
        }
        else
        {
            Assert.Null(item);
            Assert.Empty(service.Connections);
        }
    }

    [Fact]
    public async Task Count_ExpiredConnection()
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();

        using var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        var service = provider.GetRequiredService<IConnectionService>();
        await service.AddOrUpdateAsync(new ClientInfo() { Id = "test_id" }, TestContext.Current.CancellationToken);
        Assert.Equal(1, service.Count);

        options.Value.ConnectionHubOptions.TimeoutInterval = TimeSpan.Zero;

        Assert.Equal(0, service.Count);
        Assert.True(service.TryGetValue("test_id", out var item));
        Assert.Same(item, Assert.Single(service.Connections));
    }

    private static CancellationTokenSource GetCancellationTokenSource(IConnectionService service)
    {
        var field = service.GetType().GetField("_cancellationTokenSource", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(field);
        return Assert.IsType<CancellationTokenSource>(field.GetValue(service));
    }

    class RecordingLoggerProvider : ILoggerProvider
    {
        public TaskCompletionSource<(LogLevel Level, Exception? Exception, string Message)> Logged { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public ILogger CreateLogger(string categoryName) => new RecordingLogger(this);

        public void Dispose() { }

        private sealed class RecordingLogger(RecordingLoggerProvider provider) : ILogger
        {
            public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            {
                if (logLevel == LogLevel.Error)
                {
                    provider.Logged.TrySetResult((logLevel, exception, formatter(state, exception)));
                }
            }
        }
    }

    class MockConnectionService : IConnectionService
    {
        public TaskCompletionSource<ClientInfo> Received { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public TaskCompletionSource Completion { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public long Count => 0;

        public ICollection<ConnectionItem> Connections => Array.Empty<ConnectionItem>();

        public Task AddOrUpdateAsync(ClientInfo client, CancellationToken cancellationToken = default)
        {
            Received.TrySetResult(client);
            return Completion.Task;
        }

        public bool TryGetValue(string key, [NotNullWhen(true)] out ConnectionItem? value)
        {
            value = null;
            return false;
        }
    }

    class MockAuthenticationStateProvider : AuthenticationStateProvider
    {
        ClaimsPrincipal _claim = new();
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Yield();

            var state = new AuthenticationState(_claim);
            return state;
        }

        public void SetAuthorized(string userName)
        {
            _claim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, userName),
            }, "mock"));
        }

        public void SetAnonymous() => _claim = new ClaimsPrincipal(new ClaimsIdentity());
    }
}
