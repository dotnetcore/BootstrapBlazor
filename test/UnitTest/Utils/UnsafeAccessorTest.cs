// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Runtime.CompilerServices;

namespace UnitTest.Utils;

public class UnsafeAccessorTest
{
    [Fact]
    public void Field_Ok()
    {
        var foo = new MockFoo();
        var age = GetAge(foo);
        Assert.Equal(10, age);

        SetAgeMethod(foo, 20);
        Assert.Equal(20, GetAgeMethod(foo));
    }

    class MockFoo
    {
        private int _age = 10;

        private int Age { get => _age; set => _age = value; }
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_age")]
    static extern ref int GetAge(MockFoo foo);

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_Age")]
    static extern int GetAgeMethod(MockFoo foo);

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "set_Age")]
    static extern void SetAgeMethod(MockFoo foo, int value);
}
