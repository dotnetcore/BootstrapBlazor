// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;

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
