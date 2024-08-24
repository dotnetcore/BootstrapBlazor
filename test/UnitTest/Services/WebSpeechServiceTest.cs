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
            ElapsedTime = "10:00:00",
            Error = "ErrorCode"
        });
        Assert.NotNull(error);
        Assert.Equal(10, error.CharIndex);
        Assert.Equal("10:00:00", error.ElapsedTime);
        Assert.Equal("ErrorCode", error.Error);
    }
}
