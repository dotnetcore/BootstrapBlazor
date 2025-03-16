// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Represents a link within a tab component.
/// </summary>
public sealed partial class TabLink
{
    /// <summary>
    /// Gets or sets the text of the link. Default is null
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the URL of the link. Default is null
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the icon of the link. Default is null
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the tab item is closable. Default is true.
    /// </summary>
    [Parameter]
    public bool Closable { get; set; } = true;

    /// <summary>
    /// Gets or sets the callback method when the link is clicked. Default is null.
    /// </summary>
    [Parameter]
    public Func<Task>? OnClick { get; set; }

    [Inject]
    [NotNull]
    private TabItemTextOptions? TabItemOptions { get; set; }

    /// <summary>
    /// Gets or sets the content of the component. Default is null
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private async Task OnClickLink()
    {
        if (OnClick != null)
        {
            await OnClick();
        }

        TabItemOptions.Icon = Icon;
        TabItemOptions.Text = Text;
        TabItemOptions.Closable = Closable;
    }

    private RenderFragment RenderChildContent() => builder =>
    {
        if (ChildContent == null)
        {
            if (!string.IsNullOrEmpty(Icon))
            {
                builder.OpenElement(0, "i");
                builder.AddAttribute(1, "class", Icon);
                builder.CloseElement();
            }
            if (!string.IsNullOrEmpty(Text))
            {
                builder.AddContent(2, Text);
            }
        }
        else
        {
            builder.AddContent(3, ChildContent);
        }
    };
}
