// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;


public class RecognizerTest : SpeechTestBase
{
    [Fact]
    public async Task Recognizer_Ok()
    {
        var result = "";
        var recognizerService = Context.Services.GetRequiredService<RecognizerService>();
        var option = new RecognizerOption()
        {
            MethodName = "Test",
            TargetLanguage = "zh-CN",
            SpeechRecognitionLanguage = "zh-CN",
            AutoRecoginzerElapsedMilliseconds = 5000,
            Callback = new Func<RecognizerStatus, string?, Task>((status, v) =>
            {
                result = v;
                return Task.CompletedTask;
            })
        };
        await recognizerService.InvokeAsync(option);

        Assert.Equal("MockSpeechProvider", result);
        Assert.Equal(5000, option.AutoRecoginzerElapsedMilliseconds);
    }

    [Fact]
    public void SpeechWave_Show_Test()
    {
        var cut = Context.RenderComponent<SpeechWave>(pb =>
        {
            pb.Add(a => a.Show, false);
        });
        cut.Contains("speech-wave invisible");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Show, true);
            pb.Add(a => a.TotalTime, 60000);
        });
        cut.Contains("speech-wave");
        cut.Contains("<span>01:00</span>");
    }

    [Fact]
    public void SpeechWave_ShowUsedTime_Test()
    {
        var cut = Context.RenderComponent<SpeechWave>(pb =>
        {
            pb.Add(a => a.ShowUsedTime, false);
        });
        cut.DoesNotContain("speech-wave-time");
    }

    [Fact]
    public async Task SpeechWave_OnTimeout_Test()
    {
        var timeout = false;
        var cut = Context.RenderComponent<SpeechWave>(pb =>
        {
            pb.Add(a => a.Show, true);
            pb.Add(a => a.TotalTime, 1);
            pb.Add(a => a.OnTimeout, new Func<Task>(() =>
            {
                timeout = true;
                return Task.CompletedTask;
            }));
        });

        await Task.Delay(1200);
        Assert.True(timeout);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Show, true);
        });
        await Task.Delay(500);
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Show, false);
        });
    }

    [Fact]
    public void IsCancelled_Ok()
    {
        var cut = Context.RenderComponent<SpeechWave>();
        var pi = cut.Instance.GetType().GetProperty("IsShow", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        var tokenPi = cut.Instance.GetType().GetProperty("Token", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        Assert.False((bool?)pi?.GetValue(cut.Instance));

        var token = new CancellationTokenSource();
        tokenPi?.SetValue(cut.Instance, token);
        Assert.True((bool?)pi?.GetValue(cut.Instance));

        token.Cancel();
        Assert.False((bool?)pi?.GetValue(cut.Instance));
    }
}
