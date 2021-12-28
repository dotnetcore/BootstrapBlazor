// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class TitleTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTitleTest>();
        });
        var titleService = cut.FindComponent<MockTitleTest>().Instance.TitleService;
        cut.InvokeAsync(async () => await titleService.SetTitle("test"));
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.RenderComponent<Title>(pb =>
        {
            pb.Add(a => a.Text, "Text");
        });
        var text = cut.Instance.Text;
        Assert.Equal("Text", text);
    }

    private class MockTitleTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public TitleService? TitleService { get; set; }
    }
}
