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
    public void TimePickerCell_StyleName_CultureInvariant()
    {
        // Save original culture
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUICulture = CultureInfo.CurrentUICulture;
        
        try
        {
            // Set culture to Turkish which uses comma as decimal separator
            var turkishCulture = new CultureInfo("tr-TR");
            CultureInfo.CurrentCulture = turkishCulture;
            CultureInfo.CurrentUICulture = turkishCulture;
            
            var cut = Context.RenderComponent<TimePickerCell>(pb =>
            {
                pb.Add(a => a.ViewMode, TimePickerCellViewMode.Hour);
                pb.Add(a => a.Value, TimeSpan.FromHours(2.5));
            });
            
            // Call OnHeightCallback to set internal height
            cut.InvokeAsync(() => cut.Instance.OnHeightCallback(36.59375));
            
            // The StyleName property should use dots, not commas, even in Turkish culture
            var styleElement = cut.Find("ul.time-spinner-list");
            var style = styleElement.GetAttribute("style");
            
            // CSS should use dots for decimal values, not commas
            Assert.Contains(".", style);
            Assert.DoesNotContain(",", style);
            
            // Should contain valid translateY with dot decimal separator
            Assert.Matches(@"transform:\s*translateY\(-?\d+\.\d*px\);", style);
        }
        finally
        {
            // Restore original culture
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUICulture;
        }
    }
}
