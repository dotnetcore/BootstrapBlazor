// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockViewV2Panel 组件对应 DockView Group
/// </summary>
public class DockViewV2Group : DockViewV2ComponentBase
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<List<DockViewV2Panel>>>(0);
#if NET8_0_OR_GREATER
        builder.AddComponentParameter(1, nameof(CascadingValue<List<DockViewV2Panel>>.Value), Panels);
        builder.AddComponentParameter(2, nameof(CascadingValue<List<DockViewV2Panel>>.IsFixed), true);
        builder.AddComponentParameter(3, nameof(CascadingValue<List<DockViewV2Panel>>.ChildContent), ChildContent);
#else
        builder.AddAttribute(1, nameof(CascadingValue<List<DockViewV2Panel>>.Value), Panels);
        builder.AddAttribute(2, nameof(CascadingValue<List<DockViewV2Panel>>.IsFixed), true);
        builder.AddAttribute(3, nameof(CascadingValue<List<DockViewV2Panel>>.ChildContent), ChildContent);
#endif
        builder.CloseComponent();
    }
}
