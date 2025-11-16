// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
