// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class WebSpeechSynthesizerTest : BootstrapBlazorTestBase
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
        await synthesizer.SpeakAsync(new WebSpeechSynthesisUtterance()
        {
            Text = "just a test voice",
            Lang = "en-US"
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
        await synthesizer.GetVoices();
    }

    [Fact]
    public void WebSpeechSynthesisUtterance_Ok()
    {
        var utterance = new WebSpeechSynthesisUtterance()
        {
            Text = "just a test voice",
            Lang = "en-US",
            Pitch = 1,
            Rate = 1,
            Voice = new WebSpeechSynthesisVoice() { VoiceURI = "test" },
            Volume = 1
        };
        Assert.Equal("just a test voice", utterance.Text);
        Assert.Equal("en-US", utterance.Lang);
        Assert.Equal(1, utterance.Pitch);
        Assert.Equal(1, utterance.Rate);
        Assert.Equal(1, utterance.Volume);
        Assert.Equal("test", utterance.Voice.VoiceURI);
    }

    [Fact]
    public void WebSpeechSynthesisVoice_Ok()
    {
        var voice = new WebSpeechSynthesisVoice()
        {
            Default = false,
            Lang = "en-US",
            LocalService = false,
            VoiceURI = "test",
            Name = "test"
        };
        Assert.Equal("test", voice.Name);
        Assert.Equal("en-US", voice.Lang);
        Assert.Equal("test", voice.VoiceURI);
        Assert.False(voice.LocalService);
        Assert.False(voice.Default);
    }
}
