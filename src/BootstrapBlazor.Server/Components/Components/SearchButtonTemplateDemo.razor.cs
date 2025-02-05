// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// SearchButtonTemplateDemo 示例组件
/// </summary>
public partial class SearchButtonTemplateDemo<TValue>
{
    /// <summary>
    /// 获得/设置 <see cref="SearchContext{TValue}"/> 实例"/>
    /// </summary>
    [Parameter, EditorRequired, NotNull]
    public SearchContext<TValue>? Context { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private async Task OnClickSearch()
    {
        await Context.OnSearchAsync();

        await ToastService.Information("Search-ButtonTemplate", "Click Search1 Button");
    }

    private async Task OnClickClear()
    {
        await Context.OnClearAsync();

        await ToastService.Information("Search-ButtonTemplate", "Click Clear1 Button");
    }
}
