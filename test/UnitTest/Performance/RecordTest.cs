﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Performance;

/// <summary>
/// Record 测试
/// </summary>
public class RecordTest
{
    [Fact]
    public void With_Ok()
    {
        var foo = new Foo("Test");
        var foo1 = foo with { };
        var foo2 = new Foo("Test");
        var foo3 = new Foo("Test1");

        Assert.Equal(foo, foo1);
        Assert.Equal(foo, foo2);
        Assert.NotEqual(foo3, foo2);
    }

    record Foo
    {
        public Foo(string name) { Name = name; }

        public string Name { get; init; }

        public int Age { get; set; }
    }
}
