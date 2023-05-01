// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Services;

public class CacheManagerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void GetStartTime_Ok()
    {
        Cache.Clear("BootstrapBlazor_StartTime");
        var v = Cache.GetStartTime();
        Assert.Equal(DateTimeOffset.MinValue, v);

        Cache.GetOrCreate("BootstrapBlazor_StartTime", entry =>
        {
            return 10;
        });
        var v1 = Cache.GetStartTime();
        Assert.Equal(DateTimeOffset.MinValue, v);

        Cache.Clear("BootstrapBlazor_StartTime");
        Cache.SetStartTime();
        Assert.True(DateTime.Now > Cache.GetStartTime());
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
            val++;
            return val;
        });
    }
}
