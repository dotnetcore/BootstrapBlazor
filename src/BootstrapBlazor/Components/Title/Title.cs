// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Title 组件</para>
/// <para lang="en">Title component</para>
/// </summary>
public class Title : ComponentBase
{
    [Inject]
    [NotNull]
    private TitleService? TitleService { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前页标题文字</para>
    /// <para lang="en">Gets or sets 当前页标题文字</para>
    /// <para><version>10.2.2</version></para>
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
            await TitleService.SetTitle(Text, CancellationToken.None);
        }
    }
}
