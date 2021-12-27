// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Linq;
using System.Reflection;
using Xunit;

namespace UnitTest.Utils;

public class ReflectionTest
{
    [Fact]
    public void SubClass_Override()
    {
        var dog = new Dog() { Foo = "Test" };
        Assert.Equal("Test", dog.Foo);

        var p1 = dog.GetType().GetProperty("Foo");
        var p = dog.GetType().GetRuntimeProperties().FirstOrDefault(p => p.Name == "Foo");

        // 两种获取属性实例相等
        Assert.Equal(p1, p);

        // 反射获取值
        var v = p!.GetValue(dog);
        Assert.Equal("Test", v);
    }

    [Fact]
    public void SubClass_New()
    {
        var cat = new Cat() { Foo = 1 };
        Assert.Equal(1, cat.Foo);

        // 由于使用 new 关键字导致报错混淆异常
        Assert.ThrowsAny<AmbiguousMatchException>(() => cat.GetType().GetRuntimeProperty("Foo"));

        var p = cat.GetType().GetRuntimeProperties().FirstOrDefault(p => p.Name == "Foo");

        // 反射获取值
        var v = p!.GetValue(cat);
        Assert.Equal(1, v);
    }

    private class Dummy
    {
        public virtual string? Foo { get; set; }
    }

    private class Dog : Dummy
    {
        public override string? Foo { get; set; }
    }

    private class Cat : Dummy
    {
        public new int Foo { get; set; }

        private string Test { get; set; }
    }
}
