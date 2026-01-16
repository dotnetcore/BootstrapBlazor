// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">AutoComplete 组件</para>
///  <para lang="en">AutoComplete component</para>
/// </summary>
public partial class AutoComplete
{
    /// <summary>
    ///  <para lang="zh">获得/设置 通过输入字符串获得的匹配数据集合</para>
    ///  <para lang="en">Gets or sets the collection of matching data obtained by inputting a string</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<string>? Items { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 自定义集合过滤规则 默认为 null</para>
    ///  <para lang="en">Gets or sets custom collection filtering rules, default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<string>>>? OnCustomFilter { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 图标</para>
    ///  <para lang="en">Gets or sets the icon</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 加载图标</para>
    ///  <para lang="en">Gets or sets the loading icon</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 匹配数据时显示的数量</para>
    ///  <para lang="en">Gets or sets the number of items to display when matching data</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public int? DisplayCount { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否开启模糊搜索 默认为 false</para>
    ///  <para lang="en">Gets or sets whether to enable fuzzy search, default is false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsLikeMatch { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 匹配时是否忽略大小写 默认为 true</para>
    ///  <para lang="en">Gets or sets whether to ignore case when matching, default is true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 获得焦点时是否展开下拉候选菜单 默认为 true</para>
    ///  <para lang="en">Gets or sets whether to expand the dropdown candidate menu when focused, default is true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowDropdownListOnFocus { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示无匹配数据选项 默认为 true</para>
    ///  <para lang="en">Gets or sets whether to show the no matching data option, default is true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowNoDataTip { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">IStringLocalizer 服务实例</para>
    ///  <para lang="en">IStringLocalizer service instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 获得焦点时自动显示下拉框设置字符串</para>
    ///  <para lang="en">Gets the string setting for automatically displaying the dropdown when focused</para>
    /// </summary>
    private string? ShowDropdownListOnFocusString => ShowDropdownListOnFocus ? "true" : null;

    private List<string>? _filterItems;

    [NotNull]
    private RenderTemplate? _dropdown = null;

    private string? _clientValue;

    private string? ClassString => CssBuilder.Default("auto-complete")
        .AddClass("is-clearable", IsClearable)
        .Build();

    /// <summary>
    ///  <para lang="zh">获得 清除图标样式</para>
    ///  <para lang="en">Gets the clear icon class string.</para>
    /// </summary>
    private string? ClearClassString => CssBuilder.Default("clear-icon")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    private string? TriggerBlurString => OnBlurAsync != null ? "true" : null;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        SkipRegisterEnterEscJSInvoke = true;

        Items ??= [];

        if (!string.IsNullOrEmpty(Value))
        {
            _filterItems = GetFilterItemsByValue(Value);
            if (DisplayCount != null)
            {
                _filterItems = [.. _filterItems.Take(DisplayCount.Value)];
            }
        }
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NoDataTip ??= Localizer[nameof(NoDataTip)];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AutoCompleteIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.LoadingIcon);
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            if (Value != _clientValue)
            {
                _clientValue = Value;
                await InvokeVoidAsync("setValue", Id, _clientValue);
            }
        }
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Value, GetChangedEventCallbackName());

    private string? GetChangedEventCallbackName() => (OnValueChanged != null || ValueChanged.HasDelegate) ? nameof(TriggerChange) : null;

    /// <summary>
    ///  <para lang="zh">获得 是否显示清除按钮</para>
    ///  <para lang="en">Gets whether show the clear button.</para>
    /// </summary>
    /// <returns></returns>
    private bool GetClearable() => IsClearable && !IsDisabled;

    /// <summary>
    ///  <para lang="zh">点击候选项目时回调方法</para>
    ///  <para lang="en">Callback method when a candidate item is clicked</para>
    /// </summary>
    private async Task OnClickItem(string val)
    {
        CurrentValue = val;

        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }

        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }

        await TriggerFilter(val);

        // 使用脚本更新 input 值
        await InvokeVoidAsync("setValue", Id, val);
    }

    private List<string> Rows => _filterItems ?? [.. Items];

    /// <summary>
    ///  <para lang="zh">点击清空按钮时调用此方法 由 Javascript 触发</para>
    ///  <para lang="en">Method called when the clear button is clicked. Triggered by Javascript</para>
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerClear()
    {
        await TriggerFilter("");

        _clientValue = null;
        CurrentValueAsString = string.Empty;
    }

    /// <summary>
    ///  <para lang="zh">TriggerFilter 方法</para>
    ///  <para lang="en">TriggerFilter method</para>
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public async Task TriggerFilter(string val)
    {
        if (OnCustomFilter != null)
        {
            var items = await OnCustomFilter(val);
            _filterItems = [.. items];
        }
        else if (string.IsNullOrEmpty(val))
        {
            _filterItems = [.. Items];
        }
        else
        {
            _filterItems = GetFilterItemsByValue(val);
        }

        if (DisplayCount != null)
        {
            _filterItems = [.. _filterItems.Take(DisplayCount.Value)];
        }

        // only render the dropdown menu
        _dropdown.Render();
    }

    /// <summary>
    ///  <para lang="zh">支持双向绑定 由客户端 JavaScript 触发</para>
    ///  <para lang="en">Supports two-way binding. Triggered by client-side JavaScript</para>
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    [JSInvokable]
    public void TriggerChange(string v)
    {
        _clientValue = v;
        CurrentValueAsString = v;
    }

    private List<string> GetFilterItemsByValue(string val)
    {
        var comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        var items = IsLikeMatch
            ? Items.Where(s => s.Contains(val, comparison))
            : Items.Where(s => s.StartsWith(val, comparison));
        return [.. items];
    }
}
