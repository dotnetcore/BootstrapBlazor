﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
        Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middle
    });
}
