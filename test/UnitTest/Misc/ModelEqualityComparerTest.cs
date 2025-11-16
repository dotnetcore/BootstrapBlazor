// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Misc;

public class ModelEqualityComparerTest
{
    [Fact]
    public void Equals_Ok()
    {
        Assert.True(IModelEqualityComparerExtensions.Equals<Foo>(null!, null, null));
        Assert.False(IModelEqualityComparerExtensions.Equals(null!, null, new Foo()));
        Assert.False(IModelEqualityComparerExtensions.Equals(null!, new Foo(), null));
    }
}
