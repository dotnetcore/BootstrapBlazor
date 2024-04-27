// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.Globalization;

namespace UnitTest.Extensions;

public class ObjectExtensionsTest
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("95%", "95%")]
    [InlineData("95px", "95px")]
    [InlineData("95", "95px")]
    [InlineData("auto", "auto")]
    public static void ConvertToPercentString_Ok(string? source, string expect)
    {
        var actual = source.ConvertToPercentString();
        Assert.Equal(expect, actual);
    }

    [Theory]
    [InlineData(typeof(int?), true)]
    [InlineData(typeof(long?), true)]
    [InlineData(typeof(float?), true)]
    [InlineData(typeof(short?), true)]
    [InlineData(typeof(double?), true)]
    [InlineData(typeof(decimal?), true)]
    [InlineData(typeof(int), true)]
    [InlineData(typeof(long), true)]
    [InlineData(typeof(float), true)]
    [InlineData(typeof(short), true)]
    [InlineData(typeof(double), true)]
    [InlineData(typeof(decimal), true)]
    [InlineData(typeof(DateTime?), false)]
    [InlineData(typeof(DateTime), false)]
    [InlineData(typeof(string), false)]
    public static void IsNumber_Ok(Type source, bool expect)
    {
        var actual = source.IsNumber();
        Assert.Equal(expect, actual);
    }

    [Theory]
    [InlineData(typeof(DateTime?), true)]
    [InlineData(typeof(DateTime), true)]
    [InlineData(typeof(DateTimeOffset?), true)]
    [InlineData(typeof(DateTimeOffset), true)]
    [InlineData(typeof(string), false)]
    public static void IsDateTime_Ok(Type source, bool expect)
    {
        var actual = source.IsDateTime();
        Assert.Equal(expect, actual);
    }

    [Theory]
    [InlineData(typeof(TimeSpan?), true)]
    [InlineData(typeof(TimeSpan), true)]
    [InlineData(typeof(string), false)]
    public static void IsTimeSpan_Ok(Type source, bool expect)
    {
        var actual = source.IsTimeSpan();
        Assert.Equal(expect, actual);
    }

    [Theory]
    [InlineData(typeof(SortOrder), "枚举")]
    [InlineData(typeof(int), "数字")]
    [InlineData(typeof(DateTimeOffset), "日期")]
    [InlineData(typeof(string), "字符串")]
    [InlineData(typeof(Foo), "字符串")]
    public static void GetTypeDesc_Ok(Type source, string expect)
    {
        var actual = source.GetTypeDesc();
        Assert.Equal(expect, actual);
    }

    [Fact]
    public static void TryConvertTo_Ok()
    {
        var source = "test";
        var result = source.TryConvertTo(typeof(string), out var v);
        Assert.True(result);
        Assert.Equal(source, v);

        source = "123";
        result = source.TryConvertTo(typeof(int), out var i);
        Assert.True(result);
        Assert.Equal(123, i);

        source = "123";
        result = source.TryConvertTo(typeof(DateTime), out var d);
        Assert.False(result);
    }

    [Fact]
    public static void TryConvertTo_Generic()
    {
        var source = "123";
        var result = source.TryConvertTo<int?>(out var v);
        Assert.True(result);
        Assert.Equal(123, v);

        source = null;
        result = source.TryConvertTo<string?>(out var s);
        Assert.True(result);
        Assert.Null(s);

        result = source.TryConvertTo<int>(out var i);
        Assert.True(result);
        Assert.Equal(0, i);

        source = "";
        result = source.TryConvertTo<int>(out var e);
        Assert.False(result);

        source = "False";
        result = source.TryConvertTo<bool>(out var b1);
        Assert.True(result);
        Assert.False(b1);

        source = "false";
        result = source.TryConvertTo<bool>(out var b2);
        Assert.True(result);
        Assert.False(b2);

        source = "test";
        result = source.TryConvertTo<DateTime>(out var dt);
        Assert.False(result);

        source = typeof(Foo).Name;
        result = source.TryConvertTo<Foo>(out var f);
        Assert.False(result);

        source = typeof(Dummy).FullName;
        result = source.TryConvertTo<Dummy>(out var _);
        Assert.True(result);
    }

    [Theory]
    [InlineData(100f, "100 B")]
    [InlineData(1024f, "1 KB")]
    [InlineData(1024 * 1024f, "1 MB")]
    [InlineData(1024 * 1024 * 1024f, "1 GB")]
    public void ToFileSizeString_Ok(long source, string expect)
    {
        var actual = source.ToFileSizeString();
        Assert.Equal(expect, actual);
    }

    [Theory]
    [InlineData(ItemChangedType.Add, true, false)]
    [InlineData(ItemChangedType.Update, true, false)]
    [InlineData(ItemChangedType.Add, false, true)]
    [InlineData(ItemChangedType.Update, false, true)]
    public void Readonly_Ok(ItemChangedType itemChangedType, bool @readonly, bool expected)
    {
        var column = new TableColumn<Foo, string>();
        column.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>
        {
            ["Readonly"] = @readonly,
        }));
        Assert.Equal(expected, column.IsEditable(itemChangedType));
    }

    [Theory]
    [InlineData(ItemChangedType.Add, true, false, true)]
    [InlineData(ItemChangedType.Add, true, true, false)]
    [InlineData(ItemChangedType.Add, false, false, true)]
    [InlineData(ItemChangedType.Add, false, true, false)]
    public void ReadonlyWhenAdd_Ok(ItemChangedType itemChangedType, bool @readonly, bool readonlyWhenAdd, bool expected)
    {
        var column = new TableColumn<Foo, string>();
        column.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>
        {
            ["Readonly"] = @readonly,
            [nameof(ITableColumn.IsReadonlyWhenAdd)] = readonlyWhenAdd,
        }));
        Assert.Equal(expected, column.IsEditable(itemChangedType));
    }

    [Theory]
    [InlineData(ItemChangedType.Update, true, false, true)]
    [InlineData(ItemChangedType.Update, true, true, false)]
    [InlineData(ItemChangedType.Update, false, false, true)]
    [InlineData(ItemChangedType.Update, false, true, false)]
    public void ReadonlyWhenUpdate_Ok(ItemChangedType itemChangedType, bool @readonly, bool readonlyWhenUpdate, bool expected)
    {
        var column = new TableColumn<Foo, string>();
        column.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>
        {
            ["Readonly"] = @readonly,
            [nameof(ITableColumn.IsReadonlyWhenEdit)] = readonlyWhenUpdate,
        }));
        Assert.Equal(expected, column.IsEditable(itemChangedType));
    }

    [Theory]
    [InlineData(ItemChangedType.Add, true, true)]
    [InlineData(ItemChangedType.Update, true, true)]
    [InlineData(ItemChangedType.Add, false, false)]
    [InlineData(ItemChangedType.Update, false, false)]
    public void Visible_Ok(ItemChangedType itemChangedType, bool visible, bool expected)
    {
        var column = new TableColumn<Foo, string>();
        column.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>
        {
            [nameof(ITableColumn.Visible)] = visible,
        }));
        Assert.Equal(expected, column.IsVisible(itemChangedType));
    }

    [Theory]
    [InlineData(ItemChangedType.Add, true, false, false)]
    [InlineData(ItemChangedType.Add, true, true, true)]
    [InlineData(ItemChangedType.Add, false, false, false)]
    [InlineData(ItemChangedType.Add, false, true, true)]
    public void VisibleWhenAdd_Ok(ItemChangedType itemChangedType, bool visible, bool visibleWhenAdd, bool expected)
    {
        var column = new TableColumn<Foo, string>();
        column.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>
        {
            [nameof(ITableColumn.Visible)] = visible,
            [nameof(ITableColumn.IsVisibleWhenAdd)] = visibleWhenAdd,
        }));
        Assert.Equal(expected, column.IsVisible(itemChangedType));
    }

    [Theory]
    [InlineData(ItemChangedType.Update, true, false, false)]
    [InlineData(ItemChangedType.Update, true, true, true)]
    [InlineData(ItemChangedType.Update, false, false, false)]
    [InlineData(ItemChangedType.Update, false, true, true)]
    public void VisibleWhenUpdate_Ok(ItemChangedType itemChangedType, bool visible, bool visibleWhenUpdate, bool expected)
    {
        var column = new TableColumn<Foo, string>();
        column.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>
        {
            [nameof(ITableColumn.Visible)] = visible,
            [nameof(ITableColumn.IsVisibleWhenEdit)] = visibleWhenUpdate,
        }));
        Assert.Equal(expected, column.IsVisible(itemChangedType));
    }

    [Fact]
    public void CanWrite_Ok()
    {
        var item = new MockEditItem<Foo, string>() { FieldName = "Name" };
        var result = item.CanWrite(typeof(Foo));
        Assert.True(result);

        var item2 = new MockEditItem<Dummy, string>() { FieldName = "Foo.Name" };
        result = item2.CanWrite(typeof(Dummy));
        Assert.True(result);

        item2 = new MockEditItem<Dummy, string>() { FieldName = "Count" };
        result = item2.CanWrite(typeof(Dummy));
        Assert.False(result);

        // DynamicObject always return True
        Assert.True(item2.CanWrite(typeof(DynamicObject)));
    }

    [Theory]
    [InlineData("Test")]
    [InlineData("Foo.Test")]
    public void CanWrite_Exception(string fieldName)
    {
        var item = new MockEditItem<Dummy, string>() { FieldName = fieldName };
        Assert.Throws<InvalidOperationException>(() => item.CanWrite(typeof(Dummy)));
    }

    [Fact]
    public void IsStatic_Ok()
    {
        var v = new MockStatic();
        var pi = v.GetType().GetProperty(nameof(MockStatic.Test))!;
        Assert.True(pi.IsStatic());
    }

    private class MockStatic
    {
        private static int _test;

        public static int Test { set { _test = value; } }
    }

    [TypeConverter(typeof(DummyConverter))]
    private class Dummy
    {
        public string? Name { get; set; }

        public Foo Foo { get; set; } = new Foo();

        public int Count { get; }
    }

    private class DummyConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return true;
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            return new Dummy();
        }
    }

    private class MockEditItem<TModel, TValue> : EditorItem<TModel, TValue>, IEditorItem
    {
        public string? FieldName { get; set; }

        string IEditorItem.GetFieldName() => FieldName!;

        public Dummy Dummy { get; set; } = new Dummy();
    }
}
