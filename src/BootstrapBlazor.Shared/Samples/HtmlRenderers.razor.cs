// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

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
