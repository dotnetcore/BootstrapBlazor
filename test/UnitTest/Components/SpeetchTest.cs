// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;


public class SpeetchTest : SpeechTestBase
{
    [Fact]
    public async Task Speech_Ok()
    {
        var result = "";
        var speechService = Context.Services.GetRequiredService<SpeechService>();
        var cut = Context.RenderComponent<Speech>();
        await speechService.InvokeAsync(new SpeechOption()
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
}
