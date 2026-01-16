// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

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
    /// 
    /// </summary>
    [Parameter]
    public Type? Type { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Title ??= Localizer[nameof(Title)];

        if (Items == null && Type != null)
        {
            Items = ComponentAttributeCacheService.GetAttributes(Type);
        }
    }
}
