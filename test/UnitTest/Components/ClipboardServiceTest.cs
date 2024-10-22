// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
