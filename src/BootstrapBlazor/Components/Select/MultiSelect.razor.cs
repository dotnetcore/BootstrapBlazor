// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;
using System.Collections;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">MultiSelect component</para>
/// <para lang="en">MultiSelect component</para>
/// </summary>
public partial class MultiSelect<TValue>
{
    private List<SelectedItem> SelectedItems { get; } = [];

    private string? ClassString => CssBuilder.Default("select dropdown multi-select")
        .AddClass("is-clearable", IsClearable)
        .Build();

    private string? DropdownMenuClassString => CssBuilder.Default("dropdown-menu")
        .AddClass("is-fixed-toolbar", ShowToolbar)
        .Build();

    private string? EditSubmitKeyString => EditSubmitKey == EditSubmitKey.Space ? EditSubmitKey.ToDescriptionString() : null;

    private string? ToggleClassString => CssBuilder.Default("dropdown-toggle scroll")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled)
        .AddClass("is-fixed", IsFixedHeight)
        .AddClass("is-single-line", IsSingleLine)
        .AddClass("disabled", IsDisabled)
        .AddClass("show", ValidateForm != null && _isToggle)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    private string? GetItemClassString(SelectedItem item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", GetCheckedState(item))
        .AddClass("disabled", item.IsDisabled)
        .Build();

    private string? PlaceHolderClassString => CssBuilder.Default("multi-select-ph")
        .AddClass("d-none", SelectedItems.Count != 0)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 显示部分模板 默认 null</para>
    /// <para lang="en">Gets or sets Display Template. Default null</para>
    /// </summary>
    [Parameter]
    public RenderFragment<List<SelectedItem>>? DisplayTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭按钮 默认为 true 显示</para>
    /// <para lang="en">Gets or sets Whether to show close button. Default true</para>
    /// </summary>
    [Parameter]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮图标 默认为 null</para>
    /// <para lang="en">Gets or sets Close Button Icon. Default null</para>
    /// </summary>
    [Parameter]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示功能按钮 默认为 false 不显示</para>
    /// <para lang="en">Gets or sets Whether to show toolbar. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示默认功能按钮 默认为 true 显示</para>
    /// <para lang="en">Gets or sets Whether to show default buttons. Default true</para>
    /// </summary>
    [Parameter]
    public bool ShowDefaultButtons { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否固定高度 默认 false</para>
    /// <para lang="en">Gets or sets Whether fixed height. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsFixedHeight { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为单行模式 默认 false</para>
    /// <para lang="en">Gets or sets Whether single line mode. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsSingleLine { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑模式下输入选项更新后回调方法 默认 null</para>
    /// <para lang="en">Gets or sets Callback method after input option updated in edit mode. Default null</para>
    /// <para lang="zh">返回 <see cref="SelectedItem"/> 实例时输入选项生效，返回 null 时选项不生效进行舍弃操作，建议在回调方法中自行提示</para>
    /// <para lang="en">Return <see cref="SelectedItem"/> instance to take effect, return null to discard, recommend prompt in callback method</para>
    /// </summary>
    /// <remarks>Effective when <see cref="SimpleSelectBase{TValue}.IsEditable"/> is set.</remarks>
    [Parameter]
    public Func<string, Task<SelectedItem>>? OnEditCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑提交按键 默认 Enter</para>
    /// <para lang="en">Gets or sets Edit Submit Key. Default Enter</para>
    /// </summary>
    [Parameter]
    public EditSubmitKey EditSubmitKey { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 扩展按钮模板</para>
    /// <para lang="en">Gets or sets Extension Button Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ButtonTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选中项集合发生改变时回调委托方法</para>
    /// <para lang="en">Gets or sets Selected Items Changed Callback Method</para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<SelectedItem>, Task>? OnSelectedItemsChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the default virtualize items text.</para>
    /// <para lang="en">Gets or sets the default virtualize items text.</para>
    /// </summary>
    [Parameter]
    public string? DefaultVirtualizeItemText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 全选按钮显示文本</para>
    /// <para lang="en">Gets or sets Select All Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SelectAllText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 全选按钮显示文本</para>
    /// <para lang="en">Gets or sets Reverse Select Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ReverseSelectText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 全选按钮显示文本</para>
    /// <para lang="en">Gets or sets Clear Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项最大数 默认为 0 不限制</para>
    /// <para lang="en">Gets or sets Max items. Default 0 (unlimited)</para>
    /// </summary>
    [Parameter]
    public int Max { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 设置最大值时错误消息文字</para>
    /// <para lang="en">Gets or sets Max Error Message</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MaxErrorMessage { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项最小数 默认为 0 不限制</para>
    /// <para lang="en">Gets or sets Min items. Default 0 (unlimited)</para>
    /// </summary>
    [Parameter]
    public int Min { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 设置最小值时错误消息文字</para>
    /// <para lang="en">Gets or sets Min Error Message</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MinErrorMessage { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<MultiSelect<TValue>>? Localizer { get; set; }

    private string? PlaceholderString => SelectedItems.Count == 0 ? PlaceHolder : null;

    private string? ScrollIntoViewBehaviorString => ScrollIntoViewBehavior == ScrollIntoViewBehavior.Smooth ? null : ScrollIntoViewBehavior.ToDescriptionString();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        SelectAllText ??= Localizer[nameof(SelectAllText)];
        ReverseSelectText ??= Localizer[nameof(ReverseSelectText)];
        ClearText ??= Localizer[nameof(ClearText)];
        MinErrorMessage ??= Localizer[nameof(MinErrorMessage)];
        MaxErrorMessage ??= Localizer[nameof(MaxErrorMessage)];
        NoSearchDataText ??= Localizer[nameof(NoSearchDataText)];

        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.MultiSelectDropdownIcon);
        CloseButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.MultiSelectCloseIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.MultiSelectClearIcon);

        ResetItems();
        ResetRules();

        _itemsCache = null;

        // <para lang="zh">通过 Value 对集合进行赋值</para>
        // <para lang="en">Assign collection by Value</para>
        var _currentValue = CurrentValueAsString;
        if (_lastSelectedValueString != _currentValue)
        {
            _lastSelectedValueString = _currentValue;

            SelectedItems.Clear();
            if (IsVirtualize)
            {
                SelectedItems.AddRange(GetItemsByVirtualize());
            }
            else
            {
                var list = _currentValue.Split(',', StringSplitOptions.RemoveEmptyEntries);
                SelectedItems.AddRange(Rows.Where(item => list.Any(i => i.Trim() == item.Value)));
            }

            if (SelectedItems.Count == 0)
            {
                _lastSelectedValueString = string.Empty;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        _isToggle = false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new
    {
        ConfirmMethodCallback = nameof(ConfirmSelectedItem),
        SearchMethodCallback = nameof(TriggerOnSearch),
        TriggerEditTag = nameof(TriggerEditTag),
        ToggleRow = nameof(ToggleRow)
    });

    private List<SelectedItem> GetItemsByVirtualize()
    {
        var ret = new List<SelectedItem>();
        var texts = new List<string>();
        if (!string.IsNullOrEmpty(DefaultVirtualizeItemText))
        {
            texts.AddRange(DefaultVirtualizeItemText.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
        var values = CurrentValueAsString.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        for (int i = 0; i < values.Count; i++)
        {
            var text = i < texts.Count ? texts[i] : values[i];
            ret.Add(new SelectedItem(values[i].Trim(), text.Trim()));
        }
        return ret;
    }

    private int _totalCount;
    private ItemsProviderResult<SelectedItem> _result;

    private List<SelectedItem> GetVirtualItems() => [.. FilterBySearchText(GetRowsByItems())];

    private async ValueTask<ItemsProviderResult<SelectedItem>> LoadItems(ItemsProviderRequest request)
    {
        // <para lang="zh">有搜索条件时使用原生请求数量</para>
        // <para lang="en">Use original request count when there is search condition</para>
        // <para lang="zh">有总数时请求剩余数量</para>
        // <para lang="en">Request remaining count when there is total count</para>
        var count = !string.IsNullOrEmpty(SearchText) ? request.Count : GetCountByTotal();
        var data = await OnQueryAsync(new() { StartIndex = request.StartIndex, Count = count, SearchText = SearchText });

        _itemsCache = null;
        _totalCount = data.TotalCount;
        var items = data.Items ?? [];
        _result = new ItemsProviderResult<SelectedItem>(items, _totalCount);
        return _result;

        int GetCountByTotal() => _totalCount == 0 ? request.Count : Math.Min(request.Count, _totalCount - request.StartIndex);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnClearValue()
    {
        await base.OnClearValue();

        SelectedItems.Clear();
        await SetValue();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    protected override string? FormatValueAsString(TValue? value) => value == null
        ? null
        : Utility.ConvertValueToString(value);

    private bool _isToggle;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override List<SelectedItem> GetRowsByItems()
    {
        var items = new List<SelectedItem>();
        if (_result.Items != null)
        {
            items.AddRange(_result.Items);
        }
        else if (Items != null)
        {
            items.AddRange(Items);
        }
        return items;
    }

    /// <summary>
    /// <para lang="zh">客户端回车回调方法</para>
    /// <para lang="en">Client-side Enter Callback Method</para>
    /// </summary>
    /// <param name="index"></param>
    [JSInvokable]
    public async Task ConfirmSelectedItem(int index)
    {
        var rows = Rows;
        if (index < rows.Count)
        {
            await ToggleRow(rows[index].Value);
            StateHasChanged();
        }
    }

    /// <summary>
    /// <para lang="zh">切换当前选项方法</para>
    /// <para lang="en">Toggle Current Option Method</para>
    /// </summary>
    [JSInvokable]
    public async Task ToggleRow(string val)
    {
        if (!IsDisabled)
        {
            var item = SelectedItems.FirstOrDefault(i => i.Value == val);
            if (item != null)
            {
                SelectedItems.Remove(item);
            }
            else
            {
                var d = Rows.FirstOrDefault(i => i.Value == val);
                if (d != null)
                {
                    SelectedItems.Add(d);
                }
            }

            _isToggle = true;
            // <para lang="zh">更新选中值</para>
            // <para lang="en">Update selected value</para>
            await SetValue();
        }
    }

    /// <summary>
    /// <para lang="zh">客户端编辑提交数据回调方法</para>
    /// <para lang="en">Client-side Edit Submit Data Callback Method</para>
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public async Task<bool> TriggerEditTag(string val)
    {
        SelectedItem? ret = null;
        val = val.Trim();
        if (OnEditCallback != null)
        {
            ret = await OnEditCallback(val);
        }
        else if (!string.IsNullOrEmpty(val))
        {
            ret = Rows.Find(i => i.Text.Equals(val, StringComparison.OrdinalIgnoreCase)) ?? new SelectedItem(val, val);
        }
        if (ret != null)
        {
            if (SelectedItems.Find(i => i.Text.Equals(val, StringComparison.OrdinalIgnoreCase)) == null)
            {
                SelectedItems.Add(ret);
            }
            // <para lang="zh">更新选中值</para>
            // <para lang="en">Update selected value</para>
            _isToggle = true;
            await SetValue();
        }
        return ret != null;
    }

    private string? GetValueString(SelectedItem item) => IsPopover ? item.Value : null;

    private int _min;
    private int _max;
    private void ResetRules()
    {
        if (Max != _max)
        {
            _max = Max;
            Rules.RemoveAll(v => v is MaxValidator);

            if (Max > 0)
            {
                Rules.Add(new MaxValidator() { Value = Max, ErrorMessage = MaxErrorMessage });
            }
        }

        if (Min != _min)
        {
            _min = Min;
            Rules.RemoveAll(v => v is MinValidator);

            if (Min > 0)
            {
                Rules.Add(new MinValidator() { Value = Min, ErrorMessage = MinErrorMessage });
            }
        }
    }

    private async Task SetValue()
    {
        if (ValueType == typeof(string))
        {
            CurrentValueAsString = string.Join(",", SelectedItems.Select(i => i.Value));
        }
        else if (ValueType.IsGenericType || ValueType.IsArray)
        {
            var t = ValueType.IsGenericType ? ValueType.GenericTypeArguments[0] : ValueType.GetElementType()!;
            var listType = typeof(List<>).MakeGenericType(t);
            var instance = (IList)Activator.CreateInstance(listType, SelectedItems.Count)!;

            foreach (var item in SelectedItems)
            {
                if (item.Value.TryConvertTo(t, out var val))
                {
                    instance.Add(val);
                }
            }
            CurrentValue = (TValue)(ValueType.IsGenericType ? instance : listType.GetMethod("ToArray")!.Invoke(instance, null)!);
        }
        else if (ValueType.IsFlagEnum())
        {
            CurrentValue = (TValue?)SelectedItems.ParseFlagEnum<TValue>(ValueType);
        }

        if (ValidateForm == null && (Min > 0 || Max > 0))
        {
            var validationContext = new ValidationContext(Value!) { MemberName = FieldIdentifier?.FieldName };
            var validationResults = new List<ValidationResult>();

            await ValidatePropertyAsync(CurrentValue, validationContext, validationResults);
            await ToggleMessage(validationResults);
        }

        if (OnSelectedItemsChanged != null)
        {
            await OnSelectedItemsChanged(SelectedItems);
        }

        _lastSelectedValueString = CurrentValueAsString;
        if (!ValueChanged.HasDelegate)
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// <para lang="zh">清除选择项方法</para>
    /// <para lang="en">Clear Items Method</para>
    /// </summary>
    public async Task Clear()
    {
        SelectedItems.Clear();
        await SetValue();
    }

    /// <summary>
    /// <para lang="zh">全选选择项方法</para>
    /// <para lang="en">Select All Items Method</para>
    /// </summary>
    public async Task SelectAll()
    {
        SelectedItems.Clear();
        SelectedItems.AddRange(Rows);
        await SetValue();
    }

    /// <summary>
    /// <para lang="zh">翻转选择项方法</para>
    /// <para lang="en">Invert Selection Method</para>
    /// </summary>
    public async Task InvertSelect()
    {
        var items = Rows.Where(item => !SelectedItems.Any(i => i.Value == item.Value)).ToList();
        SelectedItems.Clear();
        SelectedItems.AddRange(items);
        await SetValue();
    }

    private bool GetCheckedState(SelectedItem item) => SelectedItems.Any(i => i.Value == item.Value);

    private string? GetCheckedString(SelectedItem item) => GetCheckedState(item) ? "checked" : null;

    private bool CheckCanTrigger(SelectedItem item)
    {
        var ret = true;
        if (Max > 0)
        {
            ret = SelectedItems.Count < Max || GetCheckedState(item);
        }
        return ret;
    }

    private bool CheckCanSelect(SelectedItem item)
    {
        var ret = GetCheckedState(item);
        if (!ret)
        {
            ret = CheckCanTrigger(item);
        }
        return !ret;
    }

    private bool CheckCanEdit()
    {
        var ret = IsEditable;
        if (ret == false)
        {
            return false;
        }

        if (Max > 0)
        {
            ret = SelectedItems.Count < Max;
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">客户端检查完成时调用此方法</para>
    /// <para lang="en">Called when client-side validation is completed</para>
    /// </summary>
    /// <param name="valid"></param>
    protected override void OnValidate(bool? valid)
    {
        if (valid != null)
        {
            Color = valid.Value ? Color.Success : Color.Danger;
        }
    }

    private void ResetItems()
    {
        if (Items == null)
        {
            // <para lang="zh">判断 IEnumerable&lt;T&gt; 泛型 T 是否为 Enum</para>
            // <para lang="en">Determine if generic T of IEnumerable&lt;T&gt; is Enum</para>
            // <para lang="zh">特别注意 string 是 IEnumerable 的实例</para>
            // <para lang="en">Note that string is an instance of IEnumerable</para>
            var type = typeof(TValue);
            Type? innerType;
            if (type.IsGenericType && type.IsAssignableTo(typeof(IEnumerable)))
            {
                innerType = type.GetGenericArguments()[0];
            }
            else
            {
                innerType = NullableUnderlyingType ?? type;
            }
            if (innerType.IsEnum)
            {
                Items = innerType.ToSelectList();
            }
            else
            {
                Items = [];
            }
        }
    }
}
