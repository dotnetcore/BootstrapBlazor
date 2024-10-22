// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
