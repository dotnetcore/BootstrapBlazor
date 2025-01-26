// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Extensions;

public class ICacheEntryExtensionsTest : BootstrapBlazorTestBase
{
    [Fact]
    public void GetLastAccessed_Ok()
    {
        Cache.GetOrCreate("test_01", entry =>
        {
            return 1;
        });

        Assert.True(Cache.TryGetCacheEntry("test_01", out var entry));
        var v = entry.GetLastAccessed();
        Assert.NotNull(v);
    }
}
