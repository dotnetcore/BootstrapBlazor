// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

/// <summary>
/// Test for OtpInput component
/// </summary>
public class OtpInputTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OtpInput_Ok()
    {
        var cut = Context.RenderComponent<OtpInput>(pb =>
        {
            pb.Add(a => a.Value, "123");
            pb.Add(a => a.Digits, 6);
        });

        var items = cut.FindAll(".bb-opt-item");
        Assert.Equal(6, items.Count);

        var item = items[0];
        Assert.Contains("value=\"1\"", item.OuterHtml);
        item = items[1];
        Assert.Contains("value=\"2\"", item.OuterHtml);
    }

    [Fact]
    public void Readonly_Ok()
    {
        var cut = Context.RenderComponent<OtpInput>(pb =>
        {
            pb.Add(a => a.IsReadonly, true);
        });

        var item = cut.Find(".bb-opt-item");
        Assert.Equal("<span class=\"bb-opt-item\"></span>", item.OuterHtml);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsReadonly, false);
        });
        item = cut.Find(".bb-opt-item");
        Assert.Equal("<input type=\"number\" class=\"bb-opt-item input-number-fix\" inputmode=\"numeric\">", item.OuterHtml);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        item = cut.Find(".bb-opt-item");
        Assert.Equal("<span class=\"bb-opt-item disabled\"></span>", item.OuterHtml);
    }

    [Fact]
    public void Type_Ok()
    {
        var cut = Context.RenderComponent<OtpInput>(pb =>
        {
            pb.Add(a => a.Type, OtpInputType.Text);
            pb.Add(a => a.PlaceHolder, "X");
        });

        var item = cut.Find(".bb-opt-item");
        Assert.Equal("<input type=\"text\" class=\"bb-opt-item\" maxlength=\"1\" placeholder=\"X\">", item.OuterHtml);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Type, OtpInputType.Password);
            pb.Add(a => a.PlaceHolder, null);
        });
        item = cut.Find(".bb-opt-item");
        Assert.Equal("<input type=\"password\" class=\"bb-opt-item\" maxlength=\"1\">", item.OuterHtml);
    }

    [Fact]
    public async Task TriggerSetValue_Ok()
    {
        var v = "123456";
        var cut = Context.RenderComponent<OtpInput>(pb =>
        {
            pb.Add(a => a.Type, OtpInputType.Text);
            pb.Add(a => a.Value, v);
            pb.Add(a => a.OnValueChanged, val =>
            {
                v = val;
                return Task.CompletedTask;
            });
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerSetValue("234567"));
        Assert.Equal("234567", v);
    }
}
