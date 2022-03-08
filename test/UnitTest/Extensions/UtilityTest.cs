// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Extensions;

public class UtilityTest : BootstrapBlazorTestBase
{
    [Fact]
    public void GetKeyValue_Ok()
    {
        var foo = new Foo() { Id = 1 };
        var v = Utility.GetKeyValue<Foo, int>(foo);
        Assert.Equal(1, v);

        object foo1 = new Foo() { Id = 2 };
        v = Utility.GetKeyValue<object, int>(foo1);
        Assert.Equal(2, v);
    }
}
