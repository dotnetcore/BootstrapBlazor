// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 组件单元格自定义组件
///</para>
/// <para lang="en">Table component单元格自定义component
///</para>
/// </summary>
public class TableCellComponent : ComponentBase, ITableCellComponent, IDisposable
{
    /// <summary>
    /// <para lang="zh">获得/设置 子组件
    ///</para>
    /// <para lang="en">Gets or sets 子component
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Table 扩展按钮集合实例
    ///</para>
    /// <para lang="en">Gets or sets Table 扩展buttoncollectioninstance
    ///</para>
    /// </summary>
    [CascadingParameter]
    protected TableExtensionButton? Buttons { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 默认 true 显示
    ///</para>
    /// <para lang="en">Gets or sets whetherdisplay Default is true display
    ///</para>
    /// <para><version>10.2.2</version></para>
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
    }

    /// <summary>
    /// <para lang="zh">Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    ///</para>
    /// <para lang="en">Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    ///</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Buttons?.RemoveButton(this);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
