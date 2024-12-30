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

    /// <summary>
    /// 获得 最终候选数据源
    /// </summary>
    [NotNull]
    private List<TValue>? FilterItems { get; set; }

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
    /// 获得/设置 获得焦点时是否展开下拉候选菜单 默认 true
    /// </summary>
    [Parameter]
    public bool ShowDropdownListOnFocus { get; set; } = true;

    /// <summary>
    /// 获得/设置 通过模型获得显示文本方法 默认使用 ToString 重载方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TValue, string?>? OnGetDisplayText { get; set; }

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
    /// 获得/设置 候选项模板 默认 null
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ItemTemplate 代替")]
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

        OnGetDisplayText ??= v => v?.ToString();
        _displayText = OnGetDisplayText(Value);

        FilterItems ??= Items?.ToList() ?? [];
    }

    /// <summary>
    /// 鼠标点击候选项时回调此方法
    /// </summary>
    private async Task OnClickItem(TValue val)
    {
        CurrentValue = val;
        _displayText = OnGetDisplayText(val);

        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }
    }

    /// <summary>
    /// TriggerOnChange 方法
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public async Task TriggerOnChange(string val)
    {
        if (OnCustomFilter != null)
        {
            var items = await OnCustomFilter(val);
            FilterItems = items.ToList();
        }
        if (DisplayCount != null)
        {
            FilterItems = FilterItems.Take(DisplayCount.Value).ToList();
        }

        _displayText = val;
        StateHasChanged();
    }
}
