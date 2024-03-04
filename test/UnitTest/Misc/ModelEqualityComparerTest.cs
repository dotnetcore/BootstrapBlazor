// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Misc;

public class ModelEqualityComparerTest
{
    [Fact]
    public void Equals_Ok()
    {
        Assert.True(IModelEqualityComparerExtensions.Equals<Foo>(null!, null, null));
        Assert.False(IModelEqualityComparerExtensions.Equals<Foo>(null!, null, new Foo()));
        Assert.False(IModelEqualityComparerExtensions.Equals<Foo>(null!, new Foo(), null));
    }
}
