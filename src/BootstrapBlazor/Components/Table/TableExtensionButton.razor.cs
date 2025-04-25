// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TableExtensionButton 表格扩展按钮类
/// </summary>
[JSModuleNotInherited]
public partial class TableExtensionButton
{
    /// <summary>
    /// 获得 Toolbar 扩展按钮集合
    /// </summary>
    private readonly List<ITableCellComponent> _buttons = [];

    /// <summary>
    /// Specifies the content to be rendered inside this
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 扩展按钮点击回调方法
    /// </summary>
    [Parameter]
    public Func<TableCellButtonArgs, Task>? OnClickButton { get; set; }

    /// <summary>
    /// 添加按钮到工具栏方法
    /// </summary>
    public void AddButton(ITableCellComponent button) => _buttons.Add(button);

    /// <summary>
    ///从工具栏中移除按钮
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
