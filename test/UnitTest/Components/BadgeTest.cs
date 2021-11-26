// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components
{
    public class BadgeTest : TestBase
    {
        [Fact]
        public void Color_Ok()
        {
            var cut = Context.RenderComponent<Badge>(new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(nameof(Badge.Color),Color.Primary)
            });

            Assert.Contains("primary", cut.Markup);
        }

        [Fact]
        public void IsPill_Ok()
        {
            var cut = Context.RenderComponent<Badge>(new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(nameof(Badge.IsPill),true)
            });

            Assert.Contains("rounded-pill", cut.Markup);
        }

        [Fact]
        public void ChildContent_Ok()
        {
            var cut = Context.RenderComponent<Badge>(new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(nameof(Badge.ChildContent),CreateComponent())
            });

            Assert.Contains("badge", cut.Markup);
        }

        private static RenderFragment CreateComponent() => builder =>
        {
            builder.OpenElement(1, "div");
            builder.AddContent(2, "badge");
            builder.CloseComponent();
        };
    }
}
