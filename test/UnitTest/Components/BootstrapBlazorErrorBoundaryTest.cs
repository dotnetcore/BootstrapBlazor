// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class BootstrapBlazorErrorBoundaryTest : BootstrapBlazorTestBase
{
    // 1. Test rendering of child content when there is no exception
    [Fact]
    public void ShouldRenderChildContent_WhenNoException()
    {
        var cut = Context.RenderComponent<BootstrapBlazorErrorBoundary>(parameters => parameters
            .AddChildContent("<p>Normal Content</p>")
        );
        cut.MarkupMatches("<p>Normal Content</p>");
    }

    // 2. Test rendering custom error content when an exception is thrown and ErrorContent is set
    [Fact]
    public void ShouldRenderCustomErrorContent_WhenExceptionOccurs()
    {
        var errorContentRendered = false;

        var cut = Context.RenderComponent<BootstrapBlazorErrorBoundary>(parameters => parameters
            .Add(p => p.ErrorContent, ex => builder =>
            {
                errorContentRendered = true;
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "custom-error");
                builder.AddContent(2, $"Custom error caught: {ex.Message}");
                builder.CloseElement();
            })
            .AddChildContent<ExceptionThrowingComponent>()
        );

        cut.MarkupMatches("""<div class="custom-error">Custom error caught: Boom!</div>""");
        Assert.True(errorContentRendered);
    }

    // 3. Test rendering default error content when an exception is thrown and ErrorContent is null
    [Fact]
    public void ShouldRenderDefaultExceptionContent_WhenExceptionOccurs_AndErrorContentIsNull()
    {
        var cut = Context.RenderComponent<BootstrapBlazorErrorBoundary>(parameters => parameters
            .AddChildContent<ExceptionThrowingComponent>()
        );
        Assert.Contains("Boom!", cut.Markup);
    }

    private class ExceptionThrowingComponent : ComponentBase
    {
        protected override void OnInitialized()
        {
            throw new InvalidOperationException("Boom!");
        }
    }
}

