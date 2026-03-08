// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">AutoFill 组件</para>
/// <para lang="en">AutoFill component</para>
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public partial class AutoFill<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 组件数据集合</para>
    /// <para lang="en">Gets or sets the collection of items for the component</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TValue>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 匹配数据时显示的数量 默认为 null</para>
    /// <para lang="en">Gets or sets the number of items to display when matching data. Default is null</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public int? DisplayCount { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否开启模糊搜索 默认为 false</para>
    /// <para lang="en">Gets or sets whether to enable fuzzy search. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsLikeMatch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 匹配时是否忽略大小写 默认为 true</para>
    /// <para lang="en">Gets or sets whether to ignore case when matching. Default is true</para>
    /// </summary>
    [Parameter]
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 获得焦点时是否展开下拉候选菜单 默认为 true</para>
    /// <para lang="en">Gets or sets whether to expand the dropdown candidate menu when focused. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowDropdownListOnFocus { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 获取显示文本方法 默认为使用 ToString 方法</para>
    /// <para lang="en">Gets or sets the method to get the display text from the model. Default is to use the ToString override method</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TValue?, string?>? OnGetDisplayText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标</para>
    /// <para lang="en">Gets or sets the icon</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 加载图标</para>
    /// <para lang="en">Gets or sets the loading icon</para>
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义集合过滤规则</para>
    /// <para lang="en">Gets or sets the custom collection filtering rules</para>
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<TValue>>>? OnCustomFilter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示无匹配数据选项 默认为 true</para>
    /// <para lang="en">Gets or sets whether to show the no matching data option. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowNoDataTip { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 候选项模板 默认为 null</para>
    /// <para lang="en">Gets or sets the candidate item template. Default is null</para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ItemTemplate 代替；Deprecated please use ItemTemplate parameter")]
    [ExcludeFromCodeCoverage]
    public RenderFragment<TValue>? Template { get => ItemTemplate; set => ItemTemplate = value; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否开启虚拟滚动 默认为 false</para>
    /// <para lang="en">Gets or sets whether virtual scrolling is enabled. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsVirtualize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 虚拟滚动行高 默认为 50f</para>
    /// <para lang="en">Gets or sets the row height for virtual scrolling. Default is 50f</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">当 <see cref="IsVirtualize"/> 为 true 时生效</para>
    /// <para lang="en">Effective when <see cref="IsVirtualize"/> is set to true</para>
    /// </remarks>
    [Parameter]
    public float RowHeight { get; set; } = 50f;

    /// <summary>
    /// <para lang="zh">获得/设置 虚拟滚动预加载行数 默认为 3</para>
    /// <para lang="en">Gets or sets the overscan count for virtual scrolling. Default is 3</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">当 <see cref="IsVirtualize"/> 为 true 时生效</para>
    /// <para lang="en">Effective when <see cref="IsVirtualize"/> is set to true</para>
    /// </remarks>
    [Parameter]
    public int OverscanCount { get; set; } = 3;

    /// <summary>
    /// <para lang="zh">获得/设置 虚拟滚动加载回调方法</para>
    /// <para lang="en">Gets or sets the callback method for loading virtualized items</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<VirtualizeQueryOption, Task<QueryData<TValue>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击清除按钮回调方法 默认为 null</para>
    /// <para lang="en">Gets or sets the callback method when the clear button is clicked. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 输入框内容无效时是否自动清空内容 默认 false</para>
    /// <para lang="en">Gets or sets whether to clear the content automatically when the input is invalid. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsAutoClearWhenInvalid { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    private string? ShowDropdownListOnFocusString => ShowDropdownListOnFocus ? "true" : null;

    private string? TriggerBlurString => (OnBlurAsync != null || IsAutoClearWhenInvalid) ? "true" : null;

    private string? _displayText;

    private List<TValue>? _filterItems;

    [NotNull]
    private Virtualize<TValue>? _virtualizeElement = null;

    [NotNull]
    private RenderTemplate? _dropdown = null;

    private string? _lastClientValue;

    private string? ClassString => CssBuilder.Default("auto-complete auto-fill")
        .AddClass("is-clearable", IsClearable)
        .Build();

    private string? ClearClassString => CssBuilder.Default("clear-icon")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    private string? PlaceHolderStyleString => Math.Abs(RowHeight - 50f) > 0.1f
        ? CssBuilder.Default().AddClass($"height: {RowHeight.ToString(CultureInfo.InvariantCulture)}px;").Build()
        : null;

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
        _clientValue = _displayText;
        Items ??= [];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _lastClientValue = _clientValue;
        }

        if (_lastClientValue != _clientValue)
        {
            _lastClientValue = _clientValue;
            _filterItems = null;

            _dropdown.Render();
            await InvokeVoidAsync("setValue", Id, _clientValue);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, _displayText, nameof(TriggerChange));

    private string? _clientValue;

    /// <summary>
    /// <para lang="zh">由客户端 JavaScript 触发</para>
    /// <para lang="en">Triggered by client-side JavaScript</para>
    /// </summary>
    /// <param name="v"></param>
    [JSInvokable]
    public void TriggerChange(string v)
    {
        _clientValue = v;
    }

    private bool IsNullable() => !ValueType.IsValueType || NullableUnderlyingType != null;

    private bool GetClearable() => IsClearable && !IsDisabled && IsNullable();

    private async Task OnClickItem(TValue val)
    {
        CurrentValue = val;
        _displayText = GetDisplayText(val);
        _clientValue = _displayText;

        // 使用脚本更新 input 值
        await InvokeVoidAsync("setValue", Id, _displayText);

        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }

        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }

        await TriggerFilter(_displayText!);
    }

    private string? GetDisplayText(TValue item) => OnGetDisplayText?.Invoke(item) ?? item?.ToString();

    private List<TValue> Rows => _filterItems ?? [.. Items];

    private async ValueTask<ItemsProviderResult<TValue>> LoadItems(ItemsProviderRequest request)
    {
        var data = await OnQueryAsync(new() { StartIndex = request.StartIndex, Count = request.Count, SearchText = _searchText });
        var _totalCount = data.TotalCount;
        var items = data.Items ?? [];
        return new ItemsProviderResult<TValue>(items, _totalCount);
    }

    private string? _searchText;

    /// <summary>
    /// <para lang="zh">触发过滤方法</para>
    /// <para lang="en">Triggers the filter method</para>
    /// </summary>
    /// <param name="val"><para lang="zh">过滤值</para><para lang="en">The value to filter by</para></param>
    [JSInvokable]
    public async Task TriggerFilter(string val)
    {
        if (string.IsNullOrEmpty(val))
        {
            CurrentValue = default;
            _filterItems = null;
            _displayText = null;

            if (OnClearAsync != null)
            {
                await OnClearAsync();
            }
        }

        if (OnQueryAsync != null)
        {
            _searchText = val;
            await _virtualizeElement.RefreshDataAsync();
            _dropdown.Render();
            return;
        }

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
            var comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            var items = IsLikeMatch
                ? Items.Where(i => OnGetDisplayText?.Invoke(i)?.Contains(val, comparison) ?? false)
                : Items.Where(i => OnGetDisplayText?.Invoke(i)?.StartsWith(val, comparison) ?? false);
            _filterItems = [.. items];
        }

        if (!IsVirtualize && DisplayCount != null)
        {
            _filterItems = [.. _filterItems.Take(DisplayCount.Value)];
        }
        _dropdown.Render();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnBeforeBlurAsync()
    {
        if (IsAutoClearWhenInvalid && GetDisplayText(Value) != _clientValue)
        {
            CurrentValue = default;
            await InvokeVoidAsync("setValue", Id, "");
        }
    }
}
