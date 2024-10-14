// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Core.Converter;
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
