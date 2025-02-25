// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class LayoutSplitebarTest : BootstrapBlazorTestBase
{
    [Fact]
    public void LayoutSplitebar_Ok()
    {
        var cut = Context.RenderComponent<LayoutSplitebar>(pb =>
        {
            pb.Add(a => a.ContainerSelector, ".layout");
        });
        cut.Contains("data-bb-selector=\".layout\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Min, 100);
            pb.Add(a => a.Max, 200);
        });
        cut.Contains("data-bb-min=\"100\"");
        cut.Contains("data-bb-max=\"200\"");
    }
}
