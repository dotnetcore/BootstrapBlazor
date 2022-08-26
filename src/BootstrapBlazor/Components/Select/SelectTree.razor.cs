// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Select 组件实现类
/// </summary>
/// <typeparam name="TValue"></typeparam>
[CascadingTypeParameter(nameof(TValue))]
public partial class SelectTree<TValue>
{
    private ElementReference SelectElement { get; set; }

    private JSInterop<SelectTree<TValue>>? Interop { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("select dropdown")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? InputClassName => CssBuilder.Default("form-select")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? AppendClassName => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// 设置当前项是否 Active 方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? ActiveItem(TreeViewItem<TValue> item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", () => FormatValueAsString(item.Value) == CurrentValueAsString)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    /// <summary>
    /// 获得/设置 颜色 默认 Color.None 无设置
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得/设置 搜索文本发生变化时回调此方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<string, IEnumerable<TreeViewItem<TValue>>>? OnSearchTextChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否显示搜索框 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// 获得/设置 选中候选项后是否自动清空搜索框内容 默认 false 不清空
    /// </summary>
    [Parameter]
    public bool AutoClearSearchText { get; set; }

    /// <summary>
    /// 获得 PlaceHolder 属性
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 选项模板支持静态数据
    /// </summary>
    [Parameter]
    public RenderFragment? Options { get; set; }

    /// <summary>
    /// 获得/设置 字符串比较规则 默认 StringComparison.OrdinalIgnoreCase 大小写不敏感 
    /// </summary>
    [Parameter]
    public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// 获得/设置 带层次数据集合
    /// </summary>
    [Parameter]
    [NotNull]
    public List<TreeViewItem<TValue>>? Items { get; set; }

    /// <summary>
    /// SelectedItemChanged 回调方法
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// 获得/设置 下拉框项目改变前回调委托方法 返回 true 时选项值改变，否则选项值不变
    /// </summary>
    [Parameter]
    public Func<TValue, Task<bool>>? OnBeforeSelectedItemChange { get; set; }

    /// <summary>
    /// 获得/设置 Swal 图标 默认 Question
    /// </summary>
    [Parameter]
    public SwalCategory SwalCategory { get; set; } = SwalCategory.Question;

    /// <summary>
    /// 获得/设置 Swal 标题 默认 null
    /// </summary>
    [Parameter]
    public string? SwalTitle { get; set; }

    /// <summary>
    /// 获得/设置 Swal 内容 默认 null
    /// </summary>
    [Parameter]
    public string? SwalContent { get; set; }

    /// <summary>
    /// 获得/设置 Footer 默认 null
    /// </summary>
    [Parameter]
    public string? SwalFooter { get; set; }

    /// <summary>
    /// 获得/设置 点击节点获取子数据集合回调方法
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TValue>, Task<IEnumerable<TreeViewItem<TValue>>>>? OnExpandNodeAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Type CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

    /// <summary>
    /// 获得/设置 比较数据是否相同回调方法 默认为 null
    /// </summary>
    /// <remarks>提供此回调方法时忽略 <see cref="CustomKeyAttribute"/> 属性</remarks>
    [Parameter]
    public Func<TValue, TValue, bool>? ModelEqualityComparer { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Icon 图标 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<SelectTree<TValue>>? Localizer { get; set; }

    /// <summary>
    /// 获得 input 组件 Id 方法
    /// </summary>
    /// <returns></returns>
    protected override string? RetrieveId() => InputId;

    /// <summary>
    /// 获得/设置 Select 内部 Input 组件 Id
    /// </summary>
    private string? InputId => $"{Id}_input";

    /// <summary>
    /// 获得/设置 搜索文字
    /// </summary>
    private string SearchText { get; set; } = "";

    [NotNull]
    private List<TreeViewItem<TValue>>? DataSource { get; set; }

    private TreeViewItem<TValue>? SelectedItem { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (OnSearchTextChanged == null)
        {
            OnSearchTextChanged = text => Items.Where(i => i.Text?.Contains(text, StringComparison) ?? false);
        }
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= new();
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
    }

    //private void ResetSelectedItem()
    //{
    //    if (string.IsNullOrEmpty(SearchText))
    //    {
    //        DataSource = Items;
    //    }
    //    else
    //    {
    //        DataSource = OnSearchTextChanged(SearchText).ToList();
    //    }

    //    SelectedItem = DataSource.FirstOrDefault(i => ComparerItem(i.Value, SelectedItem?.Value))
    //        ?? DataSource.FirstOrDefault(i => i.IsActive)
    //        ?? DataSource.FirstOrDefault();

    //    // 检查 Value 值是否在候选项中存在
    //    // Value 不等于 选中值即不存在
    //    if (SelectedItem != null && SelectedItem.Value != null)
    //    {
    //        var v = FormatValueAsString(SelectedItem.Value);
    //        if (CurrentValueAsString != v)
    //        {
    //            CurrentValueAsString = v;
    //        }
    //    }
    //}

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (Interop == null)
            {
                Interop = new JSInterop<SelectTree<TValue>>(JSRuntime);
            }
            await Interop.InvokeVoidAsync(this, SelectElement, "bb_select", nameof(ConfirmSelectedItem));

            // 选项值不为 null 后者 string.Empty 时触发一次 OnSelectedItemChanged 回调
            if (SelectedItem != null && OnSelectedItemChanged != null)
            {
                await OnSelectedItemChanged.Invoke(SelectedItem.Value);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task ConfirmSelectedItem(int index)
    {
        var ds = string.IsNullOrEmpty(SearchText)
            ? DataSource
            : OnSearchTextChanged.Invoke(SearchText);
        var item = ds.ElementAt(index);
        await OnItemClick(item);
    }

    /// <summary>
    /// 下拉框选项点击时调用此方法
    /// </summary>
    private async Task OnItemClick(TreeViewItem<TValue> item)
    {
        var ret = true;
        if (OnBeforeSelectedItemChange != null)
        {
            ret = await OnBeforeSelectedItemChange(item.Value);
            if (ret)
            {
                // 返回 True 弹窗提示
                var option = new SwalOption()
                {
                    Category = SwalCategory,
                    Title = SwalTitle,
                    Content = SwalContent
                };
                if (!string.IsNullOrEmpty(SwalFooter))
                {
                    option.ShowFooter = true;
                    option.FooterTemplate = builder => builder.AddContent(0, SwalFooter);
                }
                ret = await SwalService.ShowModal(option);
            }
            else
            {
                // 返回 False 直接运行
                ret = true;
            }
        }
        if (ret)
        {
            await ItemChanged(item);
        }

        SelectedItem = item;

        // 关闭弹窗
        await JSRuntime.InvokeVoidAsync(SelectElement, "bb_select_tree");

        StateHasChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task ItemChanged(TreeViewItem<TValue> item)
    {
        item.IsActive = true;
        SelectedItem = item;
        CurrentValue = SelectedItem.Value;

        // 触发 SelectedItemChanged 事件
        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged.Invoke(item.Value);
        }

        if (AutoClearSearchText)
        {
            SearchText = string.Empty;
        }
    }

    /// <summary>
    /// 比较数据是否相同
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    protected bool ComparerItem(TValue a, TValue b) => ModelEqualityComparer?.Invoke(a, b)
        ?? Utility.GetKeyValue<TValue, object>(a, CustomKeyAttribute)?.Equals(Utility.GetKeyValue<TValue, object>(b, CustomKeyAttribute))
        ?? ModelComparer.EqualityComparer(a, b)
        ?? a?.Equals(b)
        ?? false;
}
