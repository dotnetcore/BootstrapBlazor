// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using AngleSharp.Dom;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace UnitTest.Components;

public class DateTimeRangeTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ClearButtonText_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue());
            builder.Add(a => a.ClearButtonText, "Clear");
        });
        Assert.Equal("Clear", cut.Find(".is-clear").TextContent);
    }

    [Fact]
    public void ShowLunar_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue());
            builder.Add(a => a.ShowLunar, true);
            builder.Add(a => a.ShowSolarTerm, true);
            builder.Add(a => a.ShowFestivals, true);
            builder.Add(a => a.ShowHolidays, true);
        });
        var body = cut.FindComponent<DatePickerBody>();
        Assert.True(body.Instance.ShowLunar);
        Assert.True(body.Instance.ShowSolarTerm);
        Assert.True(body.Instance.ShowFestivals);
        Assert.True(body.Instance.ShowHolidays);
    }

    [Fact]
    public void RenderMode_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue());
            builder.Add(a => a.RenderMode, DateTimeRangeRenderMode.Single);
            builder.Add(a => a.TimeFormat, "HH:mm:ss");
        });

        var body = cut.FindComponents<DatePickerBody>();
        Assert.Single(body);
    }

    [Fact]
    public void StarEqualEnd_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue() { Start = DateTime.Now, End = DateTime.Now.AddDays(1) });
        });

        var v = cut.Instance.Value;
        Assert.NotEqual(DateTime.Today, v.Start);
        Assert.NotEqual(DateTime.Today.AddDays(2).AddSeconds(-1), v.End);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, new DateTimeRangeValue() { Start = DateTime.Today, End = DateTime.Today.AddDays(1) });
        });
        v = cut.Instance.Value;
        Assert.Equal(DateTime.Today, v.Start);
        Assert.Equal(DateTime.Today.AddDays(1), v.End);
    }

    [Fact]
    public async Task RangeValue_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>();
        var cells = cut.FindAll(".date-table tbody span");
        var end = cells.First(i => i.TextContent == "7");
        await cut.InvokeAsync(() =>
        {
            end.Click();
        });

        cells = cut.FindAll(".date-table tbody span");
        var first = cells.First(i => i.TextContent == "1");
        await cut.InvokeAsync(() =>
        {
            first.Click();
        });

        // confirm
        var confirm = cut.FindAll(".is-confirm")[cut.FindAll(".is-confirm").Count - 1];
        await cut.InvokeAsync(() =>
        {
            confirm.Click();
        });

        var value = cut.Instance.Value;
        var startDate = DateTime.Today.AddDays(1 - DateTime.Today.Day).AddMonths(-1);
        var endDate = startDate.AddDays(7).AddSeconds(-1);
        Assert.Equal(startDate, value.Start);
        Assert.Equal(endDate, value.End);
    }

    [Fact]
    public void OnTimeChanged_Ok()
    {
        // TODO: 等待 Range 支持 DateTime 模式
        //var cut = Context.RenderComponent<DateTimeRange>(builder =>
        //{
        //    builder.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
        //});

        //var panel = cut.FindComponent<ClockPicker>();
        //cut.InvokeAsync(() => panel.Instance.SetTime(0, 0, 0));

        //var body = cut.FindComponent<DatePickerBody>();
        //Assert.Equal(TimeSpan.Zero, body.Instance.Value.TimeOfDay);
    }

    [Fact]
    public void TodayButtonText_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.TodayButtonText, "Today");
            builder.Add(a => a.ShowToday, true);
        });
        var today = cut.FindAll("button").FirstOrDefault(s => s.TextContent == "Today");
        Assert.NotNull(today);
    }

    [Fact]
    public void ConfirmButtonText_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.ConfirmButtonText, "Confirm");
            builder.Add(a => a.ShowToday, true);
        });
        var today = cut.FindAll("button").FirstOrDefault(s => s.TextContent == "Confirm");
        Assert.NotNull(today);
    }

    [Fact]
    public void Placement_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.Placement, Placement.Top);
        });
        cut.Contains($"data-bs-placement=\"{Placement.Top.ToDescriptionString()}\"");
    }

    [Fact]
    public void ShowClearButton_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.ShowClearButton, true);
        });
        Assert.True(cut.Instance.ShowClearButton);
    }

    [Fact]
    public void NullValue_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>();
        Assert.NotNull(cut.Instance.Value);
        Assert.Equal(DateTime.MinValue, cut.Instance.Value.Start);
        Assert.Equal(DateTime.MinValue, cut.Instance.Value.End);
        Assert.Null(cut.Instance.Value.NullStart);
        Assert.Null(cut.Instance.Value.NullEnd);

        cut.Instance.Value.NullStart = DateTime.Now;
        Assert.NotNull(cut.Instance.Value.NullStart);
        cut.Instance.Value.NullEnd = DateTime.Now;
        Assert.NotNull(cut.Instance.Value.NullEnd);

        cut.Instance.Value.NullStart = null;
        Assert.Equal(DateTime.MinValue, cut.Instance.Value.Start);
        cut.Instance.Value.NullEnd = null;
        Assert.Equal(DateTime.MinValue, cut.Instance.Value.End);
    }

    [Fact]
    public void ShowSidebar_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.ShowSidebar, true);
        });

        Assert.NotNull(cut.Find(".picker-panel-sidebar"));
    }

    [Fact]
    public void SidebarItems_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.ShowSidebar, true);
            builder.Add(a => a.AutoCloseClickSideBar, true);
            builder.Add(a => a.SidebarItems,
            [
                new DateTimeRangeSidebarItem(){ Text = "Test" }
            ]);
        });

        var item = cut.Find(".sidebar-item > div");
        var text = item.TextContent;
        Assert.Equal("Test", text);

        cut.InvokeAsync(() => item.Click());
    }

    [Fact]
    public async Task OnConfirm_Ok()
    {
        var value = false;
        var cut = Context.RenderComponent<DateTimeRange>(pb =>
        {
            pb.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.MinValue });
            pb.Add(a => a.ValueChanged, v => _ = v);
            pb.Add(a => a.OnValueChanged, v => Task.CompletedTask);
            pb.Add(a => a.DateFormat, "MM/dd/yyyy");
            pb.Add(a => a.OnConfirm, (e) =>
            {
                value = true;
                return Task.CompletedTask;
            });
        });

        await cut.InvokeAsync(() =>
        {
            // 选择开始未选择结束
            cut.Find(".cell").Click();
            var cells = cut.FindAll(".is-confirm");
            cells.First(s => s.TextContent == "确定").Click();

            // 选择时间大于当前时间
            cells = cut.FindAll(".date-table .cell");
            cells[cells.Count - 1].Click();
            cells = cut.FindAll(".is-confirm");
            cells.First(s => s.TextContent == "确定").Click();
        });
        Assert.True(value);

        var input = cut.Find(".datetime-range-input");
        Assert.False(input.ClassList.Contains("datetime"));
        Assert.True(DateTime.TryParseExact(input.GetAttribute("Value"), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _));

        // TODO: 等待 Range 支持 DateTime 模式
        // datetime
        //cut.SetParametersAndRender(pb =>
        //{
        //    pb.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
        //    pb.Add(a => a.DateTimeFormat, "MM/dd/yyyy HH:mm:ss");
        //});
        //await cut.InvokeAsync(() =>
        //{
        //    // 选择开始未选择结束
        //    cut.Find(".cell").Click();
        //    var cells = cut.FindAll(".is-confirm");
        //    cells.First(s => s.TextContent == "确定").Click();

        //    // 选择时间大于当前时间
        //    cells = cut.FindAll(".date-table .cell");
        //    cells[cells.Count - 1].Click();
        //    cells = cut.FindAll(".is-confirm");
        //    cells.First(s => s.TextContent == "确定").Click();
        //});
        //input = cut.Find(".datetime-range-input");
        //Assert.True(input.ClassList.Contains("datetime"));
        //Assert.True(DateTime.TryParseExact(input.GetAttribute("Value"), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _));

        // time format
        //cut.SetParametersAndRender(pb =>
        //{
        //    pb.Add(a => a.TimeFormat, "hhmmss");
        //});
        //var labels = cut.FindAll(".picker-panel-header-label");
        //Assert.Equal(6, labels.Count);

        //var timeLabel = labels[2];
        //timeLabel.MarkupMatches("<span role=\"button\" class=\"picker-panel-header-label\" diff:ignore>00000</span>");
    }

    [Fact]
    public void OnClearValue_Ok()
    {
        var value = false;
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.OnClearValue, (e) =>
            {
                value = true; return Task.CompletedTask;
            });
        });
        cut.FindAll(".is-confirm").First(s => s.TextContent == "清空").Click();
        Assert.True(value);
    }

    [Fact]
    public void OnValueChanged_Ok()
    {
        var value = new DateTimeRangeValue() { Start = DateTime.Now, End = DateTime.Now.AddDays(10) };
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.OnValueChanged, v => { value = v; return Task.CompletedTask; });
        });
        cut.FindAll(".is-confirm").First(s => s.TextContent == "清空").Click();
    }

    [Fact]
    public void ValueChanged_Ok()
    {
        var value = new DateTimeRangeValue() { Start = DateTime.Now, End = DateTime.Now.AddDays(10) };
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<DateTimeRangeValue?>(this, v => { value = v; }));
        });
        cut.FindAll(".is-confirm").First(s => s.TextContent == "清空").Click();
    }

    [Fact]
    public void ClickToday_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.ShowToday, true);
        });
        cut.FindAll(".is-confirm").First(s => s.TextContent == "今天").Click();
        Assert.Equal(DateTime.Today.Date, cut.Instance.Value.Start);
    }

    [Fact]
    public void OnClickSidebarItem_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.ShowSidebar, true);
            builder.Add(a => a.AutoCloseClickSideBar, true);
        });

        cut.Find(".sidebar-item > div").Click();

        // 得到最近一周时间范围
        Assert.Equal(DateTime.Today.AddDays(-7), cut.Instance.Value.Start);
        Assert.Equal(DateTime.Today.AddDays(1).AddSeconds(-1), cut.Instance.Value.End);
    }

    [Fact]
    public void UpdateValue_Year()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue());
        });

        cut.InvokeAsync(() =>
        {
            // 选择开始时间
            cut.Find(".date-table .cell").Click();
            // 选择结束时间
            cut.FindAll(".date-table .cell").ElementAt(2).Click();

            cut.Find(".date-table .cell").Click();
            cut.Find(".pick-panel-arrow-right").Click();

            var cells = cut.FindAll(".date-table .cell");
            cells[cells.Count - 1].Click();

            // 下一年
            cells = cut.FindAll(".picker-panel-icon-btn");
            cells[cells.Count - 1].Click();
            cut.Find(".date-table .cell").Click();

            cells = cut.FindAll(".date-table .cell");
            cells[cells.Count - 1].Click();
        });
    }

    [Fact]
    public void UpdateValue_Month()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue()
            {
                Start = new DateTime(2022, 11, 1, 0, 0, 0, DateTimeKind.Local),
                End = new DateTime(2022, 11, 14, 0, 0, 0, DateTimeKind.Local)
            });
        });

        // 翻页下一月
        var next = cut.Find(".picker-panel-icon-btn.pick-panel-arrow-right");
        next.Click();

        // 选择开始时间
        var cells = cut.FindAll(".date-table tbody .cell");
        cells.ElementAt(7).Click();

        // 选择结束时间
        cells = cut.FindAll(".date-table tbody .cell");
        cells.ElementAt(37).Click();

        // 选择开始时间
        cells = cut.FindAll(".date-table tbody .cell");
        cells.ElementAt(7).Click();

        // 选择结束时间
        cells = cut.FindAll(".date-table tbody .cell");
        cells.ElementAt(47).Click();

        // 没有点击确定 Value 值不变
        Assert.Equal(new DateTime(2022, 11, 1, 0, 0, 0, DateTimeKind.Local), cut.Instance.Value.Start);
        Assert.Equal(new DateTime(2022, 11, 14, 0, 0, 0, DateTimeKind.Local), cut.Instance.Value.End);
    }

    [Fact]
    public async Task InValidateForm_Ok()
    {
        var foo = new Dummy
        {
            Value = new DateTimeRangeValue()
        };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.ValidateAllProperties, true);
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<DateTimeRange>(pb =>
            {
                pb.Add(a => a.Value, foo.Value);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, nameof(Dummy.Value), typeof(DateTimeRangeValue)));
            });
        });
        // ValidateForm 自动自动生成标签
        cut.Contains("class=\"form-label\"");

        // 验证
        var validate = true;
        await cut.InvokeAsync(() => validate = cut.Instance.Validate());
        Assert.False(validate);

        var range = cut.FindComponent<DateTimeRange>();
        var clear = range.Find(".is-clear");
        clear.Click();

        range.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
            pb.Add(a => a.ShowClearButton, true);
        });
        await cut.InvokeAsync(() => clear.Click());

        foo.Value.Start = DateTime.Now;
        foo.Value.End = DateTime.Now;
        range.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, false);
        });
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Model, foo);
        });
        validate = false;
        await cut.InvokeAsync(() => validate = cut.Instance.Validate());
        Assert.True(validate);
    }

    [Fact]
    public async Task InValidateForm_Confirm()
    {
        var foo = new Dummy
        {
            Value = new DateTimeRangeValue()
        };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.ValidateAllProperties, true);
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<DateTimeRange>(pb =>
            {
                pb.Add(a => a.ShowClearButton, false);
                pb.Add(a => a.Value, foo.Value);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, nameof(Dummy.Value), typeof(DateTimeRangeValue)));
            });
        });

        var range = cut.FindComponent<DateTimeRange>();
        var confirm = range.Find(".is-confirm");
        await cut.InvokeAsync(() => confirm.Click());

        range.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        await cut.InvokeAsync(() => confirm.Click());
    }

    [Fact]
    public void PrevButton_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue());
        });

        var buttons = cut.FindAll(".picker-panel-header button");

        // 上一月
        cut.InvokeAsync(() => buttons[1].Click());

        // 上一年
        cut.InvokeAsync(() => buttons[0].Click());
    }

    [Fact]
    public void MaxValue_Ok()
    {
        var currentToday = DateTime.Today.AddDays(7 - DateTime.Today.Day);
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.MinValue, currentToday.AddDays(-2));
            builder.Add(a => a.MaxValue, currentToday.AddDays(2));
            builder.Add(a => a.Value, new DateTimeRangeValue());
        });
        var components = cut.FindComponents<DatePickerBody>();
        Assert.Equal(2, components.Count);
        foreach (var comp in components)
        {
            Assert.Equal(currentToday.AddDays(-2), comp.Instance.MinValue);
            Assert.Equal(currentToday.AddDays(2), comp.Instance.MaxValue);
        }
    }

    [Fact]
    public void ToString_Ok()
    {
        var v1 = new DateTimeRangeValue();
        Assert.Equal("", v1.ToString());

        v1.Start = DateTime.Today;
        Assert.Equal($"{DateTime.Today}", v1.ToString());

        v1.End = DateTime.Today;
        Assert.Equal($"{DateTime.Today} - {DateTime.Today}", v1.ToString());
    }

    private class Dummy
    {
        [Required]
        public DateTimeRangeValue? Value { get; set; }
    }

    [Fact]
    public async Task GetSafeStartValue_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue());
            builder.Add(a => a.ShowToday, true);
            builder.Add(a => a.ShowClearButton, false);
        });
        var button = cut.Find(".picker-panel-link-btn.is-confirm");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });

        Assert.Equal(DateTime.Today, cut.Instance.Value.Start);
        Assert.Equal(DateTime.Today.AddDays(1).AddSeconds(-1), cut.Instance.Value.End);
    }

    [Fact]
    public async Task IsEditable_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(pb =>
        {
            pb.Add(a => a.Value, new DateTimeRangeValue());
            pb.Add(a => a.IsEditable, true);
            pb.Add(a => a.ViewMode, DatePickerViewMode.Date);
            pb.Add(a => a.DateFormat, "MM/dd/yyyy");
        });
        var inputs = cut.FindAll(".datetime-range-input");
        Assert.False(inputs[0].HasAttribute("readonly"));
        Assert.False(inputs[1].HasAttribute("readonly"));

        // input value
        var input = cut.Find(".datetime-range-input");
        await cut.InvokeAsync(() =>
        {
            input.Change("02/15/2024");
        });

        inputs = cut.FindAll(".datetime-range-input");
        await cut.InvokeAsync(() =>
        {
            inputs[1].Change("02/16/2024");
        });
        Assert.Equal("02/15/2024", cut.Instance.Value.Start.ToString("MM/dd/yyyy"));
        Assert.Equal("02/16/2024", cut.Instance.Value.End.ToString("MM/dd/yyyy"));
    }

    [Fact]
    public async Task ViewMode_Year()
    {
        var cut = Context.RenderComponent<DateTimeRange>(pb =>
        {
            pb.Add(a => a.Value, new DateTimeRangeValue());
            pb.Add(a => a.IsEditable, true);
            pb.Add(a => a.ViewMode, DatePickerViewMode.Year);
            pb.Add(a => a.DateFormat, "yyyy");
        });

        var cells = cut.FindAll(".year-table .current > span");
        await cut.InvokeAsync(() => cells[0].Click());
        await cut.InvokeAsync(() => cells[1].Click());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, new DateTimeRangeValue() { Start = new DateTime(2023, 1, 1), End = new DateTime(2024, 1, 1) });
        });
        cells = cut.FindAll(".month-table .current > span");
        await cut.InvokeAsync(() => cells[0].Click());
        await cut.InvokeAsync(() => cells[1].Click());
    }

    [Fact]
    public async Task ViewMode_Month()
    {
        var cut = Context.RenderComponent<DateTimeRange>(pb =>
        {
            pb.Add(a => a.Value, new DateTimeRangeValue());
            pb.Add(a => a.IsEditable, true);
            pb.Add(a => a.ViewMode, DatePickerViewMode.Month);
            pb.Add(a => a.DateFormat, "yyyy-MM");
        });

        var cells = cut.FindAll(".month-table .current > span");
        await cut.InvokeAsync(() => cells[0].Click());
        await cut.InvokeAsync(() => cells[1].Click());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, new DateTimeRangeValue() { Start = new DateTime(2023, 1, 1), End = new DateTime(2024, 1, 1) });
        });
        cells = cut.FindAll(".month-table .current > span");
        await cut.InvokeAsync(() => cells[0].Click());
        await cut.InvokeAsync(() => cells[1].Click());
    }

    [Fact]
    public async Task TriggerHideCallback_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(pb =>
        {
            pb.Add(a => a.Value, new DateTimeRangeValue());
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerHideCallback());
    }
}
