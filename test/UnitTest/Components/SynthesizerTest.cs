// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class SynthesizerTest : SpeechTestBase
{
    [Fact]
    public async Task Synthesizer_Ok()
    {
        var result = SynthesizerStatus.Error;
        var recognizerService = Context.Services.GetRequiredService<SynthesizerService>();
        await recognizerService.InvokeAsync(new SynthesizerOption()
        {
            Text = "Test",
            MethodName = "Test",
            SpeechSynthesisLanguage = "zh-CN",
            SpeechSynthesisVoiceName = "zh-CN-XiaoxiaoNeural",
            Callback = new Func<SynthesizerStatus, Task>(v =>
            {
                result = v;
                return Task.CompletedTask;
            })
        });

        Assert.Equal(SynthesizerStatus.Finished, result);
    }
}
