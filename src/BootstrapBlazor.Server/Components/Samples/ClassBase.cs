// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Server.Components.Samples;

public class ClassBase : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public string? Value { get; set; }
}

public class ClassTest : ClassBase
{
    [Parameter]
    public string? Value2 { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, Value);
        builder.AddContent(1, Value2);
    }
}
