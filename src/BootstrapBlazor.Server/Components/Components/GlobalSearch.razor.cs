// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Server.Options;
using System.Globalization;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// Pre 组件
/// </summary>
public partial class GlobalSearch
{
    [Inject, NotNull]
    private IConfiguration? Configuration { get; set; }

    private string _searchId => $"{Id}_search";

    private MeiliSearchOptions? _options = null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var section = Configuration.GetSection(nameof(MeiliSearchOptions));
        if (section.Exists())
        {
            _options = section.Get<MeiliSearchOptions>();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { _options?.Host, _options?.Key, Index = GetIndex(), SearchStatus = Localizer["SearchStatus"].Value });

    private string GetIndex()
    {
        var lang = CultureInfo.CurrentUICulture.Name;
        var segs = lang.Split('-');
        if (segs.Length > 1)
        {
            lang = segs[0];
        }
        lang = lang == "zh" ? "" : $"-{lang}";
        return $"{_options?.Index}{lang}";
    }
}
