// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Select 组件实现类</para>
/// <para lang="en">Select Component Implementation Class</para>
/// </summary>
/// <typeparam name="TValue"></typeparam>
public partial class SelectTree<TValue> : IModelEqualityComparer<TValue>
{
    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Get Class Name</para>
    /// </summary>
    private string? ClassName => CssBuilder.Default("select dropdown select-tree")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Get Input Class Name</para>
    /// </summary>
    private string? InputClassName => CssBuilder.Default("form-select form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Get Append Class Name</para>
    /// </summary>
    private string? AppendClassName => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 颜色 默认 Color.None 无设置</para>
    /// <para lang="en">Get/Set Color. Default Color.None</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// <para lang="zh">获得 PlaceHolder 属性</para>
    /// <para lang="en">Get PlaceHolder Attribute</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 nodes can be expanded or collapsed when the component is disabled. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether nodes can be expanded or collapsed when the component is disabled. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool CanExpandWhenDisabled { get; set; } = false;

    /// <summary>
    /// <para lang="zh">获得/设置 字符串比较规则 默认 StringComparison.OrdinalIgnoreCase 大小写不敏感</para>
    /// <para lang="en">Get/Set String Comparison. Default StringComparison.OrdinalIgnoreCase</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <para lang="zh">获得/设置 带层次数据集合</para>
    /// <para lang="en">Get/Set Hierarchical Data Collection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public List<TreeViewItem<TValue>>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">SelectedItemChanged 回调方法</para>
    /// <para lang="en">SelectedItemChanged Callback Method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击节点获取子数据集合回调方法</para>
    /// <para lang="en">Get/Set OnExpandNodeAsync Callback Method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TreeViewItem<TValue>, Task<IEnumerable<TreeViewItem<TValue>>>>? OnExpandNodeAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Type CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

    /// <summary>
    /// <para lang="zh">获得/设置 比较数据是否相同回调方法 默认为 null</para>
    /// <para lang="en">Get/Set Model Equality Comparer. Default null</para>
    /// <para lang="zh">提供此回调方法时忽略 <see cref="CustomKeyAttribute"/> 属性</para>
    /// <para lang="en">Ignore <see cref="CustomKeyAttribute"/> when providing this callback</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TValue, TValue, bool>? ModelEqualityComparer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Icon 图标 默认 false 不显示</para>
    /// <para lang="en">Get/Set Whether to show Icon. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下拉箭头 Icon 图标</para>
    /// <para lang="en">Get/Set Dropdown Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可编辑 默认 false</para>
    /// <para lang="en">Get/Set Whether editable. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [ExcludeFromCodeCoverage]
    [Obsolete("已过期，请使用 IsEditable Please use IsEditable parameter")]
    public bool IsEdit { get => IsEditable; set => IsEditable = value; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可编辑 默认 false</para>
    /// <para lang="en">Get/Set Whether editable. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsEditable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示搜索栏 默认 false 不显示</para>
    /// <para lang="en">Get/Set Whether to show search box. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否固定搜索栏 默认 false 不固定</para>
    /// <para lang="en">Get/Set Whether fixed search box. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    public bool IsFixedSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示重置搜索栏按钮 默认 true 显示</para>
    /// <para lang="en">Get/Set Whether to show reset search button. Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowResetSearchButton { get; set; } = true;

    [Inject]
    [NotNull]
    private IStringLocalizer<SelectTree<TValue>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh">获得 input 组件 Id 方法</para>
    /// <para lang="en">Get input Component Id Method</para>
    /// </summary>
    /// <returns></returns>
    protected override string? RetrieveId() => InputId;

    /// <summary>
    /// <para lang="zh">获得/设置 Select 内部 Input 组件 Id</para>
    /// <para lang="en">Get/Set Select Internal Input Component Id</para>
    /// </summary>
    private string? InputId => $"{Id}_input";

    private TreeViewItem<TValue>? _selectedItem;
    private List<TreeViewItem<TValue>>? _itemCache;
    private List<TreeViewItem<TValue>>? _expandedItemsCache;
    private TreeView<TValue> _tv = default!;

    private string? SelectTreeCustomClassString => CssBuilder.Default(CustomClassString)
        .AddClass("select-tree", IsPopover)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // <para lang="zh">处理 Required 标签</para>
        // <para lang="en">Process Required Tag</para>
        AddRequiredValidator();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectTreeDropdownIcon);
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        Items ??= [];

        if (Value == null)
        {
            // <para lang="zh">组件未赋值 Value 通过 IsActive 设置默认值</para>
            // <para lang="en">Value is not set, set default value by IsActive</para>
            await TriggerItemChanged(s => s.IsActive);
        }
        else
        {
            // <para lang="zh">组件已赋值 Value 通过 Value 设置默认值</para>
            // <para lang="en">Value is set, set default value by Value</para>
            await TriggerItemChanged(s => Equals(s.Value, Value));
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    /// <returns></returns>
    protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage)
    {
        result = (TValue)(object)value;
        validationErrorMessage = null;
        return true;
    }

    private void OnChange(ChangeEventArgs args)
    {
        if (args.Value is string v)
        {
            CurrentValueAsString = v;

            // <para lang="zh">选中节点更改为当前值</para>
            // <para lang="en">Selected node changed to current value</para>
            _tv.SetActiveItem(Value);
        }
    }

    private async Task TriggerItemChanged(Func<TreeViewItem<TValue>, bool> predicate)
    {
        var currentItem = GetExpandedItems().FirstOrDefault(predicate);
        if (currentItem != null)
        {
            currentItem.IsActive = true;

            if (_selectedItem == null || !Equals(_selectedItem.Value, Value))
            {
                await ItemChanged(currentItem);
            }
        }
    }

    private List<TreeViewItem<TValue>> GetExpandedItems()
    {
        if (_itemCache != Items)
        {
            _itemCache = Items;
            _expandedItemsCache = [.. TreeViewExtensions.GetAllItems(_itemCache)];
        }
        return _expandedItemsCache!;
    }

    /// <summary>
    /// <para lang="zh">下拉框选项点击时调用此方法</para>
    /// <para lang="en">Called when dropdown option is clicked</para>
    /// </summary>
    private async Task OnItemClick(TreeViewItem<TValue> item)
    {
        if (!Equals(item.Value, CurrentValue))
        {
            await ItemChanged(item);
            StateHasChanged();
        }
    }

    /// <summary>
    /// <para lang="zh">选中项更改处理方法</para>
    /// <para lang="en">Selected Item Changed Method</para>
    /// </summary>
    /// <returns></returns>
    private async Task ItemChanged(TreeViewItem<TValue> item)
    {
        _selectedItem = item;
        CurrentValue = item.Value;

        // <para lang="zh">触发 SelectedItemChanged 事件</para>
        // <para lang="en">Trigger SelectedItemChanged Event</para>
        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged.Invoke(CurrentValue);
        }
    }

    /// <summary>
    /// <para lang="zh">比较数据是否相同</para>
    /// <para lang="en">Compare Data Equality</para>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(TValue? x, TValue? y) => this.Equals<TValue>(x, y);
}
