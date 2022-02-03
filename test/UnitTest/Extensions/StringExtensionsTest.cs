// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Diagnostics;

namespace UnitTest.Extensions;

public class StringExtensionsTest
{
    private ITestOutputHelper Logger { get; }

    public StringExtensionsTest(ITestOutputHelper logger) => Logger = logger;

    [Fact]
    public void SpanSplit_Ok()
    {
        var source = "Test1;Test2;Test3";
        var result = source.SpanSplit(";");
        Assert.Equal(new List<string> { "Test1", "Test2", "Test3" }, result);

        source = "";
        result = source.SpanSplit(";");
        Assert.Empty(result);

        source = null;
        result = source.SpanSplit(";");
        Assert.Empty(result);
    }

    [Fact]
    public void SpanSplitWithoutParameter_Ok()
    {
        var source = $"Test1{Environment.NewLine} Test2";
        var result = source.SpanSplit();
        Assert.Equal(new List<string> { "Test1", " Test2" }, result);

        result = source.SpanSplit(stringSplitOptions: StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(new List<string> { "Test1", "Test2" }, result);
    }

    [Fact]
    public void SpanSplitAny_Ok()
    {
        var source = "Test1,Test2;Test3";
        var result = source.SpanSplitAny(";,");
        Assert.Equal(new List<string> { "Test1", "Test2", "Test3" }, result);

        source = "";
        result = source.SpanSplitAny(";");
        Assert.Empty(result);

        source = null;
        result = source.SpanSplitAny(";");
        Assert.Empty(result);
    }

    [Fact]
    public void SpanSplitAnyWithoutParameter_Ok()
    {
        var source = "Test1,Test2; Test3";
        var result = source.SpanSplitAny("");
        Assert.Equal(new List<string> { "Test1,Test2; Test3" }, result);

        result = source.SpanSplitAny(";,", StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(new List<string> { "Test1", "Test2", "Test3" }, result);
    }

    [Fact]
    public void SpanSplit_Perf()
    {
        // 本段代码有点意思
        // count 为 10000 时 SpanSplit 快
        // 其他条件时 SpanSplit 慢
        var count = 10000;

        var sw = Stopwatch.StartNew();
        for (var i = 0; i < count; i++)
        {
            var source = $"Test1;Test2;Test3;{i + 4}";
            source.SpanSplit(";");
        }
        sw.Stop();
        Logger.WriteLine($"SpanSplit: {count} - {sw.ElapsedMilliseconds}");

        sw.Restart();
        for (var i = 0; i < count; i++)
        {
            var source = $"Test1;Test2;Test3;{i + 4}";
            _ = source.Split(";");
        }
        sw.Stop();
        Logger.WriteLine($"Split: {count} - {sw.ElapsedMilliseconds}");
    }
}
