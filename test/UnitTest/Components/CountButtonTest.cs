// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class CountButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task CountText_Ok()
    {
        var cut = Context.RenderComponent<CountButton>(pb =>
        {
            pb.Add(a => a.Count, 1);
            pb.Add(a => a.Text, "DisplayText");
        });
        Assert.Contains("DisplayText", cut.Markup);

        await cut.InvokeAsync(() =>
        {
            var button = cut.Find("button");
            button.Click();
        });
        Assert.Contains("disabled=\"disabled\"", cut.Markup);

        await Task.Delay(500);
        Assert.Contains("(1) DisplayText", cut.Markup);

        await Task.Delay(600);
        Assert.DoesNotContain("disabled=\"disabled\"", cut.Markup);
        Assert.Contains("DisplayText", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.CountText, "CountText");
        });
        await cut.InvokeAsync(() =>
        {
            var button = cut.Find("button");
            button.Click();
        });
        await Task.Delay(500);
        Assert.Contains("(1) CountText", cut.Markup);
        await Task.Delay(600);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.CountTextCallback, count =>
            {
                return $"{count + 1}-test-callback";
            });
        });
        await cut.InvokeAsync(() =>
        {
            var button = cut.Find("button");
            button.Click();
        });
        Assert.Contains("disabled=\"disabled\"", cut.Markup);

        await Task.Delay(500);
        Assert.Contains("2-test-callback", cut.Markup);

        await Task.Delay(600);
        Assert.DoesNotContain("disabled=\"disabled\"", cut.Markup);
        Assert.Contains("DisplayText", cut.Markup);

        // OnClick
        var clickCount = 0;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnClick, () =>
            {
                clickCount++;
            });
        });
        await cut.InvokeAsync(() =>
        {
            var button = cut.Find("button");
            button.Click();
        });
        await Task.Delay(500);
        Assert.Equal(1, clickCount);
        await Task.Delay(600);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnClickWithoutRender, () =>
            {
                clickCount++;
                return Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() =>
        {
            var button = cut.Find("button");
            button.Click();
        });
        await Task.Delay(500);
        Assert.Equal(3, clickCount);
        await Task.Delay(600);
    }
}
