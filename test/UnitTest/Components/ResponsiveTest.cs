// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ResponsiveTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Responsive_Ok()
    {
        BreakPoint? point = null;
        var cut = Context.Render<Responsive>();

        var resp = cut.Instance;
        cut.InvokeAsync(() => resp.OnResize(BreakPoint.ExtraLarge));

        Assert.Null(point);
        cut.Render(pb =>
        {
            pb.Add(a => a.OnBreakPointChanged, b =>
            {
                point = b;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => resp.OnResize(BreakPoint.ExtraLarge));
        Assert.Equal(BreakPoint.ExtraLarge, point);
    }
}
