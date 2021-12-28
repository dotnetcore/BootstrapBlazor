// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class ColorPickerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void DisplayText_OK()
    {
        var cut = Context.RenderComponent<ColorPicker>(builder =>
        {
            builder.Add(a => a.ShowLabel, true);
            builder.Add(a => a.DisplayText, "Test_Color");
        });

        Assert.Equal("Test_Color", cut.Find("label").TextContent);
    }
}
