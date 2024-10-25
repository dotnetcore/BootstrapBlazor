// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Title 组件
/// </summary>
public class Title : ComponentBase
{
    [Inject]
    [NotNull]
    private TitleService? TitleService { get; set; }

    /// <summary>
    /// 获得/设置 当前页标题文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && Text != null)
        {
            await TitleService.SetTitle(Text);
        }
    }
}
