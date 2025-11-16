// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Utils;

public class QueryHelperTest
{
    [Fact]
    public void ParseQuery_Ok()
    {
        var url = "?test1=1&test2=2";
        var querys = QueryHelper.ParseQuery(url);
        Assert.Equal("1", querys["test1"]);
        Assert.Equal("2", querys["test2"]);

        url = "?test1=%E6%9B%B4%E6%96%B0%E6%97%A5%E5%BF%97&test2=2";
        Assert.Equal("更新日志", Uri.UnescapeDataString("%E6%9B%B4%E6%96%B0%E6%97%A5%E5%BF%97"));
        querys = QueryHelper.ParseQuery(url);
        Assert.Equal("更新日志", querys["test1"]);
        Assert.Equal("2", querys["test2"]);
    }
}
