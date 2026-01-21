// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">单元格内按钮组件</para>
/// <para lang="en">Button component in table cell</para>
/// </summary>
[JSModuleNotInherited]
public class TableCellButton : ButtonBase, ITableCellButton
{
    /// <summary>
    /// <para lang="zh">获得/设置 Table 扩展按钮集合实例</para>
    /// <para lang="en">Gets or sets Table extension buttons collection instance</para>
    /// </summary>
    [CascadingParameter]
    protected TableExtensionButton? Buttons { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool AutoSelectedRowWhenClick { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool AutoRenderTableWhenClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示，默认为 true 显示</para>
    /// <para lang="en">Gets or sets whether to display. Default is true.</para>
    /// </summary>
    /// <remarks>一般是通过 context 进行业务判断是否需要显示功能按钮</remarks>
    [Parameter]
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Buttons?.AddButton(this);

        if (Size == Size.None)
        {
            Size = Size.ExtraSmall;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override ValueTask DisposeAsync(bool disposing)
    {
        Buttons?.RemoveButton(this);
        return base.DisposeAsync(disposing);
    }
}
