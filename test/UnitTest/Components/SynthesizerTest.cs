// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

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

        Assert.Equal(SynthesizerStatus.Close, result);
    }
}
