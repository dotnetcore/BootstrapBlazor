// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TransferPanel 穿梭框面板组件</para>
/// <para lang="en">TransferPanel Component</para>
/// </summary>
public partial class TransferPanel
{
    /// <summary>
    /// <para lang="zh">获得/设置 搜索关键字</para>
    /// <para lang="en">Gets or sets the search keyword</para>
    /// </summary>
    protected string? SearchText { get; set; }

    private string? PanelClassString => CssBuilder.Default("transfer-panel")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 搜索图标样式</para>
    /// <para lang="en">Gets the search icon style</para>
    /// </summary>
    private string? SearchClass => CssBuilder.Default("input-prefix")
        .AddClass("is-on", !string.IsNullOrEmpty(SearchText))
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 Panel 样式</para>
    /// <para lang="en">Gets the panel style</para>
    /// </summary>
    private string? PanelListClassString => CssBuilder.Default("transfer-panel-list scroll")
        .AddClass("search", ShowSearch)
        .AddClass("disabled", IsDisabled)
        .Build();

    private string? GetItemClass(SelectedItem item) => CssBuilder.Default("transfer-panel-item")
        .AddClass(OnSetItemClass?.Invoke(item))
        .Build();

    /// <summary>
    /// <para lang="zh">获得 组件是否被禁用属性值</para>
    /// <para lang="en">Gets the disabled attribute value</para>
    /// </summary>
    private string? Disabled => IsDisabled ? "disabled" : null;

    /// <summary>
    /// <para lang="zh">获得/设置 数据集合</para>
    /// <para lang="en">Gets or sets the data collection</para>
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public List<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据样式回调方法，默认为 null</para>
    /// <para lang="en">Gets or sets the data style callback method. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<SelectedItem, string?>? OnSetItemClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 面板显示文字</para>
    /// <para lang="en">Gets or sets the panel display text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示搜索框</para>
    /// <para lang="en">Gets or sets whether to display the search box</para>
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索框图标</para>
    /// <para lang="en">Gets or sets the search box icon</para>
    /// </summary>
    [Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项状态变化时回调方法</para>
    /// <para lang="en">Gets or sets the callback method when selected items change</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnSelectedItemsChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索框的 placeholder 字符串</para>
    /// <para lang="en">Gets or sets the search box placeholder string</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SearchPlaceHolderString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用，默认为 false</para>
    /// <para lang="en">Gets or sets whether to disable. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 模板</para>
    /// <para lang="en">Gets or sets the header template</para>
    /// </summary>
    [Parameter]
    public RenderFragment<List<SelectedItem>>? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Item 模板</para>
    /// <para lang="en">Gets or sets the item template</para>
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
    /// <inheritdoc/>
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
    /// <para lang="zh">获得头部复选框初始化状态</para>
    /// <para lang="en">Gets the header checkbox initialization state</para>
    /// </summary>
    protected CheckboxState HeaderCheckState()
    {
        var ret = CheckboxState.Indeterminate;
        if (Items.Count > 0 && Items.All(i => i.Active))
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
    /// <para lang="zh">处理头部复选框状态变化事件</para>
    /// <para lang="en">Handles the header checkbox state change event</para>
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
    /// <para lang="zh">处理项目复选框状态变化事件</para>
    /// <para lang="en">Handles the item checkbox state change event</para>
    /// </summary>
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
    /// <para lang="zh">搜索框文本改变时的回调方法</para>
    /// <para lang="en">Callback method when search box text changes</para>
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
    /// <para lang="zh">搜索文本框按键回调方法</para>
    /// <para lang="en">Callback method for search text box key press</para>
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
    /// <para lang="zh">清空搜索条件方法</para>
    /// <para lang="en">Clears the search condition</para>
    /// </summary>
    protected void ClearSearch()
    {
        SearchText = "";
    }

    private List<SelectedItem> GetShownItems() => (string.IsNullOrEmpty(SearchText)
        ? Items
        : Items.Where(i => i.Text.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList());
}
