// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

/// <summary>
/// SvgIcon 组件单元测试
/// </summary>
public class SvgIconTest : BootstrapBlazorTestBase
{
    [Fact]
    public void SvgIcon_Ok()
    {
        var cut = Context.RenderComponent<SvgIcon>(pb =>
        {
            pb.Add(a => a.Name, "test-icon");
        });
        cut.MarkupMatches("<div class=\"bb-svg-icon bb-svg-icon-test-icon\"><svg xmlns=\"http://www.w3.org/2000/svg\"><use href=\"./_content/BootstrapBlazor.IconPark/icon-park.svg#test-icon\"></use></svg></div>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Href, "test-url");
        });
        cut.MarkupMatches("<div class=\"bb-svg-icon bb-svg-icon-test-icon\"><svg xmlns=\"http://www.w3.org/2000/svg\"><use href=\"test-url#test-icon\"></use></svg></div>");
    }
}
