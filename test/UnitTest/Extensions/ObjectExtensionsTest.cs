// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;
using System.Globalization;

namespace UnitTest.Extensions;

public class ObjectExtensionsTest : BootstrapBlazorTestBase
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
    [InlineData(typeof(sbyte?), true)]
    [InlineData(typeof(byte?), true)]
    [InlineData(typeof(int?), true)]
    [InlineData(typeof(uint?), true)]
    [InlineData(typeof(long?), true)]
    [InlineData(typeof(ulong?), true)]
    [InlineData(typeof(float?), true)]
    [InlineData(typeof(short?), true)]
    [InlineData(typeof(ushort?), true)]
    [InlineData(typeof(double?), true)]
    [InlineData(typeof(decimal?), true)]
    [InlineData(typeof(sbyte), true)]
    [InlineData(typeof(byte), true)]
    [InlineData(typeof(int), true)]
    [InlineData(typeof(uint), true)]
    [InlineData(typeof(long), true)]
    [InlineData(typeof(ulong), true)]
    [InlineData(typeof(float), true)]
    [InlineData(typeof(short), true)]
    [InlineData(typeof(ushort), true)]
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

    [Fact]
    public void IsNumber_Culture()
    {
        var culture = new CultureInfo("es-ES");
        CultureInfo.CurrentUICulture = culture;
        Assert.True(typeof(long).IsNumber());
        Assert.False(typeof(long).IsNumberWithDotSeparator());

        culture = new CultureInfo("en-US");
        CultureInfo.CurrentUICulture = culture;
        Assert.True(typeof(long).IsNumber());
        Assert.True(typeof(long).IsNumberWithDotSeparator());
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

    [Fact]
    public static void TryConvertTo_GenericCulture_StringNullAndEmpty()
    {
        var culture = CultureInfo.InvariantCulture;

        Assert.True("test".TryConvertTo<string>(culture, out var text));
        Assert.Equal("test", text);

        Assert.True(((string?)null).TryConvertTo<string?>(culture, out var nullText));
        Assert.Null(nullText);

        Assert.True(((string?)null).TryConvertTo<int>(culture, out var nullInteger));
        Assert.Equal(0, nullInteger);

        Assert.True(((string?)null).TryConvertTo<int?>(culture, out var nullNullableInteger));
        Assert.Null(nullNullableInteger);

        Assert.False(string.Empty.TryConvertTo<int>(culture, out var emptyInteger));
        Assert.Equal(0, emptyInteger);

        Assert.True(string.Empty.TryConvertTo<int?>(culture, out var emptyNullableInteger));
        Assert.Null(emptyNullableInteger);
    }

    [Theory]
    [InlineData("true", true)]
    [InlineData("TRUE", true)]
    [InlineData("false", false)]
    [InlineData("False", false)]
    public static void TryConvertTo_GenericCulture_Boolean(string source, bool expected)
    {
        Assert.True(source.TryConvertTo<bool>(CultureInfo.InvariantCulture, out var actual));
        Assert.Equal(expected, actual);
    }

    [Fact]
    public static void TryConvertTo_GenericCulture_NumericBoundaries()
    {
        var culture = CultureInfo.InvariantCulture;

        AssertConversion("-128", culture, sbyte.MinValue);
        AssertConversion("255", culture, byte.MaxValue);
        AssertConversion("-32768", culture, short.MinValue);
        AssertConversion("65535", culture, ushort.MaxValue);
        AssertConversion("-2147483648", culture, int.MinValue);
        AssertConversion("4294967295", culture, uint.MaxValue);
        AssertConversion("-9223372036854775808", culture, long.MinValue);
        AssertConversion("18446744073709551615", culture, ulong.MaxValue);
        AssertConversion("-3.4028235E+38", culture, float.MinValue);
        AssertConversion("1.7976931348623157E+308", culture, double.MaxValue);
        AssertConversion("79228162514264337593543950335", culture, decimal.MaxValue);
    }

    [Fact]
    public static void TryConvertTo_GenericCulture_NumericFailuresReturnDefault()
    {
        var culture = CultureInfo.InvariantCulture;

        AssertConversionFails<sbyte>("-129", culture);
        AssertConversionFails<byte>("256", culture);
        AssertConversionFails<short>("-32769", culture);
        AssertConversionFails<ushort>("65536", culture);
        AssertConversionFails<int>("-2147483649", culture);
        AssertConversionFails<uint>("4294967296", culture);
        AssertConversionFails<long>("-9223372036854775809", culture);
        AssertConversionFails<ulong>("18446744073709551616", culture);
        AssertConversionFails<float>("not-a-number", culture);
        AssertConversionFails<double>("not-a-number", culture);
        AssertConversionFails<decimal>("79228162514264337593543950336", culture);
    }

    [Fact]
    public static void TryConvertTo_GenericCulture_NullableNumeric()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");

        AssertConversion<decimal?>("$ 2", culture, 2m);
        AssertConversion<int?>("1,234", culture, 1234);
    }

    [Fact]
    public static void TryConvertTo_GenericCulture_NumberFormats()
    {
        var enUs = CultureInfo.GetCultureInfo("en-US");
        AssertConversion("$ 2", enUs, 2d);
        AssertConversion("$1,234.50", enUs, 1234.50m);

        var deDe = CultureInfo.GetCultureInfo("de-DE");
        AssertConversion("1.234,5", deDe, 1234.5d);

        var frFr = CultureInfo.GetCultureInfo("fr-FR");
        AssertConversion(1234.5m.ToString("N2", frFr), frFr, 1234.5m);
    }

    [Fact]
    public static void TryConvertTo_GenericCulture_PreservesPrecision()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");

        AssertConversion("$12,345,678,901,234,567,890.123456789", culture, 12345678901234567890.123456789m);
        AssertConversion("18,446,744,073,709,551,615", culture, ulong.MaxValue);
    }

    [Fact]
    public static void TryConvertTo_GenericCulture_NonNumericUsesBindConverter()
    {
        var culture = CultureInfo.InvariantCulture;
        var guid = Guid.NewGuid();

        AssertConversion(guid.ToString(), culture, guid);

        Assert.False("not-a-date".TryConvertTo<DateTime>(culture, out var invalidDate));
        Assert.Equal(default, invalidDate);

        var deDe = CultureInfo.GetCultureInfo("de-DE");
        AssertConversion("31.12.2025", deDe, new DateTime(2025, 12, 31));
    }

    [Theory]
    [InlineData(100f, "100 B")]
    [InlineData(1024f, "1.0 KB")]
    [InlineData(1024 * 1024f, "1.0 MB")]
    [InlineData(1024 * 1024 * 1024f, "1.0 GB")]
    [InlineData(1024L * 1024 * 1024 * 1024, "1.0 TB")]
    [InlineData(1024L * 1024 * 1024 * 1024 * 1024, "1.0 PB")]
    [InlineData(1024L * 1024 * 1024 * 1024 * 1024 * 1024, "1.0 EB")]
    public void ToFileSizeString_Ok(long source, string expect)
    {
        var actual = source.ToFileSizeString();
        Assert.Equal(expect, actual);
    }

    [Theory]
    [InlineData("de-DE", "1,5 GB")]
    [InlineData("en-US", "1.5 GB")]
    [InlineData("zh-CN", "1.5 GB")]
    public void ToFileSizeString_WithCulture(string cultureName, string expect)
    {
        // 显式传入文化，输出随之本地化
        var bytes = (long)(1024L * 1024 * 1024 * 1.5);
        var actual = bytes.ToFileSizeString(new CultureInfo(cultureName));
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
    [InlineData(ItemChangedType.Add, true, null, false)]
    [InlineData(ItemChangedType.Add, true, false, false)]
    [InlineData(ItemChangedType.Add, true, true, false)]
    [InlineData(ItemChangedType.Add, false, null, true)]
    [InlineData(ItemChangedType.Add, false, false, true)]
    [InlineData(ItemChangedType.Add, false, true, false)]
    public void ReadonlyWhenAdd_Ok(ItemChangedType itemChangedType, bool @readonly, bool? readonlyWhenAdd, bool expected)
    {
        var column = new TableColumn<Foo, string>();
        column.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>
        {
            [nameof(ITableColumn.Readonly)] = @readonly,
            [nameof(ITableColumn.IsReadonlyWhenAdd)] = readonlyWhenAdd,
        }));
        Assert.Equal(expected, column.IsEditable(itemChangedType));
    }

    [Theory]
    [InlineData(ItemChangedType.Update, true, null, false)]
    [InlineData(ItemChangedType.Update, true, false, false)]
    [InlineData(ItemChangedType.Update, true, true, false)]
    [InlineData(ItemChangedType.Update, false, null, true)]
    [InlineData(ItemChangedType.Update, false, false, true)]
    [InlineData(ItemChangedType.Update, false, true, false)]
    public void ReadonlyWhenUpdate_Ok(ItemChangedType itemChangedType, bool @readonly, bool? readonlyWhenUpdate, bool expected)
    {
        var column = new TableColumn<Foo, string>();
        column.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>
        {
            [nameof(ITableColumn.Readonly)] = @readonly,
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
    [InlineData(ItemChangedType.Add, true, null, true)]
    [InlineData(ItemChangedType.Add, true, false, false)]
    [InlineData(ItemChangedType.Add, true, true, true)]
    [InlineData(ItemChangedType.Add, false, null, false)]
    [InlineData(ItemChangedType.Add, false, false, false)]
    [InlineData(ItemChangedType.Add, false, true, true)]
    public void VisibleWhenAdd_Ok(ItemChangedType itemChangedType, bool visible, bool? visibleWhenAdd, bool expected)
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
    [InlineData(ItemChangedType.Update, true, null, true)]
    [InlineData(ItemChangedType.Update, true, false, false)]
    [InlineData(ItemChangedType.Update, true, true, true)]
    [InlineData(ItemChangedType.Update, false, null, false)]
    [InlineData(ItemChangedType.Update, false, false, false)]
    [InlineData(ItemChangedType.Update, false, true, true)]
    public void VisibleWhenUpdate_Ok(ItemChangedType itemChangedType, bool visible, bool? visibleWhenUpdate, bool expected)
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
        Assert.True(item2.CanWrite(typeof(DataTableDynamicObject)));

        // TableTemplateColumn always return False
        var templateColumn = new TableTemplateColumn<Foo>();
        Assert.False(templateColumn.CanWrite(typeof(Foo)));
        Assert.False(templateColumn.CanWrite(typeof(DynamicObject)));
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

    [Fact]
    public void HasParameterAttribute_Ok()
    {
        var instance = new MockObject();
        var pi = instance.GetType().GetProperty("Mock");
        Assert.False(pi.HasParameterAttribute(typeof(Foo)));

        pi = instance.GetType().GetProperty(nameof(instance.Foo));
        Assert.False(pi.HasParameterAttribute(typeof(Foo)));
    }

    [Fact]
    public void CreateInstance_Ok()
    {
        var exception = Assert.ThrowsAny<Exception>(() => ObjectExtensions.CreateInstance<MockComplexObject>(true));

        var mi = typeof(ObjectExtensions).GetMethod("EnsureInitialized", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
        Assert.NotNull(mi);
        mi.Invoke(null, [null, false]);

        var instance = ObjectExtensions.CreateInstance<MockComplexObject>(false);
        Assert.NotNull(instance);
        Assert.Null(instance.Test);

        // 接口类型不报错
        Assert.Null(ObjectExtensions.CreateInstance<MockInterface>(true));

        var bar = ObjectExtensions.CreateInstance<MockObject>(true);
        Assert.NotNull(bar);
        Assert.NotNull(bar.Foo);
        Assert.Null(bar.Bar);
    }

    private interface MockInterface
    {
        string? Name { get; set; }
    }

    private static void AssertConversion<TValue>(string source, CultureInfo culture, TValue expected)
    {
        Assert.True(source.TryConvertTo<TValue>(culture, out var actual));
        Assert.Equal(expected, actual);
    }

    private static void AssertConversionFails<TValue>(string source, CultureInfo culture)
    {
        Assert.False(source.TryConvertTo<TValue>(culture, out var actual));
        Assert.Equal(default, actual);
    }

    private class MockComplexObject
    {
        public Foo? Foo { get; set; }

        public (string Name, int Count)[]? Test { get; set; }
    }

    private class MockObject
    {
        public string? Name { get; set; }

        public Foo? Foo { get; set; }

        public Foo? Bar { get; }
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
