// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class DateTimePickerTest : BootstrapBlazorTestBase
{
    #region DateTimePicker
    [Fact]
    public void Value_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(pb =>
        {
            pb.Add(a => a.Value, DateTime.MinValue);
        });
        // 设置 Value 为 MinValue 内部更改为 DateTime.Now
        Assert.Equal(DateTime.MinValue, cut.Instance.Value);
    }

    [Fact]
    public void AutoToday_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.Value, DateTime.MinValue);
        });
        Assert.Equal(DateTime.Today, cut.Instance.Value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.AutoToday, false);
            pb.Add(a => a.Value, DateTime.MinValue);
        });
        Assert.Equal(DateTime.MinValue, cut.Instance.Value);
    }

    [Fact]
    public void AllowNull_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime?>>(pb =>
        {
            pb.Add(a => a.Value, DateTime.MinValue);
        });
        Assert.Equal(DateTime.MinValue, cut.Instance.Value);
    }

    [Fact]
    public void DataTimeOffsetNull_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTimeOffset?>>(pb =>
        {
            pb.Add(a => a.Value, DateTimeOffset.MinValue);
        });
        Assert.Equal(DateTimeOffset.MinValue, cut.Instance.Value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, null);
            pb.Add(a => a.AutoToday, false);
        });
        Assert.Null(cut.Instance.Value);
    }

    [Fact]
    public void DataTimeOffset_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTimeOffset>>(pb =>
        {
            pb.Add(a => a.Value, DateTimeOffset.MinValue);
        });
        Assert.Equal(DateTimeOffset.MinValue, cut.Instance.Value);
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
    public void Format_OK()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.Format, "yyyy/MM/dd");
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
    public void OnValueChanged_Ok()
    {
        var res = false;
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.OnValueChanged, new Func<DateTime, Task>(d =>
            {
                res = true;
                return Task.CompletedTask;
            }));
        });

        cut.Find(".picker-panel-footer").Children.Last().Click();

        Assert.True(res);
    }

    [Fact]
    public void OnClear_Ok()
    {
        var changed = false;
        var cut = Context.RenderComponent<DateTimePicker<DateTime?>>(pb =>
        {
            pb.Add(a => a.OnValueChanged, v =>
            {
                changed = true;
                return Task.CompletedTask;
            });
        });
        Assert.Null(cut.Instance.Value);

        cut.InvokeAsync(() => cut.Find(".current .cell").Click());
        // confirm
        var buttons = cut.FindAll(".picker-panel-footer button");
        cut.InvokeAsync(() => buttons[2].Click());
        Assert.NotNull(cut.Instance.Value);
        Assert.True(changed);

        cut.InvokeAsync(() => buttons[0].Click());
        Assert.Null(cut.Instance.Value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.AutoToday, false);
        });
        cut.InvokeAsync(() => buttons[0].Click());
        Assert.Null(cut.Instance.Value);
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
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.DateFormat, "yyyy/MM/dd");
        });

        cut.InvokeAsync(() => cut.Find(".current .cell").Click());
        cut.Contains($"value=\"{DateTime.Today:yyyy/MM/dd}\"");
    }

    [Fact]
    public void Format_Ok()
    {
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.Format, "yyyy-MM-dd HH:mm:ss");
        });

        var body = cut.FindComponent<DatePickerBody>();
        Assert.Equal("yyyy-MM-dd", body.Instance.DateFormat);
    }

    [Fact]
    public void DatePickerViewModel_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.ViewMode, DatePickerViewMode.Year);
        });

        var labels = cut.FindAll(".date-picker-header-label");
        Assert.Equal(GetYearPeriod(), labels[0].TextContent);

        // 上一年
        var buttons = cut.FindAll(".date-picker-header button");
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

        var buttons = cut.FindAll(".date-picker-header button");
        // 下一月
        await cut.InvokeAsync(() => buttons[2].Click());
        // 下一年
        await cut.InvokeAsync(() => buttons[3].Click());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, DateTime.MinValue);
            pb.Add(a => a.ViewMode, DatePickerViewMode.Date);
        });

        buttons = cut.FindAll(".date-picker-header button");
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

        var buttons = cut.FindAll(".date-picker-header button");

        // 上一年
        cut.InvokeAsync(() => buttons[0].Click());
        var labels = cut.FindAll(".date-picker-header-label");
        Assert.Equal(DateTime.Today.AddYears(-1).Year.ToString() + " 年", labels[0].TextContent);

        // 下一年
        cut.InvokeAsync(() => buttons[3].Click());
        labels = cut.FindAll(".date-picker-header-label");
        Assert.Equal(DateTime.Today.Year.ToString() + " 年", labels[0].TextContent);

        // 上一月
        cut.InvokeAsync(() => buttons[1].Click());
        labels = cut.FindAll(".date-picker-header-label");
        Assert.Equal(DateTime.Today.AddMonths(-1).Month.ToString() + " 月", labels[1].TextContent);

        // 下一月
        cut.InvokeAsync(() => buttons[2].Click());
        labels = cut.FindAll(".date-picker-header-label");
        Assert.Equal(DateTime.Today.Month.ToString() + " 月", labels[1].TextContent);

        // 年视图
        labels = cut.FindAll(".date-picker-header-label");
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
        Assert.DoesNotContain("fa-solid fa-angles-left", cut.Find(".date-picker-header").ToMarkup());

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowRightButtons, false));
        Assert.DoesNotContain("fa-solid fa-angles-right", cut.Find(".date-picker-header").ToMarkup());
    }

    [Fact]
    public void MonthView_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
        });
        var labels = cut.FindAll(".date-picker-header-label");
        cut.InvokeAsync(() => labels[1].Click());
        cut.Contains("class=\"month-table\"");

        cut.InvokeAsync(() => cut.Find(".month-table .current.today .cell").Click());
    }

    [Fact]
    public void IsDiabledCell_Ok()
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
            builder.Add(a => a.AllowNull, true);
        });

        var ele = cut.Find(".picker-panel-footer");
        Assert.NotNull(ele);

        var buttons = cut.FindAll(".picker-panel-footer button");

        // Click Now
        cut.InvokeAsync(() => buttons[1].Click());
        Assert.Equal(DateTime.Today, cut.Instance.Value.Date);

        cut.InvokeAsync(() => buttons[0].Click());
        Assert.Equal(DateTime.Today, cut.Instance.Value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFooter, false);
        });
        cut.InvokeAsync(() => cut.Find(".current.today .cell").Click());
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
    public void PlaceholderString_Ok()
    {
        using var cut = Context.RenderComponent<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
        });

        // 打开 Time 弹窗
        var inputs = cut.FindAll(".date-picker-time-header input");
        cut.InvokeAsync(() => inputs[1].Click());
        cut.Contains("date-picker-time-header is-open");

        // 关闭 Time 弹窗
        var buttons = cut.FindAll(".time-panel-footer button");
        cut.InvokeAsync(() => buttons[0].Click());

        cut.InvokeAsync(() => inputs[1].Click());
        cut.InvokeAsync(() => buttons[1].Click());

        using var cut1 = Context.RenderComponent<TimePickerBody>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromSeconds(1));
        });
        buttons = cut1.FindAll(".time-panel-footer button");
        cut1.InvokeAsync(() => buttons[0].Click());
        cut1.InvokeAsync(() => buttons[1].Click());
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
    public void IsDiabled_Ok()
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
            builder.Add(a => a.OnClose, new Action(() =>
            {
                res = true;
            }));
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
            builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<TimeSpan>(this, t =>
            {
                value = true;
            }));
            builder.Add(a => a.OnConfirm, new Action(() =>
            {
                res = true;
            }));
        });

        cut.Find(".time-panel-footer .confirm").Click();

        Assert.True(res);
        Assert.True(value);
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
        var buttons = cut.FindAll(".date-picker-header button");
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
            builder.Add(a => a.Value, DateTime.Today);
            builder.Add(a => a.AutoClose, false);
            builder.Add(a => a.OnValueChanged, dt =>
            {
                val = dt;
                return Task.CompletedTask;
            });
        });

        var button = cut.Find(".picker-panel-content .cell");
        await cut.InvokeAsync(() => button.Click());

        Assert.Equal(val, DateTime.MinValue);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.AutoClose, true);
        });
        button = cut.Find(".picker-panel-content .cell");
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
    public void GetSafeMonthDateTime_Ok()
    {
        Assert.True(MockDateTimePicker.GetSafeMonthDateTime_Ok());
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
        Assert.False(confirm);

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

    class MockDateTimePicker : DatePickerBody
    {
        public static bool GetSafeYearDateTime_Ok()
        {
            var dtm = DatePickerBody.GetSafeYearDateTime(DateTime.MaxValue, 1);
            return dtm == DateTime.MaxValue.Date;
        }

        public static bool GetSafeMonthDateTime_Ok()
        {
            var v1 = DatePickerBody.GetSafeMonthDateTime(DateTime.MaxValue, 1);
            var v2 = DatePickerBody.GetSafeMonthDateTime(DateTime.MinValue, -1);
            return v1 == DateTime.MaxValue.Date && v2 == DateTime.MinValue.Date;
        }
    }
}
