// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class ConsoleLoggerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IsHtml_Ok()
    {
        var cut = Context.RenderComponent<ConsoleLogger>(pb =>
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
        var cut = Context.RenderComponent<ConsoleLogger>(pb =>
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
