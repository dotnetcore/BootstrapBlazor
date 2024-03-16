﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Extensions;

public class DateTimeExtensionsTest
{
    [Fact]
    public void ToLunar_Ok()
    {
        var dt = new DateTime(2024, 3, 16).ToLunar();
        Assert.Equal("2024-02-07", dt.ToString("yyyy-MM-dd"));

        dt = new DateTime(2023, 2, 20).ToLunar();
        Assert.Equal("2023-02-01", dt.ToString("yyyy-MM-dd"));

        dt = new DateTime(2023, 3, 22).ToLunar();
        Assert.Equal("2023-02-01", dt.ToString("yyyy-MM-dd"));

        dt = new DateTime(2023, 4, 20).ToLunar();
        Assert.Equal("2023-03-01", dt.ToString("yyyy-MM-dd"));
    }

    [Fact]
    public void ToLunarText_Ok()
    {
        var v = new DateTime(2024, 3, 16).ToLunarText();
        Assert.Equal("初七", v);

        v = new DateTime(2023, 2, 20).ToLunarText();
        Assert.Equal("二月", v);

        v = new DateTime(2023, 3, 22).ToLunarText();
        Assert.Equal("闰二月", v);

        v = new DateTime(2024, 2, 10).ToLunarText();
        Assert.Equal("正月", v);
    }

    [Theory]
    [InlineData(2021, 7, 7, "小暑")]
    [InlineData(2021, 7, 22, "大暑")]
    [InlineData(2024, 3, 5, "惊蛰")]
    [InlineData(2024, 3, 20, "春分")]
    public void GetSolarTerm_Ok(int year, int month, int day, string name)
    {
        var dt = new DateTime(year, month, day);
        Assert.Equal(name, dt.GetSolarTermName());
    }
}
