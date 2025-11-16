// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

/// <summary>
/// SvgIcon 组件单元测试
/// </summary>
public class SvgIconTest : BootstrapBlazorTestBase
{
    [Fact]
    public void SvgIcon_Ok()
    {
        var cut = Context.Render<SvgIcon>(pb =>
        {
            pb.Add(a => a.Name, "test-icon");
        });
        cut.MarkupMatches("<div class=\"bb-svg-icon bb-svg-icon-test-icon\"><svg xmlns=\"http://www.w3.org/2000/svg\"><use href=\"./_content/BootstrapBlazor.IconPark/icon-park.svg#test-icon\"></use></svg></div>");

        cut.Render(pb =>
        {
            pb.Add(a => a.Href, "test-url");
        });
        cut.MarkupMatches("<div class=\"bb-svg-icon bb-svg-icon-test-icon\"><svg xmlns=\"http://www.w3.org/2000/svg\"><use href=\"test-url#test-icon\"></use></svg></div>");
    }
}
