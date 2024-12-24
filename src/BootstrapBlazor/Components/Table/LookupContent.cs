// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

internal class LookupContent : ComponentBase
{
    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务实例
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务实例
    /// </summary>
    [Parameter]
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值 常用于外键自动转换为名称操作，可以通过 <see cref="LookupServiceData"/> 传递自定义数据
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值自定义数据，通过 <see cref="LookupServiceKey"/> 指定键值
    /// </summary>
    [Parameter]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源字符串比较规则 默认 <see cref="StringComparison.OrdinalIgnoreCase" /> 大小写不敏感 
    /// </summary>
    [Parameter]
    public StringComparison LookupStringComparison { get; set; }

    /// <summary>
    /// 获得/设置 显示值
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    [Inject]
    [NotNull]
    private ILookupService? InjectLookupService { get; set; }

    private string? _content;

    private List<SelectedItem>? _items;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        _items ??= await GetLookupItemsAsync();
        var item = _items.Find(i => i.Value.Equals(Value, LookupStringComparison));
        _content = item?.Text ?? Value;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (!string.IsNullOrEmpty(_content))
        {
            builder.AddContent(0, _content);
        }
    }

    private async Task<List<SelectedItem>> GetLookupItemsAsync()
    {
        IEnumerable<SelectedItem>? items;
        if (Lookup != null)
        {
            items = Lookup;
        }
        else
        {
            var lookupService = LookupService ?? InjectLookupService;
            items = await lookupService.GetItemsAsync(LookupServiceKey, LookupServiceData);
        }
        return items?.ToList() ?? [];
    }
}
