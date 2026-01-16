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
    /// Component name for auto-loading attributes
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

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
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // 如果指定了 Name 但没有提供 Items，则自动从生成的元数据中获取
        if (!string.IsNullOrEmpty(Name) && Items == null)
        {
            Items = ComponentAttributeProvider.GetAttributes(Name);
        }
        
        Title ??= Localizer[nameof(Title)];
    }
}
