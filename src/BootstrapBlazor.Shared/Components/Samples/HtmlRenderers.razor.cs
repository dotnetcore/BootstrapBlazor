// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// HtmlRenderers
/// </summary>
public partial class HtmlRenderers
{
    private string? RawString { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        RawString = await HtmlRenderer.RenderAsync<Button>(new Dictionary<string, object?>() { { nameof(Button.Icon), "fa-solid fa-gear" }, { nameof(Button.Text), "ButtonText" } });
    }
}
