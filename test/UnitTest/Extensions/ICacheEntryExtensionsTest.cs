// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

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
        var v = entry.GetLastAccessed(true);
        Assert.NotNull(v);
    }

    [Fact]
    public void GetLastAccessed_Null()
    {
        var mock = new MockCacheEntry();
        var v = mock.GetLastAccessed(true);
        Assert.Null(v);
    }

    class MockCacheEntry : ICacheEntry
    {
        public object Key { get; }
        public object? Value { get; set; }
        public DateTimeOffset? AbsoluteExpiration { get; set; }
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
        public IList<IChangeToken> ExpirationTokens { get; }
        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; }
        public CacheItemPriority Priority { get; set; }
        public long? Size { get; set; }

        private int LastAccessed { get; set; }

        public MockCacheEntry()
        {
            Key = "_test";
            ExpirationTokens = [];
            PostEvictionCallbacks = [];
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
