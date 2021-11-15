// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using Xunit;

namespace UnitTest.Components
{
    public class AnchorTest : IDisposable
    {
        private TestContext Context { get; }

        public AnchorTest()
        {
            Context = new TestContext();

            Context.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        [Fact]
        public void Target_Ok()
        {

            var cut = Context.RenderComponent<Anchor>(new ComponentParameter[]
            {
                 ComponentParameter.CreateParameter(nameof(Anchor.Target), "anchor")
            });

            Assert.Contains("anchor", cut.Markup);
        }

        [Fact]
        public void Container_Ok()
        {

            var cut = Context.RenderComponent<Anchor>(new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(nameof(Anchor.Container), "anchor"),
                 ComponentParameter.CreateParameter(nameof(Anchor.ChildContent), new RenderFragment(builder=>
                 {
                     builder.OpenElement(1, "div");
                     builder.AddAttribute(2, "id", "anchor");
                     builder.CloseElement();
                 }))
            });

            Assert.Contains("anchor", cut.Markup);
        }

        [Fact]
        public void Offset_Ok()
        {

            var cut = Context.RenderComponent<Anchor>(new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(nameof(Anchor.Offset), 20)
            });

            Assert.Contains("data-offset=\"20\"", cut.Markup);
        }



        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
