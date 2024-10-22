// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Utils;

public class TItemComparerTest
{
    [Fact]
    public void GetHashCode_Ok()
    {
        var comparer = new ModelComparer<Dummy>((x, y) => x.Id == y.Id);

        var obj = new Dummy();
        Assert.Equal(obj.GetHashCode(), comparer!.GetHashCode(obj));
        Assert.True(comparer.Equals(null, null));
        Assert.True(comparer.Equals(new Dummy() { Id = 1 }, new Dummy() { Id = 1 }));

        Assert.False(comparer.Equals(new Dummy(), null));
        Assert.False(comparer.Equals(null, new Dummy()));
    }

    class Dummy
    {
        public int Id { get; set; }
    }
}
