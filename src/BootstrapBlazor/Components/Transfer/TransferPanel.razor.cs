// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TransferPanelBase 穿梭框面板组件
///</para>
/// <para lang="en">TransferPanelBase 穿梭框面板component
///</para>
/// </summary>
public partial class TransferPanel
{
    /// <summary>
    /// <para lang="zh">获得/设置 搜索关键字
    ///</para>
    /// <para lang="en">Gets or sets 搜索关键字
    ///</para>
    /// </summary>
    protected string? SearchText { get; set; }

    private string? PanelClassString => CssBuilder.Default("transfer-panel")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 搜索图标样式
    ///</para>
    /// <para lang="en">Gets 搜索iconstyle
    ///</para>
    /// </summary>
    private string? SearchClass => CssBuilder.Default("input-prefix")
        .AddClass("is-on", !string.IsNullOrEmpty(SearchText))
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 Panel 样式
    ///</para>
    /// <para lang="en">Gets Panel style
    ///</para>
    /// </summary>
    private string? PanelListClassString => CssBuilder.Default("transfer-panel-list scroll")
        .AddClass("search", ShowSearch)
        .AddClass("disabled", IsDisabled)
        .Build();

    private string? GetItemClass(SelectedItem item) => CssBuilder.Default("transfer-panel-item")
        .AddClass(OnSetItemClass?.Invoke(item))
        .Build();

    /// <summary>
    /// <para lang="zh">获得 组件是否被禁用属性值
    ///</para>
    /// <para lang="en">Gets componentwhether被禁用property值
    ///</para>
    /// </summary>
    private string? Disabled => IsDisabled ? "disabled" : null;

    /// <summary>
    /// <para lang="zh">获得/设置 数据集合
    ///</para>
    /// <para lang="en">Gets or sets datacollection
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public List<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据样式回调方法 默认为 null
    ///</para>
    /// <para lang="en">Gets or sets datastylecallback method Default is为 null
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<SelectedItem, string?>? OnSetItemClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 面板显示文字
    ///</para>
    /// <para lang="en">Gets or sets 面板display文字
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示搜索框
    ///</para>
    /// <para lang="en">Gets or sets whetherdisplay搜索框
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索框图标
    ///</para>
    /// <para lang="en">Gets or sets 搜索框icon
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项状态变化时回调方法
    ///</para>
    /// <para lang="en">Gets or sets 选项状态变化时callback method
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnSelectedItemsChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索框的 placeholder 字符串
    ///</para>
    /// <para lang="en">Gets or sets 搜索框的 placeholder 字符串
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SearchPlaceHolderString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用 默认为 false
    ///</para>
    /// <para lang="en">Gets or sets whether禁用 Default is为 false
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 模板
    ///</para>
    /// <para lang="en">Gets or sets Header template
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<List<SelectedItem>>? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Item 模板
    ///</para>
    /// <para lang="en">Gets or sets Item template
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Transfer<string>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法
    ///</para>
    /// <para lang="en">OnParametersSet 方法
    ///</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= [];
        SearchPlaceHolderString ??= Localizer[nameof(SearchPlaceHolderString)];
        Text ??= Localizer[nameof(Text)];

        SearchIcon ??= IconTheme.GetIconByKey(ComponentIcons.TransferPanelSearchIcon);
    }

    /// <summary>
    /// <para lang="zh">头部复选框初始化值方法
    ///</para>
    /// <para lang="en">头部复选框初始化值方法
    ///</para>
    /// </summary>
    protected CheckboxState HeaderCheckState()
    {
        var ret = CheckboxState.Indeterminate;
        if (Items.Any() && Items.All(i => i.Active))
        {
            ret = CheckboxState.Checked;
        }
        else if (!Items.Any(i => i.Active))
        {
            ret = CheckboxState.UnChecked;
        }

        return ret;
    }

    /// <summary>
    /// <para lang="zh">左侧头部复选框初始化值方法
    ///</para>
    /// <para lang="en">左侧头部复选框初始化值方法
    ///</para>
    /// </summary>
    protected async Task OnHeaderCheck(CheckboxState state, SelectedItem item)
    {
        if (Items != null)
        {
            if (state == CheckboxState.Checked)
            {
                GetShownItems().ForEach(i => i.Active = true);
            }
            else
            {
                GetShownItems().ForEach(i => i.Active = false);
            }

            if (OnSelectedItemsChanged != null)
            {
                await OnSelectedItemsChanged();
            }
        }
    }

    /// <summary>
    /// <para lang="zh">///</para>
    /// <para lang="en">///</para>
    /// </summary>
    /// <returns></returns>
    protected async Task OnStateChanged(CheckboxState state, SelectedItem item)
    {
        // trigger when transfer item clicked
        item.Active = state == CheckboxState.Checked;

        // set header
        if (OnSelectedItemsChanged != null)
        {
            await OnSelectedItemsChanged();
        }
    }

    /// <summary>
    /// <para lang="zh">搜索框文本改变时回调此方法
    ///</para>
    /// <para lang="en">搜索框文本改变时回调此方法
    ///</para>
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnSearch(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            SearchText = e.Value.ToString();
        }
    }

    /// <summary>
    /// <para lang="zh">搜索文本框按键回调方法
    ///</para>
    /// <para lang="en">搜索文本框按键callback method
    ///</para>
    /// </summary>
    /// <param name="e"></param>
    protected void OnKeyUp(KeyboardEventArgs e)
    {
        // Escape
        if (e.Key == "Escape")
        {
            ClearSearch();
        }
    }

    /// <summary>
    /// <para lang="zh">清空搜索条件方法
    ///</para>
    /// <para lang="en">清空搜索条件方法
    ///</para>
    /// </summary>
    protected void ClearSearch()
    {
        SearchText = "";
    }

    private List<SelectedItem> GetShownItems() => (string.IsNullOrEmpty(SearchText)
        ? Items
        : Items.Where(i => i.Text.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList());
}
