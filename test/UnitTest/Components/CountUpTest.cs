// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class CountUpTest : BootstrapBlazorTestBase
{
    [Fact]
    public void CountUp_Ok()
    {
        var cut = Context.RenderComponent<CountUp<int>>(pb =>
        {
            pb.Add(a => a.Value, 1234);
        });
        cut.Contains("<div id=");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, 23);
        });
    }

    [Fact]
    public void Class_Ok()
    {
        var cut = Context.RenderComponent<CountUp<int>>(pb =>
        {
            pb.Add(a => a.Value, 1234);
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>() { { "class", "test1" } });
        });
        cut.Contains("<div class=\"test1\" id=");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, 23);
        });
    }

    [Fact]
    public void CountUp_Error()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            Context.RenderComponent<CountUp<string>>();
        });
    }
}
