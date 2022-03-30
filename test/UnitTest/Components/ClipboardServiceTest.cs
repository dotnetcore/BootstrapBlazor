// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Components;

public class ClipboardServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Clipboard_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockClipboardServiceTest>();
        });
        var comp = cut.FindComponent<MockClipboardServiceTest>();
        comp.Instance.Copy(null, () => Task.CompletedTask);
        comp.Instance.Copy("Test", () => Task.CompletedTask);
    }

    private class MockClipboardServiceTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public ClipboardService? ClipboardService { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
        }

        public Task Copy(string? text, Func<Task> callback) => ClipboardService.Copy(text, callback);
    }
}
