// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Newtonsoft.Json.Linq;
using UnitTest.Pages;

namespace UnitTest.Components;

public class CalendarTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Value_Ok()
    {
        var cut = Context.RenderComponent<Calendar>();
        Assert.Contains(DateTime.Today.Year.ToString(), cut.Markup);

        cut.SetParametersAndRender(builder => builder.Add(s => s.Value, new DateTime(1999, 1, 1)));
        Assert.Contains("1999", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Calendar>(builder =>
        {
            builder.Add(s => s.ViewModel, CalendarViewModel.Week);
            builder.Add(a => a.ChildContent, CreateComponent());
        });
        Assert.Contains("test", cut.Markup);

        static RenderFragment CreateComponent() => builder =>
        {
            builder.OpenElement(1, "div");
            builder.AddContent(2, "test");
            builder.CloseComponent();
        };
    }

    [Fact]
    public void ButtonClick_Ok()
    {
        var cut = Context.RenderComponent<Calendar>();
        var buttons = cut.FindAll(".calendar-button-group button");

        // btn 上一年
        buttons[0].Click();
        Assert.Contains($"{DateTime.Now.Year - 1} 年 {DateTime.Now.Month} 月", cut.Find(".calendar-title").ToMarkup());

        // btn 下一年
        buttons[4].Click();
        Assert.Contains($"{DateTime.Now.Year} 年 {DateTime.Now.Month} 月", cut.Find(".calendar-title").ToMarkup());

        // btn 上一月
        buttons[1].Click();
        Assert.Contains($"{DateTime.Now.Year} 年 {DateTime.Now.Month - 1 } 月", cut.Find(".calendar-title").ToMarkup());

        // btn 下一月
        buttons[3].Click();
        Assert.Contains($"{DateTime.Now.Year} 年 {DateTime.Now.Month } 月", cut.Find(".calendar-title").ToMarkup());

        // btn 今天
        buttons[2].Click();
        Assert.Contains(DateTime.Now.Day.ToString(), cut.Find(".current.is-selected.is-today").ToMarkup());
    }


    [Fact]
    public void ValueChanged_Ok()
    {
        DateTime value = DateTime.MinValue;
        var cut = Context.RenderComponent<Calendar>(builder =>
        {
            builder.Add(a => a.ValueChanged, v => value = v);
        });

        // 点击第一个 current 得到本月 1号
        cut.Find(".current").Click();
        Assert.Equal(1, value.Day);

        cut.Find(".is-today").Click();
        Assert.Equal(value.Day, DateTime.Now.Day);
    }

    [Fact]
    public void OnChangeWeek_Ok()
    {
        var cut = Context.RenderComponent<Calendar>(builder => builder.Add(s => s.ViewModel, CalendarViewModel.Week));
        Assert.Contains("table-week", cut.Markup);

        var buttons = cut.FindAll(".calendar-button-group button");
        // 上一周
        buttons[0].Click();
        DateTime value = cut.Instance.Value;
        Assert.Contains($"第 {GetWeekCount()} 周", cut.Find(".calendar-title").ToMarkup());

        // 下一周
        buttons[2].Click();
        value = cut.Instance.Value;
        Assert.Contains($"第 {GetWeekCount()} 周", cut.Find(".calendar-title").ToMarkup());

        // 本周
        buttons[1].Click();
        value = cut.Instance.Value;
        Assert.Contains($"第 {GetWeekCount()} 周", cut.Find(".calendar-title").ToMarkup());

        int GetWeekCount()
        {
            var gc = new System.Globalization.GregorianCalendar();
            return gc.GetWeekOfYear(value, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
    }
}
