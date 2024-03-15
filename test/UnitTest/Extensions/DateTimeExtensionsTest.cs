// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
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
}
