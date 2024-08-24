// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Services;

public class WebSpeechServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task SpeakAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var synthesizer = await service.CreateSynthesizerAsync();
        await synthesizer.SpeakAsync("just a test voice", "en-US");
        await synthesizer.SpeakAsync("just a test voice", new WebSpeechSynthesisVoice()
        {
            Default = false,
            Lang = "en-US",
            LocalService = false,
            Name = "test",
            VoiceURI = "test"
        });

        var end = false;
        synthesizer.OnEndAsync = () =>
        {
            end = true;
            return Task.CompletedTask;
        };
        Assert.False(end);
        await synthesizer.TriggerEndCallback();
        Assert.True(end);
    }

    [Fact]
    public async Task OnErrorAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var synthesizer = await service.CreateSynthesizerAsync();
        WebSpeechSynthesisError? error = null;
        synthesizer.OnErrorAsync = err =>
        {
            error = err;
            return Task.CompletedTask;
        };
        Assert.Null(error);
        await synthesizer.TriggerErrorCallback(new WebSpeechSynthesisError
        {
            CharIndex = 10,
            ElapsedTime = 1144.1f,
            Error = "ErrorCode"
        });
        Assert.NotNull(error);
        Assert.Equal(10, error.CharIndex);
        Assert.Equal(1144.1f, error.ElapsedTime);
        Assert.Equal("ErrorCode", error.Error);

        await synthesizer.TriggerSpeakingCallback();
        Assert.Equal("speaking", error.Error);
    }

    [Fact]
    public async Task CancelAsync_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var synthesizer = await service.CreateSynthesizerAsync();
        await synthesizer.PauseAsync();
        await synthesizer.ResumeAsync();
        await synthesizer.CancelAsync();
    }

    [Fact]
    public async Task GetVoices_Ok()
    {
        var service = Context.Services.GetRequiredService<WebSpeechService>();
        var synthesizer = await service.CreateSynthesizerAsync();
        var voices = await synthesizer.GetVoices();
    }
}
