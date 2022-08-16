// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class FooterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Footer_Ok()
    {
        var cut = Context.RenderComponent<Footer>(pb =>
        {
            pb.Add(a => a.Text, "test-footer-text");
            pb.Add(a => a.Target, "#test-footer-target1");
        });
        Assert.Contains("test-footer-text", cut.Markup);
    }
}
