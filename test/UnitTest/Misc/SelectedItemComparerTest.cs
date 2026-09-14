// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace UnitTest.Misc;

#if NET11_0_OR_GREATER
public class SelectedItemComparerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void SelectedItem_Equals_Ok()
    {
        var comparer = CreateComparer<SelectedItem>("BootstrapBlazor.Components.SelectedItemComparer");
        var item = new SelectedItem("1", "Test 1");

        Assert.True(comparer.Equals(null, null));
        Assert.True(comparer.Equals(item, item));
        Assert.False(comparer.Equals(null, item));
        Assert.False(comparer.Equals(item, null));
        Assert.True(comparer.Equals(item, new SelectedItem("1", "Test 2")));
        Assert.False(comparer.Equals(item, new SelectedItem("2", "Test 1")));
        Assert.Equal(item.Value.GetHashCode(), comparer.GetHashCode(item));
    }

    [Fact]
    public void SelectedItemGeneric_Equals_Ok()
    {
        var modelComparer = new MockModelEqualityComparer<Model>()
        {
            ModelEqualityComparer = (x, y) => x.Id == y.Id
        };
        var comparer = CreateComparer<SelectedItem<Model>>(
            "BootstrapBlazor.Components.SelectedItemComparer`1",
            [typeof(Model)],
            modelComparer);
        var item = new SelectedItem<Model>(new() { Id = 1 }, "Test 1");

        Assert.True(comparer.Equals(null, null));
        Assert.True(comparer.Equals(item, item));
        Assert.False(comparer.Equals(null, item));
        Assert.False(comparer.Equals(item, null));
        Assert.True(comparer.Equals(item, new SelectedItem<Model>(new() { Id = 1 }, "Test 2")));
        Assert.False(comparer.Equals(item, new SelectedItem<Model>(new() { Id = 2 }, "Test 1")));
        Assert.Equal(1, comparer.GetHashCode(item));
        Assert.Equal(0, comparer.GetHashCode(new SelectedItem<Model>(null!, "Null")));
    }

    private static IEqualityComparer<TItem> CreateComparer<TItem>(string typeName, Type[]? genericArguments = null, params object[] arguments)
    {
        var type = typeof(SelectedItem).Assembly.GetType(typeName, throwOnError: true)!;
        if (genericArguments is not null)
        {
            type = type.MakeGenericType(genericArguments);
        }

        return (IEqualityComparer<TItem>)Activator.CreateInstance(
            type,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            binder: null,
            args: arguments,
            culture: null)!;
    }

    private sealed class Model
    {
        public int Id { get; set; }

        public override int GetHashCode() => Id;
    }

    private sealed class MockModelEqualityComparer<TModel> : IModelEqualityComparer<TModel>
    {
        public Func<TModel, TModel, bool>? ModelEqualityComparer { get; set; }

        public Type CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

        public bool Equals(TModel? x, TModel? y) => this.Equals<TModel>(x, y);
    }
}
#endif
