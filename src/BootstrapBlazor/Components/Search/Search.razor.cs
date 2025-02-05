// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Search 组件
/// </summary>
public partial class Search<TValue>
{
    /// <summary>
    /// 获得/设置 图标模板 默认 null 未设置
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? IconTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示清空小按钮 默认 false
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// 获得/设置 清空图标 默认为 null
    /// </summary>
    [Parameter]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示清除按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowClearButton { get; set; }

    /// <summary>
    /// Clear button icon
    /// </summary>
    [Parameter]
    public string? ClearButtonIcon { get; set; }

    /// <summary>
    /// Clear button text
    /// </summary>
    [Parameter]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// Clear button color
    /// </summary>
    [Parameter]
    public Color ClearButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 是否显示搜索按钮 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowSearchButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 搜索按钮颜色
    /// </summary>
    [Parameter]
    public Color SearchButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 搜索按钮图标
    /// </summary>
    [Parameter]
    public string? SearchButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 正在搜索按钮图标
    /// </summary>
    [Parameter]
    public string? SearchButtonLoadingIcon { get; set; }

    /// <summary>
    /// 获得/设置 搜索按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SearchButtonText { get; set; }

    /// <summary>
    /// 获得/设置 按钮模板 默认 null 未设置
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? ButtonTemplate { get; set; }

    /// <summary>
    /// 获得/设置 前置按钮模板 默认 null 未设置
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? PrefixButtonTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示前缀图标 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowPrefixIcon { get; set; }

    /// <summary>
    /// 获得/设置 前缀图标 默认为 null
    /// </summary>
    [Parameter]
    public string? PrefixIcon { get; set; }

    /// <summary>
    /// 获得/设置 前缀图标模板 默认为 null
    /// </summary>
    [Parameter]
    public RenderFragment<SearchContext<TValue>>? PrefixIconTemplate { get; set; }

    /// <summary>
    /// 获得/设置 点击搜索后是否自动清空搜索框
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it")]
    [ExcludeFromCodeCoverage]
    public bool IsAutoClearAfterSearch { get; set; }

    /// <summary>
    /// 获得/设置 搜索模式是否为输入即触发 默认 true 值为 false 时需要点击搜索按钮触发
    /// </summary>
    [Parameter]
    public bool IsTriggerSearchByInput { get; set; } = true;

    /// <summary>
    /// 获得/设置 点击搜索按钮时回调委托
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<TValue>>>? OnSearch { get; set; }

    /// <summary>
    /// 获得/设置 通过模型获得显示文本方法 默认使用 ToString 重载方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TValue, string?>? OnGetDisplayText { get; set; }

    /// <summary>
    /// 获得/设置 点击清空按钮时回调委托
    /// </summary>
    [Parameter]
    public Func<string?, Task>? OnClear { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Search<TValue>>? Localizer { get; set; }

    private string? ClassString => CssBuilder.Default("search auto-complete")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? UseInputString => IsTriggerSearchByInput ? null : "false";

    private string? ShowDropdownListOnFocusString => IsTriggerSearchByInput ? "true" : null;

    [NotNull]
    private string? ButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 UI 呈现数据集合
    /// </summary>
    [NotNull]
    private List<TValue>? _filterItems = null;

    private SearchContext<TValue> _context = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _context = new SearchContext<TValue>(this, OnSearchClick, OnClearClick);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputClearIcon);
        ClearButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchClearButtonIcon);
        SearchButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonIcon);
        SearchButtonLoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonLoadingIcon);
        PrefixIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonIcon);

        SearchButtonText ??= Localizer[nameof(SearchButtonText)];
        ButtonIcon ??= SearchButtonIcon;
        NoDataTip ??= Localizer[nameof(NoDataTip)];
        _filterItems ??= [];

        if (Debounce == 0)
        {
            Debounce = 200;
        }
    }

    private string _displayText = "";
    /// <summary>
    /// 点击搜索按钮时触发此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnSearchClick()
    {
        if (OnSearch != null)
        {
            ButtonIcon = SearchButtonLoadingIcon;
            await Task.Yield();

            var items = await OnSearch(_displayText);
            _filterItems = items.ToList();
            ButtonIcon = SearchButtonIcon;
            if (IsTriggerSearchByInput == false)
            {
                await InvokeVoidAsync("showList", Id);
            }
            StateHasChanged();
        }
    }

    /// <summary>
    /// 点击搜索按钮时触发此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClearClick()
    {
        if (OnClear != null)
        {
            await OnClear(_displayText);
        }
        _displayText = "";
        _filterItems = [];
    }

    private string? GetDisplayText(TValue item)
    {
        var displayText = item?.ToString();
        if (OnGetDisplayText != null)
        {
            displayText = OnGetDisplayText(item);
        }
        return displayText;
    }

    /// <summary>
    /// 鼠标点击候选项时回调此方法
    /// </summary>
    private async Task OnClickItem(TValue val)
    {
        CurrentValue = val;
        _displayText = GetDisplayText(val) ?? "";

        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }
    }

    /// <summary>
    /// TriggerFilter 方法
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public override Task TriggerFilter(string val) => TriggerChange(val);

    /// <summary>
    /// TriggerOnChange 方法
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public override async Task TriggerChange(string val)
    {
        _displayText = val;

        if (IsTriggerSearchByInput)
        {
            await OnSearchClick();
        }
    }
}
