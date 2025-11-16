// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
