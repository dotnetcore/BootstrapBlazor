// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// StepItem 组件
/// </summary>
public class StepItem : ComponentBase, IDisposable
{
    /// <summary>
    /// 获得/设置 步骤显示文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 步骤显示文字
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 步骤显示图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 步骤完成显示图标
    /// </summary>
    [Parameter]
    public string? FinishedIcon { get; set; }

    /// <summary>
    /// 获得/设置 描述信息
    /// </summary>
    [Parameter]
    public string? Description { get; set; }

    /// <summary>
    /// 获得/设置 Header 模板
    /// </summary>
    public RenderFragment<StepOption>? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Title 模板
    /// </summary>
    public RenderFragment<StepOption>? TitleTemplate { get; set; }

    /// <summary>
    /// 获得/设置 组件内容实例
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 父级组件 Step 实例
    /// </summary>
    [CascadingParameter]
    private Step? Step { get; set; }

    private readonly StepOption _option = new();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Step?.Add(_option);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _option.Text = Text;
        _option.Icon = Icon;
        _option.Title = Title;
        _option.Description = Description;
        _option.HeaderTemplate = HeaderTemplate;
        _option.TitleTemplate = TitleTemplate;
        _option.Template = ChildContent;
    }

    /// <summary>
    /// 销毁方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Step?.Remove(_option);
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
