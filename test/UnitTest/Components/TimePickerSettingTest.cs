// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TimePickerSettingTest : BootstrapBlazorTestBase
{
    [Fact]
    public void PickerSetting_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
            pb.AddChildContent<TimePickerSetting>(pb =>
            {
                pb.Add(a => a.ShowMinute, false);
                pb.Add(a => a.ShowSecond, false);
                pb.Add(a => a.ShowClockScale, true);
                pb.Add(a => a.IsAutoSwitch, false);
            });
        });

        var picker = cut.FindComponent<ClockPicker>();
        Assert.False(picker.Instance.ShowMinute);
        Assert.False(picker.Instance.ShowSecond);
        Assert.False(picker.Instance.IsAutoSwitch);
        Assert.True(picker.Instance.ShowClockScale);
    }

    [Fact]
    public void RangeSetting_Ok()
    {
        // TODO: 等待 Range 支持 DateTime 模式
        //var cut = Context.RenderComponent<DateTimeRange>(pb =>
        //{
        //    pb.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
        //    pb.AddChildContent<TimePickerSetting>(pb =>
        //    {
        //        pb.Add(a => a.ShowMinute, false);
        //        pb.Add(a => a.ShowSecond, false);
        //        pb.Add(a => a.ShowClockScale, true);
        //        pb.Add(a => a.IsAutoSwitch, false);
        //    });
        //});

        //var picker = cut.FindComponent<ClockPicker>();
        //Assert.False(picker.Instance.ShowMinute);
        //Assert.False(picker.Instance.ShowSecond);
        //Assert.False(picker.Instance.IsAutoSwitch);
        //Assert.True(picker.Instance.ShowClockScale);
    }
}
