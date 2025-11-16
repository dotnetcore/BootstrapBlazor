// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ConsoleLoggerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IsHtml_Ok()
    {
        var cut = Context.Render<ConsoleLogger>(pb =>
        {
            pb.Add(a => a.IsHtml, true);
        });
        var instance = cut.Instance;
        cut.InvokeAsync(() => instance.Log("<div class=\"test1\">Test</div>"));
        cut.Contains("<div class=\"test1\">Test</div>");
    }

    [Fact]
    public void Max_Ok()
    {
        var cut = Context.Render<ConsoleLogger>(pb =>
        {
            pb.Add(a => a.Max, 2);
        });
        var instance = cut.Instance;
        cut.InvokeAsync(() => instance.Log("Test1"));
        cut.InvokeAsync(() => instance.Log("Test2"));
        cut.InvokeAsync(() => instance.Log("Test3"));
        cut.DoesNotContain("Test1");
    }
}
