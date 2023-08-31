// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Loader
/// </summary>
public partial class Loader
{
    /// <summary>
    /// 文本内容
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 数据数量
    /// </summary>
    [Parameter]
    public int Columns { get; set; } = 10;

    [Inject]
    [NotNull]
    private IStringLocalizer<Loader>? Localizer { get; set; }

    private string? ClassString => CssBuilder.Default("bb-loader")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Text ??= Localizer[nameof(Text)];
    }
}
