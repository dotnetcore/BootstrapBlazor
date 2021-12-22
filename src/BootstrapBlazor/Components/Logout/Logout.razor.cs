// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components;

/// <summary>
/// ListView 组件基类
/// </summary>
public partial class Logout
{
    private string? DropdownClassString => CssBuilder.Default("dropdown-menu dropdown-menu-right shadow")
        .AddClass("show", IsShow)
        .Build();

    private bool IsShow { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? DisplayName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? PrefixDisplayNameText { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? UserName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? PrefixUserNameText { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? LinkTemplate { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Logout>? Localizer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        PrefixDisplayNameText ??= Localizer[nameof(PrefixDisplayNameText)];
        PrefixUserNameText ??= Localizer[nameof(PrefixUserNameText)];
    }
}
