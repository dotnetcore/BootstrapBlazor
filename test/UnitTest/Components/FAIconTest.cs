// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class FAIconTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IconList_Ok()
    {
        var cut = Context.RenderComponent<FAIconList>(pb =>
        {
            pb.Add(a => a.ShowCatalog, false);
            pb.Add(a => a.ShowCopyDialog, false);
        });
        cut.DoesNotContain("class=\"fa-nav\"");

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowCatalog, true));
        cut.Contains("class=\"fa-nav\"");

        cut.Instance.ShowDialog("test");
    }

    [Fact]
    public void IconDialog_Ok()
    {
        var cut = Context.RenderComponent<IconDialog>(pb =>
        {
            pb.Add(a => a.IconName, "fa fa-font-awesome");
        });
        cut.Contains("fa fa-font-awesome");
    }
}
