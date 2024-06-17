// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Mask 组件示例文档
/// </summary>
public partial class Masks
{
    [Inject, NotNull]
    private MaskService? MaskService { get; set; }

    private async Task ShowMask()
    {
        await MaskService.Show(new MaskOption()
        {
            ChildContent = BootstrapDynamicComponent.CreateComponent<Counter>().Render()
        });
    }
}
