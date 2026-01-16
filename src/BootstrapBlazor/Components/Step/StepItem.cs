// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">StepItem 组件</para>
/// <para lang="en">StepItem Component</para>
/// </summary>
public class StepItem : ComponentBase, IDisposable
{
    /// <summary>
    /// <para lang="zh">获得/设置 步骤显示文字</para>
    /// <para lang="en">Get/Set Text</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 步骤显示文字</para>
    /// <para lang="en">Get/Set Title</para>
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 步骤显示图标</para>
    /// <para lang="en">Get/Set Icon</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 步骤完成显示图标</para>
    /// <para lang="en">Get/Set Finished Icon</para>
    /// </summary>
    [Parameter]
    public string? FinishedIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 描述信息</para>
    /// <para lang="en">Get/Set Description</para>
    /// </summary>
    [Parameter]
    public string? Description { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 模板</para>
    /// <para lang="en">Get/Set Header Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment<StepOption>? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Title 模板</para>
    /// <para lang="en">Get/Set Title Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment<StepOption>? TitleTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件内容实例</para>
    /// <para lang="en">Get/Set Child Content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 父级组件 Step 实例</para>
    /// <para lang="en">Get/Set Parent Step Component Instance</para>
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
        _option.FinishedIcon = FinishedIcon;
        _option.Title = Title;
        _option.Description = Description;
        _option.HeaderTemplate = HeaderTemplate;
        _option.TitleTemplate = TitleTemplate;
        _option.Template = ChildContent;
    }

    /// <summary>
    /// <para lang="zh">销毁方法</para>
    /// <para lang="en">Dispose Method</para>
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
