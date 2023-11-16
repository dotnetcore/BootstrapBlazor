// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Pages;

/// <summary>
/// Index 组件
/// </summary>
public partial class Index
{
    private string? BodyClassString => CssBuilder.Default(Localizer["BodyClassString"])
        .Build();

    [Inject]
    [NotNull]
    private IStringLocalizer<Index>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<WebsiteOptions>? Options { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Localizer["DynamicText"].Value.ToCharArray(), Localizer["DynamicText1"].Value.ToCharArray(), Localizer["DynamicText2"].Value.ToCharArray());
}
