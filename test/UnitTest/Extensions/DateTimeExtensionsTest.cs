// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Extensions;

public class DateTimeExtensionsTest
{
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

    [Theory]
    [InlineData(2024, 4, 8, 2024, 2, 30)]
    public void ToLunarDateTime_Ok(int year, int month, int day, int lunarYear, int lunarMonth, int lunarDay)
    {
        var dt = new DateTime(year, month, day);
        var lunarDate = dt.ToLunarDateTime();
        Assert.Equal(lunarYear, lunarDate.Year);
        Assert.Equal(lunarMonth, lunarDate.Month);
        Assert.Equal(lunarDay, lunarDate.Day);
    }
}
