// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoFill 组件
/// </summary>
public partial class AutoFill<TValue>
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("auto-complete auto-fill")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private List<TValue>? _filterItems;

    /// <summary>
    /// 获得/设置 组件数据集合
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TValue>? Items { get; set; }

    /// <summary>
    /// 获得/设置 匹配数据时显示的数量 默认 null 未设置
    /// </summary>
    [Parameter]
    [NotNull]
    public int? DisplayCount { get; set; }

    /// <summary>
    /// 获得/设置 是否开启模糊查询，默认为 false
    /// </summary>
    [Parameter]
    public bool IsLikeMatch { get; set; }

    /// <summary>
    /// 获得/设置 匹配时是否忽略大小写，默认为 true
    /// </summary>
    [Parameter]
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    /// 获得/设置 获得焦点时是否展开下拉候选菜单 默认 true
    /// </summary>
    [Parameter]
    public bool ShowDropdownListOnFocus { get; set; } = true;

    /// <summary>
    /// 获得/设置 通过模型获得显示文本方法 默认使用 ToString 重载方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TValue?, string?>? OnGetDisplayText { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 加载图标
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// 获得/设置 自定义集合过滤规则
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<TValue>>>? OnCustomFilter { get; set; }

    /// <summary>
    /// 获得/设置 是否显示无匹配数据选项 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowNoDataTip { get; set; } = true;

    /// <summary>
    /// 获得/设置 候选项模板 默认 null
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ItemTemplate 代替；Deprecated please use ItemTemplate parameter")]
    [ExcludeFromCodeCoverage]
    public RenderFragment<TValue>? Template { get => ItemTemplate; set => ItemTemplate = value; }

    [Inject]
    [NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    /// <summary>
    /// 获得 获得焦点自动显示下拉框设置字符串
    /// </summary>
    private string? ShowDropdownListOnFocusString => ShowDropdownListOnFocus ? "true" : null;

    private string? _displayText;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NoDataTip ??= Localizer[nameof(NoDataTip)];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AutoFillIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.LoadingIcon);

        _displayText = GetDisplayText(Value);
        Items ??= [];
    }

    /// <summary>
    /// 鼠标点击候选项时回调此方法
    /// </summary>
    private async Task OnClickItem(TValue val)
    {
        CurrentValue = val;
        _displayText = GetDisplayText(val);

        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }
    }

    private string? GetDisplayText(TValue item) => OnGetDisplayText?.Invoke(item) ?? item?.ToString();

    private List<TValue> Rows => _filterItems ?? Items.ToList();

    /// <summary>
    /// TriggerFilter 方法
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public override async Task TriggerFilter(string val)
    {
        if (OnCustomFilter != null)
        {
            var items = await OnCustomFilter(val);
            _filterItems = items.ToList();
        }
        else if (string.IsNullOrEmpty(val))
        {
            _filterItems = Items.ToList();
        }
        else
        {
            var comparision = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            var items = IsLikeMatch
                ? Items.Where(i => OnGetDisplayText?.Invoke(i)?.Contains(val, comparision) ?? false)
                : Items.Where(i => OnGetDisplayText?.Invoke(i)?.StartsWith(val, comparision) ?? false);
            _filterItems = items.ToList();
        }

        if (DisplayCount != null)
        {
            _filterItems = _filterItems.Take(DisplayCount.Value).ToList();
        }
        StateHasChanged();
    }

    /// <summary>
    /// TriggerOnChange 方法
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public override Task TriggerChange(string val)
    {
        _displayText = val;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
