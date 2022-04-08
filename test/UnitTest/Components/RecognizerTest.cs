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
        await recognizerService.InvokeAsync(new RecognizerOption()
        {
            MethodName = "Test",
            TargetLanguage = "zh-CN",
            SpeechRecognitionLanguage = "zh-CN",
            Callback = new Func<string, Task>(v =>
            {
                result = v;
                return Task.CompletedTask;
            })
        });

        Assert.Equal("MockSpeechProvider", result);
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
            pb.Add(a => a.TotalTimeSecond, 60);
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
            pb.Add(a => a.TotalTimeSecond, 1);
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
}
