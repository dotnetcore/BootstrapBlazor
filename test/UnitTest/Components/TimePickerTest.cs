// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace UnitTest.Components;

public class TimePickerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void TimePicker_Ok()
    {
        var cut = Context.RenderComponent<TimePicker>();
        cut.MarkupMatches("""
            <div class="bb-time-picker" diff:ignore>
                <div class="bb-time-panel">
                    <div class="bb-time-header"></div>
                    <div class="bb-time-body"></div>
                    <div class="bb-time-footer"></div>
                </div>
            </div>
        """);
    }

    [Fact]
    public void HeightCallback_Ok()
    {
        var cut = Context.RenderComponent<TimePicker>();
        var cell = cut.FindComponent<TimePickerCell>();
        cut.InvokeAsync(() => cell.Instance.OnHeightCallback(16));
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromSeconds(1));
        });
    }

    [Fact]
    public async Task ValueChanged_Ok()
    {
        var val = new TimeSpan(10, 10, 10);
        var cut = Context.RenderComponent<TimePicker>(pb =>
        {
            pb.Add(a => a.Value, new TimeSpan(10, 10, 10));
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<TimeSpan>(this, ts =>
            {
                val = ts;
                return Task.CompletedTask;
            }));
        });
        var button = cut.Find(".confirm");
        await cut.InvokeAsync(() => button.Click());
        Assert.Equal(new TimeSpan(10, 10, 10), cut.Instance.Value);
        Assert.Equal(new TimeSpan(10, 10, 10), val);
    }

    [Fact]
    public void HasSeconds_Ok()
    {
        var cut = Context.RenderComponent<TimePicker>(pb =>
        {
            pb.Add(a => a.Value, new TimeSpan(10, 10, 10));
            pb.Add(a => a.HasSeconds, false);
        });
        var cells = cut.FindComponents<TimePickerCell>();
        Assert.Equal(2, cells.Count);
    }

    [Fact]
    public async Task OnClickClose_Ok()
    {
        var close = false;
        var cut = Context.RenderComponent<TimePicker>(pb =>
        {
            pb.Add(a => a.OnClose, () =>
            {
                close = true;
                return Task.CompletedTask;
            });
        });
        var btn = cut.Find(".cancel");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(close);
    }

    [Fact]
    public async Task OnClickConfirm_Ok()
    {
        var confirm = false;
        var cut = Context.RenderComponent<TimePicker>(pb =>
        {
            pb.Add(a => a.OnConfirm, ts =>
            {
                confirm = true;
                return Task.CompletedTask;
            });
        });
        var btn = cut.Find(".confirm");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(confirm);
    }

    [Fact]
    public async Task TimePickerCell_StyleName_CultureInvariant()
    {
        // 保存老的 Culture 设置
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUICulture = CultureInfo.CurrentUICulture;

        // 设置为土耳其文化环境 小数点使用逗号
        var trCulture = new CultureInfo("tr-TR");
        CultureInfo.CurrentCulture = trCulture;
        CultureInfo.CurrentUICulture = trCulture;

        var cut = Context.RenderComponent<TimePickerCell>(pb =>
        {
            pb.Add(a => a.ViewMode, TimePickerCellViewMode.Hour);
            pb.Add(a => a.Value, TimeSpan.FromHours(2.5));
        });

        // 调用 OnHeightCallback 方法设置高度
        await cut.InvokeAsync(() => cut.Instance.OnHeightCallback(12.25));
        cut.SetParametersAndRender();

        // 检查高度样式是否正确生成应该是用点而不是逗号
        var styleElement = cut.Find("ul.time-spinner-list");
        var style = styleElement.GetAttribute("style");

        Assert.Contains("-24.5px", style);

        // 恢复当前线程文化设置
        CultureInfo.CurrentCulture = originalCulture;
        CultureInfo.CurrentUICulture = originalUICulture;
    }
}
