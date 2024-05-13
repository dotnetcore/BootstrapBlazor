// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.MeiliSearch.Options;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// Pre 组件
/// </summary>
public partial class GlobalSearch
{
    [Inject, NotNull]
    private IOptionsMonitor<MeiliSearchOptions>? Options { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new
    {
        Options.CurrentValue?.Url,
        Options.CurrentValue?.ApiKey,
        Index = $"{Options.CurrentValue?.Index}-{CultureInfo.CurrentUICulture.Name}",
        SearchStatus = Localizer["SearchStatus"].Value
    });
}
