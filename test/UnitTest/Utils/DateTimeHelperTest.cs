// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Utils;

/// <summary>
/// <see cref="DateTimeHelper"/> 测试类
/// </summary>
public class DateTimeHelperTest
{
    [Fact]
    public void ToDateTime_Ok()
    {
        // 紧凑格式解析成功
        Assert.Equal(new DateTime(2026, 5, 1), DateTimeHelper.ToDateTime("20260501"));

        // 带分隔符格式解析成功
        Assert.Equal(new DateTime(2026, 5, 1, 13, 4, 5), DateTimeHelper.ToDateTime("2026-05-01 13:04:05"));
    }

    [Fact]
    public void ToDateTime_Null()
    {
        // 解析失败返回 null
        Assert.Null(DateTimeHelper.ToDateTime("test"));
        Assert.Null(DateTimeHelper.ToDateTime(null!));
    }

    [Fact]
    public void ToDateTime_DefaultValue_Ok()
    {
        var defaultValue = new DateTime(2020, 1, 1);

        // 解析成功返回解析值
        Assert.Equal(new DateTime(2026, 5, 1), DateTimeHelper.ToDateTime("20260501", defaultValue));

        // 解析失败返回默认值
        Assert.Equal(defaultValue, DateTimeHelper.ToDateTime("test", defaultValue));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void TryToDateTime_NullOrWhiteSpace(string? value)
    {
        // 空字符串直接返回 false 且结果为 DateTime.MinValue
        Assert.False(DateTimeHelper.TryToDateTime(value!, out var result));
        Assert.Equal(DateTime.MinValue, result);
    }

    [Fact]
    public void TryToDateTime_Invalid()
    {
        // 两种解析方式均失败
        Assert.False(DateTimeHelper.TryToDateTime("test", out var result));
        Assert.Equal(DateTime.MinValue, result);

        Assert.False(DateTimeHelper.TryToDateTime("20261301", out _));
    }

    [Theory]
    [InlineData("20260501", 2026, 5, 1, 0, 0, 0, 0)]
    [InlineData("202605011304", 2026, 5, 1, 13, 4, 0, 0)]
    [InlineData("20260501130405", 2026, 5, 1, 13, 4, 5, 0)]
    [InlineData("20260501130405123", 2026, 5, 1, 13, 4, 5, 123)]
    [InlineData("20260501 130405", 2026, 5, 1, 13, 4, 5, 0)]
    [InlineData("20260501 13:04:05", 2026, 5, 1, 13, 4, 5, 0)]
    [InlineData("20260501 13:04", 2026, 5, 1, 13, 4, 0, 0)]
    [InlineData("2026-5-1", 2026, 5, 1, 0, 0, 0, 0)]
    [InlineData("2026/5/1", 2026, 5, 1, 0, 0, 0, 0)]
    public void TryToDateTime_CompactFormats(string value, int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
        // 紧凑格式由 TryParseExact 分支解析
        Assert.True(DateTimeHelper.TryToDateTime(value, out var result));
        Assert.Equal(new DateTime(year, month, day, hour, minute, second, millisecond), result);
    }

    [Theory]
    [InlineData("2026-05-01")]
    [InlineData("2026/05/01")]
    public void TryToDateTime_Fallback(string value)
    {
        // 标准格式由 TryParse 回退分支解析
        Assert.True(DateTimeHelper.TryToDateTime(value, out var result));
        Assert.Equal(new DateTime(2026, 5, 1), result);
    }

    [Fact]
    public void TryToDateTime_Trim()
    {
        // 前后空白被裁剪后可正常解析
        Assert.True(DateTimeHelper.TryToDateTime("  20260501  ", out var result));
        Assert.Equal(new DateTime(2026, 5, 1), result);
    }

    [Fact]
    public void ToDateTimeOffset_Unspecified()
    {
        // 无时区信息按本地时区补齐偏移
        var value = new DateTime(2026, 5, 1, 13, 4, 5);
        var expected = TimeZoneInfo.Local.GetUtcOffset(value);

        var actual = DateTimeHelper.ToDateTimeOffset("20260501 130405");
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Value.Offset);
        Assert.Equal(value, actual.Value.DateTime);
    }

    [Fact]
    public void ToDateTimeOffset_Local()
    {
        // 带时区信息的字符串解析出的 Kind 不是 Unspecified，直接构造
        var actual = DateTimeHelper.ToDateTimeOffset("2026-05-01T13:04:05Z");
        Assert.NotNull(actual);
        Assert.Equal(new DateTime(2026, 5, 1, 13, 4, 5, DateTimeKind.Utc), actual.Value.UtcDateTime);
    }

    [Fact]
    public void ToDateTimeOffset_Null()
    {
        // 解析失败返回 null
        Assert.Null(DateTimeHelper.ToDateTimeOffset("test"));
    }
}
