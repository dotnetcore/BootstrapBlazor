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
        Assert.Equal("1", item.GetAttribute("value"));
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
        Assert.Equal("<input type=\"number\" class=\"input-number-fix\" inputmode=\"numeric\" blazor:onchange=\"1\">", item.InnerHtml);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        item = cut.Find(".bb-opt-item");
        Assert.Equal("<span class=\"bb-opt-item disabled\"></span>", item.OuterHtml);
    }
}
