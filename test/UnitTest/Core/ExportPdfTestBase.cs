// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace UnitTest.Core;

[Collection("ExportPdfTestContext")]
public class ExportPdfTestBase
{
    protected TestContext Context { get; }

    public ExportPdfTestBase()
    {
        Context = ExportPdfTestHost.Instance;
    }
}

[CollectionDefinition("ExportPdfTestContext")]
public class ExportPdfTestCollection : ICollectionFixture<ExportPdfTestHost>
{

}

public class ExportPdfTestHost : IDisposable
{
    [NotNull]
    internal static TestContext? Instance { get; private set; }

    public ExportPdfTestHost()
    {
        Instance = new TestContext();

        // Mock 脚本
        Instance.JSInterop.Mode = JSRuntimeMode.Loose;

        ConfigureServices(Instance.Services);

        ConfigureConfiguration(Instance.Services);

        // 渲染 SwalRoot 组件 激活 ICacheManager 接口
        Instance.Services.GetRequiredService<ICacheManager>();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor();
        services.AddSingleton<IHtml2Pdf, MockHtml2PdfService>();
    }

    protected virtual void ConfigureConfiguration(IServiceCollection services)
    {
        // 增加单元测试 appsettings.json 配置文件
        services.AddConfiguration();
    }

    public void Dispose()
    {
        Instance.Dispose();
        GC.SuppressFinalize(this);
    }
}

class MockHtml2PdfService : IHtml2Pdf
{
    public Task<byte[]> PdfDataAsync(string url)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> PdfDataFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> PdfStreamAsync(string url)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> PdfStreamFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null) => Task.FromResult<Stream>(new MemoryStream(Encoding.UTF8.GetBytes("Hello World")));
}
