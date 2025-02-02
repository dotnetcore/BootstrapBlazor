// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class BootstrapLabelTest : BootstrapBlazorTestBase
{
    [Fact]
    public void BootstrapLabelSetting_LabelWidth_Ok()
    {
        var cut = Context.RenderComponent<BootstrapLabelSetting>(pb =>
        {
            pb.Add(a => a.LabelWidth, 120);
            pb.AddChildContent<BootstrapLabel>();
        });
        Assert.Equal("<label class=\"form-label\" style=\"--bb-row-label-width: 120px;\"></label>", cut.Markup);

        var label = cut.FindComponent<BootstrapLabel>();
        label.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.LabelWidth, 80);
        });
        Assert.Equal("<label class=\"form-label\" style=\"--bb-row-label-width: 80px;\"></label>", cut.Markup);
    }

    [Fact]
    public void LabelWidth_Ok()
    {
        var cut = Context.RenderComponent<BootstrapLabel>();
        Assert.Equal("<label class=\"form-label\"></label>", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.LabelWidth, 120);
        });
        Assert.Equal("<label class=\"form-label\" style=\"--bb-row-label-width: 120px;\"></label>", cut.Markup);
    }
}
