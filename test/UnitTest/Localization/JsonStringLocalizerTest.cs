// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace UnitTest.Localization;

public class JsonStringLocalizerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void JsonLocalizationOptions_Ok()
    {
        var option = new JsonLocalizationOptions();
        Assert.Equal("en", option.FallbackCulture);
        Assert.Equal("Locales", option.ResourcesPath);
    }

    [Fact]
    public void LocalizedString_Ok()
    {
        var factory = Context.Services.GetRequiredService<IStringLocalizerFactory>();
        var mi = factory.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(i => i.Name == "GetResourcePrefix" && i.GetParameters().Length == 1)!;
        Assert.Throws<TargetInvocationException>(() => mi.Invoke(factory, new object?[] { new MockTypeInfo() }));
    }

    [Fact]
    public void LocalizerString_Format()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var v = localizer["Foo.Name", "Test"];
        Assert.Equal("张三 Test", v);

        v = localizer["Test", "Test"];
        Assert.Equal("Test", v);
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
        var localizer = provider.GetRequiredService<IStringLocalizer<Dummy>>();
        var items = localizer.GetAllStrings(false);
        Assert.NotEmpty(items);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GetAllStrings_Resource(bool include)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Dummy>>();
        var items = localizer.GetAllStrings(include);
        Assert.NotEmpty(items);
    }

    [Fact]
    public void GetStringFromInject_Ok()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddSingleton<IStringLocalizerFactory, MockLocalizerFactory>();
        sc.AddBootstrapBlazor();

        var provider = sc.BuildServiceProvider();
        var localizer = provider.GetRequiredService<IStringLocalizer<Dummy>>();
        var v = localizer["Name"];
        Assert.Equal("Test-Name", v);

        v = localizer["Test"];
        Assert.True(v.ResourceNotFound);
        Assert.Equal("Test", v);
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
        public LocalizedString this[string name] => name == "Name" ? new(name, "Test-Name") : new(name, "", true);

        public LocalizedString this[string name, params object[] arguments] => new("Name", string.Format("{0}", arguments));

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => new List<LocalizedString>()
        {
            new LocalizedString("Name", "Test-Name"),
            new LocalizedString("Address", "Test-Address")
        };
    }

    private class Dummy
    {
        public string? Name { get; set; }
    }
}
