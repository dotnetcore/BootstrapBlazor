// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace UnitTest.Localization;

public class JsonStringLocalizerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void GetString_Ok()
    {
        var factory = Context.Services.GetRequiredService<IStringLocalizerFactory>();
        var mi = factory.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(i => i.Name == "GetResourcePrefix" && i.GetParameters().Length == 1)!;
        Assert.Throws<TargetInvocationException>(() => mi.Invoke(factory, new object?[] { new MockTypeInfo() }));
    }

    [Fact]
    public void GetString_Format()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var v = localizer["Foo.Name", "Test"];
        Assert.Equal("张三 Test", v);

        v = localizer["Test", "Test"];
        Assert.Equal("Test", v);
    }

    [Fact]
    public void GetString_FromInject()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddSingleton<IStringLocalizerFactory, MockLocalizerFactory>();
        sc.AddTransient<IStringLocalizer, MockStringLocalizer>();
        sc.AddBootstrapBlazor();

        var provider = sc.BuildServiceProvider();
        var cache = provider.GetRequiredService<ICacheManager>();
        cache.SetStartTime();

        var localizer = provider.GetRequiredService<IStringLocalizer<Dummy>>();
        var v = localizer["Mock-Name"];
        Assert.Equal("Mock-Test-Name", v);

        v = localizer["Mock-Test"];
        Assert.True(v.ResourceNotFound);
        Assert.Equal("Mock-Test", v);
    }

    [Fact]
    public void GetString_ResourceManager_Before()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();

        // 本地化服务先注册
        sc.AddSingleton<IStringLocalizerFactory, MockLocalizerFactory>();
        sc.AddBootstrapBlazor(null, options =>
        {
            options.ResourceManagerStringLocalizerType = this.GetType();
        });

        var provider = sc.BuildServiceProvider();
        var cache = provider.GetRequiredService<ICacheManager>();
        cache.SetStartTime();

        var localizer = provider.GetRequiredService<IStringLocalizer>();

        Assert.Equal("Mock-Test-Name", localizer["Mock-Name"]);
        Assert.Equal("Mock-Test-Address-Test", localizer["Mock-Address", "Test"]);
    }

    [Fact]
    public void GetString_ResourceManager_After()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddBootstrapBlazor(null, options =>
        {
            options.ResourceManagerStringLocalizerType = this.GetType();
        });

        // 本地化服务后注册
        sc.AddSingleton<IStringLocalizerFactory, MockLocalizerFactory>();

        var provider = sc.BuildServiceProvider();
        var cache = provider.GetRequiredService<ICacheManager>();
        cache.SetStartTime();

        var localizer = provider.GetRequiredService<IStringLocalizer>();

        Assert.Equal("Mock-Test-Name", localizer["Mock-Name"]);
        Assert.Equal("Mock-Test-Address-Test", localizer["Mock-Address", "Test"]);
    }

    [Fact]
    public void GetString_ResourceManager_Null()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddSingleton<IStringLocalizerFactory, MockLocalizerFactory>();

        // 未设置 ResourceManagerStringLocalizerType 类型
        sc.AddBootstrapBlazor();

        var provider = sc.BuildServiceProvider();
        var cache = provider.GetRequiredService<ICacheManager>();
        cache.SetStartTime();

        Assert.Throws<InvalidOperationException>(() => provider.GetRequiredService<IStringLocalizer>());
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GetAllStrings_Ok(bool include)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = localizer.GetAllStrings(include);
        Assert.NotEmpty(items);
    }

    [Fact]
    public void GetAllStrings_FromInject()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddSingleton<IStringLocalizerFactory, MockLocalizerFactory>();
        sc.AddBootstrapBlazor();

        var provider = sc.BuildServiceProvider();
        var cache = provider.GetRequiredService<ICacheManager>();
        cache.SetStartTime();

        var localizer = provider.GetRequiredService<IStringLocalizer<Dummy>>();
        var items = localizer.GetAllStrings(false);
        Assert.NotEmpty(items);
    }

    [Fact]
    public void GetAllStrings_FromType()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddSingleton<IStringLocalizerFactory, MockLocalizerFactory>();
        sc.AddBootstrapBlazor(null, options =>
        {
            options.ResourceManagerStringLocalizerType = this.GetType();
        });

        var provider = sc.BuildServiceProvider();
        var cache = provider.GetRequiredService<ICacheManager>();
        cache.SetStartTime();

        var localizer = provider.GetRequiredService<IStringLocalizer>();

        var items = localizer.GetAllStrings(false);
        Assert.NotEmpty(items);
    }

    [Fact]
    public void GetAllStrings_FromBase()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddBootstrapBlazor(null, options =>
        {
            options.ResourceManagerStringLocalizerType = this.GetType();
        });

        var provider = sc.BuildServiceProvider();
        var cache = provider.GetRequiredService<ICacheManager>();
        cache.SetStartTime();

        var localizer = provider.GetRequiredService<IStringLocalizer>();

        var items = localizer.GetAllStrings(false);
        Assert.Empty(items);
    }

    [Fact]
    public void GetAllStrings_FromResource()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddBootstrapBlazor(null, options =>
        {
            options.ResourceManagerStringLocalizerType = typeof(Dummy);
        });

        var provider = sc.BuildServiceProvider();
        var cache = provider.GetRequiredService<ICacheManager>();
        cache.SetStartTime();

        var localizer = provider.GetRequiredService<IStringLocalizer>();
        var items = localizer.GetAllStrings(false);
        Assert.NotEmpty(items);
        Assert.Equal("test-name", items.First(i => i.Name == "Name").Value);
    }

    [Fact]
    public void GetAllStrings_FromJson()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddBootstrapBlazor();

        var provider = sc.BuildServiceProvider();
        var cache = provider.GetRequiredService<ICacheManager>();
        cache.SetStartTime();

        var localizer = provider.GetRequiredService<IStringLocalizer<Foo>>();

        var items = localizer.GetAllStrings(false);
        Assert.Equal("姓名", items.First(i => i.Name == "Name").Value);
    }

    private class MockTypeInfo : TypeDelegator
    {
        public override string? FullName => null;
    }

    private class MockLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource) => new MockStringLocalizer();

        public IStringLocalizer Create(string baseName, string location) => new MockStringLocalizer();
    }

    private class MockStringLocalizer : IStringLocalizer
    {
        public LocalizedString this[string name] => GetAllStrings(true).FirstOrDefault(l => l.Name == name) ?? new LocalizedString(name, name, true);

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = this[name];
                return new LocalizedString(name, string.Format(format.Value, arguments));
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => new List<LocalizedString>()
        {
            new LocalizedString("Mock-Name", "Mock-Test-Name"),
            new LocalizedString("Mock-Address", "Mock-Test-Address-{0}")
        };
    }

    private class Dummy
    {
        public string? DummyName { get; set; }
    }
}
