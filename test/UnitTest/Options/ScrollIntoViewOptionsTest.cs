// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json;

namespace UnitTest.Options;

public class ScrollIntoViewOptionsTest
{
    [Fact]
    public void Options_Ok()
    {
        var options = new ScrollIntoViewOptions()
        {
            Behavior = ScrollIntoViewBehavior.Auto,
            Block = ScrollIntoViewBlock.Center,
            Inline = ScrollIntoViewInline.End
        };
        var json = JsonSerializer.Serialize(options, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        Assert.Equal("{\"behavior\":\"auto\",\"block\":\"center\",\"inline\":\"end\"}", json);
    }
}
