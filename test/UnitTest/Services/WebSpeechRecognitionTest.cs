// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class WebSpeechRecognitionTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task StartAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var recognition = await service.CreateRecognitionAsync();
        await recognition.StartAsync("zh-CN");
        WebSpeechRecognitionEvent? result = null;
        recognition.OnResultAsync = @event =>
        {
            result = @event;
            return Task.CompletedTask;
        };
        await recognition.TriggerResultCallback(new WebSpeechRecognitionEvent()
        {
            IsFinal = true,
            Transcript = "test"
        });
        Assert.NotNull(result);
        Assert.True(result.IsFinal);
        Assert.Equal("test", result.Transcript);
    }

    [Fact]
    public async Task OnErrorAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var recognition = await service.CreateRecognitionAsync();
        await recognition.StartAsync("zh-CN");
        WebSpeechRecognitionError? error = null;
        recognition.OnErrorAsync = err =>
        {
            error = err;
            return Task.CompletedTask;
        };
        Assert.Null(error);
        await recognition.TriggerErrorCallback(new WebSpeechRecognitionError
        {
            Error = "no-speech",
            Message = "test"
        });
        Assert.NotNull(error);
        Assert.Equal("no-speech", error.Error);
        Assert.Equal("test", error.Message);
    }

    [Fact]
    public async Task StopAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var recognition = await service.CreateRecognitionAsync();
        await recognition.StopAsync();
    }

    [Fact]
    public async Task AbortAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var recognition = await service.CreateRecognitionAsync();
        await recognition.AbortAsync();
    }

    [Fact]
    public async Task OnEndAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var recognition = await service.CreateRecognitionAsync();
        var end = false;
        recognition.OnEndAsync = () =>
        {
            end = true;
            return Task.CompletedTask;
        };
        Assert.False(end);
        await recognition.TriggerEndCallback();
        Assert.True(end);
    }

    [Fact]
    public async Task OnNoMatchAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var recognition = await service.CreateRecognitionAsync();
        WebSpeechRecognitionError? error = null;
        recognition.OnNoMatchAsync = err =>
        {
            error = err;
            return Task.CompletedTask;
        };
        Assert.Null(error);
        await recognition.TriggerNoMatchCallback(new WebSpeechRecognitionError()
        {
            Error = "no-match",
            Message = "test"
        });
        Assert.NotNull(error);
    }

    [Fact]
    public async Task OnStartAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var recognition = await service.CreateRecognitionAsync();
        bool start = false;
        recognition.OnStartAsync = () =>
        {
            start = true;
            return Task.CompletedTask;
        };
        await recognition.TriggerStartCallback();
        Assert.True(start);
    }

    [Fact]
    public async Task OnSpeechStartAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var recognition = await service.CreateRecognitionAsync();
        bool start = false;
        recognition.OnSpeechStartAsync = () =>
        {
            start = true;
            return Task.CompletedTask;
        };
        await recognition.TriggerSpeechStartCallback();
        Assert.True(start);
    }

    [Fact]
    public async Task OnSpeechEndAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var recognition = await service.CreateRecognitionAsync();
        bool end = false;
        recognition.OnSpeechEndAsync = () =>
        {
            end = true;
            return Task.CompletedTask;
        };
        await recognition.TriggerSpeechEndCallback();
        Assert.True(end);
    }

    [Fact]
    public void WebSpeechRecognitionOption_Ok()
    {
        var option = new WebSpeechRecognitionOption()
        {
            Lang = "zh-CN",
            Continuous = true,
            InterimResults = true,
            MaxAlternatives = 1
        };
        Assert.True(option.Continuous);
        Assert.True(option.InterimResults);
        Assert.Equal("zh-CN", option.Lang);
        Assert.Equal(1, option.MaxAlternatives);
    }
}
