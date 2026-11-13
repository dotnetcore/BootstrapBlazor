// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;

namespace UnitTest.Options;

public class ScrollIntoViewOptionsTest
{
    private static readonly JsonSerializerOptions _option = new(JsonSerializerDefaults.Web);

    [Fact]
    public void Options_Ok()
    {
        var options = new ScrollIntoViewOptions()
        {
            Behavior = ScrollIntoViewBehavior.Auto,
            Block = ScrollIntoViewBlock.Center,
            Inline = ScrollIntoViewInline.End
        };
        var json = JsonSerializer.Serialize(options, _option);
        Assert.Equal("{\"behavior\":\"auto\",\"block\":\"center\",\"inline\":\"end\"}", json);
    }
}
