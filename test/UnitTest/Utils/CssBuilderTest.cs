// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.Utils;

public class CssBuilderTest
{
    [Fact]
    public void AddClass_When()
    {
        var classString = CssBuilder.Default()
            .AddClass(() => "cls_test", () => false)
            .Build();
        Assert.DoesNotContain("cls_test", classString);

        classString = CssBuilder.Default()
            .AddClass(() => "cls_test", () => true)
            .Build();
        Assert.Contains("cls_test", classString);
    }

    [Fact]
    public void AddClass_Builder()
    {
        var builder = CssBuilder.Default("cls_test");

        var classString = CssBuilder.Default()
            .AddClass(builder, () => false)
            .Build();
        Assert.DoesNotContain("cls_test", classString);

        classString = CssBuilder.Default()
            .AddClass(builder, () => true)
            .Build();
        Assert.Contains("cls_test", classString);
    }

    [Fact]
    public void AddStyleFromAttributes_Ok()
    {
        var classString = CssBuilder.Default()
            .AddStyleFromAttributes(null)
            .Build();
        Assert.Null(classString);

        var style = new Dictionary<string, object>()
        {
            ["style"] = "padding: 0;"
        };
        classString = CssBuilder.Default()
            .AddStyleFromAttributes(style)
            .Build();
        Assert.Equal("padding: 0;", classString);

        var style1 = new Dictionary<string, object>()
        {
            ["style"] = null!
        };
        classString = CssBuilder.Default()
            .AddStyleFromAttributes(style1)
            .Build();
        Assert.Null(classString);
    }
}
