// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace UnitTest.Core;

public class SpeechTestBase : BootstrapBlazorTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor();
        services.TryAddScoped<RecognizerService>();
        services.TryAddScoped<IRecognizerProvider, MockRecognizerProvider>();
        services.TryAddScoped<SynthesizerService>();
        services.TryAddScoped<ISynthesizerProvider, MockSynthesizerProvider>();
    }

    class MockRecognizerProvider : IRecognizerProvider
    {
        public async Task InvokeAsync(RecognizerOption option)
        {
            var method = option.MethodName;
            var language = option.TargetLanguage;
            var recognitionLanguage = option.SpeechRecognitionLanguage;
            if (option.Callback != null)
            {
                await option.Callback(RecognizerStatus.Start, "MockSpeechProvider");
            }
        }
    }

    class MockSynthesizerProvider : ISynthesizerProvider
    {
        public async Task InvokeAsync(SynthesizerOption option)
        {
            var method = option.MethodName;
            var language = option.SpeechSynthesisLanguage;
            var recognitionLanguage = option.SpeechSynthesisVoiceName;
            var text = option.Text;
            if (option.Callback != null)
            {
                await option.Callback(SynthesizerStatus.Finished);
            }
        }
    }
}
