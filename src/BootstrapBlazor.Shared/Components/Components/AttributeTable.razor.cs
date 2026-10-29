// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Components;

/// <summary>
/// 组件参数表格组件
/// </summary>
public sealed partial class AttributeTable
{
    private readonly string _anchorPrefix = $"attribute-table";

    [Inject]
    [NotNull]
    private IStringLocalizer<AttributeTable>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    [NotNull]
    private ISampleDocumentationService? DocumentationService { get; set; }

    /// <summary>
    /// 获得/设置 表格标题
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 表格关联组件类型
    /// </summary>
    [Parameter]
    public Type? Type { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置预加载的参数文档，避免客户端引用服务器组件程序集。</para>
    /// <para lang="en">Gets or sets preloaded parameter documentation without referencing server component assemblies on the client.</para>
    /// </summary>
    [Parameter]
    public List<AttributeItem>? Items { get; set; }

    /// <summary>
    /// 是否显示合计信息 默认 false
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; }

    private List<AttributeItem> _items = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (Items != null)
        {
            _items = Items;
        }
        else if (Type != null)
        {
            _items = await DocumentationService.GetAttributesAsync(Type);

            Title ??= FormatTypeName(Type);
        }
    }

    private static string FormatTypeName(Type type)
    {
        if (!type.IsGenericType)
        {
            return type.Name;
        }

        var index = type.Name.IndexOf('`');
        var typeName = type.Name[..index];
        var genericArgs = string.Join(", ", type.GetGenericArguments().Select(i => i.Name));
        return $"{typeName}<{genericArgs}>";
    }

    private string GetPropertyAnchor(string? name) => $"{_anchorPrefix}-{name}";

    private string GetPropertyAnchorUrl(string? name)
    {
        var url = NavigationManager.ToBaseRelativePathWithoutQueryAndFragment();
        return $"{url}#{GetPropertyAnchor(name)}";
    }
}
