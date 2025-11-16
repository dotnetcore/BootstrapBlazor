// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TimePickerSettingTest : BootstrapBlazorTestBase
{
    [Fact]
    public void PickerSetting_Ok()
    {
        var cut = Context.Render<DateTimePicker<DateTime>>(pb =>
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
        //var cut = Context.Render<DateTimeRange>(pb =>
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
