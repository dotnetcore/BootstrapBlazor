// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 组件参数表格组件
/// </summary>
public sealed partial class AttributeTable
{
    [Inject]
    [NotNull]
    private IStringLocalizer<AttributeTable>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 表格标题
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 表格数据
    /// </summary>
    [Parameter]
    public IEnumerable<AttributeItem> Items { get; set; } = [];

    /// <summary>
    /// 获得/设置 表格关联组件类型
    /// </summary>
    [Parameter]
    public Type? Type { get; set; }

    /// <summary>
    /// 是否显示合计信息 默认 false
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Title ??= Localizer[nameof(Title)];

        if (Type != null)
        {
            Items = ComponentAttributeCacheService.GetAttributes(Type);
        }
    }
}
