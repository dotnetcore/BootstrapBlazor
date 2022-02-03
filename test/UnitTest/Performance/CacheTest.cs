// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace UnitTest.Performance;

public class CacheTest : BootstrapBlazorTestBase
{
    private IStringLocalizer<Foo> Localizer { get; }

    private static readonly Random random = new();

    private ITestOutputHelper Logger { get; }

    private const int Count = 200;

    public CacheTest(ITestOutputHelper logger)
    {
        Localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        Logger = logger;
    }

    [Fact]
    public void NoCache_Ok()
    {
        var sw = Stopwatch.StartNew();
        for (var index = 0; index < Count; index++)
        {
            NoCacheMethod();
        }
        sw.Stop();
        Logger.WriteLine(sw.Elapsed.ToString());
    }

    [Fact]
    public void Cache_Ok()
    {
        // 缓存 IEnumerable<Foo> 效果差
        var cache = new ConcurrentDictionary<string, IEnumerable<Foo>>();
        var sw = Stopwatch.StartNew();
        for (var index = 0; index < Count; index++)
        {
            CacheMethod();
        }
        sw.Stop();
        Logger.WriteLine(sw.Elapsed.ToString());

        IEnumerable<Foo> CacheMethod() => cache.GetOrAdd("test", key => NoCacheMethod());
    }

    private IEnumerable<Foo> NoCacheMethod() => Enumerable.Range(1, 80).Select(i => new Foo()
    {
        Id = i,
        Name = Localizer["Foo.Name", $"{i:d4}"],
        DateTime = System.DateTime.Now.AddDays(i - 1),
        Address = Localizer["Foo.Address", $"{random.Next(1000, 2000)}"],
        Count = random.Next(1, 100),
        Complete = random.Next(1, 100) > 50,
        Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel
    });
}
