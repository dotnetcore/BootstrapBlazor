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
    }

    [JsonConverter(typeof(JsonDescriptionEnumConverter<TestEnum>))]
    public enum TestEnum
    {
        [Description("Test1")]
        Item1,

        [Description("test2")]
        Item2
    }
}
