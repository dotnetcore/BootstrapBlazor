// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;
using System.Collections;

namespace BootstrapBlazor.Components;

/// <summary>
/// MultiSelect component
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
    /// 获得/设置 显示部分模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment<List<SelectedItem>>? DisplayTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示关闭按钮 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 关闭按钮图标 默认为 null
    /// </summary>
    [Parameter]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示功能按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    /// 获得/设置 是否显示默认功能按钮 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowDefaultButtons { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否固定高度 默认 false
    /// </summary>
    [Parameter]
    public bool IsFixedHeight { get; set; }

    /// <summary>
    /// 获得/设置 是否为单行模式 默认 false
    /// </summary>
    [Parameter]
    public bool IsSingleLine { get; set; }

    /// <summary>
    /// 获得/设置 编辑模式下输入选项更新后回调方法 默认 null
    /// <para>返回 <see cref="SelectedItem"/> 实例时输入选项生效，返回 null 时选项不生效进行舍弃操作，建议在回调方法中自行提示</para>
    /// </summary>
    /// <remarks>Effective when <see cref="SimpleSelectBase{TValue}.IsEditable"/> is set.</remarks>
    [Parameter]
    public Func<string, Task<SelectedItem>>? OnEditCallback { get; set; }

    /// <summary>
    /// 获得/设置 编辑提交按键 默认 Enter
    /// </summary>
    [Parameter]
    public EditSubmitKey EditSubmitKey { get; set; }

    /// <summary>
    /// 获得/设置 扩展按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment? ButtonTemplate { get; set; }

    /// <summary>
    /// 获得/设置 选中项集合发生改变时回调委托方法
    /// </summary>
    [Parameter]
    public Func<IEnumerable<SelectedItem>, Task>? OnSelectedItemsChanged { get; set; }

    /// <summary>
    /// Gets or sets the default virtualize items text.
    /// </summary>
    [Parameter]
    public string? DefaultVirtualizeItemText { get; set; }

    /// <summary>
    /// 获得/设置 全选按钮显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SelectAllText { get; set; }

    /// <summary>
    /// 获得/设置 全选按钮显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ReverseSelectText { get; set; }

    /// <summary>
    /// 获得/设置 全选按钮显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearText { get; set; }

    /// <summary>
    /// 获得/设置 选项最大数 默认为 0 不限制
    /// </summary>
    [Parameter]
    public int Max { get; set; }

    /// <summary>
    /// 获得/设置 设置最大值时错误消息文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MaxErrorMessage { get; set; }

    /// <summary>
    /// 获得/设置 选项最小数 默认为 0 不限制
    /// </summary>
    [Parameter]
    public int Min { get; set; }

    /// <summary>
    /// 获得/设置 设置最小值时错误消息文字
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

        // 通过 Value 对集合进行赋值
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
    /// <returns></returns>
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
        // 有搜索条件时使用原生请求数量
        // 有总数时请求剩余数量
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
    /// <returns></returns>
    protected override async Task OnClearValue()
    {
        await base.OnClearValue();

        SelectedItems.Clear();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected override string? FormatValueAsString(TValue? value) => value == null
        ? null
        : Utility.ConvertValueToString(value);

    private bool _isToggle;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
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
    /// 客户端回车回调方法
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
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
    /// 切换当前选项方法
    /// </summary>
    /// <returns></returns>
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
            // 更新选中值
            await SetValue();
        }
    }

    /// <summary>
    /// 客户端编辑提交数据回调方法
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
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
            // 更新选中值
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
            ToggleMessage(validationResults);
        }

        if (OnSelectedItemsChanged != null)
        {
            await OnSelectedItemsChanged.Invoke(SelectedItems);
        }

        _lastSelectedValueString = CurrentValueAsString;
        if (!ValueChanged.HasDelegate)
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// 清除选择项方法
    /// </summary>
    /// <returns></returns>
    public async Task Clear()
    {
        SelectedItems.Clear();
        await SetValue();
    }

    /// <summary>
    /// 全选选择项方法
    /// </summary>
    /// <returns></returns>
    public async Task SelectAll()
    {
        SelectedItems.Clear();
        SelectedItems.AddRange(Rows);
        await SetValue();
    }

    /// <summary>
    /// 翻转选择项方法
    /// </summary>
    /// <returns></returns>
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
    /// 客户端检查完成时调用此方法
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
            // 判断 IEnumerable<T> 泛型 T 是否为 Enum
            // 特别注意 string 是 IEnumerable 的实例
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
