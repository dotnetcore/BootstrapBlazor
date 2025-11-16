// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

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
    }
}
