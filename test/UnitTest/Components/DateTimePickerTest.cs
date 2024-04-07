// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using AngleSharp.Dom;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class DateTimePickerTest : BootstrapBlazorTestBase
{
    #region DateTimePicker
    [Fact]
    public void AutoToday_DateTime()
    {
        // 设置为 最小值或者 null 时 当 AutoToday 为 true 时自动设置为当前时间
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.AutoToday, true);
            pb.Add(a => a.Value, DateTime.MinValue);
        });
        Assert.Equal(DateTime.Today, cut.Instance.Value);

        var input = cut.Find(".datetime-picker-input");
        Assert.Equal(DateTime.Today.ToString("yyyy-MM-dd"), input.GetAttribute("value"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.AutoToday, false);
            pb.Add(a => a.Value, DateTime.MinValue);
        });
        Assert.Equal(DateTime.MinValue, cut.Instance.Value);
        input = cut.Find(".datetime-picker-input");
        Assert.Equal(DateTime.MinValue.ToString("yyyy-MM-dd"), input.GetAttribute("value"));
    }

    [Fact]
    public void AutoToday_DateTimeOffset()
    {
        // 设置为 最小值或者 null 时 当 AutoToday 为 true 时自动设置为当前时间
        var cut = Context.RenderComponent<DateTimePicker<DateTimeOffset>>(pb =>
        {
            pb.Add(a => a.AutoToday, true);
            pb.Add(a => a.Value, DateTimeOffset.MinValue);
        });
        Assert.Equal(new DateTimeOffset(DateTime.Today), cut.Instance.Value);

        var input = cut.Find(".datetime-picker-input");
        Assert.Equal($"{DateTimeOffset.Now:yyyy-MM-dd}", input.GetAttribute("value"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.AutoToday, false);
            pb.Add(a => a.Value, DateTimeOffset.MinValue);
        });
        Assert.Equal(DateTimeOffset.MinValue, cut.Instance.Value);
        input = cut.Find(".datetime-picker-input");
        Assert.Equal(DateTimeOffset.MinValue.ToString("yyyy-MM-dd"), input.GetAttribute("value"));
    }

    [Fact]
    public void DisplayMinValueAsEmpty_NullableDateTime()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime?>>(pb =>
        {
            pb.Add(a => a.Value, null);
        });
        Assert.Null(cut.Instance.Value);

        var input = cut.Find(".datetime-picker-input");
        Assert.Equal("", input.GetAttribute("value"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, DateTime.MinValue);
            pb.Add(a => a.DisplayMinValueAsEmpty, true);
        });
        Assert.Null(cut.Instance.Value);
        input = cut.Find(".datetime-picker-input");
        Assert.Equal("", input.GetAttribute("value"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, DateTime.MinValue);
            pb.Add(a => a.DisplayMinValueAsEmpty, false);
        });
        Assert.Equal(DateTime.MinValue, cut.Instance.Value);
        input = cut.Find(".datetime-picker-input");
        Assert.Equal($"{DateTime.MinValue:yyyy-MM-dd}", input.GetAttribute("value"));
    }

    [Fact]
    public void DisplayMinValueAsEmpty_NullableDateTimeOffset()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTimeOffset?>>(pb =>
        {
            pb.Add(a => a.Value, null);
        });
        Assert.Null(cut.Instance.Value);

        var input = cut.Find(".datetime-picker-input");
        Assert.Equal("", input.GetAttribute("value"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, DateTimeOffset.MinValue);
        });
        Assert.Null(cut.Instance.Value);
        input = cut.Find(".datetime-picker-input");
        Assert.Equal("", input.GetAttribute("value"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, DateTimeOffset.MinValue);
            pb.Add(a => a.DisplayMinValueAsEmpty, false);
        });
        Assert.Equal(DateTimeOffset.MinValue, cut.Instance.Value);
        input = cut.Find(".datetime-picker-input");
        Assert.Equal($"{DateTimeOffset.MinValue:yyyy-MM-dd}", input.GetAttribute("value"));
    }

    [Fact]
    public void OnClear_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime?>>(pb =>
        {
            pb.Add(a => a.AutoToday, false);
            pb.Add(a => a.DisplayMinValueAsEmpty, false);
            pb.Add(a => a.Value, null);
        });

        // 点击 0001-01-01 单元格
        var cell = cut.Find(".current .cell");
        cut.InvokeAsync(() => cell.Click());
        // 文本框内容
        var input = cut.Find(".datetime-picker-input");
        Assert.Equal($"{DateTime.MinValue:yyyy-MM-dd}", input.GetAttribute("value"));

        // 点击清空按钮
        var clear = cut.Find(".picker-panel-footer button");
        cut.InvokeAsync(() => clear.Click());

        // 文本框内容 为 ""
        input = cut.Find(".datetime-picker-input");
        Assert.Equal("", input.GetAttribute("value"));

        // 设置 DisplayMinValueAsEmpty="true" MinValue 自动为 ToDay
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DisplayMinValueAsEmpty, true);
        });
        cell = cut.Find(".current .cell");
        cut.InvokeAsync(() => cell.Click());

        // 文本框内容
        input = cut.Find(".datetime-picker-input");
        Assert.Equal($"{DateTime.Today:yyyy-MM-dd}", input.GetAttribute("value"));

        // 点击清空按钮
        clear = cut.Find(".picker-panel-footer button");
        cut.InvokeAsync(() => clear.Click());

        // 文本框内容 为 ""
        input = cut.Find(".datetime-picker-input");
        Assert.Equal("", input.GetAttribute("value"));
    }

    [Fact]
    public void ShowSiderBar_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(builder => builder.Add(a => a.ShowSidebar, true));

        var ele = cut.Find(".picker-panel-sidebar");
        Assert.NotNull(ele);
    }

    [Fact]
    public void Placement_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(builder => builder.Add(a => a.Placement, Placement.Top));

        Assert.Contains("data-bs-placement=\"top\"", cut.Markup);
    }

    [Fact]
    public void DateTimeFormat_OK()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.DateFormat, "yyyy/MM/dd");
        });

        var value = cut.Find(".datetime-picker-input").GetAttribute("value");

        Assert.Equal(value, DateTime.Now.ToString("yyyy/MM/dd"));
    }

    [Fact]
    public void MaxValue_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(builder => builder.Add(a => a.MaxValue, DateTime.Today.AddDays(1)));
    }

    [Fact]
    public void MinValue_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(builder => builder.Add(a => a.MinValue, DateTime.Today.AddDays(-1)));
    }

    [Fact]
    public void OnTimeChanged_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(builder =>
        {
            builder.Add(a => a.Value, new DateTime(2023, 10, 1, 1, 0, 0));
            builder.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
        });

        var panel = cut.FindComponent<ClockPicker>();
        cut.InvokeAsync(() => panel.Instance.SetTime(0, 0, 0));
        // 点击确定
        var buttons = cut.FindAll(".picker-panel-footer button");
        cut.InvokeAsync(() => buttons[1].Click());

        var body = cut.FindComponent<DatePickerBody>();
        Assert.Equal(TimeSpan.Zero, body.Instance.Value.TimeOfDay);
    }

    [Fact]
    public void SwitchTimeView_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(builder =>
        {
            builder.Add(a => a.Value, new DateTime(2023, 10, 1, 1, 0, 0));
            builder.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
        });

        var labels = cut.FindAll(".picker-panel-header-label");
        cut.InvokeAsync(() => labels[2].Click());

        cut.Contains("picker-pannel-body-main-wrapper is-open");
    }

    [Fact]
    public void ValidateForm_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<DateTimePicker<DateTime?>>(pb =>
            {
                pb.Add(a => a.Value, foo.DateTime);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression(nameof(Foo.DateTime), typeof(DateTime?)));
            });
        });
        cut.Contains("class=\"form-label\"");
    }

    [Fact]
    public void NotDateTime_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() =>
        {
            Context.RenderComponent<DateTimePicker<int>>();
        });
    }
    #endregion

    #region DatePicker
    [Fact]
    public void DateFormat_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.ShowFooter, false);
            builder.Add(a => a.Value, DateTime.Today.AddDays(-1));
            builder.Add(a => a.DateFormat, "yyyy/MM/dd");
        });

        cut.InvokeAsync(() => cut.Find(".current .cell").Click());
    }

    [Fact]
    public void ShowIcon_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.ShowIcon, false);
        });
        cut.DoesNotContain("datetime-picker-bar");
    }

    [Fact]
    public void Format_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.DateTimeFormat, "yyyy-MM-dd HH:mm:ss");
        });

        var body = cut.FindComponent<DatePickerBody>();
        Assert.Equal("yyyy-MM-dd", body.Instance.DateFormat);
    }

    [Fact]
    public void ShowLunar_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.ShowLunar, true);
            pb.Add(a => a.Value, new DateTime(2024, 6, 10));
        });

        cut.Contains("三十");
        cut.Contains("廿一");
        cut.Contains("初二");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, new DateTime(2023, 3, 22));
        });
        cut.Contains("闰二月");
    }

    [Fact]
    public void ShowSolarTerm_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.ShowLunar, true);
            pb.Add(a => a.ShowSolarTerm, true);
            pb.Add(a => a.Value, new DateTime(2024, 3, 5));
        });
        cut.Contains("惊蛰");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFestivals, true);
        });
        cut.Contains("妇女节");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, new DateTime(2023, 2, 20));
        });
        cut.Contains("二月");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, new DateTime(2023, 3, 22));
        });
        cut.Contains("闰二月");
    }

    [Fact]
    public void ShowHolidays_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.ShowHolidays, true);
            pb.Add(a => a.Value, new DateTime(2024, 3, 5));
        });
        cut.DoesNotContain("休");
    }

    [Fact]
    public void ShowWorkdays_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.ShowHolidays, true);
            pb.Add(a => a.Value, new DateTime(2024, 4, 7));
        });
        cut.DoesNotContain("班");
    }

    [Fact]
    public void ShowHolidays_Custom()
    {
        var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var services = context.Services;
        services.AddBootstrapBlazor();
        services.AddSingleton<ICalendarHolidays, MockCalendarHolidayService>();

        var cut = context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.ShowHolidays, true);
            pb.Add(a => a.Value, new DateTime(2024, 3, 17));
        });
        cut.Contains("休");
    }

    [Fact]
    public void ShowWorkdays_Custom()
    {
        var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        var services = context.Services;
        services.AddBootstrapBlazor();
        services.AddSingleton<ICalendarHolidays, MockCalendarHolidayService>();

        var cut = context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.ShowHolidays, true);
            pb.Add(a => a.Value, new DateTime(2024, 4, 7));
        });
        cut.Contains("班");
    }

    class MockCalendarHolidayService : ICalendarHolidays
    {
        public bool IsHoliday(DateTime dt) => dt == new DateTime(2024, 3, 17);
        public bool IsWorkday(DateTime dt) => dt == new DateTime(2024, 4, 7);
    }

    [Fact]
    public void DayTemplate_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.DayTemplate, dt => builder =>
            {
                builder.AddContent(0, "day-template");
            });
        });

        cut.Contains("day-template");
    }

    [Fact]
    public void DayDisabledTemplate_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.DayDisabledTemplate, dt => builder =>
            {
                builder.AddContent(0, "day-disabled-template");
            });
            pb.Add(a => a.MinValue, new DateTime(2024, 3, 7));
            pb.Add(a => a.MaxValue, new DateTime(2024, 3, 17));
            pb.Add(a => a.Value, new DateTime(2024, 3, 10));
        });

        cut.Contains("day-disabled-template");
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowLunar, true);
            pb.Add(a => a.ShowSolarTerm, true);
        });
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFestivals, true);
        });
    }

    [Fact]
    public void DatePickerViewModel_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.ViewMode, DatePickerViewMode.Year);
        });

        var labels = cut.FindAll(".picker-panel-header-label");
        Assert.Equal(GetYearPeriod(), labels[0].TextContent);

        // 上一年
        var buttons = cut.FindAll(".picker-panel-header button");
        cut.InvokeAsync(() => buttons[0].Click());

        // 下一年
        cut.InvokeAsync(() => buttons[3].Click());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ViewMode, DatePickerViewMode.Month);
            pb.Add(a => a.Value, GetToday());
        });

        DateTime GetToday()
        {
            var buffer = 1;
            var month = DateTime.Today.Month + buffer;
            if (month > 6)
            {
                buffer = -1;
            }
            return DateTime.Today.AddMonths(month);
        }

        string GetYearPeriod()
        {
            var start = DateTime.Today.AddYears(0 - DateTime.Today.Year % 20).Year;
            return string.Format("{0} 年 - {1} 年", start, start + 19);
        }
    }

    [Fact]
    public async Task IsYearOverflow_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.MinValue.AddDays(1));
            builder.Add(a => a.ViewMode, DatePickerViewMode.Year);
        });

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, DateTime.MaxValue);
            pb.Add(a => a.ViewMode, DatePickerViewMode.Date);
        });

        var buttons = cut.FindAll(".picker-panel-header button");
        // 下一月
        await cut.InvokeAsync(() => buttons[2].Click());
        // 下一年
        await cut.InvokeAsync(() => buttons[3].Click());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, DateTime.MinValue);
            pb.Add(a => a.ViewMode, DatePickerViewMode.Date);
        });

        buttons = cut.FindAll(".picker-panel-header button");
        // 上一年
        await cut.InvokeAsync(() => buttons[0].Click());
        // 上一月
        await cut.InvokeAsync(() => buttons[1].Click());
    }

    [Fact]
    public void IsDayOverflow()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.MaxValue.AddDays(-1));
            builder.Add(a => a.ViewMode, DatePickerViewMode.Date);
        });
    }

    [Fact]
    public void ShowSidebar_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.ShowSidebar, true);
        });

        var ele = cut.Find(".picker-panel-sidebar");
        Assert.NotNull(ele);

        var cells = cut.FindAll(".cell");
        cut.InvokeAsync(() => cells[0].Click());
        cut.InvokeAsync(() => cells[1].Click());
        cut.InvokeAsync(() => cells[2].Click());
    }

    [Fact]
    public void ShowButtons_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
        });

        var buttons = cut.FindAll(".picker-panel-header button");

        // 上一年
        cut.InvokeAsync(() => buttons[0].Click());
        var labels = cut.FindAll(".picker-panel-header-label");
        Assert.Equal(DateTime.Today.AddYears(-1).Year.ToString() + " 年", labels[0].TextContent);

        // 下一年
        cut.InvokeAsync(() => buttons[3].Click());
        labels = cut.FindAll(".picker-panel-header-label");
        Assert.Equal(DateTime.Today.Year.ToString() + " 年", labels[0].TextContent);

        // 上一月
        cut.InvokeAsync(() => buttons[1].Click());
        labels = cut.FindAll(".picker-panel-header-label");
        Assert.Equal(DateTime.Today.AddMonths(-1).Month.ToString() + " 月", labels[1].TextContent);

        // 下一月
        cut.InvokeAsync(() => buttons[2].Click());
        labels = cut.FindAll(".picker-panel-header-label");
        Assert.Equal(DateTime.Today.Month.ToString() + " 月", labels[1].TextContent);

        // 年视图
        labels = cut.FindAll(".picker-panel-header-label");
        cut.InvokeAsync(() => labels[0].Click());
        cut.Contains("class=\"year-table\"");

        cut.InvokeAsync(() => cut.Find(".year-table .current.today .cell").Click());
    }

    [Fact]
    public void NotShowButtons_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.ShowLeftButtons, false);
        });
        Assert.DoesNotContain("fa-solid fa-angles-left", cut.Find(".picker-panel-header").ToMarkup());

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowRightButtons, false));
        Assert.DoesNotContain("fa-solid fa-angles-right", cut.Find(".picker-panel-header").ToMarkup());
    }

    [Fact]
    public void MonthView_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
        });
        var labels = cut.FindAll(".picker-panel-header-label");
        cut.InvokeAsync(() => labels[1].Click());
        cut.Contains("class=\"month-table\"");

        cut.InvokeAsync(() => cut.Find(".month-table .current.today .cell").Click());
    }

    [Fact]
    public void IsDisabledCell_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.MinValue, DateTime.Today.AddDays(-1));
        });

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.MinValue, null);
            pb.Add(a => a.MaxValue, DateTime.Today.AddDays(7));
        });
    }

    [Fact]
    public void ShowFooter_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.ShowFooter, true);
            builder.Add(a => a.ShowClearButton, true);
        });

        var ele = cut.Find(".picker-panel-footer");
        Assert.NotNull(ele);

        var buttons = cut.FindAll(".picker-panel-footer button");

        // Click Now
        cut.InvokeAsync(() => buttons[1].Click());
        Assert.Equal(DateTime.Today, cut.Instance.Value.Date);

        // Click Clear Button
        cut.InvokeAsync(() => buttons[0].Click());
        Assert.Equal(DateTime.MinValue, cut.Instance.Value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFooter, false);
        });
        cut.DoesNotContain("picker-panel-footer");
    }

    [Fact]
    public void ClickNowButton_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
            builder.Add(a => a.ShowFooter, true);
            builder.Add(a => a.Value, DateTime.Today.AddDays(-10));
        });
        var button = cut.Find(".is-now");
        cut.InvokeAsync(() => button.Click());

        // 有最小值 无 Now 按钮
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.MinValue, DateTime.Today.AddDays(10));
        });
        cut.DoesNotContain(".picker-panel-footer .is-now");
    }

    [Fact]
    public void ClickDay_Validate()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
            builder.Add(a => a.ShowFooter, false);
            builder.Add(a => a.Value, DateTime.Today);
        });

        // 不显示 Footer 点击日期直接确认 OnConfirm
        var cell = cut.Find(".current.today .cell");
        cut.InvokeAsync(() => cell.Click());
    }

    [Fact]
    public void HeightCallback_Ok()
    {
        var cut = Context.RenderComponent<TimePickerBody>();
        var cell = cut.FindComponent<TimePickerCell>();
        cut.InvokeAsync(() => cell.Instance.OnHeightCallback(16));
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromSeconds(1));
        });
    }

    [Fact]
    public async Task OnClickClose_Ok()
    {
        var cut = Context.RenderComponent<TimePickerBody>();
        var btn = cut.Find(".time-panel-footer > button");
        await cut.InvokeAsync(() => btn.Click());
    }

    [Fact]
    public async Task OnClickConfirm_Ok()
    {
        var cut = Context.RenderComponent<TimePickerBody>();
        var buttons = cut.FindAll(".time-panel-footer > button");
        await cut.InvokeAsync(() => buttons[1].Click());
    }

    [Fact]
    public void Validate_Ok()
    {
        // (!MinValue.HasValue || Value >= MinValue.Value) && (!MaxValue.HasValue || Value <= MaxValue.Value)
        var cut = Context.RenderComponent<DatePickerBody>(pb =>
        {
            pb.Add(a => a.ShowFooter, true);
            pb.Add(a => a.MinValue, DateTime.Now.AddDays(-1));
            pb.Add(a => a.Value, DateTime.Now);
        });
        var button = cut.Find(".is-confirm");
        cut.InvokeAsync(() => button.Click());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, DateTime.Now.AddDays(-2));
        });
        button = cut.Find(".is-confirm");
        cut.InvokeAsync(() => button.Click());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.MinValue, null);
            pb.Add(a => a.MaxValue, DateTime.Now.AddDays(1));
            pb.Add(a => a.Value, DateTime.Now);
        });
        button = cut.Find(".is-confirm");
        cut.InvokeAsync(() => button.Click());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.MaxValue, DateTime.Now.AddDays(1));
            pb.Add(a => a.Value, DateTime.Now.AddDays(2));
        });
        button = cut.Find(".is-confirm");
        cut.InvokeAsync(() => button.Click());
    }

    [Fact]
    public void IsDisabled_Ok()
    {
        // MinValue != null && MaxValue != null && (day < MinValue || day > MaxValue)
        var cut = Context.RenderComponent<DatePickerBody>(pb =>
        {
            pb.Add(a => a.MinValue, new DateTime(2022, 02, 15));
            pb.Add(a => a.MaxValue, new DateTime(2022, 02, 17));
            pb.Add(a => a.Value, new DateTime(2022, 02, 16));
        });

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.MinValue, null);
        });
    }
    #endregion

    #region TimePicker
    [Fact]
    public void OnClose_Ok()
    {
        var res = false;
        var cut = Context.RenderComponent<TimePickerBody>(builder =>
        {
            builder.Add(a => a.Value, TimeSpan.FromDays(1));
            builder.Add(a => a.OnClose, () =>
            {
                res = true;
                return Task.CompletedTask;
            });
        });

        cut.Find(".time-panel-footer .cancel").Click();

        Assert.True(res);
    }

    [Fact]
    public void OnConfirm_Ok()
    {
        var res = false;
        var value = false;
        var cut = Context.RenderComponent<TimePickerBody>(builder =>
        {
            builder.Add(a => a.Value, TimeSpan.FromDays(1));
            builder.Add(a => a.OnConfirm, val =>
            {
                value = true;
                res = true;
                return Task.CompletedTask;
            });
        });

        cut.Find(".time-panel-footer .confirm").Click();

        Assert.True(res);
        Assert.True(value);
    }

    [Fact]
    public void HasSeconds_Ok()
    {
        var cut = Context.RenderComponent<TimePickerBody>(builder =>
        {
            builder.Add(a => a.Value, TimeSpan.FromDays(1));
            builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<TimeSpan>(this, t =>
            {
            }));
        });

        var ele = cut.Find(".time-panel-content .has-seconds");
        Assert.NotNull(ele);
    }

    [Fact]
    public void HaveNotSeconds_Ok()
    {
        var cut = Context.RenderComponent<TimePickerBody>(builder =>
        {
            builder.Add(a => a.Value, TimeSpan.FromDays(1));
            builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<TimeSpan>(this, t =>
            {
            }));
            builder.Add(a => a.HasSeconds, false);
        });

        var ele = cut.Find(".time-panel-content .havenot-seconds");
        Assert.NotNull(ele);
    }
    #endregion

    #region TimeCell
    [Theory]
    [InlineData(TimePickerCellViewMode.Hour)]
    [InlineData(TimePickerCellViewMode.Minute)]
    [InlineData(TimePickerCellViewMode.Second)]
    public async Task TimeCell_Up(TimePickerCellViewMode viewMode)
    {
        var valueChanged = false;
        var cut = Context.RenderComponent<TimePickerCell>(pb =>
        {
            pb.Add(a => a.ViewMode, viewMode);
            pb.Add(a => a.Value, new TimeSpan(6, 6, 6));
            pb.Add(a => a.ValueChanged, v =>
            {
                valueChanged = true;
            });
        });
        var i = cut.Find(".time-spinner-arrow.fa-angle-up");
        await cut.InvokeAsync(() => i.Click());
        Assert.True(valueChanged);

        i = cut.Find(".time-spinner-arrow.fa-angle-down");
        await cut.InvokeAsync(() => i.Click());
        Assert.True(valueChanged);
    }

    [Fact]
    public async Task TimeCell_OverDay()
    {
        var ts = new TimeSpan(23, 59, 59);
        var cut = Context.RenderComponent<TimePickerCell>(pb =>
        {
            pb.Add(a => a.ViewMode, TimePickerCellViewMode.Second);
            pb.Add(a => a.Value, ts);
            pb.Add(a => a.ValueChanged, v =>
            {
                ts = v;
            });
        });

        var i = cut.Find(".time-spinner-arrow.fa-angle-down");
        await cut.InvokeAsync(() => i.Click());
        Assert.Equal(0, ts.Days);
        Assert.Equal(TimeSpan.Zero, ts);

        i = cut.Find(".time-spinner-arrow.fa-angle-up");
        await cut.InvokeAsync(() => i.Click());
        Assert.Equal(new TimeSpan(23, 59, 59), ts);
    }
    #endregion

    [Fact]
    public async Task ViewMode_DateTime()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
            builder.Add(a => a.Value, DateTime.Today.AddDays(-10));
        });

        // 选择年
        var buttons = cut.FindAll(".picker-panel-header button");
        await cut.InvokeAsync(() => buttons[2].Click());

        var button = cut.Find(".year-table tbody .cell");
        await cut.InvokeAsync(() => button.Click());

        button = cut.Find(".month-table tbody .cell");
        await cut.InvokeAsync(() => button.Click());

        cut.Contains("class=\"date-table\"");
    }

    [Fact]
    public async Task AutoClose_OK()
    {
        DateTime val = DateTime.MinValue;
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Today.AddDays(-10));
            builder.Add(a => a.AutoClose, false);
            builder.Add(a => a.OnValueChanged, dt =>
            {
                val = dt;
                return Task.CompletedTask;
            });
        });
        var input = cut.Find(".datetime-picker-input");
        Assert.Equal($"{DateTime.Today.AddDays(-10):yyyy-MM-dd}", input.GetAttribute("value"));

        // 点击当前日期不触发 OnValueChanged
        var button = cut.Find(".picker-panel-content .cell");
        await cut.InvokeAsync(() => button.Click());
        Assert.Equal(val, DateTime.MinValue);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.AutoClose, true);
        });
        await cut.InvokeAsync(() => button.Click());
        Assert.NotEqual(val, DateTime.MinValue);
    }

    [Fact]
    public void SidebarTemplate_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.ShowSidebar, true);
            pb.Add(a => a.SidebarTemplate, new RenderFragment<Func<DateTime, Task>>(cb => builder =>
            {
                builder.AddContent(0, "test-sidebar-template");
            }));
        });
        cut.Contains("test-sidebar-template");
    }

    [Fact]
    public void GetSafeYearDateTime_Ok()
    {
        Assert.True(MockDateTimePicker.GetSafeYearDateTime_Ok());
    }

    [Fact]
    public void PickerBodyShowFooter_Ok()
    {
        var confirm = false;
        var cut = Context.RenderComponent<DatePickerBody>(pb =>
        {
            pb.Add(a => a.ShowFooter, true);
            pb.Add(a => a.AutoClose, true);
            pb.Add(a => a.ViewMode, DatePickerViewMode.Month);
            pb.Add(a => a.OnConfirm, () =>
            {
                confirm = true;
                return Task.CompletedTask;
            });
        });

        // 设定为 月视图 切换 日视图时自动关闭触发 Confirm
        var cell = cut.Find(".month-table span.cell");
        cut.InvokeAsync(() => cell.Click());
        Assert.True(confirm);

        // 设置 AutoClose = false 不会触发 confirm
        confirm = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.AutoClose, false);
        });
        cut.InvokeAsync(() => cell.Click());
        Assert.False(confirm);
    }

    [Fact]
    public void OnClickShortLink_Ok()
    {
        var confirm = false;
        var cut = Context.RenderComponent<DatePickerBody>(pb =>
        {
            pb.Add(a => a.ShowFooter, false);
            pb.Add(a => a.AutoClose, true);
            pb.Add(a => a.ShowSidebar, true);
            pb.Add(a => a.ViewMode, DatePickerViewMode.Month);
            pb.Add(a => a.OnConfirm, () =>
            {
                confirm = true;
                return Task.CompletedTask;
            });
        });

        var cell = cut.Find(".sidebar-item span.cell");
        cut.InvokeAsync(() => cell.Click());
        Assert.True(confirm);

        confirm = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFooter, true);
            pb.Add(a => a.AutoClose, false);
        });
        cut.InvokeAsync(() => cell.Click());
        Assert.True(confirm);

        // 不显示 Footer AutoClose 参数不起作用自动关闭
        confirm = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFooter, false);
            pb.Add(a => a.AutoClose, false);
        });
        cut.InvokeAsync(() => cell.Click());
        Assert.True(confirm);
    }

    [Fact]
    public void FormatValueAsString_Ok()
    {
        // 设置为 最小值或者 null 时 当 AutoToday 为 true 时自动设置为当前时间
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.AutoToday, true);
            pb.Add(a => a.Value, DateTime.MinValue);
        });
        Assert.Equal(DateTime.Today, cut.Instance.Value);

        var picker = cut.Instance;
        var mi = picker.GetType().GetMethod("FormatValueAsString", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var v = mi.Invoke(picker, [DateTime.MinValue]);
        Assert.Equal($"{DateTime.Today:yyyy-MM-dd}", v);
    }

    [Fact]
    public async Task IsEditable_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.IsEditable, true);
            pb.Add(a => a.ViewMode, DatePickerViewMode.Date);
            pb.Add(a => a.DateFormat, "MM/dd/yyyy");
        });
        var input = cut.Find(".datetime-picker-input");
        Assert.False(input.HasAttribute("readonly"));

        // input value
        await cut.InvokeAsync(() =>
        {
            input.Change("02/15/2024");
        });
        Assert.Equal("02/15/2024", cut.Instance.Value.ToString("MM/dd/yyyy"));

        // error input value
        await cut.InvokeAsync(() =>
        {
            input.Change("test");
        });
        Assert.Equal("02/15/2024", cut.Instance.Value.ToString("MM/dd/yyyy"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
            pb.Add(a => a.DateTimeFormat, "MM/dd/yyyy HH:mm:ss");
        });
        await cut.InvokeAsync(() =>
        {
            input.Change("02/15/2024 01:00:00");
        });
        Assert.Equal("02/15/2024 01:00:00", cut.Instance.Value.ToString("MM/dd/yyyy HH:mm:ss"));
    }

    class MockDateTimePicker : DatePickerBody
    {
        public static bool GetSafeYearDateTime_Ok()
        {
            var dtm = GetSafeYearDateTime(DateTime.MaxValue, 1);
            return dtm == DateTime.MaxValue.Date;
        }
    }
}
