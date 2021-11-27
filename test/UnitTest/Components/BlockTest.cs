// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components
{
    public class BlockTest : TestBase
    {
        [Fact]
        public void Show_Ok()
        {
            var cut = Context.RenderComponent<Block>(builder =>
            {
                builder.Add(a => a.OnQueryCondition, new Func<string, Task<bool>>(_ => Task.FromResult(true)));
                builder.Add(a => a.ChildContent, BuildComponent());
            });
            Assert.Equal("<div>test</div>", cut.Markup);
        }

        [Fact]
        public void Hide_Ok()
        {
            var cut = Context.RenderComponent<Block>(builder =>
            {
                builder.Add(a => a.OnQueryCondition, new Func<string, Task<bool>>(_ => Task.FromResult(false)));
                builder.Add(a => a.ChildContent, BuildComponent());
            });
            Assert.Equal("", cut.Markup);
        }

        private static RenderFragment BuildComponent() => builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddContent(1, "test");
            builder.CloseElement();
        };
    }
}
