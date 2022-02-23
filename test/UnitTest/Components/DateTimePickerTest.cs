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
        Assert.NotEqual(DateTime.MinValue, cut.Instance.Value);
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

        var value = cut.Find(".datetime-picker-bar").Children.First().GetAttribute("value");

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
    public void OnDateTimeChanged_Ok()
    {
        var res = false;
        var cut = Context.RenderComponent<DateTimePicker<DateTime>>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.OnDateTimeChanged, new Func<DateTime, Task>(d =>
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
            pb.Add(a => a.OnDateTimeChanged, v =>
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
    public void DatePickerViewModel_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.ViewModel, DatePickerViewModel.Year);
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
            pb.Add(a => a.ViewModel, DatePickerViewModel.Month);
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
        Assert.Equal((DateTime.Today.Year - 1).ToString() + " 年", labels[0].TextContent);

        // 下一年
        cut.InvokeAsync(() => buttons[3].Click());
        labels = cut.FindAll(".date-picker-header-label");
        Assert.Equal(DateTime.Today.Year.ToString() + " 年", labels[0].TextContent);

        // 上一月
        cut.InvokeAsync(() => buttons[1].Click());
        labels = cut.FindAll(".date-picker-header-label");
        Assert.Equal((DateTime.Today.Month - 1).ToString() + " 月", labels[1].TextContent);

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
        Assert.DoesNotContain("fa fa-angle-double-left", cut.Find(".date-picker-header").ToMarkup());

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowRightButtons, false));
        Assert.DoesNotContain("fa fa-angle-double-right", cut.Find(".date-picker-header").ToMarkup());
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
            builder.Add(a => a.MaxValue, DateTime.Today.AddDays(7));
        });
    }

    [Fact]
    public void IsShown_Ok()
    {
        var cut = Context.RenderComponent<DatePickerBody>(builder =>
        {
            builder.Add(a => a.Value, DateTime.Now);
            builder.Add(a => a.IsShown, true);
        });

        var value = cut.Find(".picker-panel").ClassList.Contains("d-none");

        Assert.False(value);
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
            builder.Add(a => a.ViewModel, DatePickerViewModel.DateTime);
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
            builder.Add(a => a.ViewModel, DatePickerViewModel.DateTime);
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
            pb.Add(a => a.ViewModel, DatePickerViewModel.DateTime);
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
    public void Validate_Ok()
    {
        // (!MinValue.HasValue || Value >= MinValue.Value) && (!MaxValue.HasValue || Value <= MaxValue.Value)
        var cut = Context.RenderComponent<DatePickerBody>(pb =>
        {
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
}
