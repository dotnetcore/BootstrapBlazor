﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
        await b.OnConfirm();

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
