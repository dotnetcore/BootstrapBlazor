﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;

namespace UnitTest.Services;

public class CacheManagerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void GetStartTime_Ok()
    {
        Cache.Clear("BootstrapBlazor_StartTime");
        var v = Cache.GetStartTime();
        Assert.Equal(DateTimeOffset.MinValue, v);

        Cache.SetStartTime();
        Assert.Equal(1, Cache.Count);
        Cache.Clear("BootstrapBlazor_StartTime");
        Assert.Equal(1, Cache.Count);

        Cache.Clear();
        Assert.Equal(1, Cache.Count);
        Assert.NotEqual(DateTimeOffset.MinValue, Cache.GetStartTime());
    }

    [Fact]
    public void GetStartTime_Number()
    {
        var context = new TestContext();
        context.Services.AddBootstrapBlazor();
        var cache = context.Services.GetRequiredService<ICacheManager>();

        var v = cache.GetOrCreate("BootstrapBlazor_StartTime", entry =>
        {
            return 1;
        });
        Assert.Equal(1, v);

        var v2 = cache.GetStartTime();
        Assert.Equal(DateTimeOffset.MinValue, v2);
        Assert.Empty(Cache.Keys);
    }

    [Fact]
    public void TryGetValue()
    {
        Cache.GetOrCreate("test_01", entry =>
        {
            return 1;
        });
        Cache.TryGetValue("test_01", out int v1);
        Cache.TryGetValue("test_01", out string? v2);
        Cache.TryGetValue("test_02", out int v3);
    }

    [Fact]
    public async Task GetOrCreateAsync_Ok()
    {
        var key = new object();
        var val = 0;
        var actual = await GetOrCreateAsync(key);
        Assert.Equal(1, actual);

        actual = await GetOrCreateAsync(key);
        Assert.Equal(1, actual);

        Task<int> GetOrCreateAsync(object key) => Cache.GetOrCreateAsync<int>(key, entry =>
        {
            val++;
            return Task.FromResult(val);
        });
    }

    [Fact]
    public void GetOrCreate_Ok()
    {
        var key = new object();
        var val = 0;
        var actual = GetOrCreate(key);
        Assert.Equal(1, actual);

        actual = GetOrCreate(key);
        Assert.Equal(1, actual);

        int GetOrCreate(object key) => Cache.GetOrCreate<int>(key, entry =>
        {
            val++;
            return val;
        });
    }

    [Fact]
    public void Clear_Ok()
    {
        var key = "test_clear";
        var val = 0;
        var actual = GetOrCreate(key);
        Assert.Equal(1, actual);

        Cache.Clear(key);
        Assert.Equal(1, actual);

        int GetOrCreate(string key) => Cache.GetOrCreate<int>(key, entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromSeconds(1);
            val++;
            return val;
        });
    }

    [Fact]
    public void MemoryCacheClear_Ok()
    {
        var key = "test_clear";
        var val = 0;
        var actual = GetOrCreate(key);
        Assert.Equal(1, actual);

        Cache.Clear();
        Assert.Equal(1, actual);

        int GetOrCreate(string key) => Cache.GetOrCreate<int>(key, entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(1);
            val++;
            return val;
        });
    }
}
