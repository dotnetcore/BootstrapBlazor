// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components
{
    public class AnchorLinkTest : BootstrapBlazorTestBase
    {
        [Fact]
        public void Id_Ok()
        {
            var cut = Context.RenderComponent<AnchorLink>(new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(nameof(AnchorLink.Id), "anchorlink")
            });

            Assert.Contains("id=\"anchorlink\"", cut.Markup);
        }

        [Fact]
        public void Text_Ok()
        {
            var cut = Context.RenderComponent<AnchorLink>(new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(nameof(AnchorLink.Text), "anchorlink")
            });

            Assert.Contains("anchorlink", cut.Markup);
        }

        [Fact]
        public void TooltipText_Ok()
        {
            var cut = Context.RenderComponent<AnchorLink>(new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(nameof(AnchorLink.TooltipText), "anchorlink")
            });

            Assert.Contains("anchorlink", cut.Markup);
        }
    }
}
