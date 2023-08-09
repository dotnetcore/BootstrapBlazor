// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

        Assert.Equal(foo, foo1);
        Assert.Equal(foo, foo2);
    }

    record Foo
    {
        public Foo(string name) { Name = name; }

        public string Name { get; init; }

        public int Age { get; set; }
    }
}
