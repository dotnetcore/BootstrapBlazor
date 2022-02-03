// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class AttributeTable
{
    [Inject]
    [NotNull]
    private IStringLocalizer<AttributeTable>? Localizer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Title { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter] public IEnumerable<AttributeItem>? Items { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Title ??= Localizer[nameof(Title)];
    }
}
