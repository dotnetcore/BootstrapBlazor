// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

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
    public void StarEqualEnd_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue() { Start = DateTime.Today, End = DateTime.Today });
        });

        // 内部 StartValue 自动减一个月
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
        var placement = cut.FindAll("div").Select(s => s.GetAttribute("data-bs-placement")).FirstOrDefault();

        Assert.Equal("top", placement);
    }

    [Fact]
    public void IsShown_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.IsShown, true);
        });

        Assert.Contains("d-none", cut.Markup);
    }

    [Fact]
    public void AllowNull_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.AllowNull, true);
        });
    }

    [Fact]
    public void NullValue_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>();
        Assert.NotNull(cut.Instance.Value);
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
            builder.Add(a => a.SidebarItems, new DateTimeRangeSidebarItem[]
            {
                    new DateTimeRangeSidebarItem(){ Text = "Test" }
            });
        });

        var text = cut.Find(".picker-panel-sidebar").TextContent;
        Assert.Equal("Test", text);
    }

    [Fact]
    public void OnConfirm_Ok()
    {
        var value = false;
        var cut = Context.RenderComponent<DateTimeRange>(pb =>
        {
            pb.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now, End = DateTime.MinValue });
            pb.Add(a => a.ValueChanged, v => _ = v);
            pb.Add(a => a.OnValueChanged, v => Task.CompletedTask);
            pb.Add(a => a.OnConfirm, (e) =>
            {
                value = true; return Task.CompletedTask;
            });
        });
        // 选择开始未选择结束
        cut.Find(".cell").Click();
        cut.FindAll(".is-confirm").First(s => s.TextContent == "确定").Click();

        // 选择时间大于当前时间
        cut.FindAll(".date-table .cell").Last().Click();
        cut.FindAll(".is-confirm").First(s => s.TextContent == "确定").Click();
        Assert.True(value);
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
            builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<DateTimeRangeValue>(this, v => { value = v; }));
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
        Assert.Equal(DateTime.Today.Date, cut.Instance.Value.Start.Date);
    }

    [Fact]
    public void OnClickSidebarItem_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue { Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(30) });
            builder.Add(a => a.ShowSidebar, true);
        });

        cut.Find(".sidebar-item > div").Click();

        // 得到最近一周时间范围
        Assert.Equal(DateTime.Today.AddDays(-7), cut.Instance.Value.Start);
        Assert.Equal(DateTime.Today.AddDays(1).AddSeconds(-1), cut.Instance.Value.End);
    }

    [Fact]
    public void UpdateValue_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue());
        });

        // 选择开始时间
        cut.Find(".date-table .cell").Click();
        // 选择结束时间
        cut.FindAll(".date-table .cell").ElementAt(2).Click();

        cut.Find(".date-table .cell").Click();
        cut.Find(".pick-panel-arrow-right").Click();
        cut.FindAll(".date-table .cell").Last().Click();

        // 下一年
        cut.FindAll(".picker-panel-icon-btn").Last().Click();
        cut.Find(".date-table .cell").Click();
        cut.FindAll(".date-table .cell").Last().Click();
    }

    [Fact]
    public void InValidateForm_Ok()
    {
        var foo = new Dummy
        {
            Value = new DateTimeRangeValue { Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(30) }
        };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<DateTimeRange>(pb =>
            {
                pb.Add(a => a.Value, foo.Value);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, nameof(Dummy.Value), typeof(DateTimeRangeValue)));
            });
        });
        cut.Contains("class=\"form-label\"");
    }

    [Fact]
    public void PrevButton_Ok()
    {
        var cut = Context.RenderComponent<DateTimeRange>(builder =>
        {
            builder.Add(a => a.Value, new DateTimeRangeValue());
        });

        var buttons = cut.FindAll(".date-picker-header button");

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

    private class Dummy
    {
        [Required]
        public DateTimeRangeValue? Value { get; set; }
    }
}
