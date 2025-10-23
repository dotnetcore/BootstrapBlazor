﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
