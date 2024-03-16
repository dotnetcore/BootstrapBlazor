// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTestHoliday;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var services = new ServiceCollection();
        services.AddBootstrapHolidayService();

        var provider = services.BuildServiceProvider();
        var holidayService = provider.GetRequiredService<ICalendarHolidays>();
        Assert.True(holidayService.IsHoliday(new DateTime(2016, 01, 01)));
    }
}
