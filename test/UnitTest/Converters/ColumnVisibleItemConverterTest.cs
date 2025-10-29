// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;

namespace UnitTest.Converters;

public class ColumnVisibleItemConverterTest
{
    [Fact]
    public void ColumnVisibleItemConverter_Ok()
    {
        var item = new ColumnVisibleItem("name", true) { DisplayName = "display" };
        var json = JsonSerializer.Serialize(item);
        Assert.Equal("{\"name\":\"name\",\"visible\":true}", json);

        var item2 = JsonSerializer.Deserialize<List<ColumnVisibleItem>>("[{\"test\":\"test\"}]");
    }
}
