// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace UnitTest.Localization;

public class JsonStringLocalizerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void GetString_Ok()
    {
        var factory = Context.Services.GetRequiredService<IStringLocalizerFactory>();
        var mi = factory.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(i => i.Name == "GetResourcePrefix" && i.GetParameters().Length == 1)!;
        Assert.Throws<TargetInvocationException>(() => mi.Invoke(factory, [new MockTypeInfo()]));
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
    public void GetString_FromService()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddSingleton<IStringLocalizerFactory, MockLocalizerFactory>();
        sc.AddTransient<IStringLocalizer, MockStringLocalizer>();
        sc.AddBootstrapBlazor();

        var provider = sc.BuildServiceProvider();
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
    public void GetAllStrings_Dynamic()
    {
        var dynamicType = EmitHelper.CreateTypeByName("test_type", new InternalTableColumn[] { new("Name", typeof(string)) });
        Assert.NotNull(dynamicType);

        var factory = Context.Services.GetRequiredService<IStringLocalizerFactory>();
        var localizer = factory.Create(dynamicType);
        var items = localizer.GetAllStrings();
        Assert.Empty(items);
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
        var localizer = provider.GetRequiredService<IStringLocalizer>();

        var items = localizer.GetAllStrings(false);
        Assert.Empty(items);
    }

    [Fact]
    public void DisableGetLocalizerFromResourceManager_Ok()
    {
        var sc = new ServiceCollection();
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json");
        builder.AddInMemoryCollection(new Dictionary<string, string?>()
        {
            ["BootstrapBlazorOptions:DisableGetLocalizerFromService"] = "true",
            ["BootstrapBlazorOptions:DisableGetLocalizerFromResourceManager"] = "true"
        });
        var config = builder.Build();
        sc.AddSingleton<IConfiguration>(config);
        sc.AddBootstrapBlazor();

        var provider = sc.BuildServiceProvider();
        var localizer = provider.GetRequiredService<IStringLocalizer<Dummy>>();
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
        var localizer = provider.GetRequiredService<IStringLocalizer>();
        var items = localizer.GetAllStrings(false);

        // TODO: vs+windows pass
        // mac Linux rider+windows failed
        Assert.NotEmpty(items);
        Assert.Equal("test-name", items.First(i => i.Name == "Name").Value);

        var name = Utility.GetDisplayName(typeof(Dummy), "DummyName");
        Assert.Equal("test-name", name);
    }

    [Fact]
    public void GetAllStrings_FromJson()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddBootstrapBlazor();

        var provider = sc.BuildServiceProvider();
        var localizer = provider.GetRequiredService<IStringLocalizer<Foo>>();

        var items = localizer.GetAllStrings(false);
        Assert.Equal("姓名", items.First(i => i.Name == "Name").Value);
        Assert.DoesNotContain("Test-JsonName", items.Select(i => i.Name));

        var resolve = provider.GetRequiredService<ILocalizationResolve>();
        Assert.Empty(resolve.GetAllStringsByCulture(true));
    }

    [Fact]
    public void GetAllStrings_FromResolver()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddSingleton<ILocalizationResolve, MockLocalizationResolve>();
        sc.AddBootstrapBlazor();

        var provider = sc.BuildServiceProvider();
        var localizer = provider.GetRequiredService<IStringLocalizer<Foo>>();

        // test-localizer-name 通过 MockLocalizationResolve 获得
        Assert.Equal("name", localizer["test-localizer-name"]);
        Assert.Equal("test-name", localizer["test-name"]);
    }

    [Fact]
    public void HandleMissingItem()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddSingleton<ILocalizationMissingItemHandler, MockLocalizationMissingItemHandler>();
        sc.AddBootstrapBlazor();

        var provider = sc.BuildServiceProvider();
        var localizer = provider.GetRequiredService<IStringLocalizer<Foo>>();
        var val = localizer["missing-item"];
        Assert.True(val.ResourceNotFound);

        var handler = provider.GetRequiredService<ILocalizationMissingItemHandler>();
        MockLocalizationMissingItemHandler? mockHandler = null;
        if (handler is MockLocalizationMissingItemHandler h)
        {
            mockHandler = h;
        }
        Assert.NotNull(mockHandler);
        Assert.Equal("missing-item", mockHandler.Name);
    }

    [Fact]
    public void FormatException_Ok()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddSingleton<IStringLocalizerFactory, MockLocalizerFactory>();
        sc.AddTransient<IStringLocalizer, MockStringLocalizer>();
        sc.AddBootstrapBlazor();

        var provider = sc.BuildServiceProvider();
        var localizer = provider.GetRequiredService<IStringLocalizer<Dummy>>();
        var v = localizer["Mock-FakeAddress", "Test"];
        Assert.True(v.ResourceNotFound);
        Assert.Equal("Mock-FakeAddress", v);
    }

    [Fact]
    public void GetResourcePrefix_Ok()
    {
        // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I5SRA1
        var builder = new TestContext();
        builder.Services.AddConfiguration();

        // 注入其他 Localization
        builder.Services.AddLocalization();

        // 注入 Bootstrap Localization
        builder.Services.AddBootstrapBlazor();

        var fac = builder.Services.GetRequiredService<IStringLocalizerFactory>();
        var l = fac.Create("Lang", "UnitTest");
        var result = l["test"];
        Assert.True(result.ResourceNotFound);
        Assert.Equal("test", result.Value);
    }

    private static readonly string[] localizationConfigure = ["zh-CN.json"];

    [Fact]
    public void Validate_ResourceManagerStringLocalizerType()
    {
        var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        context.Services.AddConfiguration();
        context.Services.AddBootstrapBlazor(localizationConfigure: option =>
        {
            option.ResourceManagerStringLocalizerType = typeof(Foo);
            option.AdditionalJsonFiles = localizationConfigure;
        });
        context.Services.GetRequiredService<ICacheManager>();

        var foo = new Foo();
        var cut = context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(v => v.Model, foo);
            pb.Add(a => a.OnInvalidSubmit, context =>
            {
                return Task.CompletedTask;
            });
        });

        // 反射触发 Validate 方法
        var mi = cut.Instance.GetType().GetMethod("ValidateDataAnnotations", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
        var pi = foo.GetType().GetProperty("Name");
        var result = new List<ValidationResult>();
        mi.Invoke(cut.Instance, [null, new ValidationContext(cut.Instance), result, pi, "Name"]);
        Assert.Equal("Test", result[0].ErrorMessage);
    }

    [Fact]
    public void ValidateFixEndlessLoop()
    {
        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddBootstrapBlazor();
        sc.AddSingleton<IStringLocalizerFactory, MockLocalizerFactory>();
        sc.AddSingleton<IStringLocalizerFactory, MockLocalizerFactory2>();

        var provider = sc.BuildServiceProvider();
        var localizer = provider.GetRequiredService<IStringLocalizer<Foo>>();

        Assert.Equal("姓名", localizer["Name"]);

        var items = localizer.GetAllStrings(false);
        Assert.Equal("姓名", items.First(i => i.Name == "Name").Value);
        Assert.DoesNotContain("Test-JsonName", items.Select(i => i.Name));
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

    private class MockLocalizerFactory2 : IStringLocalizerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MockLocalizerFactory2(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            var stringLocalizerFactorys = _serviceProvider.GetServices<IStringLocalizerFactory>();
            IStringLocalizerFactory stringLocalizerFactory;
            if (resourceSource == typeof(Foo))
            {
                stringLocalizerFactory = stringLocalizerFactorys.Single(s => s.GetType().Name == "JsonStringLocalizerFactory");
            }
            else
            {
                stringLocalizerFactory = _serviceProvider.GetServices<IStringLocalizerFactory>().Single(s => s is MockLocalizerFactory);
            }
            return stringLocalizerFactory.Create(resourceSource);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            var stringLocalizerFactory = _serviceProvider.GetServices<IStringLocalizerFactory>().Single(s => s is MockLocalizerFactory);
            return stringLocalizerFactory.Create(baseName, location);
        }
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
            new("Mock-Name", "Mock-Test-Name"),
            new("Mock-Address", "Mock-Test-Address-{0}"),
            new("Mock-FakeAddress", "Mock-Test-Address-{ 0}")
        };
    }

    private class Dummy
    {
        [Display(Name = "Name")]
        public string? DummyName { get; set; }
    }

    internal class MockLocalizationResolve : ILocalizationResolve
    {
        public IEnumerable<LocalizedString> GetAllStringsByCulture(bool includeParentCultures) => new LocalizedString[]
        {
            new("test-localizer-name", "name"),
            new("test-localizer-age", "age")
        };

        public IEnumerable<LocalizedString> GetAllStringsByType(string typeName, bool includeParentCultures) => new LocalizedString[]
        {
            new("test-localizer-name", "name"),
            new("test-localizer-age", "age")
        };
    }

    internal class MockLocalizationMissingItemHandler : ILocalizationMissingItemHandler
    {
        [NotNull]
        public string? Name { get; set; }

        public void HandleMissingItem(string name, string typeName, string cultureName)
        {
            Name = name;
        }
    }
}

public class JsonStringLocalizerFactoryTest
{
    [Fact]
    public void GetAllStrings_FromInject()
    {
        // 由于某些应用场景如 (WTM) Blazor 还未加载时 Localizer 模块先开始工作了
        // 为了保证 CacheManager 内部 Instance 可用这里需要使 ICacheManager 先实例化

        var sc = new ServiceCollection();
        sc.AddConfiguration();
        sc.AddBootstrapBlazor(op =>
        {
            op.IgnoreLocalizerMissing = true;
        });

        var provider = sc.BuildServiceProvider();
        var localizer = provider.GetRequiredService<IStringLocalizer<Foo>>();
        var item = localizer["Foo.Name"];
        Assert.NotEqual("Foo.Name", item);
    }
}
