// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Dialog 组件基类</para>
///  <para lang="en">Dialog Base Component</para>
/// </summary>
public abstract class DialogBase<TModel> : BootstrapModuleComponentBase
{
    /// <summary>
    ///  <para lang="zh">获得/设置 EditModel 实例</para>
    ///  <para lang="en">Get/Set EditModel Instance</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public TModel? Model { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 BodyTemplate 实例</para>
    ///  <para lang="en">Get/Set BodyTemplate Instance</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TModel>? BodyTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 数据项集合</para>
    ///  <para lang="en">Get Items</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public IEnumerable<IEditorItem>? Items { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示标签</para>
    ///  <para lang="en">Get/Set Whether to Show Label</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowLabel { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 每行显示组件数量 默认为 null</para>
    ///  <para lang="en">Get/Set Items Per Row. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? ItemsPerRow { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 设置行格式 默认 Row 布局</para>
    ///  <para lang="en">Get/Set Row Layout. Default is Row</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RowType RowType { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 设置 <see cref="RowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐</para>
    ///  <para lang="en">Get/Set Label Alignment in <see cref="RowType" /> Inline Mode. Default is None, equivalent to Left</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Alignment LabelAlign { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 未分组编辑项布局位置 默认 false 在尾部</para>
    ///  <para lang="en">Get/Set Unset Group Items Position. Default is false (at the end)</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    ///  <para lang="zh">OnInitialized 方法</para>
    ///  <para lang="en">OnInitialized Method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Model == null)
        {
            throw new InvalidOperationException("Model value not set to null");
        }
    }
}
