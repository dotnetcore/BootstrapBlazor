// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TreeViewToolbarEditButton 组件</para>
/// <para lang="en">TreeViewToolbarEditButton Component</para>
/// </summary>
/// <typeparam name="TItem"></typeparam>
public partial class TreeViewToolbarEditButton<TItem> : ComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 树视图项，默认为 null</para>
    /// <para lang="en">Gets or sets the tree view item. Default is null</para>
    /// </summary>
    [Parameter, NotNull]
    public TreeViewItem<TItem>? Item { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 项更改事件回调</para>
    /// <para lang="en">Gets or sets the item changed event callback</para>
    /// </summary>
    [Parameter]
    public EventCallback<TreeViewItem<TItem>> ItemChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 更新树文本值的回调方法，默认为 null。如果返回 true 将更新树文本值，否则不更新</para>
    /// <para lang="en">Gets or sets the callback method to update the tree text value. Default is null. If it returns true, the tree text value will be updated; otherwise, it will not be updated</para>
    /// </summary>
    [Parameter]
    public Func<TItem, string?, Task<bool>>? OnUpdateCallbackAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹出窗口的标题，默认为 null</para>
    /// <para lang="en">Gets or sets the title of the popup window. Default is null</para>
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹出窗口标签的文本，默认为 null</para>
    /// <para lang="en">Gets or sets the text of the popup window label. Default is null</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑按钮的图标，默认为 null</para>
    /// <para lang="en">Gets or sets the icon of the edit button. Default is null</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? _text;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _text = Item.Text;
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.TreeViewToolbarEditButton);
    }

    private async Task OnConfirm()
    {
        var ret = true;
        if (OnUpdateCallbackAsync != null)
        {
            ret = await OnUpdateCallbackAsync(Item.Value, _text);
        }

        if (ret)
        {
            Item.Text = _text;
            if (ItemChanged.HasDelegate)
            {
                await ItemChanged.InvokeAsync(Item);
            }
        }
    }
}
