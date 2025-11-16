// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UnitTest.Converters;

public class JsonDescriptionEnumConverterTest : TestBase
{
    [Fact]
    public void Serializer_Ok()
    {
        var value = TestEnum.Item1;
        var json = JsonSerializer.Serialize(value);
        Assert.Equal("\"Test1\"", json);

        value = TestEnum.Item2;
        json = JsonSerializer.Serialize(value);
        Assert.Equal("\"test2\"", json);

        var v = JsonSerializer.Deserialize<TestEnum>("\"test2\"");
        Assert.Equal(TestEnum.Item2, v);

        v = JsonSerializer.Deserialize<TestEnum>("\"Test1\"");
        Assert.Equal(TestEnum.Item1, v);

        v = JsonSerializer.Deserialize<TestEnum>("\"Test3\"");
        Assert.Equal(TestEnum.Item1, v);
    }

    [Fact]
    public void JsonEnumConverter_Ok()
    {
        var value = MockEnum.Item1;
        var json = JsonSerializer.Serialize(value);
        Assert.Equal("\"item1\"", json);

        var value2 = MockEnum2.Item1;
        json = JsonSerializer.Serialize(value2);
        Assert.Equal("\"Item1\"", json);

        var value3 = EnumBarcodeTextFontOption.Bold_Italic;
        json = JsonSerializer.Serialize(value3);
        Assert.Equal("\"bold italic\"", json);

        var value4 = EnumBarcodeTextFontOption.Normal;
        json = JsonSerializer.Serialize(value4);
        Assert.Equal("\"\"", json);
    }

    [Fact]
    public void ColumnVisibleItemConverter_Ok()
    {
        var item = new ColumnVisibleItem("name", true) { DisplayName = "display" };
        var json = JsonSerializer.Serialize(item);

        Assert.Equal("{\"name\":\"name\",\"visible\":true}", json);

        var item1 = JsonSerializer.Deserialize<ColumnVisibleItem>(json);
        Assert.NotNull(item1);
        Assert.Equal("name", item1.Name);
        Assert.True(item1.Visible);
    }

    [JsonConverter(typeof(JsonDescriptionEnumConverter<TestEnum>))]
    public enum TestEnum
    {
        [Description("Test1")]
        Item1,

        [Description("test2")]
        Item2
    }

    [JsonEnumConverter(true)]
    public enum MockEnum
    {
        Item1,

        Item2
    }

    [JsonEnumConverter]
    public enum MockEnum2
    {
        Item1,

        Item2
    }

    [JsonConverter(typeof(JsonDescriptionEnumConverter<EnumBarcodeTextFontOption>))]
    public enum EnumBarcodeTextFontOption
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        Normal,
        /// <summary>
        /// 
        /// </summary>
        Bold,
        /// <summary>
        /// 
        /// </summary>
        Italic,
        /// <summary>
        /// 
        /// </summary>
        [Description("bold italic")]
        Bold_Italic,
    }
}
