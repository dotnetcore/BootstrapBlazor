// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
        var cell = cut.Find("td.current");
        cut.InvokeAsync(() => cell.Click());

        cut.SetParametersAndRender(builder => builder.Add(s => s.ValueChanged, EventCallback.Factory.Create<DateTime>(this, () => new DateTime(1999, 1, 1))));
        cut.InvokeAsync(() => cell.Click());
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Calendar>(builder =>
        {
            builder.Add(s => s.ViewMode, CalendarViewMode.Week);
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
    public void CellTemplate_Ok()
    {
        var cut = Context.RenderComponent<Calendar>(builder =>
        {
            builder.Add(a => a.CellTemplate, context => builder =>
            {
                builder.OpenElement(0, "td");
                builder.OpenElement(1, "div");
                builder.AddAttribute(2, "class", context.DefaultCss);
                builder.AddContent(3, context.CellValue.Day);
                builder.CloseElement();
                builder.CloseElement();
            });
        });
    }

    [Fact]
    public void HeaderTemplate_Ok()
    {
        var cut = Context.RenderComponent<Calendar>(builder =>
        {
            builder.Add(a => a.ViewMode, CalendarViewMode.Month);
            builder.Add(a => a.HeaderTemplate, builder =>
            {
                builder.AddContent(0, "HeaderTemplate");
            });
            builder.Add(a => a.BodyTemplate, context => builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "data-bb-value", context.Values.Count);
                builder.CloseElement();
            });
        });

        Assert.Contains("HeaderTemplate", cut.Markup);
        Assert.Contains("data-bb-value=\"7\"", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ViewMode, CalendarViewMode.Week);
        });
    }

    [Fact]
    public async Task ButtonClick_Ok()
    {
        var v = DateTime.MinValue;
        var cut = Context.RenderComponent<Calendar>(pb =>
        {
            pb.Add(a => a.Value, DateTime.Today);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<DateTime>(this, d => v = d));
            pb.Add(a => a.OnValueChanged, d => Task.CompletedTask);
        });

        var buttons = cut.FindAll(".calendar-button-group button");
        await cut.InvokeAsync(() =>
        {
            // btn 上一年
            buttons[0].Click();
        });
        Assert.Contains($"{DateTime.Now.Year - 1} 年 {DateTime.Now.Month} 月", cut.Find(".calendar-title").ToMarkup());
        Assert.Equal(v, DateTime.Today.AddYears(-1));

        await cut.InvokeAsync(() =>
        {
            // btn 下一年
            buttons[4].Click();
        });
        Assert.Contains($"{DateTime.Now.Year} 年 {DateTime.Now.Month} 月", cut.Find(".calendar-title").ToMarkup());

        await cut.InvokeAsync(() =>
        {
            // btn 上一月
            buttons[1].Click();
        });
        Assert.Contains($"{DateTime.Now.AddMonths(-1).Year} 年 {DateTime.Now.AddMonths(-1).Month} 月", cut.Find(".calendar-title").ToMarkup());
        Assert.Equal(v, DateTime.Today.AddMonths(-1));

        await cut.InvokeAsync(() =>
        {
            // btn 下一月
            buttons[3].Click();
        });
        Assert.Contains($"{DateTime.Now.Year} 年 {DateTime.Now.Month} 月", cut.Find(".calendar-title").ToMarkup());

        await cut.InvokeAsync(() =>
        {
            // btn 今天
            buttons[2].Click();
        });
        Assert.Contains(DateTime.Now.Day.ToString(), cut.Find(".current.is-selected.is-today").ToMarkup());
    }

    [Fact]
    public async Task ValueChanged_Ok()
    {
        var value = DateTime.MinValue;
        var cut = Context.RenderComponent<Calendar>(pb =>
        {
            pb.Add(a => a.ValueChanged, v => value = v);
            pb.Add(a => a.OnValueChanged, d => Task.CompletedTask);
        });

        await cut.InvokeAsync(() =>
        {
            // 点击第一个 current 得到本月 1号
            cut.Find(".current").Click();
            Assert.Equal(1, value.Day);

            cut.Find(".is-today").Click();
            Assert.Equal(value.Day, DateTime.Now.Day);
        });
    }

    [Fact]
    public async Task OnChangeWeek_Ok()
    {
        var v = DateTime.MinValue;
        var cut = Context.RenderComponent<Calendar>(pb =>
        {
            pb.Add(s => s.ViewMode, CalendarViewMode.Week);
            pb.Add(a => a.Value, DateTime.Today);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<DateTime>(this, d => v = d));
            pb.Add(a => a.OnValueChanged, d => Task.CompletedTask);
        });
        Assert.Contains("table-week", cut.Markup);

        await cut.InvokeAsync(() =>
        {
            var buttons = cut.FindAll(".calendar-button-group button");
            // 上一周
            buttons[0].Click();
            var value = cut.Instance.Value;
            Assert.Contains($"第 {GetWeekCount()} 周", cut.Find(".calendar-title").ToMarkup());
            Assert.Equal(v, DateTime.Today.AddDays(-7));

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
        });
    }

    [Fact]
    public void ShowYearButtons_Ok()
    {
        var cut = Context.RenderComponent<Calendar>(pb =>
        {
            pb.Add(s => s.ViewMode, CalendarViewMode.Month);
            pb.Add(a => a.Value, DateTime.Today);
            pb.Add(a => a.ShowYearButtons, false);
        });

        var buttons = cut.FindAll(".btn-group > .btn-sm");
        Assert.Equal(3, buttons.Count);
    }

    [Fact]
    public async Task OnValueChanged_Ok()
    {
        var v = DateTime.MinValue;
        var cut = Context.RenderComponent<Calendar>(pb =>
        {
            pb.Add(s => s.ViewMode, CalendarViewMode.Week);
            pb.Add(a => a.Value, DateTime.Today);
            pb.Add(a => a.OnValueChanged, d =>
            {
                v = d;
                return Task.CompletedTask;
            });
        });
        Assert.Contains("table-week", cut.Markup);

        await cut.InvokeAsync(() =>
        {
            var buttons = cut.FindAll(".calendar-button-group button");
            // 上一周
            buttons[0].Click();
        });
        Assert.NotEqual(v, DateTime.MinValue);
    }

    [Fact]
    public void FirstDayOfWeek_Ok()
    {
        var cut = Context.RenderComponent<Calendar>(pb =>
        {
            pb.Add(a => a.Value, new DateTime(2025, 02, 20));
            pb.Add(a => a.FirstDayOfWeek, DayOfWeek.Monday);
        });
        var labels = cut.FindAll(".calendar-table thead > tr > th");
        Assert.Equal("一", labels[0].TextContent);
        Assert.Equal("日", labels[6].TextContent);
    }
}
