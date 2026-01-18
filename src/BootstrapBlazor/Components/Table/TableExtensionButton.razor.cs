// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TableExtensionButton 表格扩展按钮类</para>
/// <para lang="en">TableExtensionButton table extension button class</para>
/// </summary>
[JSModuleNotInherited]
public partial class TableExtensionButton
{
    private readonly List<ITableCellComponent> _buttons = [];

    /// <summary>
    /// <para lang="zh">获得/设置 子组件内容</para>
    /// <para lang="en">Gets or sets the content to be rendered inside this</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 扩展按钮点击回调方法</para>
    /// <para lang="en">Gets or sets extension button click callback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TableCellButtonArgs, Task>? OnClickButton { get; set; }

    /// <summary>
    /// <para lang="zh">添加按钮到工具栏方法</para>
    /// <para lang="en">Add button to toolbar method</para>
    /// </summary>
    public void AddButton(ITableCellComponent button) => _buttons.Add(button);

    /// <summary>
    /// <para lang="zh">从工具栏中移除按钮</para>
    /// <para lang="en">Remove button from toolbar</para>
    /// </summary>
    public void RemoveButton(ITableCellComponent button) => _buttons.Remove(button);

    private async Task OnClick(TableCellButton b)
    {
        if (b.OnClick.HasDelegate)
        {
            await b.OnClick.InvokeAsync();
        }
        if (b.OnClickWithoutRender != null)
        {
            await b.OnClickWithoutRender();
        }

        if (OnClickButton != null)
        {
            await OnClickButton(new TableCellButtonArgs()
            {
                AutoRenderTableWhenClick = b.AutoRenderTableWhenClick,
                AutoSelectedRowWhenClick = b.AutoSelectedRowWhenClick
            });
        }
    }

    private async Task OnClickConfirm(TableCellPopConfirmButton b)
    {
        if (b.OnConfirm != null)
        {
            await b.OnConfirm();
        }

        if (OnClickButton != null)
        {
            await OnClickButton(new TableCellButtonArgs()
            {
                AutoRenderTableWhenClick = b.AutoRenderTableWhenClick,
                AutoSelectedRowWhenClick = b.AutoSelectedRowWhenClick
            });
        }
    }
}
