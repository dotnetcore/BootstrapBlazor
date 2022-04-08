// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace UnitTest.Core;

[Collection("SpeechTestContext")]
public class SpeechTestBase
{
    protected TestContext Context { get; }

    public SpeechTestBase()
    {
        Context = SpeechTestHost.Instance;
    }
}

[CollectionDefinition("SpeechTestContext")]
public class SpeechTestCollection : ICollectionFixture<SpeechTestHost>
{

}

public class SpeechTestHost : IDisposable
{
    [NotNull]
    internal static TestContext? Instance { get; private set; }

    public SpeechTestHost()
    {
        Instance = new TestContext();

        // Mock 脚本
        Instance.JSInterop.Mode = JSRuntimeMode.Loose;

        ConfigureServices(Instance.Services);

        ConfigureConfigration(Instance.Services);

        // 渲染 SpeechRoot 组件 激活 ICacheManager 接口
        Instance.Services.GetRequiredService<ICacheManager>();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor();
        services.TryAddScoped<RecognizerService>();
        services.TryAddScoped<IRecognizerProvider, MockRecognizerProvider>();
        services.TryAddScoped<SynthesizerService>();
        services.TryAddScoped<ISynthesizerProvider, MockSynthesizerProvider>();
        services.ConfigureIPLocatorOption(options =>
        {
            options.LocatorFactory = provider => new BaiDuIPLocator();
        });
        services.ConfigureJsonLocalizationOptions(op => op.AdditionalJsonAssemblies = new[] { typeof(Alert).Assembly });
    }

    protected virtual void ConfigureConfigration(IServiceCollection services)
    {
        // 增加单元测试 appsettings.json 配置文件
        services.AddConfiguration();
    }

    public void Dispose()
    {
        Instance.Dispose();
        GC.SuppressFinalize(this);
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
                await option.Callback("MockSpeechProvider");
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
                await option.Callback(SynthesizerStatus.Close);
            }
        }
    }
}
