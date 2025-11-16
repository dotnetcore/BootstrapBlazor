// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace UnitTest.Utils;

public class HasSetComparerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void GetHashCode_NoKey_Ok()
    {
        var comparer = new ModelHashSetComparer<Dummy>(new MockModelEqualityComparer<Dummy>() { });

        var obj = new Dummy() { Id = 1 };

        // 未提供 KeyAttribute 标签内部等同 obj.GetHashCode()
        Assert.Equal(obj.GetHashCode(), comparer.GetHashCode(obj));

        // 空值相等
        Assert.True(comparer.Equals(null, null));

        // 未提供 ModelEqualityComparer 两个对象不相等
        Assert.False(comparer.Equals(new Dummy() { Id = 1 }, new Dummy() { Id = 1 }));

        // 空与非空比较
        Assert.False(comparer.Equals(new Dummy(), null));
        Assert.False(comparer.Equals(null, new Dummy()));
    }

    [Fact]
    public void GetHashCode_Key_Ok()
    {
        var comparer = new ModelHashSetComparer<Dog>(new MockModelEqualityComparer<Dog>() { });

        var obj = new Dog() { Id = 1 };

        // 提供 KeyAttribute 标签内部不等同 obj.GetHashCode()
        Assert.NotEqual(1, obj.GetHashCode());
        Assert.Equal(1, comparer.GetHashCode(obj));
        Assert.True(comparer.Equals(new Dog() { Id = 1 }, new Dog() { Id = 1 }));

        // 空值相等
        Assert.True(comparer.Equals(null, null));

        // 空与非空比较
        Assert.False(comparer.Equals(new Dog(), null));
        Assert.False(comparer.Equals(null, new Dog()));
    }

    [Fact]
    public void GetHashCode_Comparer_Ok()
    {
        var comparer = new ModelHashSetComparer<Dummy>(new MockModelEqualityComparer<Dummy>() { ModelEqualityComparer = (x, y) => x.Id == y.Id });

        var obj = new Dummy() { Id = 1 };

        // 未提供 KeyAttribute 标签内部等同 obj.GetHashCode()
        Assert.Equal(obj.GetHashCode(), comparer.GetHashCode(obj));

        // 空值相等
        Assert.True(comparer.Equals(null, null));

        // 提供 ModelEqualityComparer 两个对象相等
        Assert.True(comparer.Equals(new Dummy() { Id = 1 }, new Dummy() { Id = 1 }));

        // 空与非空比较
        Assert.False(comparer.Equals(new Dummy(), null));
        Assert.False(comparer.Equals(null, new Dummy()));
    }

    class Dummy
    {
        public int Id { get; set; }
    }

    class Dog
    {
        [Key]
        public int Id { get; set; }
    }

    class MockModelEqualityComparer<TModel> : IModelEqualityComparer<TModel>
    {
        public Func<TModel, TModel, bool>? ModelEqualityComparer { get; set; }

        public Type CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

        public bool Equals(TModel? x, TModel? y) => this.Equals<TModel>(x, y);
    }
}
