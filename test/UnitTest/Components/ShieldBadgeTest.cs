// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ShieldBadgeTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ShieldBadge_Ok()
    {
        var cut = Context.RenderComponent<ShieldBadge>();
        cut.Contains("shield-badge");
        cut.Contains("shield-badge-label");
        cut.Contains("shield-badge-text");
        cut.DoesNotContain("shield-badge-icon");

        cut.SetParametersAndRender(p => p.Add(i => i.Icon, "fa fa-user"));
        cut.Contains("shield-badge-icon");

        cut.SetParametersAndRender(p => p.Add(i => i.Label, "test-label"));
        cut.Contains("test-label");

        cut.SetParametersAndRender(p => p.Add(i => i.Text, "test-text"));
        cut.Contains("test-text");

        cut.SetParametersAndRender(p => p.Add(i => i.IconColor, "#123"));
        cut.Contains("--bb-shield-badge-icon-color: #123;");

        cut.SetParametersAndRender(p => p.Add(i => i.LabelColor, "#123"));
        cut.Contains("--bb-shield-badge-label-color: #123;");

        cut.SetParametersAndRender(p => p.Add(i => i.LabelBackgroundColor, "#123"));
        cut.Contains("--bb-shield-badge-label-bg: #123;");

        cut.SetParametersAndRender(p => p.Add(i => i.TextColor, "#123"));
        cut.Contains("--bb-shield-badge-text-color: #123;");

        cut.SetParametersAndRender(p => p.Add(i => i.TextBackgroundColor, "#123"));
        cut.Contains("--bb-shield-badge-text-bg: #123;");

        cut.SetParametersAndRender(p => p.Add(i => i.Radius, 4));
        cut.Contains("--bb-shield-badge-radius: 4px;");
    }
}
