// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Runtime.CompilerServices;

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
            pb.Add(a => a.ShowUsedTime, false);
        });
        await Task.Delay(1200);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Show, false);
        });
    }

    [Fact]
    public void Token_Ok()
    {
        var cut = Context.RenderComponent<SpeechWave>(pb =>
        {
            pb.Add(a => a.Show, true);
        });
        var token = GetToken(cut.Instance);
        var shown = GetShow(cut.Instance);
        Assert.True(shown);

        GetToken(cut.Instance) = null;
        shown = GetShow(cut.Instance);
        Assert.False(shown);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_token")]
    static extern ref CancellationTokenSource? GetToken(SpeechWave @this);

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_IsShow")]
    static extern bool GetShow(SpeechWave @this);
}
